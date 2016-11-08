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
        private CubicSpline splineTop;

        public Form1()
        {
            InitializeComponent();

            // For faster access to pixels.
            dbmp = new DirectBitmap(pbxGraph.Width, pbxGraph.Height);

            pbxGraph.Image = dbmp.Bitmap;

            halfWidth = pbxGraph.Width >> 1;
            halfHeight = pbxGraph.Height >> 1;

            graphics = Graphics.FromImage(pbxGraph.Image);

            splineTop = new CubicSpline(pbxGraph.Width, halfHeight);
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

        private void bgwDrawing_DoWork(object sender, DoWorkEventArgs e)
        {
            double t;
            PointD ptOnPiece1, ptOnPiece2;
            PointD [] topCps =
            {
                new PointD(0.0, 0.0),
                new PointD(0.5, 0.5),
                new PointD(1.0, 0.0)
            };

            graphics.Clear(Color.White);
            DrawAxes();

            splineTop.setCtrlPts(topCps.Length, topCps);
            splineTop.Init();

            for (int i = 0; i < topCps.Length - 1; i++)
            {
                t = 0.0;

                splineTop.SetCoeForPiece(i);

                ptOnPiece1 = splineTop.getScreenPts(t, i);

                while (t < 1.0)
                { 
                    t = Math.Round(t + 0.1, 2);

                    ptOnPiece2 = splineTop.getScreenPts(t, i);

                    graphics.DrawLine(Pens.Red, (float) ptOnPiece1.X, (float) ptOnPiece1.Y, (float) ptOnPiece2.X, (float) ptOnPiece2.Y);

                    ptOnPiece1 = ptOnPiece2;
                }
            }
        }
    }
}
