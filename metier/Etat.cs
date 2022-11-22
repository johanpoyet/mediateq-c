using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediateq_AP_SIO2.metier
{
    internal class Etat
    {
        private string id;
        private string libelle;

        public Etat(string unId, string unLibelle)
        {
            ID = unId;
            Libelle = unLibelle;

        }

        public string ID { get => id; set => id = value; }
        public string Libelle { get => libelle; set => libelle = value; }
    }
}
