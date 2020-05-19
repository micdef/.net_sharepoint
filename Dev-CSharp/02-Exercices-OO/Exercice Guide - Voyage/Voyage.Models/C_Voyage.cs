using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voyage.Models
{
    public class Voyage
    {
        #region Fields

        private readonly double _prixAdulte;
        private readonly double _prixEnfant;
        private readonly int _nombreMaxFilms;
        private List<Personne> _participants;
        private List<String> _filmDisponibles;

        #endregion

        #region Constructors

        public Voyage()
        {
            _prixAdulte = 60.0;
            _prixEnfant = 40.0;
            _nombreMaxFilms = 4;
            _participants = new List<Personne>();
            _filmDisponibles = new List<string>();
        }

        #endregion

        #region Getters-Setters

        public Double PrixAdulte
        {
            get { return _prixAdulte; }
        }

        public Double PrixEnfant
        {
            get { return _prixEnfant; }
        }

        public List<Personne> Participants
        {
            get { return _participants; }
            private set { value = null; }
        }

        public List<String> FilmsDisponibles
        {
            get { return _filmDisponibles; }
            private set { value = null; }
        }

        #endregion

        #region Methods

        public void AjoutParticipant(Personne p)
        {
            bool trouve = false;
            for (int i = 0; i < _participants.Count && !trouve; i++)
                if (p.Equals(_participants[i])) trouve = true;
            if (!trouve) _participants.Add(p);
        }

        public void RetraitParticipant(Personne p)
        {
            bool trouve = false;
            for (int i = 0; i < _participants.Count && !trouve; i++)
                if (p.Equals(_participants[i]))
                {
                    _participants.Remove(p);
                    trouve = true;
                }
        }

        public void AjoutFilm(string film)
        {
            bool trouve = false;
            for (int i = 0; i < _filmDisponibles.Count && !trouve; i++)
                if (film.Equals(_filmDisponibles[i])) trouve = true;
            if (!trouve && _filmDisponibles.Count < _nombreMaxFilms) 
                _filmDisponibles.Add(film);
            else
                G_Console.DrawError($"Le nombre maximum de film {_nombreMaxFilms} est déjà atteint. Veuillez retirer un film pour pouvoir en ajouter un");
        }

        public void RetraitFilm(string film)
        {
            bool trouve = false;
            for (int i = 0; i < _filmDisponibles.Count && !trouve; i++)
                if (film.Equals(_filmDisponibles[i]))
                {
                    _filmDisponibles.Remove(film);
                    trouve = true;
                }
        }

        #endregion
    }
}
