using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            this._nom = null;
            this._prenom = null;
            this._dateNaissance = DateTime.MinValue;
        }

        public Personne(string nom, string prenom, DateTime dateNaissance, string groupe)
        {
            this.Nom = nom;
            this.Prenom = Prenom;
            this.Groupe = groupe;
            this.DateNaissance = dateNaissance;
        }

        #endregion

        #region Getters-Setters

        public string Nom
        {
            get { return _nom; }
            set
            {
                if (value == null || value.Trim().Length == 0)
                    throw new ArgumentNullException("L'argument passé ne peut pas être vide");
                _nom = value;
            }
        }

        public string Prenom
        {
            get { return _prenom; }
            set
            {
                if (value == null || value.Trim().Length == 0)
                    throw new ArgumentNullException("L'argument passé ne peut pas être vide");
                _prenom = value;
            }
        }

        public DateTime DateNaissance
        {
            get { return _dateNaissance; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("L'argument passé ne peut pas être vide");
                if (DateTime.Compare(value, DateTime.Now) >= 0)
                    throw new Exception("La personne doit être née pour pouvoir participer à un voyage");
                _dateNaissance = value;
            }
        }

        public string Groupe
        {
            get { return _groupe; }
            set
            {
                if (value == null || value.Trim().Length == 0)
                    throw new ArgumentNullException("L'argument passé ne peut pas être vide");
                _groupe = value;
            }
        }

        public int Age
        {
            get { return new DateTime((DateTime.Now - _dateNaissance).Ticks).Year; }
        }

        #endregion

        #region Overrides
        public override bool Equals(object obj)
        {
            if (obj == null)
                throw new NullReferenceException("L'objet passé ne peut pas être vide");
            if (GetType() != obj.GetType())
                throw new InvalidCastException($"L'objet passé n'est pas un objet de type {GetType()}");
            Personne p = (Personne)obj;
            return p.Nom.Equals(this.Nom) && p.Prenom.Equals(this.Prenom) && DateTime.Compare(p.DateNaissance, this.DateNaissance) == 0;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            string s = "";
            s += $"Nom : {_nom}\n";
            s += $"Prénom : {_prenom}\n";
            s += $"Date de naissance : {_dateNaissance} ==> Age : {Age}";
            return s;
        }

        public static bool operator ==(Personne p1, Personne p2)
        {
            if ((object)p1 == null)
                throw new NullReferenceException("L'objet passé pour l'argument p1 ne peut pas être vide");
            if ((object)p2 == null)
                throw new NullReferenceException("L'objet passé pour l'argument p2 ne peut pas être vide");
            return p1.Equals(p2);
        }

        public static bool operator !=(Personne p1, Personne p2)
        {
            if ((object)p1 == null)
                throw new NullReferenceException("L'objet passé pour l'argument p1 ne peut pas être vide");
            if ((object)p2 == null)
                throw new NullReferenceException("L'objet passé pour l'argument p2 ne peut pas être vide");
            return !p1.Equals(p2);
        }

        #endregion
    }
}
