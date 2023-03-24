using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediateq_AP_SIO2.metier
{
    internal class Service
    {
        private int id;
        private string libelle;
        



        public Service(int unId, string unLibelle)
        {
            id = unId;
            libelle = unLibelle;
           
           
        }

        public int Id { get => id; set => id = value; }
        public string Libelle { get => libelle; set => libelle = value; }
    
    }
}
