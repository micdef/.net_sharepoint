using System;
using System.Globalization;

namespace Voyage.Models
{
    public abstract class Personne
    {
        #region Fields

        private string _nom;
        private string _prenom;
        private DateTime _dateNaissance;
        private string _groupe;

        #endregion

        #region Constructors

        public Personne()
        {
            _nom = null;
            _prenom = null;
            _dateNaissance = DateTime.MinValue;
        }

        public Personne(string nom, string prenom, DateTime datenaiss)
        {
            Nom = nom;
            Prenom = prenom;
            DateNaissance = datenaiss;
        }

        #endregion

        #region Getters-Setters

        public string Nom
        {
            get { return _nom; }
            set
            {
                if (value == null || value.Trim().Length == 0)
                    throw new Exception("Le nom ne peut pas être vide.");
                _nom = value;
            }
        }

        public string Prenom
        {
            get { return _prenom; }
            set
            {
                if(value == null || value.Trim().Length == 0)
                    throw new Exception("Le prénom ne peut être vide.");
                _prenom = value;
            }
        }

        public DateTime DateNaissance
        {
            get { return _dateNaissance; }
            set
            {
                if (value == null || DateTime.Compare(value, DateTime.Now) >= 0)
                    throw new Exception("La personne doit être née pour pouvoir particier à un voyage.");
                _dateNaissance = value;
            }
        }

        public string Groupe
        {
            get { return _groupe; }
            set
            {

            }
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            string s = null;
            s += $"Nom : {Nom}\n";
            s += $"Prénom : {Prenom}\n";
            s += $"Date de naissance : {DateNaissance}";
            return s;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                throw new NullReferenceException("L'objet passé ne peut pas être vide.");
            Personne p = (Personne)obj;
            if (this._nom.Equals(p.Nom) &&
                this._prenom.Equals(p.Prenom) &&
                DateTime.Compare(this._dateNaissance, p.DateNaissance) == 0)
            {
                p = null;
                return true;
            }
            else
            {
                p = null;
                return false;
            }
        }

        #endregion
    }
}
