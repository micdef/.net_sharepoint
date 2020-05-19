using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exo_06___CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            GestionPoints();
            Console.ReadKey();
        }

        static void GestionPoints()
        {
            // Déclaration des variables locales
            Point?[,] tabPoints = new Point?[5,5];
            Point p;

            // Remplissage des valeurs
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    if (i == j)
                    {
                        p.x = i;
                        p.y = i;
                        tabPoints[i, j] = p;
                    }

            // Affichage des valeurs
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                    if (tabPoints[i,j].HasValue)
                        Console.Write($"X:{tabPoints[i, j].Value.x + 1} - Y:{tabPoints[i, j].Value.y + 1}");
                    else
                        Console.Write("\t");
                Console.WriteLine();
            }
        }
    }
}
