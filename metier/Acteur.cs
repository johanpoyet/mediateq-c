using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediateq_AP_SIO2.metier
{
    internal class Acteur
    {
        private string id;
        private string nom;


        public Acteur(string id, string nom)
        {
            this.id = id;
            this.nom = nom;
        }

        public string Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
    }
}
