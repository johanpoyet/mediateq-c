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

        //DVD


        private void tabDVD_Enter(object sender, EventArgs e)
        {
           

        }

        private void cbxDVd_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Objet Domaine sélectionné dans la comboBox

            // Parcours de la collection des titres et alimentation du datagridview
            
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
            string titre = txTitre.Text;
            string id = txId.Text;
            string synopsis = txSynopsis.Text;
            string realisateur = txRealisateur.Text;
            int duree = Int32.Parse(txDuree.Text);
            string image = txImage.Text;
            
            Categorie categ = (Categorie)cbxCategorie.SelectedItem;


            Dvd dvd1 = new Dvd(id, titre, synopsis, realisateur, duree, image, categ);
            DAODocuments.creerDvd(dvd1);

            lesDvd = DAODocuments.getAllDvd();
            dtDvd.Rows.Clear();

            foreach (Dvd d in lesDvd)
            {

                dtDvd.Rows.Add(d.IdDoc, d.synopsis, d.realisateur, d.duree, d.Titre, d.Image, d.LaCategorie);
            }
        }

        private void btnAfficherCreation_Click(object sender, EventArgs e)
        {
            foreach(Dvd d in lesDvd)
            {
                dtDvd.Rows.Add(d.IdDoc, d.synopsis, d.realisateur, d.duree, d.Titre, d.Image, d.LaCategorie.Libelle);
            }
            
        }

        private void tabDVD_Click(object sender, EventArgs e)
        {

        }

        private void tabDVD_Enter_1(object sender, EventArgs e)
        {
            dtDvd.Rows.Clear();


            cbxDvd.DataSource = lesDvd;
            cbxDvd.DisplayMember = "titre";
            cbxCategorie.DataSource = lesCategories;
            cbxCategorie.DisplayMember = "libelle";
            cbxCategorieDvdModif.DataSource = lesCategories;
            cbxCategorieDvdModif.DisplayMember = "libelle";




            foreach (Dvd d in lesDvd)
             {
                 
                 dtDvd.Rows.Add(d.IdDoc, d.synopsis, d.realisateur, d.duree, d.Titre, d.Image, d.LaCategorie);
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
            cbxLivres.DataSource = lesLivres;
            cbxLivres.DisplayMember = "titre";
            cbxCategorieLivre.DataSource = lesCategories;
            cbxCategorieLivre.DisplayMember = "libelle";

            foreach (Livre l in lesLivres)
            {

                dtCrudLivre.Rows.Add(l.IdDoc, l.Titre, l.Auteur, l.LaCollection, l.Image, l.LaCategorie);
            }
        }

        private void cbxDvd_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Dvd DvdSelection = (Dvd)cbxDvd.SelectedItem;
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
           
            
            Dvd DvdSelection = (Dvd)cbxDvd.SelectedItem;           
            string idModif= txIdModifDvd.Text;
            string titreModif = txTitreModifDvd.Text;
            string synopsisModif = txSynopsisModifDvd.Text;
            string realisateurModif = txRealisateurModifDvd.Text;
            int dureeModif = Int32.Parse(txDureeModifDvd.Text);
            string imageModif = txImageModifDvd.Text;
            Categorie categorieModif = (Categorie)cbxCategorieDvdModif.SelectedItem;

            Dvd dvdModif = new Dvd(idModif, titreModif, synopsisModif, realisateurModif, dureeModif, imageModif, categorieModif);
            DAODocuments.modifierDvd(dvdModif);

            lesDvd = DAODocuments.getAllDvd();
            dtDvd.Rows.Clear();

            foreach (Dvd d in lesDvd)
            {

                dtDvd.Rows.Add(d.IdDoc, d.synopsis, d.realisateur, d.duree, d.Titre, d.Image, d.LaCategorie);
            }






        }

        private void btnSupprimerDvd_Click(object sender, EventArgs e)
        {
            Dvd dvdSelection = (Dvd)cbxDvd.SelectedItem;
            string idModif = txIdModifDvd.Text;
            string titreModif = txTitreModifDvd.Text;
            string synopsisModif = txSynopsisModifDvd.Text;
            string realisateurModif = txRealisateurModifDvd.Text;
            int dureeModif = Int32.Parse(txDureeModifDvd.Text);
            string imageModif = txImageModifDvd.Text;
            Categorie categorieModif = (Categorie)cbxCategorieDvdModif.SelectedItem;

            Dvd dvdSuppr = new Dvd(idModif, titreModif, synopsisModif, realisateurModif, dureeModif, imageModif, categorieModif);


            DAODocuments.supprimerDvd(dvdSuppr);
            lesDvd = DAODocuments.getAllDvd();
            dtDvd.Rows.Clear(); 

            foreach (Dvd d in lesDvd)
            {

                dtDvd.Rows.Add(d.IdDoc, d.synopsis, d.realisateur, d.duree, d.Titre, d.Image, d.LaCategorie);
            }
        }
    }
}
