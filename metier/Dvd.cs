using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediateq_AP_SIO2.metier
{
    internal class Dvd : Document
    {
        private string Synopsis;
        private string Realisateur;
        private int Duree;
        private List<Acteur> lesActeurs;



        public Dvd(string unId, string unTitre, string unSynopsis, string unRealisateur, int uneDuree, string uneImage, Categorie uneCategorie) : base(unId, unTitre, uneImage, uneCategorie)
        {
            synopsis = unSynopsis;
            realisateur = unRealisateur;
            duree = uneDuree;
        }

        public string synopsis { get => Synopsis; set => Synopsis = value; }
        public string realisateur { get => Realisateur; set => Realisateur = value; }
        public int duree { get => Duree; set => Duree = value; }
        internal List<Acteur> LesActeurs { get => lesActeurs; set => lesActeurs = value; }

        public void ajouterActeur(Acteur acteur)
        {
            lesActeurs.Add(acteur);

        }

    }
}
