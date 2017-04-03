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
        unsafe private void Render()
        {
            using(var g = Graphics.FromImage(imgBuffer))
            {
                int width = imgBuffer.Width;
                int height = imgBuffer.Height;
                var pts = new double[width, height, 2];
                var total = Environment.ProcessorCount * 8;
                int done = 0;
                int workload = width + height;
                Parallel.For(0, total, (i) =>
                {
                    int startx = i * width / total;
                    int endx = Math.Min((i + 1) * width / total, width);
                    int starty = i * height / total;
                    int endy = Math.Min((i + 1) * height / total, height);

                    // Because of CSharp's raw first storage, this "for" must be slow. Does anyone have ideas?
                    for (int x = startx; x < endx; ++x)
                    {
                        var fy = function.Apply("x", x - width / 2).Compile("y");
                        var dfy = function.Differentiate("y").Compile("y");

                        for (int y = 0; y < height; ++y)
                        {
                            pts[x, y, 1] = fy.Solve(dfy, height / 2 - y);
                        }

                        int current = Interlocked.Increment(ref done);
                        Invoke(new MethodInvoker(() =>
                        {
                            lblMessage.Text = "[Rendering] " + current + " / " + workload;
                        }));
                    }

                    for (int y = starty; y < endy; ++y)
                    {
                        var fx = function.Apply("y", height / 2 - y).Compile("x");
                        var dfx = function.Differentiate("x").Compile("x");

                        for (int x = 0; x < width; ++x)
                        {
                            pts[x, y, 0] = fx.Solve(dfx, x - width / 2);
                        }

                        int current = Interlocked.Increment(ref done);
                        Invoke(new MethodInvoker(() =>
                        {
                            lblMessage.Text = "[Rendering] " + current + " / " + workload;
                        }));
                    }
                });

                var bitmap = imgBuffer.LockBits(new Rectangle(0, 0, width, height)
                    , ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                
                for(int y = 0; y < height; ++y)
                {
                    for (int x = 0; x < width; ++x)
                    {
                        if (!double.IsInfinity(pts[x, y, 0]) && !double.IsNaN(pts[x, y, 0]))
                        {
                            var ix = (int)(pts[x, y, 0] + width / 2);
                            if(ix >= 0 && ix < width)
                            {
                                var pixel = (byte*)bitmap.Scan0 + y * bitmap.Stride + ix * 3;
                                pixel[0] = 255;
                                pixel[1] = 0;
                                pixel[2] = 0;
                            }
                        }
                        if (!double.IsInfinity(pts[x, y, 1]) && !double.IsNaN(pts[x, y, 1]))
                        {
                            var iy = (int)(height / 2 - pts[x, y, 1]);
                            if(iy >= 0 && iy < height)
                            {
                                var pixel = (byte*)bitmap.Scan0 + iy * bitmap.Stride + x * 3;
                                pixel[0] = 255;
                                pixel[1] = 0;
                                pixel[2] = 0;
                            }
                        }
                    }
                }
                imgBuffer.UnlockBits(bitmap);
            }
        }
    }
}
