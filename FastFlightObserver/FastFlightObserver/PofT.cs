using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFlightObserver
{
    public class PofT
    {
        public Func<double, double> GetX;
        public Func<double, double> GetY;
        public Func<double, double> GetZ;
        public double Frequency = 550 * 10^12;
    }
}
