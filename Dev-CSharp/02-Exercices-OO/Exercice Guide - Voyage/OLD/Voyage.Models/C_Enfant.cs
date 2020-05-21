using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Adulte _responsble;

        #endregion

        #region Constructors

        private Enfant()
        {
            _carnets = null;
            _carnetChoisi = null;
        }

        public Enfant(string nom, string prenom, DateTime dateNaiss)  : base(nom, prenom, dateNaiss)
        {
            _carnets = new List<string>();
            _carnetChoisi = null;
            FillCarnets();
        }

        #endregion

        #region Getters-Setters

        public string CarnetChoisi
        {
            get { return _carnetChoisi; }
            set 
            {
                if (value == null || value.Trim().Length == 0)
                    throw new ArgumentNullException("L'élement ne peut pas être vide");
                bool trouve = false;
                for (int i = 0; i < Carnets.Count && !trouve; i++)
                    trouve = value.Equals(Carnets[i]);
                if (!trouve)
                    throw new MissingMemberException("L'élément n'existe pas dans la liste.");
                _carnetChoisi = value;
            }
        }

        public List<string> Carnets
        {
            get
            {
                List<string> c = _carnets;
                return c;
            }
        }

        #endregion

        #region Methods

        private void FillCarnets()
        {
            Carnets.Add("Colorier avec Babar");
            Carnets.Add("Dessiner avec les amis Disney");
            Carnets.Add("Apprendre le Mandela");
            Carnets.Add("Apprendre avec Franklin");
            Carnets.Add("Mon premier jeu de logique");
        }

        #endregion
    }
}
