using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voyage.Models
{
    public class Enfant : Personne
    {
        #region Fields

        private readonly List<string> _carnets;
        private string _carnetChoisi;

        #endregion

        #region Constructors

        private Enfant()
        {
            _carnets = null;
            CarnetChoisi = null;
        }

        public Enfant(string nom, string prenom, DateTime dateNaiss)  : base(nom, prenom, dateNaiss)
        {
            _carnets = new List<string>();
            CarnetChoisi = null;
            FillCarnets();
        }

        #endregion

        #region Getters-Setters

        public string CarnetChoisi
        {
            get { return _carnetChoisi; }
            private set { _carnetChoisi = value; }
        }

        #endregion

        #region Methods

        private void FillCarnets()
        {
            _carnets.Add("Colorier avec Babar");
            _carnets.Add("Dessiner avec les amis Disney");
            _carnets.Add("Apprendre le Mandela");
            _carnets.Add("Apprendre avec Franklin");
            _carnets.Add("Mon premier jeu de logique");
        }

        public void ChoiceCarnet()
        {
            // Var declaration
            int page = 1;
            char choice;
            bool quit = false;

            // Choice carnet
            do
            {
                G_Console.ShowText($"Choisissez le carnet d'activités pour votre enfant (Carnet Actuel : {(_carnetChoisi.Equals("") ? "N/A" : _carnetChoisi)}");
                G_Console.DrawLine('-');
                choice = G_Console.ShowPaginatedText("Carnet d'activité", _carnets, page, 9, false);
                switch (choice)
                {
                    case 'P':
                    case 'p':
                        if (page > 1 && page < (_carnets.Count % 9 == 0 ? _carnets.Count / 9 : (_carnets.Count / 9) + 1))
                            page--;
                        quit = false;
                        break;
                    case 'S':
                    case 's':
                        if (page > 1 && page < (_carnets.Count % 9 == 0 ? _carnets.Count / 9 : (_carnets.Count / 9) + 1))
                            page++;
                        quit = false;
                        break;
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        quit = true;
                        break;
                    default:
                        G_Console.DrawError("Le choix effectué n'est pas valide, veuillez recommencer svp.");
                        quit = false;
                        break;
                }
            } while (!quit);
            _carnetChoisi = _carnets[(int.Parse(choice.ToString()) - 1) * page];
        }

        #endregion
    }
}
