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


        public Login(int unId, string unPseudo, string unPassword)
        {
            id = unId;
            pseudo = unPseudo;
            password = unPassword;
        }

        public int Id { get => id; set => id = value; }
        public string Pseudo { get => pseudo; set => pseudo = value; }
        public string Password { get => password; set => password = value; }
    }
}
