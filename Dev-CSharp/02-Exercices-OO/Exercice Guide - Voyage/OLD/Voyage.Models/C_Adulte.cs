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
        private int _nbFilmsMax;
        private List<Enfant> _enfantsACharge;

        #endregion

        #region Constructors

        private Adulte()
        {
            _films = null;
            _filmsChoisis = null;
            _nbFilmsMax = 4;
            _nbFilmsRestant = _nbFilmsMax;
        }

        public Adulte(string nom, string prenom, DateTime dateNaiss) : base(nom, prenom, dateNaiss)
        {
            _films = new List<String>();
            _filmsChoisis = new List<string>();
            _nbFilmsMax = 4;
            _nbFilmsRestant = _nbFilmsMax;
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

        public void AjoutFilm(string film)
        {
            if (film == null || film.Trim().Length == 0)
                throw new ArgumentNullException("L'argument ne peut pas être null");
            if (_filmsChoisis.Contains(film))
                throw new Exception("L'élément existe déjà dans la liste");
            if (_nbFilmsRestant == 0)
                throw new IndexOutOfRangeException($"La liste dépasse le nombre autorisé d'élements. Maximum : {_nbFilmsMax}");
            _filmsChoisis.Add(film);
            _nbFilmsRestant--;
        }

        public void EnleverFilm(string film)
        {
            if (film == null || film.Trim().Length == 0)
                throw new ArgumentNullException("L'argument ne peut pas être null");
            if (!_filmsChoisis.Contains(film))
                throw new MissingMemberException("L'élément choisi n'existe pas dans la liste");
            if (_filmsChoisis.Count == 0)
                throw new IndexOutOfRangeException($"La liste ne contient aucun élement");
            _filmsChoisis.Remove(film);
            _nbFilmsRestant++;
        }


        public void AjoutEnfant(Enfant enfant)
        {
            if (enfant == null)
                throw new NullReferenceException("L'objet passé ne peut pas être vide.");
            if (_enfantsACharge.Contains(enfant))
                throw new Exception("L'élément existe déjà dans la liste");
            _enfantsACharge.Add(enfant);
        }

        public void EnleverEnfant(Enfant enfant)
        {
            if (enfant == null)
                throw new NullReferenceException("L'objet passé ne peut pas être vide.");
            if (!_enfantsACharge.Contains(enfant))
                throw new MissingMemberException("L'élément choisi n'existe pas dans la liste");
            _enfantsACharge.Remove(enfant);
        }

        #endregion

    }
}
