using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFlightObserver
{
    public class PofT
    {
        public delegate double GetX(double t);
        public delegate double GetY(double t);
        public delegate double GetZ(double t);
        public double Frequency = 550 * 10^12;
    }
}
