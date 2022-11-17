using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Mediateq_AP_SIO2.metier;

namespace Mediateq_AP_SIO2
{
    class DAOPresse
    {
        /*public static List<Domaine> getAllDomaines()
        {
            List<Domaine> lesDomaines = new List<Domaine>();
            string req = "Select * from domaine";

            DAOFactory.connecter();

            MySqlDataReader reader = DAOFactory.execSQLRead(req);

            while (reader.Read())
            {
                Domaine domaine = new Domaine(reader[0].ToString(), reader[1].ToString());
                lesDomaines.Add(domaine);
            }
            DAOFactory.deconnecter();
            return lesDomaines;
        }

        public static Domaine getDomainesById(string pId)
        {
            Domaine domaine;
            string req = "Select * from domaine where idDomaine = " + pId;

            DAOFactory.connecter();

            MySqlDataReader reader = DAOFactory.execSQLRead(req);

            if (reader.Read())
            {
                domaine = new Domaine(reader[0].ToString(), reader[1].ToString());
            }
            else
            {
                domaine =null;
            }
            DAOFactory.deconnecter();
            return domaine;
        }

        public static Domaine getDomainesByTitre(Revue pTitre)
        {
            Domaine domaine;
            string req = "Select d.idDomaine,d.libelle from domaine d,titre t where d.idDomaine = t.idDomaine and t.idTitre='" ;
            req += pTitre.IdTitre + "'";

            DAOFactory.connecter();

            MySqlDataReader reader = DAOFactory.execSQLRead(req);

            if (reader.Read())
            {
                domaine = new Domaine(reader[0].ToString(), reader[1].ToString());
            }
            else
            {
                domaine = null;
            }
            DAOFactory.deconnecter();
            return domaine;
        }
        */

        public static List<Revue> getAllTitre()
        {
            List<Revue> lesTitres = new List<Revue>();
            string req = "Select * from revue";

            DAOFactory.connecter();

            MySqlDataReader reader = DAOFactory.execSQLRead(req);

            while (reader.Read())
            {
                Revue titre = new Revue(reader[0].ToString(), reader[1].ToString(), char.Parse(reader[2].ToString()), reader[3].ToString(),DateTime.Parse(reader[5].ToString()), int.Parse(reader[4].ToString()), reader[6].ToString());
                lesTitres.Add(titre);
            }
            DAOFactory.deconnecter();

            return lesTitres;
        }
        
        public static List<Parution> getParutionByTitre(Revue pTitre)
        {
            List<Parution> lesParutions = new List<Parution>();
            string req = "Select * from parution where idRevue = " + pTitre.Id;

            DAOFactory.connecter();

            MySqlDataReader reader = DAOFactory.execSQLRead(req);

            while (reader.Read())
            {
                Parution parution = new Parution(int.Parse(reader[1].ToString()), DateTime.Parse(reader[2].ToString()), reader[3].ToString(), pTitre.Id);
                lesParutions.Add(parution);
            }
            DAOFactory.deconnecter();
            return lesParutions;
        }

    }
}

