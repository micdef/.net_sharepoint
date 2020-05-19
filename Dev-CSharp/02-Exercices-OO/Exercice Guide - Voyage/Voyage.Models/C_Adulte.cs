using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Net.Sockets;
using System.Text;

namespace Voyage.Models
{
    public class Adulte : Personne
    {
        #region Fields

        private readonly List<string> _films;
        private List<String> _filmsChoisis;
        private int _nbFilmsRestant;
        private List<Enfant> _enfantsACharge;

        #endregion

        #region Constructors

        private Adulte()
        {
            _films = null;
            _filmsChoisis = null;
            _nbFilmsRestant = 4;
        }

        public Adulte(string nom, string prenom, DateTime dateNaiss) : base(nom, prenom, dateNaiss)
        {
            _films = new List<String>();
            _filmsChoisis = new List<string>();
            _nbFilmsRestant = 4;
            FillFilmList();
        }

        #endregion

        #region Methods

        private void FillFilmList()
        {
            _films.Add("Star Wars I : La menace fantôme");
            _films.Add("Star Wars II : L'attaque des clones");
            _films.Add("Star Wars III : La revanche des Sith");
            _films.Add("Star Wars IV : Un Nouvel Espoir");
            _films.Add("Star Wars V : L'empire contre-attaque");
            _films.Add("Star Wars VI : Le retour du Jedi");
            _films.Add("Star Wars VII : Le réveil de la Force");
            _films.Add("Star Wars VIII : Les derniers Jedi");
            _films.Add("Star Wars IX : L'ascension de Skywalker");
            _films.Add("Shining");
            _films.Add("Carrie");
            _films.Add("Dreamcatcher");
            _films.Add("Ca");
            _films.Add("Le monde de Némo");
            _films.Add("Les nouveaux héros");
            _films.Add("La reine des neiges");
            _films.Add("Sonic : Le Film");
            _films.Add("Iron Man");
            _films.Add("Iron Man 2");
            _films.Add("Iron Man 3");
            _films.Add("Avengers");
            _films.Add("Avengers : L'ère d'Ultron");
            _films.Add("Avengers : Infinity War");
            _films.Add("Avengers : End Game");
            _films.Add("Sherlock Holmes 1 & 2");
            _films.Add("Alita : Batlle Angel");
            _films.Add("Ghost In The Shell");
        }

        public void AddFilm()
        {
            do
            {
                // Var declaration
                int page = 1;
                char choice;
                bool quit = false;

                // Choice Film
                do
                {
                    G_Console.ShowText($"Choisissez votre film (Maximum 4) ==> Restant {_nbFilmsRestant}");
                    G_Console.DrawLine('-');
                    choice = G_Console.ShowPaginatedText("Films", _films, page, 9, true);
                    switch (choice)
                    {
                        case 'P':
                        case 'p':
                            if (page > 1 && page < (_films.Count % 9 == 0 ? _films.Count / 9 : (_films.Count / 9) + 1))
                                page--;
                            quit = false;
                            break;
                        case 'S':
                        case 's':
                            if (page > 1 && page < (_films.Count % 9 == 0 ? _films.Count / 9 : (_films.Count / 9) + 1))
                                page++;
                            quit = false;
                            break;
                        case 'Q':
                        case 'q':
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

                // Insert film to list
                if (choice != 'Q' || choice != 'q')
                {
                    _filmsChoisis.Add(_films[(int.Parse(choice.ToString()) - 1) * page]);
                    _nbFilmsRestant--;
                }

            } while (_nbFilmsRestant >= 4);
        }

        public void RemoveFilm()
        {
            char choice;
            bool quit = false;
            do
            {
                G_Console.ShowText("Choisissez le film à retirer");
                G_Console.DrawLine('-');
                choice = G_Console.ShowPaginatedText("Films Choisis", _filmsChoisis, 1, 4, true);
                switch (choice)
                {
                    case 'Q':
                    case 'q':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                        quit = true;
                        break;
                    default:
                        G_Console.DrawError("Le choix effectué n'est pas valide, veuillez recommencer svp.");
                        quit = false;
                        break;
                }
            } while (!quit);

            if (choice != 'Q' || choice != 'q')
            {
                _filmsChoisis.Remove(_filmsChoisis[int.Parse(choice.ToString())]);
                _nbFilmsRestant++;
            }
        }

        public void AjoutEnfant(Enfant e)
        {
            bool trouve = false;
            for (int i = 0; i < _enfantsACharge.Count && !trouve; i++)
                if (e.Equals(_enfantsACharge[i]))
                    trouve = true;
            if (!trouve)
                _enfantsACharge.Add(e);
        }

        public void EnleverEnfant(Enfant e)
        {
            bool trouve = false;
            for (int i = 0; i < _enfantsACharge.Count && !trouve; i++)
                if (e.Equals(_enfantsACharge[i]))
                {
                    _enfantsACharge.Remove(e);
                    trouve = true;
                }
        }

        #endregion

    }
}
