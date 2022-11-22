using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mediateq_AP_SIO2.metier;


namespace Mediateq_AP_SIO2
{
    public partial class FrmMediateq : Form
    {
        #region Variables globales

        static List<Categorie> lesCategories;
        static List<Descripteur> lesDescripteurs;
        static List<Revue> lesTitres;
        static List<Livre> lesLivres;
        static List<Dvd> lesDvd;

        #endregion


        #region Procédures évènementielles

        public FrmMediateq()
        {
            InitializeComponent();
        }

        private void FrmMediateq_Load(object sender, EventArgs e)
        {
            // Création de la connexion avec la base de données
            DAOFactory.creerConnection();

            // Chargement des objets en mémoire
            lesDescripteurs = DAODocuments.getAllDescripteurs();
            lesTitres = DAOPresse.getAllTitre();
            lesCategories = DAODocuments.getAllCategories();
            lesDvd = DAODocuments.getAllDvd();
            lesLivres = DAODocuments.getAllLivres();



        }

        #endregion


        #region Parutions
        //-----------------------------------------------------------
        // ONGLET "PARUTIONS"
        //------------------------------------------------------------
        private void tabParutions_Enter(object sender, EventArgs e)
        {
            cbxTitres.DataSource = lesTitres;
            cbxTitres.DisplayMember = "titre";
        }

        private void cbxTitres_SelectedIndexChanged(object sender, EventArgs e)
        {
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
                if (revue.IdDescripteur==domaineSelectionne.Id)
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
            // On réinitialise les labels
            lblNumero.Text = "";
            lblTitre.Text = "";
            lblAuteur.Text = "";
            lblCollection.Text = "";
            lblISBN.Text = "";
            lblImage.Text = "";
            lblCategorie.Text = "";

            // On recherche le livre correspondant au numéro de document saisi.
            // S'il n'existe pas: on affiche un popup message d'erreur
            bool trouve = false;
            foreach (Livre livre in lesLivres)
            {
                if (livre.IdDoc==txbNumDoc.Text)
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
                MessageBox.Show("Document non trouvé dans les livres");
        }

        private void txbTitre_TextChanged(object sender, EventArgs e)
        {
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
                    dgvLivres.Rows.Add(livre.IdDoc,livre.Titre,livre.Auteur,livre.ISBN1,livre.LaCollection);
                }
            }
        }
        #endregion

