using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mediateq_AP_SIO2.metier;
using System.Text.RegularExpressions;
using K4os.Compression.LZ4.Encoders;
using System.Security.Cryptography;
using System.Text;

namespace Mediateq_AP_SIO2
{
    public partial class FrmMediateq : Form
    {

        #region Variables globales
        private login login;
        static List<Categorie> lesCategories;
        static List<Categorie> lesCategoriesModif;
        static List<Descripteur> lesDescripteurs;
        static List<Revue> lesTitres;
        static List<Livre> lesLivres;
        static List<Dvd> lesDvd;
        static List<Document> lesDocuments;
        static List<Document> lesDocumentsByEtat;
        static List<Commande> lesCommandes;
        static List<EtatCommande> lesEtatsCommande;
        static List<EtatCommande> lesEtatsCommande2;
        static List<Acteur> lesActeurs;
        static List<Login> lesUsers;
        static List<Service> lesServices;
        static Dvd dvd1;


        #endregion



        #region Procédures évènementielles
        
        public FrmMediateq()
        {
            InitializeComponent();

        }

        private void FrmMediateq_Load(object sender, EventArgs e)
        {
            try
            {
                // Création de la connexion avec la base de données
                DAOFactory.creerConnection();

                // Chargement des objets en mémoire
                lesDescripteurs = DAODocuments.getAllDescripteurs();
                lesTitres = DAOPresse.getAllTitre();
                lesCategories = DAODocuments.getAllCategories();
                lesCategoriesModif = DAODocuments.getAllCategories();
                lesDvd = DAODocuments.getAllDvd();
                lesLivres = DAODocuments.getAllLivres();
                lesDocuments = DAODocuments.getAllDocuments();
                lesCommandes = DAODocuments.getAllCommandes();
                lesEtatsCommande = DAODocuments.getAllEtatsCommande();
                lesActeurs = DAODocuments.getAllActeurs();
                lesUsers = DAODocuments.getAllUsers();
                lesServices = DAODocuments.getAllService();
            }
            catch(ExceptionSIO exc)
            {
                MessageBox.Show(exc.NiveauExc + " - " + exc.LibelleExc + " - " + exc.Message);
            }
        }

        #endregion



        #region Parutions
        //-----------------------------------------------------------
        // ONGLET "PARUTIONS"
        //------------------------------------------------------------
        private void tabParutions_Enter(object sender, EventArgs e)
        {
            //alimentation de la combobox avec les titres
            cbxTitres.DataSource = lesTitres;
            cbxTitres.DisplayMember = "titre";
        }

        private void cbxTitres_SelectedIndexChanged(object sender, EventArgs e)
        {
            //création de la liste lesParutions d'objets Parution
            List<Parution> lesParutions;

            Revue titreSelectionne = (Revue)cbxTitres.SelectedItem;
            lesParutions = DAOPresse.getParutionByTitre(titreSelectionne);

            // ré-initialisation du dataGridView
            dgvParutions.Rows.Clear();

            // Parcours de la collection des titres et alimentation du datagridview
            foreach (Parution parution in lesParutions)
            {
                dgvParutions.Rows.Add(parution.Numero, parution.DateParution, parution.Photo);
            }

        }
        #endregion



        #region Revues
        //-----------------------------------------------------------
        // ONGLET "TITRES"
        //------------------------------------------------------------
        private void tabTitres_Enter(object sender, EventArgs e)
        {
            cbxDomaines.DataSource = lesDescripteurs;
            cbxDomaines.DisplayMember = "libelle";
        }

        private void cbxDomaines_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Objet Domaine sélectionné dans la comboBox
            Descripteur domaineSelectionne = (Descripteur)cbxDomaines.SelectedItem;

            // ré-initialisation du dataGridView
            dgvTitres.Rows.Clear();

            // Parcours de la collection des titres et alimentation du datagridview
            foreach (Revue revue in lesTitres)
            {
                if (revue.IdDescripteur == domaineSelectionne.Id)
                {
                    dgvTitres.Rows.Add(revue.Id, revue.Titre, revue.Empruntable, revue.DateFinAbonnement, revue.DelaiMiseADispo);
                }
            }
        }




        private void tabDVD_Enter(object sender, EventArgs e)
        {


        }

        private void cbxDVd_SelectedIndexChanged(object sender, EventArgs e)
        {


        }
        #endregion



        #region Livres
        //-----------------------------------------------------------
        // ONGLET "LIVRES"
        //-----------------------------------------------------------

        private void tabLivres_Enter(object sender, EventArgs e)
        {
            // Chargement des objets en mémoire
            lesCategories = DAODocuments.getAllCategories();
            lesDescripteurs = DAODocuments.getAllDescripteurs();
            lesLivres = DAODocuments.getAllLivres();
            //DAODocuments.setDescripteurs(lesLivres);
        }

