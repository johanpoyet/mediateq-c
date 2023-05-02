using Mediateq_AP_SIO2.metier;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Mediateq_AP_SIO2
{
    public partial class register : Form
    {
        
        static List<Login> lesUsers;

        public register()
        {
            InitializeComponent();
        }

        private void btnCreerUser_Click(object sender, EventArgs e)
        {
            Regex rx = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

            if (!rx.IsMatch(txtPasswordNew.Text))
            {
                MessageBox.Show("Le mot de passe doit comporter au moin 8 caractères dont 1 lettre majuscule, 1 caractère spécial, les caractères alphanumériques", "Erreur", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Etes-vous sur de vouloir créer cet utilisateur ?", "Validation !", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    string id = txtIdNew.Text;
                    string pseudo = txtLoginNew.Text;
                    string password = txtPasswordNew.Text;
                    string passwordConfirm = txtPasswordNewConfirm.Text;

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
                                string nom = "test";
                                string prenom = "test";
                                Service service = new Service (1, "Administratif");

                                Login user = new Login(int.Parse(id), pseudo, password, prenom, nom, service);
                                DAODocuments.creerUser(user);
                                MessageBox.Show("Utilisateur créé avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                login login1 = new login();
                                login1.Show();
                                this.Hide();

                            }
                            else
                            {
                                MessageBox.Show("Les mots de passe ne correspondent pas", "Erreur", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                            }


                        }
                    }
                }
                else if(dialogResult == DialogResult.No)
                {

                }


            }
           
            
        }

        private void register_Load(object sender, EventArgs e)
        {
            DAOFactory.creerConnection();
            lesUsers = DAODocuments.getAllUsers();
        }

        private void btnRedirectLogin_Click(object sender, EventArgs e)
        {
            login login1 = new login();
            login1.Show();
            this.Hide();
        }
    }
}
