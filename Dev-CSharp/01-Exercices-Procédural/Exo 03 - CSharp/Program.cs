using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Exo_03___CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            CalculDivisionEntiere();
            Console.WriteLine();
            Console.WriteLine();
            VerificationBBAN();
            Console.WriteLine();
            Console.WriteLine();
            BBANToIban();
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

        static string GetBBANNumber()
        {
            string numCompte;
            bool quit = false;
            int tmp;
            do
            {
                Console.WriteLine("Veuillez entrer le numéro de compte à vérifier (au format 123-1234567-12) : ");
                numCompte = Console.ReadLine();
                if (numCompte.Length == 14)
                    for (int i = 0; i < 14; i++)
                        switch (i)
                        {
                            case 3:
                            case 11:
                                quit = numCompte.Substring(i, 1).Equals("-");
                                i = (!quit ? 14 : i);
                                break;
                            default:
                                quit = int.TryParse(numCompte.Substring(i, 1), out tmp);
                                i = (!quit ? 14 : i);
                                break;
                        }
                if (!quit) Console.WriteLine("Le numéro de compte entré n'est pas au bon format, veuillez recommencer svp.");
            } while (!quit);
            return numCompte;
        }

        

        static void CalculDivisionEntiere()
        {
            int dividende, diviseur;
            dividende = GetInteger("Veuillez entrer le nombre dividende : ");
            diviseur = GetInteger("Veuillez entrer le nombre diviseur : ");
            Console.WriteLine($"Le résultat de la division entière de {dividende} par {diviseur} vaut : {dividende/diviseur}");
            Console.WriteLine($"Le reste (modulo) de la division entière de {dividende} par {diviseur} vaut : {dividende%diviseur}");
            Console.WriteLine($"Le résultat de la division réelle de {dividende} par {diviseur} vaut : {(float)dividende/diviseur}");
        }

        static void VerificationBBAN()
        {
            string numCompte, baseCompte, moduloCompte;
            numCompte = GetBBANNumber();
            baseCompte = numCompte.Substring(0, numCompte.Length - 3);
            baseCompte = baseCompte.Replace("-", "");
            moduloCompte = numCompte.Substring(numCompte.Length - 2, 2);
            Console.WriteLine((((long.Parse(baseCompte) % 97) == 0 ? 97 : long.Parse(baseCompte) % 97) == int.Parse(moduloCompte)) ? "OK" : "KO");
            
        }

        static void BBANToIban()
        {
            string numCompte, dirt, bankCode;
            numCompte = GetBBANNumber();
            dirt = numCompte.Substring(numCompte.Length - 2, 2) + numCompte.Substring(numCompte.Length - 2, 2) +
                   "111400";
            bankCode = (98 - (int)(long.Parse(dirt) % 97)).ToString();
            Console.WriteLine($"BE{bankCode}{numCompte.Replace("-", "")}");
        }
    }
}
