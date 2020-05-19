using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exo_08___CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            PaquetCartesCours();
            Console.ReadKey();
            PaquetCartes();
            Console.ReadKey();
        }

        static void PaquetCartesCours()
        {
            Carte[] paquet = new Carte[52];
            Carte c;
            int i = 0;
            foreach (Valeurs val in Enum.GetValues(typeof(Valeurs)))
                foreach (Couleurs col in Enum.GetValues(typeof(Couleurs)))
                {
                    c.couleur = col;
                    c.valeur = val;
                    paquet[i++] = c;
                }
            AffichePaquetCartes(ref paquet);
        }

        static void ConstruirePaquetCartes(ref Carte[] paquet)
        {
            Carte c;
            int k = 0;
            for (int i = 2; i < 15; i++)
                for (int j = 1; j < 5; j++)
                {
                    c.valeur = (Valeurs) i;
                    c.couleur = (Couleurs) j;
                    paquet[k++] = c;
                }
        }

        static void AffichePaquetCartes(ref Carte[] paquet)
        {
            foreach (Carte c in paquet)
                Console.WriteLine($"{c.valeur} de {c.couleur}");
        }

        static void PaquetCartes()
        {
            Carte[] paquet = new Carte[52];
            ConstruirePaquetCartes(ref paquet);
            AffichePaquetCartes(ref paquet);
        }
    }
}