        private void btnRechercher_Click(object sender, EventArgs e)
        {
            // On réinitialise les champs
            lblNumero.Text = "";
            lblTitre.Text = "";
            lblAuteur.Text = "";
            lblCollection.Text = "";
            lblISBN.Text = "";
            lblImage.Text = "";
            lblCategorie.Text = "";

            // On recherche le livre correspondant au numéro de document saisi en parcourant la listes des livres.
            // S'il n'existe pas: on affiche une popup avec un message d'erreur
            bool trouve = false;
            foreach (Livre livre in lesLivres)
            {
                if (livre.IdDoc == txbNumDoc.Text)
                {
                    lblNumero.Text = livre.IdDoc;
                    lblTitre.Text = livre.Titre;
                    lblAuteur.Text = livre.Auteur;
                    lblCollection.Text = livre.LaCollection;
                    lblISBN.Text = livre.ISBN1;
                    lblImage.Text = livre.Image;
                    lblCategorie.Text = livre.LaCategorie.Libelle;
                    trouve = true;
                }
            }
            if (!trouve)
                MessageBox.Show("Document non trouvé dans les livres", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void txbTitre_TextChanged(object sender, EventArgs e)
        {
            //réinitialisation du dataGridView
            dgvLivres.Rows.Clear();

            // On parcourt tous les livres. Si le titre matche avec la saisie, on l'affiche dans le datagrid.
            foreach (Livre livre in lesLivres)
            {
                // on passe le champ de saisie et le titre en minuscules car la méthode Contains
                // tient compte de la casse.
                string saisieMinuscules;
                saisieMinuscules = txbTitre.Text.ToLower();
                string titreMinuscules;
                titreMinuscules = livre.Titre.ToLower();

                //on teste si le titre du livre contient ce qui a été saisi
                if (titreMinuscules.Contains(saisieMinuscules))
                {
                    dgvLivres.Rows.Add(livre.IdDoc, livre.Titre, livre.Auteur, livre.ISBN1, livre.LaCollection);
                }
            }
        }

        private void tabCrudLivre_Enter(object sender, EventArgs e)
        {
            // ré-initialisation du dataGridView
            dtCrudLivre.Rows.Clear();

            //alimentation de la 
            lesLivres = DAODocuments.getAllLivres();

            // alimentation des différentes combo box lors de l'entrée dans la page crud livre
            cbxLivre.DataSource = lesLivres;
            cbxLivre.DisplayMember = "titre";
            cbxCategorieLivreModif.DataSource = lesCategories;
            cbxCategorieLivreModif.DisplayMember = "libelle";
            cbxCategorieLivre.DataSource = lesCategories;
            cbxCategorieLivre.DisplayMember = "libelle";

            // Parcours de la collection des livres et alimentation du datagridview
            foreach (Livre l in lesLivres)
            {

                dtCrudLivre.Rows.Add(l.IdDoc, l.Titre, l.Auteur, l.LaCollection, l.Image, l.LaCategorie.Libelle);
            }
        }

        private void btnCreerLivre_Click(object sender, EventArgs e)
        {
            try
            {
                // on remplit les variables a l'aide des champs textes
                string titre = txTitreLivre.Text;
                string id = txIdLivre.Text;
                string auteur = txAuteurLivre.Text;
                string ISBN = txISBNLivre.Text;

                //on vérifie que les entrées id et isbn sont bien des int avec la fonction verifRegexInt
                bool verif = verifRegexInt(id);
                bool verif2 = verifRegexInt(ISBN);

                //on met les variables dans un tableau qu'on parcours pour vérifier si tous les champs sont remplis,
                //et si l'id et l'isbn sont des int en mettant des messages d'erreurs si ce n'est pas le cas
                string[] entrees = { id, titre, auteur, ISBN };
                bool verifEntrees = verifEntree(entrees);

                if (!verifEntrees)
                {
                    MessageBox.Show("Veuillez remplir tous les champs", "Erreur", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
                else
                {
                    if (!verif)
                    {
                        MessageBox.Show("L'id ne doit contenir que des chiffres", "Erreur", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    }
                    else if (!verif2)
                    {
                        MessageBox.Show("L'ISBN ne doit contenir que des chiffres", "Erreur", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    }
                    else
                    {
                        // mise en place d'un message box pour que l'utilisateurs valide sa création 
                        DialogResult dialogResult = MessageBox.Show("Etes-vous sur de vouloir créer ce Livre ?", "Validation !", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            // on remplit les variables a l'aide des champs textes et combo box 
                            string collection = txCollectionLivre.Text;
                            string image = txImageLivre.Text;

                            Categorie categ = (Categorie)cbxCategorieLivre.SelectedItem;

                            // création du nouveau livre
                            Livre livre1 = new Livre(id, titre, ISBN, auteur, collection, image, categ);

                            // On recherche le livre correspondant a l'id
                            // S'il existe deja : on affiche un popup message d'erreur
                            bool trouve = false;
                            foreach (Livre l in lesLivres)
                            {
                                if (l.IdDoc.Equals(id))
                                {
                                    trouve = true;
                                }
                                else
                                {

                                }
                            }
                            if (trouve)
                            {
                                MessageBox.Show("Un livre avec l'id : '" + id + "' existe deja", "Erreur", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                            }
                            else if (!trouve)
                            {
                                DAODocuments.creerLivre(livre1);

                                lesLivres = DAODocuments.getAllLivres();

                                // on met a jour la combobox des livres pour que le livre qui veint d'etre ajouté soit dedans
                                cbxLivre.DataSource = lesLivres;
                                cbxLivre.DisplayMember = "titre";

                                // ré-initialisation du dataGridView
                                dtCrudLivre.Rows.Clear();

                                // Parcours de la collection des livres et alimentation du datagridview
                                foreach (Livre l in lesLivres)
                                {

                                    dtCrudLivre.Rows.Add(l.IdDoc, l.Titre, l.Auteur, l.LaCollection, l.Image, l.LaCategorie.Libelle);
                                }

                                // vidage des champs texte
                                txTitreLivre.Text = "";
                                txIdLivre.Text = "";
                                txAuteurLivre.Text = "";
                                txISBNLivre.Text = "";
                                txCollectionLivre.Text = "";
                                txImageLivre.Text = "";

                                MessageBox.Show("Le livre '" + titre + "' a été créé avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else if (dialogResult == DialogResult.No)
                        {

                        }
                    }
                }
            }
            catch (System.Exception exc)
            {
                throw exc;
            }

        }

        private void btnSupprimerLivre_Click(object sender, EventArgs e)
        {
            try
            {
                // Objet Livre sélectionné dans la comboBox
                Livre livreSelection = (Livre)cbxLivre.SelectedItem;

                // mise en place d'un message box pour que l'utilisateurs valide sa suppression 
                DialogResult dialogResult = MessageBox.Show("Etes-vous sur de vouloir supprimer le livre : '" + livreSelection.Titre + "'", "Validation !", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    // on remplit les variables a l'aide des champs textes et combo box 
                    string idModif = txIdModifLivre.Text;
                    string titreModif = txTitreLivreModif.Text;
                    string auteurModif = txAuteurLivreModif.Text;
                    string ISBNModif = txISBNLivreModif.Text;
                    string collectionModif = txCollectionLivreModif.Text;
                    string imageModif = txImageLivreModif.Text;
                    Categorie categorieModif = (Categorie)cbxCategorieLivreModif.SelectedItem;

                    // création du livre a supprimé
                    Livre livreSuppr = new Livre(idModif, titreModif, auteurModif, ISBNModif, collectionModif, imageModif, categorieModif);

                    // suppréssion du livre
                    DAODocuments.supprimerLivre(livreSuppr);



                    // ré-initialisation du dataGridView
                    dtDvd.Rows.Clear();

                    // alimentation de la liste lesLivres avec les livres
                    lesLivres = DAODocuments.getAllLivres();

                    // alimentation des différentes combo box lors de l'entrée dans la page crud livre
                    cbxLivre.DataSource = lesLivres;
                    cbxLivre.DisplayMember = "titre";

                    // Parcours de la collection des livres et alimentation du datagridview
                    foreach (Livre l in lesLivres)
                    {
                        dtCrudLivre.Rows.Add(l.IdDoc, l.Titre, l.Auteur, l.LaCollection, l.Image, l.LaCategorie.Libelle);
                    }

                    MessageBox.Show("Le livre '" + livreSelection.Titre + "' a été supprimé avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (dialogResult == DialogResult.No)
                {

                }
            }
            catch (System.Exception exc)
            {
                throw exc;
            }


        }

        private void cbxLivre_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Objet Livre sélectionné dans la comboBox
            Livre livreSelection = (Livre)cbxLivre.SelectedItem;

            // on remplit les champs text en fonction du livre selectionné dans la combo box
            foreach (Livre l in lesLivres)
            {
                if (l.IdDoc == livreSelection.IdDoc)
                {
                    txIdModifLivre.Text = l.IdDoc;
                    txTitreLivreModif.Text = l.Titre;
                    txAuteurLivreModif.Text = l.Auteur;
                    txISBNLivreModif.Text = l.ISBN1;
                    txCollectionLivreModif.Text = l.LaCollection;
                    txImageLivreModif.Text = l.Image;
                    cbxCategorieLivreModif.Text = l.LaCategorie.Libelle;
                }

            }
        }

        private void btnModifierLivre_Click(object sender, EventArgs e)
        {
            try
            {
                // on remplit les variables a l'aide des champs textes
                string idModif = txIdModifLivre.Text;
                string titreModif = txTitreLivreModif.Text;
                string auteurModif = txAuteurLivreModif.Text;

                // on les met dans un tableaux
                string[] entrees = { idModif, titreModif, auteurModif };

                // on vérifie que tous les champs sont renseignés, sinon, on affiche un message d'erreur
                bool verifEntrees = verifEntree(entrees);

                if (!verifEntrees)
                {
                    MessageBox.Show("Veuillez remplir tous les champs", "Erreur", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
                else
                {


                    // Objet Livre sélectionné dans la comboBox
                    Livre livreSelection = (Livre)cbxLivre.SelectedItem;

                    // mise en place d'un message box pour que l'utilisateurs valide sa modification 
                    DialogResult dialogResult = MessageBox.Show("Etes-vous sur de vouloir modifier le livre : '" + livreSelection.Titre + "'", "Validation !", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        // on remplit les variables a l'aide des champs textes
                        string ISBNModif = txISBNLivreModif.Text;
                        string collectionModif = txCollectionLivreModif.Text;
                        string imageModif = txImageLivreModif.Text;

                        // Objet catégorie sélectionné dans la comboBox
                        Categorie categorieModif = (Categorie)cbxCategorieLivreModif.SelectedItem;

                        // création du livre a modifié
                        Livre livreModif = new Livre(idModif, titreModif, ISBNModif, auteurModif, collectionModif, imageModif, categorieModif);

                        // modification du livre
                        DAODocuments.modifierLivre(livreModif);



                        // ré-initialisation du dataGridView
                        dtCrudLivre.Rows.Clear();

                        lesLivres = DAODocuments.getAllLivres();

                        // alimentation des différentes combo box lors de l'entrée dans la page crud livre
                        cbxLivre.DataSource = lesLivres;
                        cbxLivre.DisplayMember = "titre";

                        // Parcours de la collection des livres et alimentation du datagridview
                        foreach (Livre l in lesLivres)
                        {
                            dtCrudLivre.Rows.Add(l.IdDoc, l.Titre, l.Auteur, l.LaCollection, l.Image, l.LaCategorie.Libelle);
                        }

                        MessageBox.Show("Le livre '" + livreSelection.Titre + "' a été modifié avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (dialogResult == DialogResult.No)
                    {

                    }
                }
            }
            catch (System.Exception exc)
            {
                throw exc;
            }


        }


        private void btnDeconnexion_Click(object sender, EventArgs e)
        {
            // popup de validation de deconnexion
            DialogResult dialogResult = MessageBox.Show("Etes-vous sur de vouloir vous déconnecter ?", "Déconnexion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                //affichage du formulaire de connexion et fermeture de l'interface utilisateur
                login login1 = new login();
                login1.Show();
                this.Close();
            }

        }

        private void btnFermerAppliLivre_Click(object sender, EventArgs e)
        {
            //fermeture de l'application
            Application.Exit();
        }
        #endregion



        #region DVD
        //-----------------------------------------------------------
        // ONGLET "DVD"
        //-----------------------------------------------------------
        private void btnCreerDvd_Click(object sender, EventArgs e)
        {

            try
            {
                // on remplit les variables a l'aide des champs textes
                string id = txId.Text;
                string duree = txDuree.Text;
                string synopsis = txSynopsis.Text;
                string realisateur = txRealisateur.Text;
                string titre = txTitre.Text;


                // on les met dans un tableaux
                string[] entrees = { id, duree, synopsis, realisateur, titre };

                //on vérifie que les champs sont tous renseignés et que id et durée sont des int, sinon on affihce des messages d'erreur
                bool verifEntrees = verifEntree(entrees);
                bool verif = verifRegexInt(id);
                bool verif2 = verifRegexInt(duree);

                if (!verifEntrees)
                {
                    MessageBox.Show("Veuillez remplir tous les champs", "Erreur", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
                else
                {
                    if (!verif)
                    {
                        MessageBox.Show("L'id ne doit contenir que des chiffres", "Erreur", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    }
                    else if (!verif2)
                    {
                        MessageBox.Show("La durée ne doit contenir que des chiffres", "Erreur", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    }
                    else
                    {
                        // mise en place d'un message box pour que l'utilisateurs valide sa création 
                        DialogResult dialogResult = MessageBox.Show("Etes-vous sur de vouloir créer ce DVD ?", "Validation !", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            // on remplit les variables a l'aide des champs textes et combo box 
                            int duree2 = Int32.Parse(txDuree.Text);
                            string image = txImage.Text;
                            Categorie categ = (Categorie)cbxCategorie.SelectedItem;
                            Acteur act = (Acteur)cbxActeurDvd.SelectedItem;


                            // création du nouveau dvd
                            Dvd dvd1 = new Dvd(id, titre, synopsis, realisateur, duree2, image, categ);

                            //DAODocuments.setActeur(lesDvd);


                            // On recherche le dvd correspondant a l'id
                            // S'il existe deja : on affiche un popup message d'erreur
                            // si il n'existe pas on créer le DVD
                            bool trouve = false;
                            foreach (Dvd d in lesDvd)
                            {
                                if (d.IdDoc.Equals(id))
                                {
                                    trouve = true;
                                }
                            }
                            if (trouve)
                            {
                                MessageBox.Show("Un dvd avec l'id : '" + id + "' existe deja", "Erreur", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                            }
                            else if (!trouve)
                            {
                                DAODocuments.creerDvd(dvd1);

                                lesDvd = DAODocuments.getAllDvd();


                                // alimentation des différentes combo box lors de l'entrée dans la page dvd
                                cbxDvd.DataSource = lesDvd;
                                cbxDvd.DisplayMember = "titre";

                                // ré-initialisation du dataGridView
                                dtDvd.Rows.Clear();

                                // Parcours de la collection des dvd et alimentation du datagridview
                                foreach (Dvd d in lesDvd)
                                {

                                    dtDvd.Rows.Add(d.IdDoc, d.synopsis, d.realisateur, d.duree, d.Titre, d.Image, d.LaCategorie.Libelle);
                                }

                                // vidage des champs texte
                                txTitre.Text = "";
                                txId.Text = "";
                                txSynopsis.Text = "";
                                txRealisateur.Text = "";
                                txDuree.Text = "";
                                txImage.Text = "";

                                MessageBox.Show("Le dvd '" + titre + "' a été créé avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }

                        }
                        else if (dialogResult == DialogResult.No)
                        {

                        }
                    }
                }
            }
            catch (System.Exception exc)
            {
                throw exc;
            }




        }

        private void tabDVD_Enter_1(object sender, EventArgs e)
        {
            // ré-initialisation du dataGridView
            dtDvd.Rows.Clear();

            lesDvd = DAODocuments.getAllDvd();
            lesActeurs = DAODocuments.getAllActeurs();

            // alimentation des différentes combo box lors de l'entrée dans la page dvd
            cbxDvd.DataSource = lesDvd;
            cbxDvd.DisplayMember = "titre";

            cbxCategorie.DataSource = lesCategories;
            cbxCategorie.DisplayMember = "libelle";

            cbxCategorieDvdModif.DataSource = lesCategoriesModif;
            cbxCategorieDvdModif.DisplayMember = "libelle";

            cbxActeurDvd.DataSource = lesActeurs;
            cbxActeurDvd.DisplayMember = "nom";



            // Parcours de la collection des dvd et alimentation du datagridview
            foreach (Dvd d in lesDvd)
            {

                dtDvd.Rows.Add(d.IdDoc, d.synopsis, d.realisateur, d.duree, d.Titre, d.Image, d.LaCategorie.Libelle);
            }



        }

        private void cbxDvd_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // Objet Dvd sélectionné dans la comboBox
            Dvd DvdSelection = (Dvd)cbxDvd.SelectedItem;

            // on remplit les champs text en fonction du dvd selectionné dans la combo box
            foreach (Dvd d in lesDvd)
            {
                if (d.IdDoc == DvdSelection.IdDoc)
                {
                    txIdModifDvd.Text = d.IdDoc;
                    txTitreModifDvd.Text = d.Titre;
                    txSynopsisModifDvd.Text = d.synopsis;
                    txRealisateurModifDvd.Text = d.realisateur;
                    txDureeModifDvd.Text = d.duree.ToString();
                    txImageModifDvd.Text = d.Image;
                    cbxCategorieDvdModif.Text = d.LaCategorie.Libelle;
                }

            }
        }

        private void btnModifDvd_Click(object sender, EventArgs e)
        {
            try
            {
                // on les met dans un tableaux
                string duree = txDureeModifDvd.Text;
                string synopsis = txSynopsisModifDvd.Text;
                string realisateur = txRealisateurModifDvd.Text;
                string titre = txTitreModifDvd.Text;

                // on les met dans un tableaux
                string[] entrees = { duree, synopsis, realisateur, titre };

                //on vérifie que les champs sont tous renseignés durée est un int, sinon on affihce un message d'erreur
                bool verifEntrees = verifEntree(entrees);
                bool verif = verifRegexInt(duree);
                if (!verifEntrees)
                {
                    MessageBox.Show("Veuillez remplir tous les champs", "Erreur", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
                else
                {




                    //Regex rx1 = new Regex("[0-9]");
                    if (!verif)
                    {
                        MessageBox.Show("La durée ne doit contenir que des chiffres", "Erreur", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    }
                    else
                    {


                        // Objet Dvd sélectionné dans la comboBox
                        Dvd DvdSelection = (Dvd)cbxDvd.SelectedItem;

                        // mise en place d'un message box pour que l'utilisateurs valide sa modification 
                        DialogResult dialogResult = MessageBox.Show("Etes-vous sur de vouloir modifier le DVD :  '" + DvdSelection.Titre + "'  ", "Validation !", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            // on remplit les variables a l'aide des champs textes et combo box 
                            string idModif = txIdModifDvd.Text;
                            string titreModif = txTitreModifDvd.Text;
                            string synopsisModif = txSynopsisModifDvd.Text;
                            string realisateurModif = txRealisateurModifDvd.Text;
                            int dureeModif = Int32.Parse(txDureeModifDvd.Text);
                            string imageModif = txImageModifDvd.Text;
                            Categorie categorieModif = (Categorie)cbxCategorieDvdModif.SelectedItem;

                            // création du dvd modifié
                            Dvd dvdModif = new Dvd(idModif, titreModif, synopsisModif, realisateurModif, dureeModif, imageModif, categorieModif);

                            //modification du dvd
                            DAODocuments.modifierDvd(dvdModif);

                            lesDvd = DAODocuments.getAllDvd();


                            // alimentation des différentes combo box lors de l'entrée dans la page dvd
                            cbxDvd.DataSource = lesDvd;
                            cbxDvd.DisplayMember = "titre";

                            // ré-initialisation du dataGridView
                            dtDvd.Rows.Clear();

                            // Parcours de la collection des dvd et alimentation du datagridview
                            foreach (Dvd d in lesDvd)
                            {

                                dtDvd.Rows.Add(d.IdDoc, d.synopsis, d.realisateur, d.duree, d.Titre, d.Image, d.LaCategorie.Libelle);
                            }
                            MessageBox.Show("Le dvd '" + DvdSelection.Titre + "' a été modifié avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        }
                        else if (dialogResult == DialogResult.No)
                        {

                        }
                    }
                }
            }
            catch (System.Exception exc)
            {
                throw exc;
            }

        }

        private void btnSupprimerDvd_Click(object sender, EventArgs e)
        {
            try
            {
                // Objet Dvd sélectionné dans la comboBox
                Dvd dvdSelection = (Dvd)cbxDvd.SelectedItem;

                // mise en place d'un message box pour que l'utilisateurs valide sa suppression 
                DialogResult dialogResult = MessageBox.Show("Etes-vous sur de vouloir supprimer le DVD : '" + dvdSelection.Titre + "'", "Validation !", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    // on remplit les variables a l'aide des champs textes et combo box 
                    string idModif = txIdModifDvd.Text;
                    string titreModif = txTitreModifDvd.Text;
                    string synopsisModif = txSynopsisModifDvd.Text;
                    string realisateurModif = txRealisateurModifDvd.Text;
                    int dureeModif = Int32.Parse(txDureeModifDvd.Text);
                    string imageModif = txImageModifDvd.Text;
                    Categorie categorieModif = (Categorie)cbxCategorieDvdModif.SelectedItem;

                    // création du dvd a supprimé
                    Dvd dvdSuppr = new Dvd(idModif, titreModif, synopsisModif, realisateurModif, dureeModif, imageModif, categorieModif);


                    DAODocuments.supprimerDvd(dvdSuppr);
                    lesDvd = DAODocuments.getAllDvd();

                    // ré-initialisation du dataGridView
                    dtDvd.Rows.Clear();

                    lesDvd = DAODocuments.getAllDvd();

                    // alimentation des différentes combo box lors de l'entrée dans la page dvd
                    cbxDvd.DataSource = lesDvd;
                    cbxDvd.DisplayMember = "titre";

                    // Parcours de la collection des dvd et alimentation du datagridview
                    foreach (Dvd d in lesDvd)
                    {

                        dtDvd.Rows.Add(d.IdDoc, d.synopsis, d.realisateur, d.duree, d.Titre, d.Image, d.LaCategorie.Libelle);
                    }

                    MessageBox.Show("Le dvd '" + dvdSelection.Titre + "' a été supprimé avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (dialogResult == DialogResult.No)
                {

                }

            }
            catch (System.Exception exc)
            {
                throw exc;
            }



        }

        private void btnDeconnexionDvd_Click(object sender, EventArgs e)
        {
            //popup de validation de déconnexion
            DialogResult dialogResult = MessageBox.Show("Etes-vous sur de vouloir vous déconnecter ?", "Déconnexion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                //affichage du formulaire de connexion et fermeture de l'interface utilisateur
                login login1 = new login();
                login1.Show();
                this.Close();
            }
        }
        private void btnFermerAppliDvd_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        #endregion



        #region Commande
        private void btnCreerComande_Click(object sender, EventArgs e)
        {
            try
            {
                // on remplit les variables a l'aide des champs textes 
                string id = txIdDocument.Text;
                string prix = txPrixUnitaire.Text;
                string nbExemplaire = txNbExemplaire.Text;

                bool verif = verifRegexInt(id);
                bool verif2 = verifRegexInt(prix);
                bool verif3 = verifRegexInt(nbExemplaire);
                string[] entrees = { id, prix, nbExemplaire };
                bool verifEntrees = verifEntree(entrees);

                if (!verifEntrees)
                {
                    MessageBox.Show("Veuillez remplir tous les champs", "Erreur", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
                else
                {
                    if (!verif)
                    {
                        MessageBox.Show("L'id ne doit contenir que des chiffres", "Erreur", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    }
                    else if (!verif2)
                    {
                        MessageBox.Show("Le prix unitaire ne doit contenir que des chiffres", "Erreur", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    }
                    else if (!verif3)
                    {
                        MessageBox.Show("Le nombre d'exemplaire ne doit contenir que des chiffres", "Erreur", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show("Etes-vous sur de vouloir créer la commande ?", "Validation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            // on remplit les variables a l'aide des champs textes et combo box 
                            int nbExemplaire2 = Int32.Parse(txNbExemplaire.Text);
                            int prix2 = Int32.Parse(txPrixUnitaire.Text);

                            double montant = prix2 * nbExemplaire2;
                            string idEtat = "00001";
                            string libelle = "en cours";


                            bool trouve = false;
                            foreach (Commande c in lesCommandes)
                            {
                                if (c.Id.Equals(id))
                                {
                                    trouve = true;
                                }
                            }
                            if (trouve)
                            {
                                lesCommandes = DAODocuments.getAllCommandes();
                                foreach (Commande c in lesCommandes)
                                {

                                    dtCrudCommande.Rows.Add(c.Id, c.NbExemplaire, c.DateCommande, c.Montant, c.Doc.Titre, c.Etat.Libelle);
                                }
                                MessageBox.Show("Une commande avec l'id : '" + id + "' existe deja", "Erreur", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                            }

                            else
                            {

                                Document docSelectionne = (Document)cbxDocument.SelectedItem;

                                EtatCommande etat = new EtatCommande(idEtat, libelle);
                                DateTime date = DateTime.Now;




                                // création de la nouvelle commande
                                Commande com = new Commande(id, nbExemplaire2, date, montant, docSelectionne, etat);


                                DAODocuments.creerCommande(com);

                                lesCommandes.Add(com);

                                lesDocuments = DAODocuments.getAllDocuments();

                                /*cbxTitreDoc.DataSource = lesDocuments;
                                 cbxTitreDoc.DisplayMember = "titre";*/

                                cbxDocument.DataSource = lesDocuments;
                                cbxDocument.DisplayMember = "idDoc";

                                MessageBox.Show("La commande '" + id + "' a été créé avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                        }
                    }
                }
            }
            catch (System.Exception exc)
            {
                throw exc;
            }

        }

        private void cbxTitreDoc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabCrudCommande_Enter(object sender, EventArgs e)
        {


            lesDocuments = DAODocuments.getAllDocuments();
            lesEtatsCommande = DAODocuments.getAllEtatsCommande();
            lesEtatsCommande2 = DAODocuments.getAllEtatsCommande();


            // alimentation des différentes combo box lors de l'entrée dans la page commande


            cbxDocument.DataSource = lesDocuments;
            cbxDocument.DisplayMember = "titre";




            // alimentation des différentes combo box lors de l'entrée dans la page commande
            cbxEtatModif.DataSource = lesEtatsCommande;
            cbxEtatModif.DisplayMember = "libelle";

            cbxModifEtat.DataSource = lesEtatsCommande2;
            cbxModifEtat.DisplayMember = "libelle";



            foreach (Commande c in lesCommandes)
            {
                dtCrudCommande.Rows.Add(c.Id, c.NbExemplaire, c.DateCommande.ToString("d"), c.Montant, c.Doc.Titre, c.Etat.Libelle);
            }
        }

        private void cbxEtatModif_SelectedIndexChanged(object sender, EventArgs e)
        {





            List<Commande> lesCommandesByEtat;

            // Objet Dvd sélectionné dans la comboBox
            EtatCommande etat = (EtatCommande)cbxEtatModif.SelectedItem;








            lesCommandesByEtat = DAODocuments.getCommandeByEtatCommande(etat);

            cbxCommandeModifEtat.DataSource = lesCommandesByEtat;
            cbxCommandeModifEtat.DisplayMember = "montant";





            // on remplit les champs text en fonction du dvd selectionné dans la combo box
            if (lesCommandesByEtat.Count == 0)
            {
                txIdModif.Text = "";
                txDocumentModif.Text = "";
                txNbExemplaireModif.Text = "";
                txMontantModif.Text = "";
                txDateModif.Text = "";
                cbxCommandeModifEtat.Text = "";
                cbxModifEtat.Text = "";

            }
            else
            {
                foreach (Commande c in lesCommandesByEtat)
                {
                    if (c.Etat.ID == etat.ID)
                    {

                        txIdModif.Text = c.Id;
                        txDocumentModif.Text = c.Doc.Titre;
                        txNbExemplaireModif.Text = c.NbExemplaire.ToString();
                        txMontantModif.Text = c.Montant.ToString();
                        txDateModif.Text = c.DateCommande.ToString();

                    }

                }
            }
        }

        private void cbxCommandeModifEtat_SelectedIndexChanged(object sender, EventArgs e)
        {

            List<Commande> lesCommandes;

            // Objet Dvd sélectionné dans la comboBox
            Commande com = (Commande)cbxCommandeModifEtat.SelectedItem;








            lesCommandes = DAODocuments.getAllCommandes();

            foreach (Commande c in lesCommandes)
            {
                if (c.Id == com.Id)
                {

                    txIdModif.Text = c.Id;
                    txDocumentModif.Text = c.Doc.Titre;
                    txNbExemplaireModif.Text = c.NbExemplaire.ToString();
                    txMontantModif.Text = c.Montant.ToString();
                    txDateModif.Text = c.DateCommande.ToString();

                }

            }

        }


        private void btnModifEtatCommande_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Etes-vous sur de vouloir modifier la commande ?", "Validation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    //objet EtatCommande de la combobox
                    EtatCommande etat = (EtatCommande)cbxModifEtat.SelectedItem;

                    //objet Commande de la combobox
                    Commande com = (Commande)cbxCommandeModifEtat.SelectedItem;

                    DAODocuments.modifierEtatComande(com, etat);

                    MessageBox.Show("La commande a été modifiée avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (System.Exception exc)
            {
                throw exc;
            }

        }
        private void btnDeconnexionCommande_Click(object sender, EventArgs e)
        {
            //affichage du formulaire de connexion et fermeture de l'interface utilisateur
            this.Close();
            login login = new login();
            login.Show();
        }

        private void cbxModifEtat_SelectedIndexChanged(object sender, EventArgs e)
        {
            // objetc EtatCommande de la combobox
            EtatCommande etat = (EtatCommande)cbxModifEtat.SelectedItem;
        }


        private void btnSupprimerCommande_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Etes-vous sur de vouloir supprimer la commande ?", "Validation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    Commande com = (Commande)cbxCommandeModifEtat.SelectedItem;
                    DAODocuments.supprimerCommande(com);

                    MessageBox.Show("La commande a été supprimée avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (System.Exception exc)
            {
                throw exc;
            }

        }

        private void btnFermerAppliCommande_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion



        #region Utilisateurs

        private void btnCreerUtilisateur_Click(object sender, EventArgs e)
        {
            try
            {
                string id = txNewId.Text;
                string pseudo = txNewPseudo.Text;
                string password = txNewPassword.Text;
                string passwordConfirm = txNewPasswordConfirm.Text;
                string nom = txNewNom.Text;
                string prenom = txNewPrenom.Text;

                string[] entrees = { id, pseudo, password, passwordConfirm};


                bool verifEntrees = verifEntree(entrees);
                bool verifInt = verifRegexInt(id);
                bool verifStringPrenom = verifRegexString(prenom);
                bool verifStringNom = verifRegexString(nom);

                //vérification des différentes entrées et vérification du contenu du mot de passe (si il respecte des règles de sécurité)
                // ensuite, on vérifie que l'id et le pseudos ne sont pas deja existants dans la base de données
                if (!verifEntrees)
                {
                    MessageBox.Show("Veuillez remplir tous les champs", "Erreur", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
                else
                {
                    if (!verifInt)
                    {
                        MessageBox.Show("L'id ne doit contenir que des chiffres", "Erreur", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Regex rx = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

                        if (!rx.IsMatch(txNewPassword.Text))
                        {
                            MessageBox.Show("Le mot de passe doit comporter au moin 8 caractères dont 1 lettre majuscule, 1 caractère spécial, les caractères alphanumériques", "Erreur", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                        }
                        else
                        {
                            
                            if (!verifStringPrenom || !verifStringNom)
                            {
                                MessageBox.Show("Le prénom et le nom ne doivent contenir que des lettres", "Erreur", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                            }
                            else
                            {
                                bool trouve = false;
                                foreach (Login l in lesUsers)
                                {
                                    if (l.Id.Equals(int.Parse(id)))
                                    {
                                        trouve = true;
                                    }
                                    else
                                    {

                                    }
                                }
                                if (trouve)
                                {
                                    MessageBox.Show("Un utilisateur avec l'id : '" + id + "' existe deja", "Erreur", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                                }
                                else if (!trouve)
                                {

                                    foreach (Login l in lesUsers)
                                    {
                                        if (l.Pseudo.Equals(pseudo))
                                        {
                                            trouve = true;
                                        }
                                        else
                                        {

                                        }
                                    }
                                    if (trouve)
                                    {
                                        MessageBox.Show("L'utilisateur '" + pseudo + "' existe deja", "Erreur", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                                    }
                                    else
                                    {

                                        if (password == passwordConfirm)
                                        {
                                            DialogResult dialogResult = MessageBox.Show("Etes-vous sur de vouloir créer cet utilisateur ?", "Validation !", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                            if (dialogResult == DialogResult.Yes)
                                            {
                                                
                                                string hashedPassword = hashPassword(password);
                                                Service service = (Service)cbxService.SelectedItem;

                                                Login user = new Login(int.Parse(id), pseudo, hashedPassword, prenom, nom, service);
                                                DAODocuments.creerUser(user);
                                                MessageBox.Show("Utilisateur créé avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                lesUsers = DAODocuments.getAllUsers();

                                                cbxPseudo.DataSource = lesUsers;
                                                cbxPseudo.DisplayMember = "pseudo";

                                                txNewId.Text = "";
                                                txNewPseudo.Text = "";
                                                txNewPassword.Text = "";
                                                txNewNom.Text = "";
                                                txNewPrenom.Text = "";
                                                txNewPasswordConfirm.Text = "";
                                            }
                                            else if (dialogResult == DialogResult.No)
                                            {

                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Les mots de passe ne correspondent pas", "Erreur", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                                        }


                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (System.Exception exc)
            {
                throw exc;
            }
        }

        private void tabCrudUtilisateurs_Enter(object sender, EventArgs e)
        {
            lesServices = DAODocuments.getAllService();
            lesUsers = DAODocuments.getAllUsers();


            // alimentation des différentes combo box lors de l'entrée dans la page utilisateur
            cbxService.DataSource = lesServices;
            cbxService.DisplayMember = "libelle";

            cbxPseudo.DataSource = lesUsers;
            cbxPseudo.DisplayMember = "pseudo";

        }

        private void cbxPseudo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Login userSelection = (Login)cbxPseudo.SelectedItem;

            // on remplit les champs text en fonction du dvd selectionné dans la combo box
            foreach (Login l in lesUsers)
            {
                if (l.Id == userSelection.Id)
                {
                    txIdSuppr.Text = l.Id.ToString();
                    txServiceSuppr.Text = l.Service.Libelle;
                    txPrenomSuppr.Text = l.Prenom;
                    txNomSuppr.Text = l.Nom;

                }

            }

        }

        private void btnSupprimerUtilisateur_Click(object sender, EventArgs e)
        {
            try
            {
                // Objet login sélectionné dans la comboBox
                Login user = (Login)cbxPseudo.SelectedItem;

                // mise en place d'un message box pour que l'utilisateur valide sa suppression 
                DialogResult dialogResult = MessageBox.Show("Etes-vous sur de vouloir supprimer l'utilisateur : '" + user.Pseudo + "' ?", "Validation !", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    DAODocuments.supprimerUtilisateur(user);

                    MessageBox.Show("L'utilisateur '" + user.Pseudo + "' a été supprimé avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    lesUsers = DAODocuments.getAllUsers();

                    cbxPseudo.DataSource = lesUsers;
                    cbxPseudo.DisplayMember = "pseudo";
                }
                else if (dialogResult == DialogResult.No)
                {

                }

            }
            catch (System.Exception exc)
            {
                throw exc;
            }
        }

        private void btnDeconnexionUtilisateur_Click(object sender, EventArgs e)
        {
            this.Close();
            login login = new login();
            login.Show();
        }
        private void btnFermerAppliUtilisateur_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion



        #region Procédures vides

        #endregion



        #region Visualisation
        //-----------------------------------------------------------
        // ONGLET "Visualisation"
        //-----------------------------------------------------------
        private void tabVisualisation_Enter(object sender, EventArgs e)
        {
            dtDvdVisu.Rows.Clear();

            lesDvd = DAODocuments.getAllDvd();
            lesActeurs = DAODocuments.getAllActeurs();

            // Parcours de la collection des dvd et alimentation du datagridview
            foreach (Dvd d in lesDvd)
            {

                dtDvdVisu.Rows.Add(d.IdDoc, d.synopsis, d.realisateur, d.duree, d.Titre, d.Image, d.LaCategorie.Libelle);
            }


            dtLivreVisu.Rows.Clear();

            lesLivres = DAODocuments.getAllLivres();


            // Parcours de la collection des livres et alimentation du datagridview
            foreach (Livre l in lesLivres)
            {

                dtLivreVisu.Rows.Add(l.IdDoc, l.Titre, l.Auteur, l.LaCollection, l.Image, l.LaCategorie.Libelle);
            }

        }

        private void btnFermerAppliVisu_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        #endregion



        #region Fonctions utilisateurs
        //-----------------------------------------------------------
        // ONGLET "Fonctions utilisateurs"
        //-----------------------------------------------------------

        public bool verifRegexInt(string chaine)
        {
            // cette fonction vérifie que tous les caractères de la chaine en parametre sont des int
            //  comptage du nombre de caractère de la chaine et parcours de la chaine en vérifiant
            //  que chaque caractère est un int ou non. si non, renvoi false, si oui, renvoi true
            int longueur = chaine.Count();
            bool regexValid = true;
            for (int i = 0; i < longueur; i++)
            {
                Regex rx = new Regex("[0-9]");
                if (!rx.IsMatch(chaine[i].ToString()))
                {
                    regexValid = false;
                }

            }
            return regexValid;
        }
        public bool verifRegexString(string chaine)
        {
            // cette fonction vérifie que tous les caractères de la chaine en parametre sont des lettres
            //  comptage du nombre de caractère de la chaine et parcours de la chaine en vérifiant
            //  que chaque caractère est une lettre ou non. si non, renvoi false, si oui, renvoi true
            int longueur = chaine.Count();
            bool regexValid = true;
            for (int i = 0; i < longueur; i++)
            {
                Regex rx = new Regex("[A-Za-z]");
                if (!rx.IsMatch(chaine[i].ToString()))
                {
                    regexValid = false;
                }

            }
            return regexValid;
        }
        public bool verifEntree(string[] entrees)
        {
            // cette fonction vérifie que tous les champs du tableau en parametre ne sont pas vides
            //  parocurs du tableau et vérification pour chaque élément qu'il n'est pas vide ou null
            // renvoi false si une chaine est vide ou true sinon
            foreach (string e in entrees)
            {
                if (string.IsNullOrEmpty(e))
                {
                    return false;
                }
            }
            return true;
        }
        public string hashPassword(string password)
        {
            // cette fonction hash la chaine passée en parametre
            // renvoi la chaine hashée sous forme de string
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(password);
            var hashedPassword = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hashedPassword);
        }





        #endregion

        
    }
}

        

