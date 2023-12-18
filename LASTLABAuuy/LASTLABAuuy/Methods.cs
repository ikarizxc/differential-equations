using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace LASTLABAuuy
{
    public partial class Methods : Form
    {
        double x0, y0, xn, h;

        public Methods(double x0, double y0, double xn, double h)
        {
            InitializeComponent();
            chart1.Series.Clear();

            this.x0 = x0;
            this.y0 = y0;
            this.xn = xn;
            this.h = h;

            GraphSettings();
            AccurateSolution();
            EulerMethod();
            ModifiedEulerMethod();
            RungeKuttaMethod();
            RungeKuttaMersonMethod();
            AdamsMethod();
        }

        public double f(double x, double y)
        {
            return Math.Pow(x, 2) - x + y;
        }

        public void AdamsMethod()
        {
            var series = new Series
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Purple,
                BorderWidth = 2,
                Name = "Метод Адамса"
            };

            double yPrev = 0;
            double[] F = new double[4];
            int k = 0;

            for (double xi = x0; xi <= xn + 1e-3; xi += h)
            {
                double yi;

                if (xi == x0)
                {
                    yi = y0;
                    k++;
                }
                else
                {
                    if (k == 1)
                    {
                        yi = yPrev + h * F[0];
                        k++;
                    }
                    else
                    {
                        yi = yPrev + h / 2 * (3 * F[0] - F[1]);
                        k++;
                    }
                }

                series.Points.AddXY(xi, yi);

                F[3] = F[2];
                F[2] = F[1];
                F[1] = F[0];
                F[0] = f(xi, yi);

                yPrev = yi;
            }

            chart1.Series.Add(series);
        }

        public void RungeKuttaMersonMethod()
        {
            var series = new Series
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Black,
                BorderWidth = 2,
                Name = "Метод Рунге–Кутты–Мерсона (с контролем точности)"
            };

            double hLocal = h;

            double epsilon = 0.00001;
            double yPrev = 0;

            for (double xi = x0; xi <= xn; xi += hLocal)
            {
                double yi;

                if (xi == x0)
                {
                    yi = y0;
                }
                else
                {
                    double k1, k2, k3, k4, k5;
                    double delta;

                    while (true)
                    {
                        k1 = hLocal * f(xi - hLocal, yPrev);
                        k2 = hLocal * f(xi - hLocal + hLocal / 3, yPrev + k1 / 3);
                        k3 = hLocal * f(xi - hLocal + hLocal / 3, yPrev + k1 / 6 + k2 / 6);
                        k4 = hLocal * f(xi - hLocal + hLocal / 2, yPrev + k1 / 8 + k3 / 8);
                        k5 = hLocal * f(xi - hLocal + hLocal, yPrev + k1 / 2 - 3 * k3 / 2 + 2 * k4);

                        delta = (2 * k1 - 9 * k3 + 8 * k4 - k5) / 30;

                        if (Math.Abs(delta) >= epsilon)
                        {
                            hLocal /= 2;
                        }
                        else
                        {
                            break;
                        }
                    }

                    yi = yPrev + k1 / 6 + 2 * k4 / 3 + k5 / 6;

                    if (Math.Abs(delta) < epsilon / 32)
                    {
                        hLocal *= 2;
                    }
                }

                series.Points.AddXY(xi, yi);

                yPrev = yi;
            }

            chart1.Series.Add(series);
        }

        public void RungeKuttaMethod()
        {
            var series = new Series
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Turquoise,
                BorderWidth = 2,
                Name = "Метод Рунге-Кутты четвертого порядка"
            };

            double yPrev = 0;

            for (double xi = x0; xi <= xn + 1e-3; xi += h)
            {
                double yi;

                if (xi == x0)
                {
                    yi = y0;
                }
                else
                {
                    double k1, k2, k3, k4;

                    k1 = f(xi - h, yPrev);
                    k2 = f(xi - h + h / 2, yPrev + h * k1 / 2);
                    k3 = f(xi - h + h / 2, yPrev + h * k2 / 2);
                    k4 = f(xi - h + h, yPrev + h * k3);

                    yi = yPrev + h / 6 * (k1 + 2 * k2 + 2 * k3 + k4);
                }

                series.Points.AddXY(xi, yi);

                yPrev = yi;
            }

            chart1.Series.Add(series);
        }

        public void ModifiedEulerMethod()
        {
            var series = new Series
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Blue,
                BorderWidth = 2,
                Name = "Модифицированный метод Эйлера"
            };

            double yPrev = 0;

            for (double xi = x0; xi <= xn + 1e-3; xi += h)
            {
                double yi;

                if (xi == x0)
                {
                    yi = y0;
                }
                else
                {
                    yi = yPrev + h * f(xi - h + h / 2, yPrev + h / 2 * f(xi - h, yPrev));
                }

                series.Points.AddXY(xi, yi);

                yPrev = yi;
            }

            chart1.Series.Add(series);
        }

        public void EulerMethod()
        {
            var series = new Series
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Green,
                BorderWidth = 2,
                Name = "Метод Эйлера"
            };

            double yPrev = 0;

            for (double xi = x0; xi <= xn + 1e-3; xi += h)
            {
                double yi;

                if (xi == x0)
                {
                    yi = y0;
                }
                else
                {
                    yi = yPrev + h * f(xi - h, yPrev);
                }

                series.Points.AddXY(xi, yi);

                yPrev = yi;
            }

            chart1.Series.Add(series);
        }

        public void AccurateSolution()
        {
            var series = new Series
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Red,
                BorderWidth = 2,
                Name = "Точное решение"
            };

            for (double xi = x0; xi <= xn; xi += 0.01)
            {
                double yi = Math.Exp(xi) - Math.Pow(xi, 2) - xi - 1;
                series.Points.AddXY(xi, yi);
            }

            chart1.Series.Add(series);
        }

        // установка параметров графика
        private void GraphSettings()
        {
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.Minimum = x0;
            chart1.ChartAreas[0].AxisX.Maximum = xn;
        }
    }
}
