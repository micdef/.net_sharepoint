using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exo2
{
    class Program
    {
        static void Main(string[] args)
        {
            Modulo();
            Console.ReadKey();
        }

        static void Modulo()
        {
            int nb1 = 0;
            bool exit = false;
            do
            {
                Console.Write("Entrez le nombre : ");
                if (!int.TryParse(Console.ReadLine(), out nb1))
                    Console.WriteLine("Le nombre entré n'est pas valide.");
                else
                    exit = true;
            } while (!exit);

            // Mode ton exo
            Console.WriteLine("Exo [Version Cours]");
            Console.WriteLine("-------------------");
            Console.WriteLine();
            if ((nb1 / 2) * 2 == nb1)
                Console.WriteLine("Le nombre est pair");
            else
                Console.WriteLine("Le nombre est impair");
            
            // Mode Modulo
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Exo [Version Modulo]");
            Console.WriteLine("--------------------");
            Console.WriteLine();
            if (nb1%2 == 0)
                Console.WriteLine("Le nombre est pair");
            else
                Console.WriteLine("Le nombre est impair");
        }
    }
}
