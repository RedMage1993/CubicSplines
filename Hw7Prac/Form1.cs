using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.ObjectModel;

namespace Homework_07
{
   public partial class Form1 : Form
    {
        private int halfWidth, halfHeight;
        private Graphics graphics;
        private DirectBitmap dbmp;

        public Form1()
        {
            InitializeComponent();

            // For faster access to pixels.
            dbmp = new DirectBitmap(pbxGraph.Width, pbxGraph.Height);

            pbxGraph.Image = dbmp.Bitmap;

            halfWidth = pbxGraph.Width >> 1;
            halfHeight = pbxGraph.Height >> 1;

            graphics = Graphics.FromImage(pbxGraph.Image);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Application.DoEvents(); // Let all messages in message queue be processed.

            bgwDrawing.RunWorkerAsync();
        }

        private void bgwDrawing_Done(object sender, RunWorkerCompletedEventArgs e)
        {
            pbxGraph.Invalidate();
        }

        private void DrawAxes()
        {
            graphics.DrawLine(Pens.Black, new Point(1, 0), new Point(1, pbxGraph.Height));
            graphics.DrawString("Y", new Font("Arial", 10), Brushes.Black, new Point(2, 0));

            graphics.DrawLine(Pens.Black, new Point(1, halfHeight), new Point(pbxGraph.Width, halfHeight));
            graphics.DrawString("X", new Font("Arial", 10), Brushes.Black, new Point(pbxGraph.Width - 12, halfHeight + 1));
        }

        private void pbxGraph_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;

            MessageBox.Show("X: " + me.X + ", " + "Y: " + me.Y);
        }

        private void drawSpline(CubicSpline spline, PointD[] ctrlPts)
        {
            double t;
            PointD ptOnPiece1, ptOnPiece2;

            spline.setCtrlPts(ctrlPts.Length, ctrlPts);
            spline.Init();

            for (int i = 0; i < ctrlPts.Length - 1; i++)
            {
                t = 0.0;

                spline.SetCoeForPiece(i);

                ptOnPiece1 = spline.getScreenPts(t, i);

                while (t < 1.0)
                {
                    t = Math.Round(t + 0.1, 2);

                    ptOnPiece2 = spline.getScreenPts(t, i);

                    graphics.DrawLine(Pens.Red, (float)ptOnPiece1.X, (float)ptOnPiece1.Y, (float)ptOnPiece2.X, (float)ptOnPiece2.Y);

                    ptOnPiece1 = ptOnPiece2;
                }
            }
        }

        private void bgwDrawing_DoWork(object sender, DoWorkEventArgs e)
        {
            CubicSpline spline = new CubicSpline(pbxGraph.Width, halfHeight);

            PointD[] topCps =
            {
                new PointD(-0.00125, 0.05),
                new PointD(0.00125, 0.085),
                new PointD(0.02125, 0.23),
                new PointD(0.07, 0.445),
                new PointD(0.14, 0.675),
                new PointD(0.22, 0.85),
                new PointD(0.31375, 0.96),
                new PointD(0.38375, 0.99),
                new PointD(0.4625, 0.975),
                new PointD(0.53875, 0.925),
                new PointD(0.62125, 0.84),
                new PointD(0.705, 0.72),
                new PointD(0.785, 0.575),
                new PointD(0.85375, 0.42),
                new PointD(0.91125, 0.27),
                new PointD(1.0, 0.0)
            };

            PointD[] botCps =
            {
                new PointD(0.01, 0),
                new PointD(0.0225, 0.005),
                new PointD(0.0375, 0.025),
                new PointD(0.07625, 0.11),
                new PointD(0.15375, 0.29),
                new PointD(0.23875, 0.46),
                new PointD(0.34375, 0.58),
                new PointD(0.48375, 0.6),
                new PointD(0.585, 0.565),
                new PointD(0.6925, 0.475),
                new PointD(0.78875, 0.365),
                new PointD(0.88625, 0.21),
                new PointD(1.0, 0.0)
            };

            graphics.Clear(Color.White);
            DrawAxes();

            drawSpline(spline, topCps);

            drawSpline(spline, botCps);
        }
    }
}
