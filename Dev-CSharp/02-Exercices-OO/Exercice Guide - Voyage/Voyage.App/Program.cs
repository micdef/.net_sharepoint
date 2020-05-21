using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voyage.Models;

namespace Voyage.App
{

    class Program
    {

        #region Constants

        private const string TITLE = "Gestion de voyage";
        private const string VERSION = "1.0.0.0";

        #endregion

        #region Public Vars

        public static Voyages v;
        public static ConsoleApplication cons;

        #endregion

        static void Main(string[] args)
        {
            cons = ConsoleApplication.GetInstance();
            try
            {
                cons.AfficherCadre();
                cons.AfficherTitre(TITLE, VERSION);
                bool quit = false;
                do
                {
                    cons.AfficherMenu("Menu principal", RecupMenuPrincipal(), 1, 10, true);
                    char choix = cons.RecupererCaract("Faites votre choix svp --> ");
                    switch (choix)
                    {
                        case '0':
                            Voyage();
                            quit = false;
                            break;
                        case '1':
                            if (v == null)
                                cons.AfficherMessage("Le voyage n'est pas encore créé. Veuillez d'abord créer le voyage svp.", ConsoleApplication.EnumMessageTypes.ERROR);
                            else
                                Participant();
                            break;
                        case 'Q':
                        case 'q':
                            quit = true;
                            break;
                        default:
                            cons.AfficherMessage("Le choix effectué n'est pas valide.", ConsoleApplication.EnumMessageTypes.ERROR);
                            quit = false;
                            break;
                    }
                    cons.VidezZoneMessage();
                    cons.VidezZoneInput();
                } while (!quit);
                cons.ViderZonePrincipale();
                cons.AfficherMessageZP("Au Revoir. Appuyez sur une touche pour arrêter le programme.");
                cons.Wait(0);
            }
            catch (Exception e)
            {
                cons.AfficherMessage(e.Message, ConsoleApplication.EnumMessageTypes.ERROR);
            }            
        }

        static List<string> RecupMenuPrincipal()
        {
            List<string> m = new List<string>();
            m.Add("Voyage");
            m.Add("Participants");
            return m;
        }

        static List<string> RecupMenuVoyageL1()
        {
            List<string> m = new List<string>();
            return m;
        }

        static void Voyage()
        {
            cons.ViderZonePrincipale();
            cons.VidezZoneMessage();
            cons.VidezZoneInput();
            cons.AfficherMessageZP("Voyages.");
            cons.Wait(0);
        }

        static void Participant()
        {
            cons.ViderZonePrincipale();
            cons.VidezZoneMessage();
            cons.VidezZoneInput();
            cons.AfficherMessageZP("Participants.");
            cons.Wait(0);
        }
    }
}
