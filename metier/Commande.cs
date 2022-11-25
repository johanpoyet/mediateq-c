using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediateq_AP_SIO2.metier
{
    internal class Commande
    {
        private string id;
        private int nbExemplaire;
        private DateTime dateCommande;
        private int montant;
        private Document doc;
        private EtatCommande etat;

        public Commande(string unId, int unNbExemplaire, DateTime uneDateCommande, int unMontant, Document unDoc, EtatCommande unEtat)
        {
            Id = unId;
            NbExemplaire = unNbExemplaire;
            DateCommande = uneDateCommande;
            Montant = unMontant;
            Doc = unDoc;
            Etat = unEtat;
        }

        public string Id { get => id; set => id = value; }
        public int NbExemplaire { get => nbExemplaire; set => nbExemplaire = value; }
        public DateTime DateCommande { get => dateCommande; set => dateCommande = value; }
        public int Montant { get => montant; set => montant = value; }
        internal Document Doc { get => doc; set => doc = value; }
        internal EtatCommande Etat { get => etat; set => etat = value; }


    }
}
