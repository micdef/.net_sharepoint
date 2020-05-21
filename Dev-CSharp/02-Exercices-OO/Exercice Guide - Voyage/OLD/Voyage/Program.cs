using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voyage.Models;

namespace Voyage
{
    class Program
    {

        private const string TITLE = "Gestion de voyage V1.0";
        
        static void Main(string[] args)
        {
            //bool quit = false;
            //G_Console.ClearConsole();
            //do
            //{
            //    G_Console.ShowTitle(TITLE);
            //    Console.ReadKey();
            //    quit = true;
            //} while (!quit);
            Console.WriteLine(new DateTime((DateTime.Now - new DateTime(1984, 07, 10)).Ticks).Year);
            Console.ReadKey();
        }
    }
}
