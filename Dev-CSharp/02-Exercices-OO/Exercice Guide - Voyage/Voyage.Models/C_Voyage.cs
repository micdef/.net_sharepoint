using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Voyage.Models
{
    public class Voyages
    {

        #region Fields

        private double _prixAdulte;
        private double _prixEnfant;
        private int _nombreMaxFilms;
        private double _pctReducOrganisateur;
        private List<Personne> _participants;
        private List<String> _filmsDemandes;
        private Dictionary<String, int> _carnetsDemandes;
        private Adulte _organisateur;

        #endregion

        #region Constructors

        private Voyages()
        {
            this._prixAdulte = 0.0;
            this._prixEnfant = 0.0;
            this._nombreMaxFilms = 0;
            this._pctReducOrganisateur = 0.0;
            this._participants = new List<Personne>();
            this._filmsDemandes = new List<string>();
            this._carnetsDemandes = new Dictionary<string, int>();
            this._organisateur = null;
        }

        public Voyages(double prixAdulte, double prixEnfant, int nombreMaxFilms, double pctReducOrganisateur, Adulte organisateur)
        {
            this.PrixAdulte = prixAdulte;
            this.PrixEnfant = prixEnfant;
            this.NombreMaxFilms = nombreMaxFilms;
            this.PctReducOrganisateur = pctReducOrganisateur;
            this.Organisateur = organisateur;
            this._participants = new List<Personne>();
            this._filmsDemandes = new List<string>();
            this._carnetsDemandes = new Dictionary<string, int>();
        }

        #endregion

        #region Getters-Setters

        public double PrixAdulte
        {
            get { return _prixAdulte; }
            private set
            {
                if (value < 0.0)
                    throw new Exception("Le montant demandé ne peut pas être négatif");
                _prixAdulte = value;
            }
        }

        public double PrixEnfant
        {
            get { return _prixEnfant; }
            private set
            {
                if (value < 0.0)
                    throw new Exception("Le montant demandé ne peut pas être négatif");
                _prixEnfant = value;
            }
        }

        public int NombreMaxFilms
        {
            get { return _nombreMaxFilms; }
            private set
            {
                if (value < 0)
                    throw new Exception("Le nombre demandé ne peut pas être négatif");
                _nombreMaxFilms = value;
            }
        }

        public double PctReducOrganisateur
        {
            get { return _pctReducOrganisateur; }
            private set { _pctReducOrganisateur = value; }
        }

        public ReadOnlyCollection<Personne> Participants
        {
            get { return _participants.AsReadOnly(); }
        }

        public ReadOnlyCollection<string> FilmsDemandes
        {
            get { return _filmsDemandes.AsReadOnly(); }
        }

        public Dictionary<string, int> CarnetsDemandes
        {
            get
            {
                Dictionary<string, int> d = _carnetsDemandes;
                return d;
            }
        }

        public Adulte Organisateur
        {
            get { return _organisateur; }
            set { _organisateur = value; }
        }

        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        public void RemplirCarnetsDemande()
        {
            if (_participants.Count == 0)
                throw new IndexOutOfRangeException("La liste des participants ne contient pas d'éléments");
            foreach (Personne p in _participants)
                if (p is Enfant)
                    if (!_carnetsDemandes.ContainsKey(((Enfant)p).CarnetChoisi))
                        _carnetsDemandes.Add(((Enfant)p).CarnetChoisi, 1);
                    else
                        _carnetsDemandes[((Enfant)p).CarnetChoisi] = _carnetsDemandes[((Enfant)p).CarnetChoisi]++;
        }

        public void RemplirFilmsDemandés()
        {
            if (_participants.Count == 0)
                throw new IndexOutOfRangeException("La liste des participants ne contient pas d'éléments");
            Dictionary<String, int> d = RecupererListeFilmsDemandes();
            var items = from pair in d
                        select pair;
            for (int i = 0; i < _nombreMaxFilms; i++)
                _filmsDemandes.Add((items.ToList())[i].Key);
        }

        public Dictionary<String, int> RecupererListeFilmsDemandes()
        {
            if (_participants.Count == 0)
                throw new IndexOutOfRangeException("La liste des participants ne contient pas d'éléments");
            Dictionary<String, int> d = new Dictionary<string, int>();
            foreach (Personne p in _participants)
                if (p is Adulte)
                    foreach (string film in ((Adulte)p).FilmsChoisis)
                        if (!d.ContainsKey(film))
                            d.Add(film, 1);
                        else
                            d[film] = d[film]++;
            var items = from pair in d
                        orderby pair.Value descending
                        select pair;
            d = new Dictionary<string, int>();
            foreach (var item in items)
                d.Add(item.Key, item.Value);
            return d;
        }

        public void AjouterParticipant(Personne participant)
        {
            if (participant == null)
                throw new NullReferenceException("L'objet passé ne peut pas être vide");
            if (_participants.Contains(participant))
                throw new Exception("Le participant existe déjà dans la liste");
            _participants.Add(participant);
        }

        public void RetirerParticipant(Personne participant)
        {
            if (participant == null)
                throw new NullReferenceException("L'objet passé ne peut pas être vide");
            if (!_participants.Contains(participant))
                throw new MissingMemberException("Le particpant ne fait pas partie de la liste");
            _participants.Remove(participant);
        }

        public List<Personne> RecupererCompositionGroupe(string nomGroupe)
        {
            List<Personne> l = new List<Personne>();
            foreach (Personne personne in _participants)
                if (personne.Groupe.Equals(nomGroupe))
                    l.Add(personne);
            return l;
        }

        public double CalculPctReducOrganisateur()
        {
            double pctTotal = 0.0;
            foreach (Personne p in _participants)
                pctTotal = (pctTotal + _pctReducOrganisateur >= 100.0 ? 100.0 : pctTotal + _pctReducOrganisateur);
            return pctTotal;
        }

        public double CalculPrixVoyage()
        {
            double tot = 0.0;
            foreach (Personne p in _participants)
                tot += ((p is Enfant) ? _prixEnfant : _prixEnfant);
            tot += _prixAdulte - (_prixAdulte * (CalculPctReducOrganisateur() / 100.0));
            return tot;
        }

        public Dictionary<Personne, double> CalculPrixParGroupe(string nomGroupe)
        {
            Dictionary<Personne, double> d = new Dictionary<Personne, double>();
            foreach (Personne participant in _participants)
                if (participant.Groupe.Equals(nomGroupe))
                    d.Add(participant, ((participant is Enfant) ? _prixEnfant : _prixAdulte));
            return d;
        }

        #endregion
    }
}
