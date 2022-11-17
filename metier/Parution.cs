using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediateq_AP_SIO2.metier
{
    class Parution
    {
        private int numero;
        private DateTime dateParution;
        private string photo;
        private string idRevue;

        public Parution(int numero, DateTime dateParution, string photo, string idRevue)
        {
            this.numero = numero;
            this.dateParution = dateParution;
            this.photo = photo;
            this.idRevue = idRevue;
        }

        public int Numero { get => numero; set => numero = value; }
        public DateTime DateParution { get => dateParution; set => dateParution = value; }
        public string Photo { get => photo; set => photo = value; }
        public string IdRevue { get => idRevue; set => idRevue = value; }
    }
}
