using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Voyage.Models
{
    public class Adulte : Personne
    {

        #region Fields

        private readonly List<string> _films;
        private List<string> _filmsChoisis;
        private int _nbFilmsRestants;
        private int _nbFilmsMax;
        private List<Enfant> _enfantsACharge;

        #endregion

        #region Constructors

        private Adulte()
        {
            this._films = null;
            this._filmsChoisis = null;
            this._nbFilmsMax = 0;
            this._nbFilmsRestants = 0;
            this._enfantsACharge = null;
        }

        public Adulte(string nom, string prenom, DateTime dateNaissance, string groupe, int nbFilmsMax, int ageAdulte) : base(nom, prenom, dateNaissance, groupe, ageAdulte)
        {
            this._films = new List<string>();
            this._filmsChoisis = new List<string>();
            this.NbFilmsMax = nbFilmsMax;
            this.NbFilmsRestants = this.NbFilmsMax;
            this._enfantsACharge = new List<Enfant>();
            RemplirListeFilms();
        }

        #endregion

        #region Getters-Setters

        public ReadOnlyCollection<string> Films
        {
            get { return _films.AsReadOnly(); }
        }

        public ReadOnlyCollection<string> FilmsChoisis
        {
            get { return _filmsChoisis.AsReadOnly(); }
            private set {}
        }

        public int NbFilmsRestants
        {
            get { return _nbFilmsRestants; }
            private set { _nbFilmsRestants = value; }
        }

        public int NbFilmsMax
        {
            get { return _nbFilmsMax; }
            private set { _nbFilmsMax = value; }
        }

        public ReadOnlyCollection<Enfant> EnfantsACharge
        {
            get { return _enfantsACharge.AsReadOnly(); }
            private set {}
        }

        public override DateTime DateNaissance
        {
            get { return base.DateNaissance; }
            set 
            {
                if (value == null)
                    throw new ArgumentNullException("new DateTime((DateTime.Now - _dateNaissance).Ticks).Year");
                if (new DateTime((DateTime.Now - value).Ticks).Year < base.AgeAdulte)
                    throw new Exception("La date de naissance indique que vous avez entré une personne qui n'est pas un adulte");
                base.DateNaissance = value;

            }
        }

        #endregion

        #region Private Methods

        private void RemplirListeFilms()
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

        #endregion

        #region Public Methods

        public void AjouterFilm(string film)
        {
            if (film == null || film.Trim().Length == 0)
                throw new ArgumentNullException("L'argument ne peut pas être null");
            if (_filmsChoisis.Contains(film))
                throw new Exception("L'élément existe déjà dans la liste");
            if (_nbFilmsRestants == 0)
                throw new IndexOutOfRangeException($"La liste dépasse le nombre autorisé d'élements. Maximum : {_nbFilmsMax}");
            _filmsChoisis.Add(film);
            _nbFilmsRestants--;
        }

        public void RetirerFilm(string film)
        {
            if (film == null || film.Trim().Length == 0)
                throw new ArgumentNullException("L'argument ne peut pas être null");
            if (!_filmsChoisis.Contains(film))
                throw new MissingMemberException("L'élément choisi n'existe pas dans la liste");
            if (_filmsChoisis.Count == 0)
                throw new IndexOutOfRangeException($"La liste ne contient aucun élement");
            _filmsChoisis.Remove(film);
            _nbFilmsRestants++;
        }

        public void AjouterEnfant(Enfant enfant)
        {
            if (enfant == null)
                throw new NullReferenceException("L'objet passé ne peut pas être vide.");
            if (_enfantsACharge.Contains(enfant))
                throw new Exception("L'élément existe déjà dans la liste");
            _enfantsACharge.Add(enfant);
        }

        public void RetirerEnfant (Enfant enfant)
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
