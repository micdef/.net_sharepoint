using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security;
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

        public static Voyages v = null;
        public static Affichage affich = Affichage.GetInstance();

        #endregion

        #region Main

        static void Main(string[] args)
        {
            try
            {
                affich.PreparerEcran(TITLE, VERSION);
                bool quit;
                do
                {
                    affich.AffichMenuPrincipal();
                    char choix = affich.RecupChar();
                    switch (choix)
                    {
                        case '0':
                            quit = false;
                            Voyage();
                            break;
                        case '1':
                            quit = false;
                            if (v == null)
                                affich.AfficherErreur("Le voyage doit d'abord être créé avant de pouvoir ajouter un participant", -1);
                            else
                                affich.AfficherMessage("Partie pas encore implémentée", -1);
                            break;
                        case 'q':
                        case 'Q':
                            quit = true;
                            break;
                        default:
                            quit = false;
                            affich.AfficherErreur("Le choix effectué n'est pas valide", -1);
                            break;
                    }
                } while (!quit);
                affich.AfficherEcranFin();
            }
            catch (Exception e)
            {
                affich.AfficherErreur(e.Message, -1);
            }            
        }

        #endregion

        #region Voyage

        static void Voyage()
        {
            bool quit;
            do
            {
                affich.AffichMenuVoyage();
                char choix = affich.RecupChar();
                switch (choix)
                {
                    case '0':
                        quit = false;
                        if (v == null)
                            VoyageCreation();
                        else
                            affich.AfficherErreur("Voyage déjà créé, veuillez l'éditer", -1);
                        break;
                    case '1':
                        quit = false;
                        if (v == null)
                            affich.AfficherErreur("Le voyage doit d'abord être créé avant de pouvoir le modifier", -1);
                        else
                            affich.AfficherMessage("Partie pas encore implémentée", -1);
                        break;
                    case '2':
                        quit = false;
                        if (v == null)
                            affich.AfficherErreur("Le voyage doit d'abord être créé avant de pouvoir le consulter", -1);
                        else
                            VoyageConsultation();
                        break;
                    case 'q':
                    case 'Q':
                        quit = true;
                        break;
                    default:
                        quit = false;
                        affich.AfficherErreur("Le choix effectué n'est pas valide", -1);
                        break;
                }
            } while (!quit);
        }

        #region Voyage.Creation

        static void VoyageCreation()
        {
            // Mise en place de la liste des informations
            List<string> infoVoyage = new List<string>();
            List<string> infoOrga = new List<string>();
            infoVoyage.Add("Nom du voyage : ");
            infoVoyage.Add("Nom de la destination : ");
            infoVoyage.Add("Prix pour un enfant : ");
            infoVoyage.Add("Prix pour un adulte : ");
            infoVoyage.Add("Nombre maximum de film disponibles : ");
            infoVoyage.Add("% Réduction Organisateur par participant : ");
            infoOrga.Add("Nom : ");
            infoOrga.Add("Prénom : ");
            infoOrga.Add("Date de naissance : ");

            // Encodage
            List<object> valuesVoyage = new List<object>();
            List<object> valuesOrganisateur = new List<object>();
            bool quit = false;
            do
            {
                // Vide la zone
                affich.ViderZonePrincipale();
                affich.ViderZoneEncodage();
                affich.ViderZoneMessage();

                // Récupération des valeurs du voyage
                affich.BackgroundColor = ConsoleColor.DarkGreen;
                affich.TextColor = ConsoleColor.Black;
                affich.AfficherInformations("Encodage du voyage : ", 0);
                affich.ResetDefaultColors();
                int y = 2;
                int i = 0;
                foreach (string s in infoVoyage)
                {
                    affich.AfficherInformations(s, y);
                    switch (i)
                    {
                        case 0:
                        case 1:
                            valuesVoyage.Add(affich.RecupString());
                            break;
                        case 2:
                        case 3:
                        case 5:
                            valuesVoyage.Add(affich.RecupDouble());
                            break;
                        case 4:
                            valuesVoyage.Add(affich.RecupInt());
                            break;
                        default:
                            throw new IndexOutOfRangeException("Champs non géré par le système");
                    }
                    affich.AfficherReponseCouleur(valuesVoyage[i++].ToString(), y++, s.Length, ConsoleColor.DarkCyan);
                }

                // Récupération des valeurs de l'organisateur
                affich.BackgroundColor = ConsoleColor.DarkYellow;
                affich.TextColor = ConsoleColor.Black;
                affich.AfficherInformations("Encodage de l'organisateur : ", ++y);
                affich.ResetDefaultColors();
                y += 2;
                i = 0;
                foreach (string s in infoOrga)
                {
                    affich.AfficherInformations(s, y);
                    switch (i)
                    {
                        case 0:
                        case 1:
                            valuesOrganisateur.Add(affich.RecupString());
                            break;
                        case 2:
                            valuesOrganisateur.Add(affich.RecupDateTime("(JJ/MM/AAAA) --> ", false, null, -1, '/', Affichage.EnumDateOrder.JOUR_MOIS_ANNEE));
                            break;
                        default:
                            throw new IndexOutOfRangeException("Champs non géré par le système");
                    }
                    affich.AfficherReponseCouleur(valuesOrganisateur[i++].ToString(), y++, s.Length, ConsoleColor.DarkCyan);
                }

                // Afficher la demande
                y += 2;
                affich.BackgroundColor = ConsoleColor.DarkMagenta;
                affich.TextColor = ConsoleColor.Black;
                affich.AfficherInformations("Validez-vous les saisies ?", y);
                affich.ResetDefaultColors();
                quit = affich.RecupBool();
            } while (!quit);

            // Création de l'organisateur
            affich.ViderZoneEncodage();
            affich.AfficherMessage("Création de l'organisateur...", -1);
            Adulte orga;
            try
            {
                orga = new Adulte(valuesOrganisateur[0].ToString(), valuesOrganisateur[1].ToString(), (DateTime)valuesOrganisateur[2], "Organisateur", (int)valuesVoyage[4], 12);
            }
            catch (Exception)
            {
                affich.AfficherErreur("Création de l'organisateur échouée...", -1);
                throw;
            }
            affich.AfficherReussite("Création de l'organisateur réussie...", -1);

            // Création du voyage
            affich.AfficherMessage("Création du voyage...", -1);
            try
            {
                v = new Voyages(valuesVoyage[0].ToString(), valuesVoyage[1].ToString(), (double)valuesVoyage[2], (double)valuesVoyage[3], (int)valuesVoyage[4], (double)valuesVoyage[5], orga);
            }
            catch (Exception)
            {
                affich.AfficherErreur("Création du voyage échouée...", -1);
                throw;
            }
            affich.AfficherReussite("Création du voyage réussie...", -1);
            affich.AfficherMessage("Appuyez sur une touche pour continuer...", -1);
            affich.Wait(0);
        }

        #endregion

        #region Voyage.Consultation

        static void VoyageConsultation()
        {
            bool quit;
            do
            {
                affich.AffichMenuVoyageConsultation();
                char choix = affich.RecupChar();
                switch (choix)
                {
                    case '0':
                        quit = false;
                        VoyageConsultationInfo();
                        break;
                    case '1':
                        quit = false;
                        VoyageConsultationReducOrga();
                        break;
                    case '2':
                        quit = false;
                        VoyageConsultationCarnetsChoisis();
                        break;
                    case '3':
                        quit = false;
                        VoyageConsultationFilmsChoisis();
                        break;
                    case 'q':
                    case 'Q':
                        quit = true;
                        break;
                    default:
                        quit = false;
                        affich.AfficherErreur("Le choix effectué n'est pas valide", -1);
                        break;
                }
            } while (!quit);
        }

        static void VoyageConsultationInfo()
        {
            // Vide la zone
            affich.ViderZonePrincipale();
            affich.ViderZoneEncodage();
            affich.ViderZoneMessage();

            // Afficher le titre pour le voyage
            affich.BackgroundColor = ConsoleColor.DarkGreen;
            affich.TextColor = ConsoleColor.Black;
            affich.AfficherInformations("Informations concernant le voyage : ", 0);
            affich.ResetDefaultColors();

            // Afficher les information du voyage
            affich.AfficherInformations("Nom du voyage : ", 2);
            affich.AfficherReponseCouleur(v.Nom, 2, "Nom du voyage : ".Length, ConsoleColor.DarkCyan);
            affich.AfficherInformations("Nom de la destination : ", 3);
            affich.AfficherReponseCouleur(v.Destination, 3, "Nom de la destination : ".Length, ConsoleColor.DarkCyan);
            affich.AfficherInformations("Prix pour un enfant : ", 4);
            affich.AfficherReponseCouleur($"{v.PrixEnfant.ToString("C")}", 4, "Prix pour un enfant : ".Length, ConsoleColor.DarkCyan);
            affich.AfficherInformations("Prix pour un adulte : ", 5);
            affich.AfficherReponseCouleur($"{v.PrixAdulte.ToString("C")}", 5, "Prix pour un adulte : ".Length, ConsoleColor.DarkCyan);
            affich.AfficherInformations("Nombre maximum de film disponibles : ", 6);
            affich.AfficherReponseCouleur(v.NombreMaxFilms.ToString(), 6, "Nombre maximum de film disponibles : ".Length, ConsoleColor.DarkCyan);
            affich.AfficherInformations("% Réduction Organisateur par participant : ", 7);
            affich.AfficherReponseCouleur($"{v.PctReducOrganisateur.ToString("F2")}%", 7, "% Réduction Organisateur par participant : ".Length, ConsoleColor.DarkCyan);

            // Afficher le titre pour l'organisateur
            affich.BackgroundColor = ConsoleColor.DarkYellow;
            affich.TextColor = ConsoleColor.Black;
            affich.AfficherInformations("Encodage de l'organisateur : ", 9);
            affich.ResetDefaultColors();

            // Afficher les informations de l'organisateur
            affich.AfficherInformations("Nom : ", 11);
            affich.AfficherReponseCouleur(v.Organisateur.Nom, 11, "Nom : ".Length, ConsoleColor.DarkCyan);
            affich.AfficherInformations("Prénom : ", 12);
            affich.AfficherReponseCouleur(v.Organisateur.Prenom, 12, "Prénom : ".Length, ConsoleColor.DarkCyan);
            affich.AfficherInformations("Date de naissance : ", 13);
            affich.AfficherReponseCouleur(v.Organisateur.DateNaissance.ToString(), 13, "Date de naissance : ".Length, ConsoleColor.DarkCyan);
            affich.AfficherInformations("Age : ", 14);
            affich.AfficherReponseCouleur(v.Organisateur.Age.ToString(), 14, "Age : ".Length, ConsoleColor.DarkCyan);

            // Mise en attente de l'écran
            affich.AfficherMessage("Appuyez sur une touche pour continuer...", -1);
            affich.Wait(0);
        }

        static void VoyageConsultationReducOrga()
        {
            bool ok = false;
            if (ok)
            {

            }
            else
            {
                affich.AfficherMessage("Partie pas encore implémentée", -1);
            }
        }

        static void VoyageConsultationCarnetsChoisis()
        {
            bool ok = false;
            if (ok)
            {

            }
            else
            {
                affich.AfficherMessage("Partie pas encore implémentée", -1);
            }
        }

        static void VoyageConsultationFilmsChoisis()
        {
            bool ok = false;
            if (ok)
            {

            }
            else
            {
                affich.AfficherMessage("Partie pas encore implémentée", -1);
            }
        }

        #endregion


        #endregion


    }
}
