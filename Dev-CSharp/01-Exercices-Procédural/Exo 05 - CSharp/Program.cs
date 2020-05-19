using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exo_05___CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            NombresPremier();
            Console.WriteLine();
            Console.WriteLine();
            AdditionCharToChar();
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

        static bool IsPremier(int nb)
        {
            bool estPremier = true;
            for (int i = 2; i < nb -1; i++)
                if (nb % i == 0)
                    estPremier = false;
            return estPremier;
        }

        static void NombresPremier()
        {
            try
            {
                // Déclaration des variables
                int x;
                string msg = null;
                ArrayList nbPremiers2 = new ArrayList();
                List<int> nbPremiers = new List<int>();
                x = GetInteger("Veuillez entre jusqu'à quel nombre chercher les nombres premiers : ");
            
                // Ecriture Exercices sans les boucles for
                int i = 0;
                while (i <= x)
                {
                    if (IsPremier(i)) nbPremiers2.Add(i);
                    i++;
                }

                // Affichage avec une for-each
                foreach (int n in nbPremiers2)
                    msg += $"{n}, ";
                Console.WriteLine(msg.Substring(0, msg.Length - 2));

                // Ecriture en boucle for
                for (int k = 0; k <= x; k++)
                    if (IsPremier(i)) nbPremiers.Add(i);

                // Affichage avec une for-each
                foreach (int n in nbPremiers)
                    msg += $"{n}, ";
                Console.WriteLine(msg.Substring(0, msg.Length - 2));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void AdditionCharToChar()
        {
            try
            {
                // Déclaration des variables
                string nb1, nb2;
                char[] tab1, tab2;
                decimal result = 0;
                int i;

                // Récupération des valeurs
                nb1 = GetInteger("Veuillez entrer le premier nombre svp : ").ToString();
                nb2 = GetInteger("Veulilez entrer le second nombre svp : ").ToString();

                // Récupération et inversion des tableaux
                tab1 = nb1.ToCharArray();
                Array.Reverse(tab1);
                tab2 = nb2.ToCharArray();
                Array.Reverse(tab2);

                // Addition Caractère/Caractère
                i = 0;
                if (tab1.Length > tab2.Length)
                    foreach (char c in tab1)
                    {
                        if (i < tab2.Length)
                            result += (decimal) (int.Parse(c.ToString()) * Math.Pow(10, i)) +
                                      (decimal) (int.Parse(tab2[i].ToString()) * Math.Pow(10, i));
                        else
                            result += (decimal) (int.Parse(c.ToString()) * Math.Pow(10, i));
                        i++;
                    }
                else
                    foreach (char c in tab2)
                    {
                        if (i < tab1.Length)
                            result += (decimal)(int.Parse(c.ToString()) * Math.Pow(10, i)) +
                                      (decimal)(int.Parse(tab2[i].ToString()) * Math.Pow(10, i));
                        else
                            result += (decimal)(int.Parse(c.ToString()) * Math.Pow(10, i));
                        i++;
                    }

                // Affichage du résultat
                Console.WriteLine($"{nb1} + {nb2} = {result}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
