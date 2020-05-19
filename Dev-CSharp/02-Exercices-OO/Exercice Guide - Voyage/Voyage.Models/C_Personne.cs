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
                try
                {
                    if (value.Trim().Length > 0)
                        _nom = value;
                    else
                        throw new Exception("Le nom ne peut pas être vide.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public string Prenom
        {
            get { return _prenom; }
            set
            {
                try
                {
                    if (value.Trim().Length > 0)
                        _prenom = value;
                    else
                        throw new Exception("Le prénom ne peut être vide.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public DateTime DateNaissance
        {
            get { return _dateNaissance; }
            set
            {
                try
                {
                    if (DateTime.Compare(value, DateTime.Now) < 0)
                        _dateNaissance = value;
                    else
                        throw new Exception("La personne doit être née pour pouvoir particier à un voyage.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
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
