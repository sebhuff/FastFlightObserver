using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFlightObserver
{
    public class PointCollection
    {
        public List<PofT> Points = new List<PofT>();

        public ICollection<ScreenPoint> GenerateScreenPointsInfiniteC(double time, double odfs)
        {
            ICollection<ScreenPoint> screen_points = new List<ScreenPoint>();

            foreach (PofT p in Points)
            {
                double X = p.GetX(time);
                double Y = p.GetY(time);
                double Z = p.GetZ(time);

                double xsd = (X * odfs) / Z;
                double ysd = (Y * odfs) / Z;

                double r = Math.Sqrt((X * X) + (Y * Y) + (Z * Z));

                screen_points.Add(new ScreenPoint() { X = xsd, Y = ysd, F = p.Frequency, T = time });
            }

            return screen_points;
        }

        public ICollection<ScreenPoint> GenerateScreenPoints(double run_step, double run_time, double odfs)
        {
            double min_time = double.MaxValue;
            double max_time = double.MaxValue;
            double old_r = 0;
            double c = 299792458;     // m/s

            ICollection<ScreenPoint> screen_points = new List<ScreenPoint>();

            foreach (PofT p in Points)
            {
                for (double time = 0; time < run_time; time += run_step)
                {
                    double Z = p.GetZ(time);
                    if (Z > 0)
                    {
                        double X = p.GetX(time);
                        double Y = p.GetY(time);

                        double xsd = (X * odfs) / Z;
                        double ysd = (Y * odfs) / Z;

                        double r = Math.Sqrt((X * X) + (Y * Y) + (Z * Z));

                        double T = time + r / c;

                        if (T < min_time)
                        {
                            min_time = T;
                        }

                        if (T > max_time)
                        {
                            max_time = T;
                        }

                        double f = p.Frequency;

                        if (time > 0)
                        {
                            double v = (old_r - r) / run_step;

                            if(v > c)
      +                     {

                            }
                            // v is positive means r is reducing... so object coming towards us - frequency increases
                            f = p.Frequency * (1 - (v / c));
                        }
                        old_r = r;

                        screen_points.Add(new ScreenPoint() { X = xsd, Y = ysd, F = f, T = T });
                    }
                }
            }

            return screen_points;
        }
    }
}
