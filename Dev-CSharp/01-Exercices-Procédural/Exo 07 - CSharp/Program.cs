using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exo_07___CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            ConvertCelsiusToFahrenheit();
            Console.WriteLine();
            Console.WriteLine();
            ConvertFahrenheitToCelsius();
            Console.WriteLine();
            Console.WriteLine();
            ResolutionSeconDegre();
            Console.ReadKey();
        }

        static double GetDouble(string textAffiche)
        {
            double nb;
            bool quit = false;
            do
            {
                Console.WriteLine(textAffiche);
                if (!double.TryParse(Console.ReadLine(), out nb))
                    Console.WriteLine("Le nombre entré n'est pas valide");
                else
                    quit = true;
            } while (!quit);
            return nb;
        }

        static void ConvertCelsiusToFahrenheit()
        {
            Celsius c = new Celsius();
            Fahrenheit f = new Fahrenheit();
            c.temperature = GetDouble("Veuillez entre votre températeur en °C : ");
            f.ConvertToFahrenheit(c);
            Console.WriteLine($"{c.temperature}°C = {f.temperature}°F");
        }

        static void ConvertFahrenheitToCelsius()
        {
            Celsius c = new Celsius();
            Fahrenheit f = new Fahrenheit();
            f.temperature = GetDouble("Veuillez entre votre températeur en °F : ");
            c.ConvertToCelsius(f);
            Console.WriteLine($"{f.temperature}°F = {c.temperature}°C");
        }

        static void ResolutionSeconDegre()
        {
            SecondDegre sd = new SecondDegre();
            string msg = null;
            double? x1, x2;
            sd.a = GetDouble("Veuillez entrer la valeur \"a\" de ax²+bx+c : ");
            sd.b = GetDouble("Veuillez entrer la valeur \"b\" de ax²+bx+c : ");
            sd.c = GetDouble("Veuillez entrer la valeur \"c\" de ax²+bx+c : ");
            msg = "Solution pour l'équation : ";
            msg += (sd.a.Equals(0) ? "" : sd.a + "x²");
            msg += (sd.b > 0 ? "+" + sd.b + "x" : (sd.b < 0 ? sd.b + "x" : ""));
            msg += (sd.c > 0 ? "+" + sd.c.ToString() : (sd.c < 0 ? sd.c.ToString() : "")) + " : \n\n";
            if (sd.Resoudre(out x1, out x2))
                if (x1 == x2)
                    msg += $"Une seule solution réelle : x1 = x2 = {x1}";
                else
                    msg += $"Deux solution réelles : x1 = {x1}, x2 = {x2}";
            else
                msg += "Aucune solution réelle";
            Console.WriteLine(msg);
        }
    }
}