        private void btnCreerDvd_Click(object sender, EventArgs e)
        {
            

            // mise en place d'un message box pour que l'utilisateurs valide sa création 
            DialogResult dialogResult = MessageBox.Show("Etes-vous sur de vouloir créer ce DVD ?", "Validation !", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                // on remplit les variables a l'aide des champs textes et combo box 
                string titre = txTitre.Text;
                string id = txId.Text;
                string synopsis = txSynopsis.Text;
                string realisateur = txRealisateur.Text;
                int duree = Int32.Parse(txDuree.Text);
                string image = txImage.Text;
                Categorie categ = (Categorie)cbxCategorie.SelectedItem;

                // création du nouveau dvd
                Dvd dvd1 = new Dvd(id, titre, synopsis, realisateur, duree, image, categ);


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
                    MessageBox.Show("Un dvd avec l'id : '" + id + "' existe deja");
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

                    MessageBox.Show("Le dvd '" + titre + "' a été créé avec succès !");

                }
                
            }
            else if (dialogResult == DialogResult.No)
            {

            }




        }

        private void btnAfficherCreation_Click(object sender, EventArgs e)
        {
            
            
        }

        private void tabDVD_Click(object sender, EventArgs e)
        {

        }

        private void tabDVD_Enter_1(object sender, EventArgs e)
        {
            // ré-initialisation du dataGridView
            dtDvd.Rows.Clear();

            lesDvd = DAODocuments.getAllDvd();

            // alimentation des différentes combo box lors de l'entrée dans la page dvd
            cbxDvd.DataSource = lesDvd;
            cbxDvd.DisplayMember = "titre";
            cbxCategorie.DataSource = lesCategories;
            cbxCategorie.DisplayMember = "libelle";
            cbxCategorieDvdModif.DataSource = lesCategories;
            cbxCategorieDvdModif.DisplayMember = "libelle";



            // Parcours de la collection des dvd et alimentation du datagridview
            foreach (Dvd d in lesDvd)
             {
                 
                 dtDvd.Rows.Add(d.IdDoc, d.synopsis, d.realisateur, d.duree, d.Titre, d.Image, d.LaCategorie.Libelle);
             }


            
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabCrudLivre_Click(object sender, EventArgs e)
        {
            
           
        }

        private void tabCrudLivre_Enter(object sender, EventArgs e)
        {
            // ré-initialisation du dataGridView
            dtCrudLivre.Rows.Clear();

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
            // Objet Dvd sélectionné dans la comboBox
            Dvd DvdSelection = (Dvd)cbxDvd.SelectedItem;

            // mise en place d'un message box pour que l'utilisateurs valide sa modification 
            DialogResult dialogResult = MessageBox.Show("Etes-vous sur de modifier le DVD :  '" +DvdSelection.Titre + "'  ", "Validation !", MessageBoxButtons.YesNo);
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

                
            }
            else if (dialogResult == DialogResult.No)
            {
                
            }


            






        }

        private void btnSupprimerDvd_Click(object sender, EventArgs e)
        {
            // Objet Dvd sélectionné dans la comboBox
            Dvd dvdSelection = (Dvd)cbxDvd.SelectedItem;

            // mise en place d'un message box pour que l'utilisateurs valide sa suppression 
            DialogResult dialogResult = MessageBox.Show("Etes-vous sur de vouloir supprimer le DVD : '" + dvdSelection.Titre + "'", "Validation !", MessageBoxButtons.YesNo);
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
            }
            else if (dialogResult == DialogResult.No)
            {
                
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                       

        }

        private void btnCreerLivre_Click(object sender, EventArgs e)
        {       

            // mise en place d'un message box pour que l'utilisateurs valide sa création 
            DialogResult dialogResult = MessageBox.Show("Etes-vous sur de vouloir créer ce Livre ?", "Validation !", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                // on remplit les variables a l'aide des champs textes et combo box 
                string titre = txTitreLivre.Text;
                string id = txIdLivre.Text;
                string auteur = txAuteurLivre.Text;
                string ISBN = txISBNLivre.Text;
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
                    if (l.IdDoc == txIdLivre.Text)
                    {
                        trouve = true;
                    }
                    else
                    {

                    }
                }
                if (trouve) 
                {
                    MessageBox.Show("Un livre avec l'id : '" + txIdLivre.Text + "' existe deja");
                }
                else if (!trouve)
                {
                    DAODocuments.creerLivre(livre1);

                    lesLivres = DAODocuments.getAllLivres();

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

                    MessageBox.Show("Le livre '" + titre + "' a été créé avec succès !");
                }                
            }
            else if (dialogResult == DialogResult.No)
            {
                
            }
            
        }

        private void btnSupprimerLivre_Click(object sender, EventArgs e)
        {
            // Objet Livre sélectionné dans la comboBox
            Livre livreSelection = (Livre)cbxLivre.SelectedItem;

            // mise en place d'un message box pour que l'utilisateurs valide sa suppression 
            DialogResult dialogResult = MessageBox.Show("Etes-vous sur de vouloir supprimer le livre : '"+livreSelection.Titre+"'", "Validation !", MessageBoxButtons.YesNo);
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


                DAODocuments.supprimerLivre(livreSuppr);

                lesLivres = DAODocuments.getAllLivres();

                // ré-initialisation du dataGridView
                dtDvd.Rows.Clear();

                lesLivres = DAODocuments.getAllLivres();

                // alimentation des différentes combo box lors de l'entrée dans la page crud livre
                cbxLivre.DataSource = lesLivres;
                cbxLivre.DisplayMember = "titre";

                // Parcours de la collection des livres et alimentation du datagridview
                foreach (Livre l in lesLivres)
                {
                    dtCrudLivre.Rows.Add(l.IdDoc, l.Titre, l.Auteur, l.LaCollection, l.Image, l.LaCategorie.Libelle);
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                
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
            // Objet Livre sélectionné dans la comboBox
            Livre livreSelection = (Livre)cbxLivre.SelectedItem;

            // mise en place d'un message box pour que l'utilisateurs valide sa modification 
            DialogResult dialogResult = MessageBox.Show("Etes-vous sur de vouloir modifier le livre : '"+livreSelection.Titre+"'", "Validation !", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string idModif = txIdModifLivre.Text;
                string titreModif = txTitreLivreModif.Text;
                string auteurModif = txAuteurLivreModif.Text;
                string ISBNModif = txISBNLivreModif.Text;
                string collectionModif = txCollectionLivreModif.Text;
                string imageModif = txImageLivreModif.Text;
                Categorie categorieModif = (Categorie)cbxCategorieLivreModif.SelectedItem;

                // création du livre a modifié
                Livre livreSuppr = new Livre(idModif, titreModif, ISBNModif, auteurModif, collectionModif, imageModif, categorieModif);


                DAODocuments.modifierLivre(livreSuppr);

                lesLivres = DAODocuments.getAllLivres();

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
            }
            else if (dialogResult == DialogResult.No)
            {
                
            }
            
        }
    }
}
