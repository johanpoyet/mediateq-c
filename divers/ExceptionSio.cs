using System;


namespace Mediateq_AP_SIO2.divers
{
    public class ExceptionSio : Exception
    {
        private int niveauExc;
        private string libelleExc;

        public ExceptionSio(int pNiveau, string pLibelle, string pMessage) : base(pMessage)
        {
            niveauExc = pNiveau;
            libelleExc = pLibelle;
        }

        public int NiveauExc { get => niveauExc; set => niveauExc = value; }
        public string LibelleExc { get => libelleExc; set => libelleExc = value; }
    }
}
