using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Exo_07___CSharp
{
    struct Celsius
    {
        public double temperature;

        public void ConvertToCelsius(Fahrenheit tempF)
        {
            this.temperature = (tempF.temperature - 32) * 5.0 / 9.0;
        }
    }

    struct Fahrenheit
    {
        public double temperature;

        public void ConvertToFahrenheit(Celsius tempC)
        {
            this.temperature = tempC.temperature * 9.0 / 5.0 + 32;
        }
    }

    struct SecondDegre
    {
        public double a, b, c;

        public bool Resoudre(out double? x1, out double? x2)
        {
            double ro = Math.Pow(this.b, 2) + (4 * this.a * this.c);
            if (ro < 0)
            {
                x1 = null;
                x2 = null;
                return false;
            }
            else if (ro == 0)
            {
                x1 = -(this.b / (2 * this.a));
                x2 = x1;
                return true;
            }
            else
            {
                x1 = (-this.b - Math.Sqrt(ro)) / (2 * this.a);
                x2 = (-this.b + Math.Sqrt(ro)) / (2 * this.a);
                return true;
            }
        }

    }
}
