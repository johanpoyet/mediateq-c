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

namespace Mediateq_AP_SIO2
{
    public partial class login : Form
    {
        static List<Login> lesUsers;
        MySqlConnection connexion = new MySqlConnection("datasource=localhost; port=3306; username=root; password=; database=mediateq-ap");
        public login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DAOFactory.creerConnection();
            lesUsers = DAODocuments.getAllUsers();

        }

        private void btnConnexion_Click(object sender, EventArgs e)
        {
            connexion.Open();
            MySqlDataAdapter adap = new MySqlDataAdapter("SELECT COUNT(*) FROM login WHERE pseudo='" + txtLogin.Text + "' AND password='" + txtPassword.Text + "'", connexion);
            DataTable dt = new DataTable();

            adap.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                MessageBox.Show("Vous etes connecté", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                FrmMediateq FrmMediateq = new FrmMediateq();
                FrmMediateq.Show();
                this.Hide();


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
            
            FrmMediateq.tabDVD.Enabled = false;
            FrmMediateq.tabCrudLivre.Enabled = false;
            FrmMediateq.tabCrudCommande.Enabled = false;

        }

    }
}
