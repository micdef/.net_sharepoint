using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exo_04___CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Fibonacci();
            Console.WriteLine();
            Console.WriteLine();
            Factorielle();
            Console.WriteLine();
            Console.WriteLine();
            NombresPremier();
            Console.WriteLine();
            Console.WriteLine();
            TablesMultiplications();
            Console.WriteLine();
            Console.WriteLine();
            Comptage();
            Console.WriteLine();
            Console.WriteLine();
            RacineCarree();
            Console.WriteLine();
            Console.WriteLine();
            Console.ReadKey();
        }

        static int GetInteger(string textAffiche)
        {
            int nb;
            bool quit = false;
            do
            {
                Console.WriteLine(textAffiche);
                if (!int.TryParse(Console.ReadLine(), out nb))
                    Console.WriteLine("Le nombre entré n'est pas valide");
                else
                    quit = true;
            } while (!quit);
            return nb;
        }

        static void Fibonacci()
        {
            try
            {
                int qte;
                decimal nb1=0, nb2=1, nb3=nb1+nb2;
                string msg = null;
                qte = GetInteger("Veuillez entrer le nombre d'itération de Fibonacci souhaitée : ");
                if (qte > 141) throw new Exception("Le quantité entrée amène a des nombres ne pouvant être contenus dans les variables classiques. Le maximum d'itérations possible est de 141");
                msg = $"0, {nb1}, {nb2}, {nb3}";
                for (int i = 5; i <= qte; i++)
                {
                    nb1 = nb2;
                    nb2 = nb3;
                    nb3 = nb1 + nb2;
                    msg += $", {nb3}";
                }

                Console.WriteLine(msg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void Factorielle()
        {
            try
            {
                int nb;
                decimal result = 1;
                nb = GetInteger("Veuillez entrer le nombe pour effectuer sa factorielle : ");
                for (int i = nb; i >= 2; i--)
                    result *= i;
                Console.WriteLine($"{nb}! = {result}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void NombresPremier()
        {
            try
            {
                int x;
                bool estPremier;
                string msg = null;
                x = GetInteger("Veuillez entre jusqu'à quel nombre chercher les nombres premiers : ");
                for (int i = 0; i <= x; i++)
                {
                    estPremier = true;
                    for (int j = 2; j < i - 1; j++)
                        if (i % j == 0)
                            estPremier = false;
                    msg += (estPremier? $", {i}" : "");
                }
                Console.WriteLine(msg.Substring(5));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void TablesMultiplications()
        {
            for (int i = 1; i <= 20; i++)
            {
                for (int j = 1; j <= 5; j++)
                    Console.Write($"{j:D2}x{i:D2}={i*j:D3}\t");
                Console.WriteLine();
            }
        }

        static void Comptage()
        {
            for (double i = 0.0; i < 20.0; i++)
                Console.WriteLine(i);
        }

        static void RacineCarree()
        {
            double a = 1.0, fa, e = 1.0;
            int b;
            b = GetInteger("Veuillez entrer le nombre a calculer sa racine : ");
            while (e > 0.001)
            {
                fa = (a + (double) b / a) / 2;
                if (fa > a)
                    e = fa - a;
                else
                    e = a - fa;
                a = fa;
            }
            Console.WriteLine($"Valeur obtenue : {a}, Son carré est {Math.Pow(a, 2)}");
        }
    }
}
