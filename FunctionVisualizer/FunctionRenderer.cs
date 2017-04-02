using FunctionAnalyzer.Expressions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunctionVisualizer
{
    public partial class MainForm : Form
    {
        private void Render()
        {
            using(var g = Graphics.FromImage(imgBuffer))
            {
                int width = imgBuffer.Width;
                int height = imgBuffer.Height;
                var pts = new double[width, height, 4];
                var total = Environment.ProcessorCount * 8;
                int done = 0;
                int workload = width * height;
                Parallel.For(0, total, (i) =>
                {
                    int start = i * width / total;
                    int end = Math.Min((i + 1) * width / total, width);

                    for (int x = start; x < end; ++x)
                    {
                        for (int y = 0; y < height; ++y)
                        {
                            pts[x, y, 1] = height / 2 - y;
                            pts[x, y, 2] = x - width / 2;

                            pts[x, y, 0] = function.Apply("y", y).Compile("x")
                                .Solve(function.Differentiate("x").Compile("x"), pts[x, y, 2]);

                            pts[x, y, 3] = function.Apply("x", x).Compile("y")
                                .Solve(function.Differentiate("y").Compile("y"), pts[x, y, 1]);

                            int current = Interlocked.Increment(ref done);

                            Invoke(new MethodInvoker(() =>
                            {
                                lblMessage.Text = "[Rendering] " + current + " / " + workload;
                            }));
                        }
                    }
                });
                var bitmap = imgBuffer.LockBits(new Rectangle(0, 0, width, height)
                    , ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                for(int i = 0; i < width; ++i)
                {
                    for(int j = 0; j < height; ++j)
                    {
                        if(!double.IsInfinity(pts[i, j, 0]) && !double.IsNaN(pts[i, j, 0]))
                        {
                            FillPixel(bitmap, pts[i, j, 0], pts[i, j, 1]);
                        }
                        if (!double.IsInfinity(pts[i, j, 3]) && !double.IsNaN(pts[i, j, 3]))
                        {
                            FillPixel(bitmap, pts[i, j, 2], pts[i, j, 3]);
                        }
                    }
                }
                imgBuffer.UnlockBits(bitmap);
            }
        }

        unsafe private void FillPixel(BitmapData bitmap, double x, double y)
        {
            int ix = (int)Math.Round(x + imgBuffer.Width / 2);
            int iy = (int)Math.Round(imgBuffer.Height / 2 - y);
            var pixel = (byte*)bitmap.Scan0 + iy * bitmap.Stride + ix * 3;
            pixel[0] = 255;
            pixel[1] = 0;
            pixel[2] = 0;
        }
    }
}
