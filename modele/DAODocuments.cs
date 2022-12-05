using Mediateq_AP_SIO2.metier;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediateq_AP_SIO2
{
    class DAODocuments
    {
        public static List<Categorie> getAllCategories()
        {
            List<Categorie> lesCategories = new List<Categorie>();
            try
            {
                string req = "Select * from categorie";

                DAOFactory.connecter();

                MySqlDataReader reader = DAOFactory.execSQLRead(req);

                while (reader.Read())
                {
                    Categorie categorie = new Categorie(reader[0].ToString(), reader[1].ToString());
                    lesCategories.Add(categorie);
                }
                DAOFactory.deconnecter();
            }

            catch (System.Exception exc)
            {
                throw exc;
            }
            return lesCategories;
        }

        public static List<Descripteur> getAllDescripteurs()
        {
            List<Descripteur> lesDescripteurs = new List<Descripteur>();
            try
            {
                string req = "Select * from descripteur";

                DAOFactory.connecter();

                MySqlDataReader reader = DAOFactory.execSQLRead(req);

                while (reader.Read())
                {
                    Descripteur genre = new Descripteur(reader[0].ToString(), reader[1].ToString());
                    lesDescripteurs.Add(genre);
                }
                DAOFactory.deconnecter();
            }

            catch (System.Exception exc)
            {
                throw exc;
            }
            return lesDescripteurs;
        }
        
        public static List<Livre> getAllLivres()
        {
            List<Livre> lesLivres = new List<Livre>();
            try
            {
                string req = "Select l.id, l.ISBN, l.auteur, d.titre, d.image, l.collection, d.idCategorie, c.libelle from livre l ";
                req += " join document d on l.id=d.id";
                req += " join categorie c on d.idCategorie = c.id";

                DAOFactory.connecter();

                MySqlDataReader reader = DAOFactory.execSQLRead(req);

                while (reader.Read())
                {
                    Livre livre = new Livre(reader[0].ToString(), reader[3].ToString(), reader[1].ToString(),
                    reader[2].ToString(), reader[5].ToString(), reader[4].ToString(), new Categorie(reader[6].ToString(), reader[7].ToString()));

                    lesLivres.Add(livre);

                }

                DAOFactory.deconnecter();
            }

            catch (System.Exception exc)
            {
                throw exc;
            }

            return lesLivres;
        }

        public static Categorie getCategorieByLivre(Livre pLivre)
        {
            Categorie categorie;
            string req = "Select c.id,c.libelle from categorie c,document d where c.id = d.idCategorie and d.id='";
            req += pLivre.IdDoc + "'";

            DAOFactory.connecter();

            MySqlDataReader reader = DAOFactory.execSQLRead(req);

            if (reader.Read())
            {
                categorie = new Categorie(reader[0].ToString(), reader[1].ToString());
            }
            else
            {
                categorie = null;
            }
            DAOFactory.deconnecter();
            return categorie;
        }

        public static List<Document> getAllDocuments()
        {
            List<Document> lesDocuments = new List<Document>();
            try
            {
                string req = "Select d.id, d.titre, d.image, c.id, c.libelle FROM document d JOIN categorie c ON d.idCategorie = c.id";

                DAOFactory.connecter();

                MySqlDataReader reader = DAOFactory.execSQLRead(req);

                while (reader.Read())
                {
                    Categorie categ = new Categorie(reader[3].ToString(), reader[4].ToString());
                    Document document = new Document(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), categ);

                    lesDocuments.Add(document);

                }

                DAOFactory.deconnecter();
            }

            catch (System.Exception exc)
            {
                throw exc;
            }

            return lesDocuments;
        }

        public static List<Dvd> getAllDvd()
        {
            List<Dvd> lesDvd = new List<Dvd>();



            try
            {
                string req = "Select dvd.id, dvd.synopsis, dvd.réalisateur, dvd.duree, document.titre, document.image,categorie.id, categorie.libelle from dvd join document on dvd.id=document.id JOIN categorie ON document.idCategorie = categorie.id";



                DAOFactory.connecter();



                MySqlDataReader reader = DAOFactory.execSQLRead(req);




                while (reader.Read())
                {
                    Categorie cate = new Categorie(reader[6].ToString(), reader[7].ToString());
                    // On ne renseigne pas le genre et la catégorie car on ne peut pas ouvrir 2 dataReader dans la même connexion
                    Dvd dvd = new Dvd(reader[0].ToString(), reader[4].ToString(), reader[1].ToString(),
                        reader[2].ToString(), int.Parse(reader[3].ToString()), reader[5].ToString(), cate);
                    lesDvd.Add(dvd);
                }
                DAOFactory.deconnecter();

            }



            catch (System.Exception exc)
            {
                throw exc;
            }



            return lesDvd;
        }

        public static List<Commande> getAllCommandes()
        {
            List<Commande> lesCommandes = new List<Commande>();

            try
            {
                string req = "Select c.id, c.nbExemplaire, c.dateCommande, c.montant, d.id, d.titre, d.image, categ.id, categ.libelle, e.id, e.libelle  FROM commande c JOIN document d ON c.idDoc = d.id JOIN etatcommande e ON c.idEtatCommande = e.id JOIN categorie categ ON d.idCategorie = categ.id";



                DAOFactory.connecter();



                MySqlDataReader reader = DAOFactory.execSQLRead(req);




                while (reader.Read())
                {
                    Categorie categ = new Categorie(reader[7].ToString(), reader[8].ToString());
                    EtatCommande etat = new EtatCommande(reader[9].ToString(), reader[10].ToString());
                    Document document = new Document(reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), categ);
                    Commande commande1 = new Commande(reader[0].ToString(), int.Parse(reader[1].ToString()), DateTime.Parse(reader[2].ToString()), double.Parse(reader[3].ToString()),
                    document, etat);
                    lesCommandes.Add(commande1);
                }
                DAOFactory.deconnecter();

            }



            catch (System.Exception exc)
            {
                throw exc;
            }



            return lesCommandes;
        }

        public static List<EtatCommande> getAllEtatsCommande()
        {
            List<EtatCommande> lesEtatsCommande = new List<EtatCommande>();
            try
            {
                string req = "Select id, libelle from etatcommande";

                DAOFactory.connecter();

                MySqlDataReader reader = DAOFactory.execSQLRead(req);

                while (reader.Read())
                {
                    EtatCommande etat = new EtatCommande(reader[0].ToString(), reader[1].ToString());
                    lesEtatsCommande.Add(etat);
                }
                DAOFactory.deconnecter();
            }

            catch (System.Exception exc)
            {
                throw exc;
            }

            return lesEtatsCommande;
        }

        public static List<Commande> getCommandeByEtatCommande(EtatCommande etat)
        {
            List<Commande> lesCommandesByEtat = new List<Commande>();
            Commande commande;
            string req = "Select c.id, c.nbExemplaire, c.dateCommande, c.montant, d.id, d.titre, d.image, categ.id, categ.libelle, e.id, e.libelle  FROM commande c JOIN document d ON c.idDoc = d.id JOIN etatcommande e ON c.idEtatCommande = e.id JOIN categorie categ ON d.idCategorie = categ.id WHERE id c.idEtatCommande = '" + etat.ID + "'";


            DAOFactory.connecter();

            MySqlDataReader reader = DAOFactory.execSQLRead(req);

            if (reader.Read())
            {
                Categorie categ = new Categorie(reader[7].ToString(), reader[8].ToString());
                EtatCommande etat1 = new EtatCommande(reader[9].ToString(), reader[10].ToString());
                Document document = new Document(reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), categ);
                commande = new Commande(reader[0].ToString(), int.Parse(reader[1].ToString()), DateTime.Parse(reader[2].ToString()), Double.Parse(reader[3].ToString()),
                document, etat1);

                lesCommandesByEtat.Add(commande);

            }
            else
            {
                commande = null;
            }
            DAOFactory.deconnecter();
            return lesCommandesByEtat;
        }



        public static void creerDvd(Dvd dvd)
        {





            string req2 = "INSERT INTO document(id, titre, image, idCategorie) VALUES ('" + dvd.IdDoc + "','" + dvd.Titre + "','" + dvd.Image + "','" + dvd.LaCategorie.Id + "')";
            string req = "INSERT INTO dvd(id, synopsis, réalisateur,duree) VALUES ('" + dvd.IdDoc + "','" + dvd.synopsis + "','" + dvd.realisateur + "','" + dvd.duree + "')";

            DAOFactory.connecter();

            DAOFactory.execSQLWrite(req2);
            DAOFactory.execSQLWrite(req);




            DAOFactory.deconnecter();


        }

        public static void creerLivre(Livre livre)
        {





            string req2 = "INSERT INTO document(id, titre, image, idCategorie) VALUES ('" + livre.IdDoc + "','" + livre.Titre + "','" + livre.Image + "','" + livre.LaCategorie.Id + "')";
            string req = "INSERT INTO livre(id, ISBN, auteur, collection) VALUES ('" + livre.IdDoc + "', '" + livre.ISBN1 + "','" + livre.Auteur + "','" + livre.LaCollection + "')";

            DAOFactory.connecter();

            DAOFactory.execSQLWrite(req2);
            DAOFactory.execSQLWrite(req);




            DAOFactory.deconnecter();


        }

        public static void supprimerDvd(Dvd dvd)
        {

            string req2 = "DELETE FROM document WHERE id = '"+ dvd.IdDoc +"'";
            string req = "DELETE FROM dvd WHERE id = '" + dvd.IdDoc + "'";

            DAOFactory.connecter();

            DAOFactory.execSQLWrite(req);
            DAOFactory.execSQLWrite(req2);




            DAOFactory.deconnecter();


        }


        public static void supprimerLivre(Livre livre)
        {

            string req2 = "DELETE FROM document WHERE id = '" + livre.IdDoc + "'";
            string req = "DELETE FROM livre WHERE id = '" + livre.IdDoc + "'";

            DAOFactory.connecter();

            DAOFactory.execSQLWrite(req);
            DAOFactory.execSQLWrite(req2);




            DAOFactory.deconnecter();


        }

        public static void modifierDvd(Dvd dvd)
        {





            string req2 = "UPDATE document SET id = '" + dvd.IdDoc + "', titre='" + dvd.Titre + "',image= '" + dvd.Image + "',idCategorie='" + dvd.LaCategorie.Id + "' WHERE id = '" + dvd.IdDoc + "'";
            string req = "UPDATE dvd SET id='" + dvd.IdDoc + "', synopsis='" + dvd.synopsis + "', réalisateur='" + dvd.realisateur + "',duree='" + dvd.duree + "' WHERE id= '" + dvd.IdDoc + "'";

            DAOFactory.connecter();

            DAOFactory.execSQLWrite(req2);
            DAOFactory.execSQLWrite(req);




            DAOFactory.deconnecter();


        }


        public static void modifierLivre(Livre livre)
        {





            string req2 = "UPDATE document SET id = '" + livre.IdDoc + "', titre='" + livre.Titre + "', image= '" + livre.Image + "', idCategorie='" + livre.LaCategorie.Id + "' WHERE id = '" + livre.IdDoc + "'";
            string req = "UPDATE livre SET id='" + livre.IdDoc + "', ISBN='" + livre.ISBN1 + "', auteur='" + livre.Auteur + "', collection='" + livre.LaCollection + "' WHERE id= '" + livre.IdDoc + "'";

            DAOFactory.connecter();

            DAOFactory.execSQLWrite(req2);
            DAOFactory.execSQLWrite(req);




            DAOFactory.deconnecter();


        }



        public static void setDescripteurs(List<Livre> lesLivres)
        {
            DAOFactory.connecter();

            foreach (Livre livre in lesLivres)
            {
                List<Descripteur> lesDescripteursDuLivre = new List<Descripteur>(); ;
                string req = "Select de.id, de.libelle from descripteur de ";
                req += " join est_decrit_par e on de.id = e.idDesc";
                req += " join document do on do.id = '" + livre.IdDoc + "'";
                             
                MySqlDataReader reader = DAOFactory.execSQLRead(req);
                while (reader.Read())
                {
                    lesDescripteursDuLivre.Add(new Descripteur(reader[0].ToString(), reader[1].ToString()));
                }
                livre.LesDescripteurs = lesDescripteursDuLivre;
            }
            DAOFactory.deconnecter();
        }


        public static void creerCommande(Commande com)
        {





            string req = "INSERT INTO commande(id, nbExemplaire, dateCommande, montant, idDoc, idEtatCommande) VALUES ('" + com.Id + "','" + com.NbExemplaire + "','" + com.DateCommande + "','" + com.Montant + "', '" + com.Doc.IdDoc + "', '" + com.Etat.ID + "')";
          

            DAOFactory.connecter();

            
            DAOFactory.execSQLWrite(req);




            DAOFactory.deconnecter();


        }



    }

}
