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
        /// <summary>
        /// getter sur les catégories
        /// </summary>
        /// <returns>liste des catégories</returns>
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

        /// <summary>
        /// getter sur les descripteurs
        /// </summary>
        /// <returns>liste des descripteur</returns>
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

        /// <summary>
        /// getter sur les acteurs
        /// </summary>
        /// <returns>liste des acteurs</returns>
        public static List<Acteur> getAllActeurs()
        {
            List<Acteur> lesActeurs = new List<Acteur>();
            try
            {
                string req = "Select * from acteur";

                DAOFactory.connecter();

                MySqlDataReader reader = DAOFactory.execSQLRead(req);

                while (reader.Read())
                {
                    Acteur act = new Acteur(reader[0].ToString(), reader[1].ToString());
                    lesActeurs.Add(act);
                }
                DAOFactory.deconnecter();
            }

            catch (System.Exception exc)
            {
                throw exc;
            }
            return lesActeurs;
        }

        /// <summary>
        /// getter sur les livres
        /// </summary>
        /// <returns>liste des livres</returns>
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


        /// <summary>
        /// renvoi la ctégorie du livre en paramètre
        /// </summary>
        /// <param name="pLivre"></param>
        /// <returns>ctégorie</returns>
        public static Categorie getCategorieByLivre(Livre pLivre)
        {
            try
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
            catch (System.Exception exc)
            {
                throw exc;
            }
        }


        /// <summary>
        /// getter sur les documents
        /// </summary>
        /// <returns>liste de documents</returns>
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

        /// <summary>
        /// getter sur les commandes
        /// </summary>
        /// <returns>liste de commandes</returns>
        public static List<Commande> getAllCommandes()
        {
            List<Commande> lesCommandes = new List<Commande>();
            try
            {
                Commande com;
                string req = "Select c.id, c.nbExemplaire, c.dateCommande, c.montant, d.id, d.titre, d.image, categ.id, categ.libelle, e.id, e.libelle FROM commande c JOIN document d ON c.idDoc = d.id JOIN etatcommande e ON c.idEtatCommande = e.id JOIN categorie categ ON d.idCategorie = categ.id WHERE c.idEtatCommande";

                DAOFactory.connecter();

                MySqlDataReader reader = DAOFactory.execSQLRead(req);

                while (reader.Read())
                {
                    Categorie categ = new Categorie(reader[7].ToString(), reader[8].ToString());
                    EtatCommande etat1 = new EtatCommande(reader[9].ToString(), reader[10].ToString());
                    Document document = new Document(reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), categ);
                    com = new Commande(reader[0].ToString(), int.Parse(reader[1].ToString()), DateTime.Parse(reader[2].ToString()), Double.Parse(reader[3].ToString()),
                    document, etat1);

                    lesCommandes.Add(com);

                }

                DAOFactory.deconnecter();
            }

            catch (System.Exception exc)
            {
                throw exc;
            }

            return lesCommandes;
        }


        /// <summary>
        /// getter sur les Dvd
        /// </summary>
        /// <returns>liste de DVD</returns>
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

        /// <summary>
        /// getter sur les users
        /// </summary>
        /// <returns>liste d'users</returns>
        public static List<Login> getAllUsers()
        {
            List<Login> lesUsers = new List<Login>();



            try
            {
                string req = "Select l.id, l.pseudo, l.password, l.prenom, l.nom, l.idService, s.id, s.libelle FROM login l JOIN service s ON l.idService = s.id";



                DAOFactory.connecter();



                MySqlDataReader reader = DAOFactory.execSQLRead(req);




                while (reader.Read())
                {
                    Service service = new Service(int.Parse(reader[6].ToString()), reader[7].ToString());
                    Login user = new Login(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), service);
                    // On ne renseigne pas le genre et la catégorie car on ne peut pas ouvrir 2 dataReader dans la même connexion
                    lesUsers.Add(user);
                   
                }
                DAOFactory.deconnecter();

            }



            catch (System.Exception exc)
            {
                throw exc;
            }



            return lesUsers;
        }

        /// <summary>
        /// getter sur les services
        /// </summary>
        /// <returns>liste de services</returns>
        public static List<Service> getAllService()
        {
            List<Service> lesServices = new List<Service>();



            try
            {
                string req = "SELECT * FROM service";



                DAOFactory.connecter();



                MySqlDataReader reader = DAOFactory.execSQLRead(req);




                while (reader.Read())
                {
                    Service service = new Service(int.Parse(reader[0].ToString()), reader[1].ToString());
                   
                  
                    lesServices.Add(service);

                }
                DAOFactory.deconnecter();

            }



            catch (System.Exception exc)
            {
                throw exc;
            }



            return lesServices;
        }


        /// <summary>
        /// renvoi l'utilisateur du pseudo en parametre
        /// </summary>
        /// <param name="pseudo"></param>
        /// <returns>login (user)</returns>
        public static Login getUserByPseudo(string pseudo)
        {
            Login utilisateur = null;



            try
            {
                string req = "Select l.id, l.pseudo, l.password, l.prenom, l.nom, l.idService, s.id, s.libelle FROM login l JOIN service s ON l.idService = s.id WHERE pseudo = '" + pseudo + "'";



                DAOFactory.connecter();



                MySqlDataReader reader = DAOFactory.execSQLRead(req);




                while (reader.Read())
                {
                    Service service = new Service(int.Parse(reader[6].ToString()), reader[7].ToString());
                    utilisateur = new Login(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), service);
                    // On ne renseigne pas le genre et la catégorie car on ne peut pas ouvrir 2 dataReader dans la même connexion
                   

                }
                DAOFactory.deconnecter();

            }



            catch (System.Exception exc)
            {
                throw exc;
            }



            return utilisateur;
        }

        /// <summary>
        /// renvoi le service de l'utilisateur
        /// </summary>
        /// <param name="login"></param>
        /// <returns>service</returns>
        public static Service getServiecByUser(Login login)
        {
            //List<Service> lesServices = new List<Service>();
            Service serv = null;



            try
            {
                string req = "Select s.id, s.libelle FROM service s JOIN login l on s.id = l.idService WHERE l.pseudo = '" + login.Pseudo + "'";



                DAOFactory.connecter();



                MySqlDataReader reader = DAOFactory.execSQLRead(req);




                while (reader.Read())
                {
                    serv = new Service(int.Parse(reader[0].ToString()), reader[1].ToString());
                    // On ne renseigne pas le genre et la catégorie car on ne peut pas ouvrir 2 dataReader dans la même connexion
                    //lesServices.Add(service);

                }
                DAOFactory.deconnecter();

            }



            catch (System.Exception exc)
            {
                throw exc;
            }



            return serv;
        }


        /// <summary>
        /// getter sur les etatsCommande
        /// </summary>
        /// <returns>liste d'etatsCommande</returns>
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


        /// <summary>
        /// renvoi la commande de etatCommande en parametre
        /// </summary>
        /// <param name="etat"></param>
        /// <returns>liste de commandes</returns>
        public static List<Commande> getCommandeByEtatCommande(EtatCommande etat)
        {
            List<Commande> lesCommandesByEtat = new List<Commande>();
            try
            {

            
                Commande commande;
                string req = "Select c.id, c.nbExemplaire, c.dateCommande, c.montant, d.id, d.titre, d.image, categ.id, categ.libelle, e.id, e.libelle  FROM commande c JOIN document d ON c.idDoc = d.id JOIN etatcommande e ON c.idEtatCommande = e.id JOIN categorie categ ON d.idCategorie = categ.id WHERE c.idEtatCommande = '" + etat.ID + "'";


                DAOFactory.connecter();

                MySqlDataReader reader = DAOFactory.execSQLRead(req);

                while (reader.Read())
                {
                    Categorie categ = new Categorie(reader[7].ToString(), reader[8].ToString());
                    EtatCommande etat1 = new EtatCommande(reader[9].ToString(), reader[10].ToString());
                    Document document = new Document(reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), categ);
                    commande = new Commande(reader[0].ToString(), int.Parse(reader[1].ToString()), DateTime.Parse(reader[2].ToString()), Double.Parse(reader[3].ToString()),
                    document, etat1);

                    lesCommandesByEtat.Add(commande);

            }

            

            DAOFactory.deconnecter();
            }

            catch (System.Exception exc)
            {
                throw exc;
            }
            return lesCommandesByEtat;
        }


        /// <summary>
        /// renvoi les documents de l'etatCommande en parametre
        /// </summary>
        /// <param name="etat"></param>
        /// <returns>liste de documents</returns>
        public static List<Document> getDocumentByEtatCommande(EtatCommande etat)
        {
            List<Document> lesDocumentsByEtat = new List<Document>();
            try
            {
                Document document;
                string req = "Select d.id, d.titre, d.image, categ.id, categ.libelle, e.id, e.libelle FROM commande c JOIN etatcommande e ON c.idEtatCommande = e.id JOIN document d ON c.idDoc = d.id JOIN categorie categ ON d.idCategorie = categ.id WHERE e.id ='" + etat.ID + "' ";


                DAOFactory.connecter();

                MySqlDataReader reader = DAOFactory.execSQLRead(req);

                if (reader.Read())
                {
                    Categorie categ = new Categorie(reader[3].ToString(), reader[4].ToString());
                    EtatCommande etat1 = new EtatCommande(reader[6].ToString(), reader[7].ToString());
                    Document document1 = new Document(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), categ);


                    lesDocumentsByEtat.Add(document1);

                }
                else
                {
                    document = null;
                }
                DAOFactory.deconnecter();
                return lesDocumentsByEtat;
            }
            catch (System.Exception exc)
            {
                throw exc;
            }
        }


        /// <summary>
        /// créer un dvd
        /// </summary>
        /// <param name="dvd"></param>
        public static void creerDvd(Dvd dvd)
        {
            try
            {






                string req2 = "INSERT INTO document(id, titre, image, idCategorie) VALUES ('" + dvd.IdDoc + "','" + dvd.Titre + "','" + dvd.Image + "','" + dvd.LaCategorie.Id + "')";
                string req = "INSERT INTO dvd(id, synopsis, réalisateur,duree) VALUES ('" + dvd.IdDoc + "','" + dvd.synopsis + "','" + dvd.realisateur + "','" + dvd.duree + "')";

                DAOFactory.connecter();

                DAOFactory.execSQLWrite(req2);
                DAOFactory.execSQLWrite(req);




                DAOFactory.deconnecter();
            }
            catch (System.Exception exc)
            {
                throw exc;
            }


        }

        /// <summary>
        /// créer un acteur
        /// </summary>
        /// <param name="act"></param>
        public static void creerActeur(Acteur act)
        {


            try
            {
                string req2 = "INSERT INTO acteur(id, nom) VALUES ('" + act.Id + "','" + act.Nom + "')";


                DAOFactory.connecter();

                DAOFactory.execSQLWrite(req2);

                DAOFactory.deconnecter();
            }
            catch (System.Exception exc)
            {
                throw exc;
            }


        }


        /// <summary>
        /// ajoute un acteur
        /// </summary>
        /// <param name="dvd"></param>
        /// <param name="act"></param>
        public static void ajouterActeur(Dvd dvd, Acteur act)
        {
            try
            { 
                string req2 = "INSERT INTO est_dans(idDvd, idActeur) VALUES ('" + dvd.IdDoc + "','" + act.Id + "')";


                DAOFactory.connecter();

                DAOFactory.execSQLWrite(req2);

                DAOFactory.deconnecter();
            }
            catch (System.Exception exc)
            {
                throw exc;
            }


        }


        /// <summary>
        /// créer un livre
        /// </summary>
        /// <param name="livre"></param>
        public static void creerLivre(Livre livre)
        {
            try
            {


                string req2 = "INSERT INTO document(id, titre, image, idCategorie) VALUES ('" + livre.IdDoc + "','" + livre.Titre + "','" + livre.Image + "','" + livre.LaCategorie.Id + "')";
                string req = "INSERT INTO livre(id, ISBN, auteur, collection) VALUES ('" + livre.IdDoc + "', '" + livre.ISBN1 + "','" + livre.Auteur + "','" + livre.LaCollection + "')";

                DAOFactory.connecter();

                DAOFactory.execSQLWrite(req2);
                DAOFactory.execSQLWrite(req);
                DAOFactory.deconnecter();
            }
            catch (System.Exception exc)
            {
                throw exc;
            }


        }


        /// <summary>
        /// supprimer un DVD
        /// </summary>
        /// <param name="dvd"></param>
        public static void supprimerDvd(Dvd dvd)
        {
            try
            {


                string req2 = "DELETE FROM document WHERE id = '" + dvd.IdDoc + "'";
                string req = "DELETE FROM dvd WHERE id = '" + dvd.IdDoc + "'";

                DAOFactory.connecter();

                DAOFactory.execSQLWrite(req);
                DAOFactory.execSQLWrite(req2);




                DAOFactory.deconnecter();
            }
            catch (System.Exception exc)
            {
                throw exc;
            }


        }


        /// <summary>
        /// supprime un livre
        /// </summary>
        /// <param name="livre"></param>
        public static void supprimerLivre(Livre livre)
        {
            try
            {


                string req2 = "DELETE FROM document WHERE id = '" + livre.IdDoc + "'";
                string req = "DELETE FROM livre WHERE id = '" + livre.IdDoc + "'";

                DAOFactory.connecter();

                DAOFactory.execSQLWrite(req);
                DAOFactory.execSQLWrite(req2);




                DAOFactory.deconnecter();
            }
            catch (System.Exception exc)
            {
                throw exc;
            }


        }

        /// <summary>
        /// supprim une commande
        /// </summary>
        /// <param name="com"></param>
        public static void supprimerCommande(Commande com)
        {

            try
            {


                string req = "DELETE FROM commande WHERE id = '" + com.Id + "'";

                DAOFactory.connecter();

                DAOFactory.execSQLWrite(req);





                DAOFactory.deconnecter();
            }
            catch (System.Exception exc)
            {
                throw exc;
            }


        }

        /// <summary>
        /// supprime un utilisateur
        /// </summary>
        /// <param name="user"></param>
        public static void supprimerUtilisateur(Login user)
        {
            try
            {


              
                string req = "DELETE FROM login WHERE id = '" + user.Id + "'";

                DAOFactory.connecter();

                DAOFactory.execSQLWrite(req);
             




                DAOFactory.deconnecter();
            }
            catch (System.Exception exc)
            {
                throw exc;
            }


        }

        /// <summary>
        /// modifie un dvd
        /// </summary>
        /// <param name="dvd"></param>
        public static void modifierDvd(Dvd dvd)
        {
            try
            {
                string req2 = "UPDATE document SET id = '" + dvd.IdDoc + "', titre='" + dvd.Titre + "',image= '" + dvd.Image + "',idCategorie='" + dvd.LaCategorie.Id + "' WHERE id = '" + dvd.IdDoc + "'";
                string req = "UPDATE dvd SET id='" + dvd.IdDoc + "', synopsis='" + dvd.synopsis + "', réalisateur='" + dvd.realisateur + "',duree='" + dvd.duree + "' WHERE id= '" + dvd.IdDoc + "'";

                DAOFactory.connecter();

                DAOFactory.execSQLWrite(req2);
                DAOFactory.execSQLWrite(req);




                DAOFactory.deconnecter();
            }
            catch (System.Exception exc)
            {
                throw exc;
            }


        }

        /// <summary>
        /// modifie un livre
        /// </summary>
        /// <param name="livre"></param>
        public static void modifierLivre(Livre livre)
        {
            try
            {
                string req2 = "UPDATE document SET id = '" + livre.IdDoc + "', titre='" + livre.Titre + "', image= '" + livre.Image + "', idCategorie='" + livre.LaCategorie.Id + "' WHERE id = '" + livre.IdDoc + "'";
                string req = "UPDATE livre SET id='" + livre.IdDoc + "', ISBN='" + livre.ISBN1 + "', auteur='" + livre.Auteur + "', collection='" + livre.LaCollection + "' WHERE id= '" + livre.IdDoc + "'";

                DAOFactory.connecter();

                DAOFactory.execSQLWrite(req2);
                DAOFactory.execSQLWrite(req);




                DAOFactory.deconnecter();
            }
            catch (System.Exception exc)
            {
                throw exc;
            }


        }

        /// <summary>
        /// modifie un etatCommande
        /// </summary>
        /// <param name="com"></param>
        /// <param name="etat"></param>
        public static void modifierEtatComande(Commande com, EtatCommande etat)
        {
            try
            {


                string req = "UPDATE commande SET idEtatCommande='" + etat.ID + "' WHERE id= '" + com.Id + "'";

                DAOFactory.connecter();


                DAOFactory.execSQLWrite(req);




                DAOFactory.deconnecter();
            }
            catch (System.Exception exc)
            {
                throw exc;
            }


        }


        /// <summary>
        /// setter sur descripteur
        /// </summary>
        /// <param name="lesLivres"></param>
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

        /// <summary>
        /// setter sur acteur
        /// </summary>
        /// <param name="lesDvd"></param>
        public static void setActeur(List<Dvd> lesDvd)
        {
            DAOFactory.connecter();

            foreach (Dvd dvd in lesDvd)
            {
                List<Acteur> lesActeursDuDvd = new List<Acteur>(); ;
                string req = "Select a.id, a.nom from acteur a ";
                req += " join est_dans e on a.id = e.idActeur";
                req += " join dvd d on e.idDvd = '" + dvd.IdDoc + "'";

                MySqlDataReader reader = DAOFactory.execSQLRead(req);
                while (reader.Read())
                {
                    lesActeursDuDvd.Add(new Acteur(reader[0].ToString(), reader[1].ToString()));
                }
                dvd.LesActeurs = lesActeursDuDvd;
            }
            DAOFactory.deconnecter();
        }

        /// <summary>
        /// créer une commande
        /// </summary>
        /// <param name="com"></param>
        public static void creerCommande(Commande com)
        {
            try
            {


                //création d'une date au format string pour que la date soit interprétée par la base de donnée
                string formattedDate = com.DateCommande.ToString("yyyy-MM-dd");



                string req = "INSERT INTO commande(id, nbExemplaire, dateCommande, montant, idDoc, idEtatCommande) VALUES ('" + com.Id + "','" + com.NbExemplaire + "','" + formattedDate + "','" + com.Montant + "', '" + com.Doc.IdDoc + "', '" + com.Etat.ID + "')";


                DAOFactory.connecter();


                DAOFactory.execSQLWrite(req);




                DAOFactory.deconnecter();
            }
            catch (System.Exception exc)
            {
                throw exc;
            }

        }


        /// <summary>
        /// créer un utilisateur
        /// </summary>
        /// <param name="user"></param>
        public static void creerUser(Login user)
        {
            try
            {
                string req = "INSERT INTO login(id, pseudo, password, prenom, nom, idService) VALUES ('" + user.Id + "','" + user.Pseudo + "','" + user.Password + "', '" + user.Prenom + "', '" + user.Nom + "', '" + user.Service.Id + "' )";


                DAOFactory.connecter();


                DAOFactory.execSQLWrite(req);




                DAOFactory.deconnecter();
            }
            catch (System.Exception exc)
            {
                throw exc;
            }


        }



    }

}
