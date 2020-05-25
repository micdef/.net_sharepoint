using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Voyage.Models
{
    public class Enfant : Personne
    {

        #region Fields

        private readonly List<string> _carnets;
        private string _carnetChoisi;
        private Adulte _responsable;

        #endregion

        #region Constructors

        private Enfant()
        {
            this._carnets = null;
            this._carnetChoisi = null;
            this._responsable = null;
        }

        private Enfant(string nom, string prenom, DateTime dateNaissance, string groupe, Adulte responsable, int ageAdulte) : base (nom, prenom, dateNaissance, groupe, ageAdulte)
        {
            this._carnets = new List<string>();
            this._carnetChoisi = null;
            this.Responsable = responsable;
            RemplirListeCarnets();
        }

        #endregion

        #region Getters-Setters

        public ReadOnlyCollection<string> Carnets
        {
            get { return _carnets.AsReadOnly(); }
        }

        public string CarnetChoisi
        {
            get { return _carnetChoisi; }
            set
            {
                if (value == null || value.Trim().Length == 0)
                    throw new ArgumentNullException("L'argument passé ne peut pas être vide");
                if (!_carnets.Contains(value))
                    throw new MissingMemberException("L'élément choisi ne se trouve pas dans la liste.");
                _carnetChoisi = value;
            }
        }

        public Adulte Responsable
        {
            get { return _responsable; }
            set
            {
                if (value == null)
                    throw new NullReferenceException("L'objet passé ne peut pas être vide");
                _responsable = value;
            }
        }

        public override DateTime DateNaissance
        {
            get { return base.DateNaissance; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("new DateTime((DateTime.Now - _dateNaissance).Ticks).Year");
                if (new DateTime((DateTime.Now - value).Ticks).Year >= base.AgeAdulte)
                    throw new Exception("La date de naissance indique que vous avez entré une personne qui n'est pas un enfant");
                base.DateNaissance = value;

            }
        }

        #endregion

        #region Private Method

        private void RemplirListeCarnets()
        {
            _carnets.Add("Colorier avec Babar");
            _carnets.Add("Dessiner avec les amis Disney");
            _carnets.Add("Apprendre le Mandela");
            _carnets.Add("Apprendre avec Franklin");
            _carnets.Add("Mon premier jeu de logique");
        }

        #endregion

        #region Public Method

        #endregion
    }
}
