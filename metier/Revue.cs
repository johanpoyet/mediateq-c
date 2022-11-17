using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediateq_AP_SIO2.metier
{
    class Revue
    {
        private string id;
        private string titre;
        private char empruntable;
        private string periodicite;
        private DateTime dateFinAbonnement;
        private int delaiMiseADispo;
        private string idDescripteur;

        public Revue(string id, string titre, char empruntable, string periodicite, DateTime dateFinAbonnement, int delaiMiseADispo, string idDescripteur)
        {
            this.id = id;
            this.titre = titre;
            this.empruntable = empruntable;
            this.periodicite = periodicite;
            this.dateFinAbonnement = dateFinAbonnement;
            this.delaiMiseADispo = delaiMiseADispo;
            this.idDescripteur = idDescripteur;
        }

        public string Id { get => id; set => id = value; }
        public string Titre { get => titre; set => titre = value; }
        public char Empruntable { get => empruntable; set => empruntable = value; }
        public string Periodicite { get => periodicite; set => periodicite = value; }
        public DateTime DateFinAbonnement { get => dateFinAbonnement; set => dateFinAbonnement = value; }
        public int DelaiMiseADispo { get => delaiMiseADispo; set => delaiMiseADispo = value; }
        public string IdDescripteur { get => idDescripteur; set => idDescripteur = value; }
    }
}
