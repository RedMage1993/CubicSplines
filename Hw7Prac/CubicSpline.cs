using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Homework_07
{
    public struct PointD
    {
        public double X;
        public double Y;

        public PointD(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    class CubicSpline
    {
        public PointD[] ctrlPts { get; private set; }
        public PointD[] coefficients { get; private set; }
        public int noCtrlPts { get; private set; }

        public double[,] invT { get; private set; }

        public PointD A { get; private set; }
        public PointD B { get; private set; }
        public PointD C { get; private set; }
        public PointD D { get; private set; }

        public double width { get; private set; }

        public double height { get; private set; }

        public CubicSpline(double w, double h)
        {
            noCtrlPts = 0;
            width = w;
            height = h;
        }

        public PointD getScreenPts(double t, int piece)
        {
            PointD ptOnPiece = new PointD();

            if (piece >= noCtrlPts - 1)
                return ptOnPiece;
            if (t < 0.0 || t > 1.0)
                return ptOnPiece;

            ptOnPiece.X = (A.X + B.X * t + C.X * t * t + D.X * t * t * t) * width;
            ptOnPiece.Y = height - ((A.Y + B.Y * t + C.Y * t * t + D.Y * t * t * t) * height);

            return ptOnPiece;
        }
        public void SetCoeForPiece(int piece)
        {
            if (piece >= noCtrlPts - 1)
                return;

            A = ctrlPts[piece];
            B = coefficients[piece];
            C = new PointD(3.0 * (ctrlPts[piece + 1].X - ctrlPts[piece].X) - 2.0 * coefficients[piece].X - coefficients[piece + 1].X,
                3.0 * (ctrlPts[piece + 1].Y - ctrlPts[piece].Y) - 2.0 * coefficients[piece].Y - coefficients[piece + 1].Y);
            D = new PointD(2.0 * (ctrlPts[piece].X - ctrlPts[piece + 1].X) + coefficients[piece].X + coefficients[piece + 1].X,
                2.0 * (ctrlPts[piece].Y - ctrlPts[piece + 1].Y) + coefficients[piece].Y + coefficients[piece + 1].Y);
        }

        public void Init()
        {
            double[,] T = new double[noCtrlPts, noCtrlPts];

            InitializeT(T, noCtrlPts);
            invT = InverseT(T, noCtrlPts);

            GetCoeData();
        }

        public void setCtrlPts(int noCps, PointD[] cp)
        {
            if (noCps < 3)
                return;
            if (cp.Length < 3 || noCps != cp.Length)
                return;

            ctrlPts = cp;
            noCtrlPts = noCps;
            coefficients = new PointD[noCtrlPts];
        }

        private void GetCoeData()
        {
            PointD[] Y = new PointD[noCtrlPts];

            // Fill Y in TD = Y
            Y[0].X = 3 * (ctrlPts[1].X - ctrlPts[0].X);
            Y[0].Y = 3 * (ctrlPts[1].Y - ctrlPts[0].Y);

            for (int i = 1; i < noCtrlPts - 1; i++)
            {
                Y[i].X = 3 * (ctrlPts[i + 1].X - ctrlPts[i - 1].X);
                Y[i].Y = 3 * (ctrlPts[i + 1].Y - ctrlPts[i - 1].Y);
            }

            Y[noCtrlPts - 1].X = 3 * (ctrlPts[noCtrlPts - 1].X - ctrlPts[noCtrlPts - 2].X);
            Y[noCtrlPts - 1].Y = 3 * (ctrlPts[noCtrlPts - 1].Y - ctrlPts[noCtrlPts - 2].Y);

            // Find D = coefficients
            // D = T^-1 * Y
            for (int i = 0; i < noCtrlPts; i++)
            {
                for (int j = 0; j < noCtrlPts; j++)
                {
                    coefficients[i].X += (invT[i, j] * Y[j].X);
                    coefficients[i].Y += (invT[i, j] * Y[j].Y);
                }
            }
        }

        private void InitializeT(double[,] T, int noCtrlPts)
        {
            for (int r = 0; r < noCtrlPts; r++)
            {
                for (int c = 0; c < noCtrlPts; c++)
                {
                    T[r, c] = 0;
                }
            }

            T[0, 0] = 2;
            for (int r = 1; r < noCtrlPts; r++)
            {
                T[r, r] = 4;
                T[r - 1, r] = 1;
                T[r, r - 1] = 1;
            }

            T[noCtrlPts - 1, noCtrlPts - 1] = 2;
        }

        private double theta(int i, double[,] T, double[] dp)
        {
            if (dp[i] != 0)
                return dp[i];
            else
                return T[i - 1, i - 1] * theta(i - 1, T, dp) - theta(i - 2, T, dp);
        }

        private double phi(int i, double[,] T, double[] dp)
        {
            if (dp[i - 1] != 0)
            {
                return dp[i - 1];
            }
            else
                return T[i - 1, i - 1] * phi(i + 1, T, dp) - phi(i + 2, T, dp);
        }

        private int sign(int i, int j)
        {
            return (((i + j) & 1) == 1) ? -1 : 1; // (-1) ^ (i + j); test LSB of (i + j) for parity (even power => positive)
        }

        private double[,] InverseT(double[,] T, int noCtrlPts)
        {
            double[,] invT = new double[noCtrlPts, noCtrlPts];
            double[] dpt = new double[noCtrlPts + 1];
            double[] dpp = new double[noCtrlPts + 1];
            double t, p;

            dpt[0] = 1;
            dpt[1] = 2;
            dpp[noCtrlPts] = 1;
            dpp[noCtrlPts - 1] = 2;

            for (int i = 1; i <= noCtrlPts; i++)
            {
                for (int j = 1; j <= noCtrlPts; j++)
                {
                    if (i < j)
                    {
                        t = theta(i - 1, T, dpt);
                        dpt[i - 1] = t;

                        p = phi(j + 1, T, dpp);
                        dpp[j] = p;

                        invT[i - 1, j - 1] = sign(i, j) * t * p / theta(noCtrlPts, T, dpt);
                    }
                    else if (i > j)
                    {
                        t = theta(j - 1, T, dpt);
                        dpt[j - 1] = t;

                        p = phi(i + 1, T, dpp);
                        dpp[i] = p;

                        invT[i - 1, j - 1] = sign(i, j) * t * p / theta(noCtrlPts, T, dpt);
                    }
                    else
                    {
                        t = theta(i - 1, T, dpt);
                        dpt[i - 1] = t;

                        p = phi(j + 1, T, dpp);
                        dpp[j] = p;

                        invT[i - 1, j - 1] = t * p / theta(noCtrlPts, T, dpt);
                    }
                }
            }

            return invT;
        }
    }
}
