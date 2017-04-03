using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FunctionAnalyzer;
using FunctionAnalyzer.Expressions;
using System.Reflection;
using System.Threading;

namespace FunctionVisualizer
{
    public partial class MainForm : Form
    {
        private RawExpression function;
        private Bitmap imgBuffer;

        public MainForm()
        {
            InitializeComponent();
        }

        private void txtFunctionInput_TextChanged(object sender, EventArgs e)
        {
            try
            {
                function = ExpressionParser.Parse(txtFunctionInput.Text).Simplify();
                lblMessage.Text = "[Function] Ready for: " + function + " = 0";
                btnBeginRender.Enabled = true;
            }
            catch(ParsingException ex)
            {
                lblMessage.Text = ex.Message;
                btnBeginRender.Enabled = false;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            pnlPaintBoard.GetType().GetProperty("DoubleBuffered"
                , BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(pnlPaintBoard, true, new object[] { });
            RebuildBuffer();
            RenderAxis();
        }

        private void RebuildBuffer()
        {
            if(imgBuffer != null)
            {
                imgBuffer.Dispose();
            }
            imgBuffer = new Bitmap(pnlPaintBoard.Width, pnlPaintBoard.Height);
        }

        private void RenderAxis()
        {
            using(var g = Graphics.FromImage(imgBuffer))
            {
                g.FillRectangle(Brushes.White, 0, 0, imgBuffer.Width, imgBuffer.Height);

                var leftMid = new Point(0, imgBuffer.Height / 2);
                var rightMid = new Point(imgBuffer.Width - 1, imgBuffer.Height / 2);
                var upMid = new Point(imgBuffer.Width / 2, 0);
                var downMid = new Point(imgBuffer.Width / 2, imgBuffer.Height - 1);

                g.DrawLine(Pens.Blue, leftMid, rightMid);
                g.DrawLine(Pens.Blue, upMid, downMid);

                var mid = new Point(imgBuffer.Width / 2, imgBuffer.Height / 2);

                for(int i = mid.X; i < imgBuffer.Width; i += 50)
                {
                    g.DrawLine(Pens.Blue, i, mid.Y - 5, i, mid.Y + 5);
                    g.DrawString((i - mid.X).ToString(), pnlPaintBoard.Font
                        , Brushes.DarkBlue, i - 10, mid.Y + 6);
                }

                for (int i = mid.X - 50; i >= 0; i -= 50)
                {
                    g.DrawLine(Pens.Blue, i, mid.Y - 5, i, mid.Y + 5);
                    g.DrawString((i - mid.X).ToString(), pnlPaintBoard.Font
                        , Brushes.DarkBlue, i - 10, mid.Y + 6);
                }

                for (int i = mid.Y + 50; i < imgBuffer.Width; i += 50)
                {
                    g.DrawLine(Pens.Blue, mid.X - 5, i, mid.X + 5, i);
                    g.DrawString((mid.Y - i).ToString(), pnlPaintBoard.Font
                        , Brushes.DarkBlue, mid.X + 6, i - 8);
                }

                for (int i = mid.Y - 50; i >= 0; i -= 50)
                {
                    g.DrawLine(Pens.Blue, mid.X - 5, i, mid.X + 5, i);
                    g.DrawString((mid.Y - i).ToString(), pnlPaintBoard.Font
                        , Brushes.DarkBlue, mid.X + 6, i - 8);
                }
            }
        }

        private void pnlPaintBoard_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(imgBuffer, 0, 0);
        }

        private void btnBeginRender_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback((nothing) =>
            {
                Invoke(new MethodInvoker(() =>
                {
                    btnBeginRender.Enabled = false;
                    lblMessage.Text = "[Rendering]";
                }));
                RenderAxis();
                Render();
                Invoke(new MethodInvoker(() =>
                {
                    btnBeginRender.Enabled = true;
                    lblMessage.Text = "[Finished]";
                    pnlPaintBoard.Refresh();
                }));
            }), null);
        }
    }
}
