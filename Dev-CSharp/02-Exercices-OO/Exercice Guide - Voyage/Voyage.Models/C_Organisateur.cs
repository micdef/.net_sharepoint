using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voyage.Models
{
    public class Organisateur : Personne
    {
        #region Fields

        private readonly int _pctRemiseParPersonne;
        private int _pctTotalRemise;

        #endregion

        #region Constructors

        private Organisateur()
        {
            _pctRemiseParPersonne = 2;
            _pctTotalRemise = 0;
        }

        public Organisateur(string nom, string prenom, DateTime dateNaiss) : base(nom, prenom, dateNaiss)
        {
            _pctRemiseParPersonne = 2;
            _pctTotalRemise = 0;
        }

        #endregion

        #region Getters-Setter

        public int PctTotalRemise
        {
            get { return _pctTotalRemise; }
            private set { _pctTotalRemise = value; }
        }

        #endregion

        #region Methods

        public void calculRemise(List<Personne> participants)
        {
            for (int i = 0, _pctTotalRemise = 0; i < participants.Count && _pctTotalRemise <= 100; i++)
                if (_pctTotalRemise < 100) this._pctTotalRemise += 2;
        }

        #endregion
    }
}
