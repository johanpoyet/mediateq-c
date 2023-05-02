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
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography;
using System.Text;


namespace Mediateq_AP_SIO2
{
    public partial class login : Form
    {
        static List<Login> lesUsers;
        static List<Service> lesServices;
        MySqlConnection connexion = new MySqlConnection("datasource=localhost; port=3306; username=root; password=; database=mediateq-ap");
        public login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtLogin.Text = "test1";
            txtPassword.Text = "Johan!38230";
            DAOFactory.creerConnection();
            lesUsers = DAODocuments.getAllUsers();

        }

        private void btnConnexion_Click(object sender, EventArgs e)
        {
           
            Login user = DAODocuments.getUserByPseudo(txtLogin.Text);

            connexion.Open();
            string hashedPassword = hashPassword(txtPassword.Text);
            MySqlDataAdapter adap = new MySqlDataAdapter("SELECT COUNT(*) FROM login WHERE pseudo='" + txtLogin.Text + "' AND password='" + hashedPassword + "'", connexion);
            DataTable dt = new DataTable();

            adap.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                
               
                int serv = 0;

                
                    
                    //Service service = DAODocuments.getServiecByUser(user);
                    if (user.Service.Libelle == "Administratif")
                    {
                        serv = 1;
                    }
                    else if(user.Service.Libelle == "Prets")
                    {
                        serv = 2;
                    }
                    else if (user.Service.Libelle == "Culture")
                    {
                        serv = 3;
                    }
                    else if (user.Service.Libelle == "Administrateur")
                    {
                        serv = 4;
                    }

                    MessageBox.Show("Vous etes connecté en tant que personne du service "+user.Service.Libelle+"", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
                if(serv == 1)
                {
                    FrmMediateq FrmMediateq = new FrmMediateq();
                    FrmMediateq.Owner = this;
                    FrmMediateq.Show();
                    this.Hide();
                    TabPage tabUtilisateur = FrmMediateq.tab.TabPages[7];
                    FrmMediateq.tab.TabPages.Remove(tabUtilisateur);

                }
                if(serv == 2 || serv == 3)
                {
                    FrmMediateq FrmMediateq = new FrmMediateq();
                    FrmMediateq.Owner = this;
                    FrmMediateq.Show();
                    this.Hide();
                    TabPage tabDVD = FrmMediateq.tab.TabPages[3];
                    TabPage tabLivre = FrmMediateq.tab.TabPages[4];
                    TabPage tabCommande = FrmMediateq.tab.TabPages[5];
                    TabPage tabUtilisateur = FrmMediateq.tab.TabPages[7];
                    FrmMediateq.tab.TabPages.Remove(tabDVD);
                    FrmMediateq.tab.TabPages.Remove(tabLivre);
                    FrmMediateq.tab.TabPages.Remove(tabCommande);
                    FrmMediateq.tab.TabPages.Remove(tabUtilisateur);
                }
                if (serv == 4)
                {
                    FrmMediateq FrmMediateq = new FrmMediateq();
                    FrmMediateq.Show();
                    this.Hide();

                }
            }
            else
            {
                MessageBox.Show("Mot de passe ou login incorrect", "Erreur", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            connexion.Close();



        }

        private void btnRedirectCreerUser_Click(object sender, EventArgs e)
        {
            this.Hide();
            register register = new register();
            register.Show();
        }

        private void btnVisualisation_Click(object sender, EventArgs e)
        {
            FrmMediateq FrmMediateq = new FrmMediateq();
            FrmMediateq.Owner = this;
            FrmMediateq.Show();
            this.Hide();
            TabPage tabDVD = FrmMediateq.tab.TabPages[3] ;
            TabPage tabLivre = FrmMediateq.tab.TabPages[4];
            TabPage tabCommande = FrmMediateq.tab.TabPages[5];
            //FrmMediateq.tabDVD.Enabled = false;
            //FrmMediateq.tabCrudLivre.Enabled = false;
            //FrmMediateq.tabCrudCommande.Enabled = false;
            FrmMediateq.tab.TabPages.Remove(tabDVD);
            FrmMediateq.tab.TabPages.Remove(tabLivre);
            FrmMediateq.tab.TabPages.Remove(tabCommande);



            //FrmMediateq.tabDVD.Enabled = false;

            //FrmMediateq.tabDVD.IsAccessible = false;






        }
        public string hashPassword(string password)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(password);
            var hashedPassword = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hashedPassword);
        }



    }
}
