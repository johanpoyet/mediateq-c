using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediateq_AP_SIO2.metier
{
    internal class Exemplaire
    {
        private Etat idEtat;
        private DateTime dateAchat;
        private Rayon idRayon;
        private Document idDoc;
        private string numero;

        public Exemplaire (Document unIdDoc, string unNumero, DateTime uneDateAchat, Rayon unIdRayon, Etat unIdEtat)
        {
            idDoc = unIdDoc;
            Numero = unNumero; 
            dateAchat = uneDateAchat;
            idRayon = unIdRayon;
            idEtat = unIdEtat;

        }
        
        public string Numero { get => numero; set => numero = value; }

        
        
    }
}
