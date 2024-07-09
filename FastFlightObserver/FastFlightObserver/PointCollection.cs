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

                screen_points.Add(new ScreenPoint() { X = xsd, Y = ysd, R = r });
            }

            return screen_points;
        }

        public ICollection<ScreenPoint> GenerateScreenPointsInfiniteC()
        {
            return null;
        }
    }
}
