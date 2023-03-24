using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediateq_AP_SIO2.metier
{
    internal class Login
    {
        private int id;
        private string pseudo;
        private string password;
        private string prenom;
        private string nom;
        private Service service;


        public Login(int unId, string unPseudo, string unPassword, string unPrenom, string unNom, Service unService)
        {
            id = unId;
            pseudo = unPseudo;
            password = unPassword;
            prenom = unPrenom;
            nom = unNom;
            service = unService;
        }

        public int Id { get => id; set => id = value; }
        public string Pseudo { get => pseudo; set => pseudo = value; }
        public string Password { get => password; set => password = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Nom { get => nom; set => nom = value; }
        internal Service Service { get => service; set => service = value; }

    }
}
