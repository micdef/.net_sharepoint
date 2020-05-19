using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exo01
{
    class Program
    {
        static void Main(string[] args)
        {
            AdditionParse();
            AdditionTryParse();
            Console.ReadKey();
        }

        static void AdditionParse()
        {
            int nb1 = 0, nb2 = 0;
            Console.Write("Entrez le premier nombre : "); nb1 = int.Parse(Console.ReadLine());
            Console.Write("Entrez le second nombre : "); nb2 = int.Parse(Console.ReadLine());
            Console.WriteLine($"L'addition de {nb1} et {nb2} donne {nb1 + nb2}");
        }

        static void AdditionTryParse()
        {
            int nb1 = 0, nb2 = 0;
            bool exit = false;
            do
            {
                Console.Write("Entrez le premier nombre : ");
                if (!int.TryParse(Console.ReadLine(), out nb1))
                    Console.WriteLine("Le nombre entré n'est pas valide.");
                else
                    exit = true;
            } 
            while (!exit);
            exit = false;
            do
            {
                Console.Write("Entrez le second nombre : ");
                if (!int.TryParse(Console.ReadLine(), out nb2))
                    Console.WriteLine("Le nombre entré n'est pas valide.");
                else
                    exit = true;
            } 
            while (!exit);
            Console.WriteLine($"L'addition de {nb1} et {nb2} donne {nb1 + nb2}");
        }
    }
}
