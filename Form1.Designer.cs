namespace Mediateq_AP_SIO2
{
    partial class login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnConnexion = new System.Windows.Forms.Button();
            this.btnRedirectCreerUser = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnVisualisation = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(89, 230);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nom d\'utilisateur :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label2.Location = new System.Drawing.Point(89, 324);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mot de passe :";
            // 
            // txtLogin
            // 
            this.txtLogin.BackColor = System.Drawing.SystemColors.Info;
            this.txtLogin.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLogin.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtLogin.Location = new System.Drawing.Point(93, 256);
            this.txtLogin.Multiline = true;
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(287, 34);
            this.txtLogin.TabIndex = 2;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.SystemColors.Info;
            this.txtPassword.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtPassword.Location = new System.Drawing.Point(93, 350);
            this.txtPassword.Multiline = true;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(287, 34);
            this.txtPassword.TabIndex = 3;
            // 
            // btnConnexion
            // 
            this.btnConnexion.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnConnexion.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnexion.ForeColor = System.Drawing.SystemColors.Menu;
            this.btnConnexion.Location = new System.Drawing.Point(249, 468);
            this.btnConnexion.Name = "btnConnexion";
            this.btnConnexion.Size = new System.Drawing.Size(131, 61);
            this.btnConnexion.TabIndex = 4;
            this.btnConnexion.Text = "Connexion";
            this.btnConnexion.UseVisualStyleBackColor = false;
            this.btnConnexion.Click += new System.EventHandler(this.btnConnexion_Click);
            // 
            // btnRedirectCreerUser
            // 
            this.btnRedirectCreerUser.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnRedirectCreerUser.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRedirectCreerUser.ForeColor = System.Drawing.SystemColors.Menu;
            this.btnRedirectCreerUser.Location = new System.Drawing.Point(93, 468);
            this.btnRedirectCreerUser.Name = "btnRedirectCreerUser";
            this.btnRedirectCreerUser.Size = new System.Drawing.Size(131, 61);
            this.btnRedirectCreerUser.TabIndex = 5;
            this.btnRedirectCreerUser.Text = "Créer un utilisateur";
            this.btnRedirectCreerUser.UseVisualStyleBackColor = false;
            this.btnRedirectCreerUser.Click += new System.EventHandler(this.btnRedirectCreerUser_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Comic Sans MS", 26.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label3.Location = new System.Drawing.Point(146, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(185, 50);
            this.label3.TabIndex = 6;
            this.label3.Text = "Connexion";
            // 
            // btnVisualisation
            // 
            this.btnVisualisation.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnVisualisation.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVisualisation.ForeColor = System.Drawing.SystemColors.Menu;
            this.btnVisualisation.Location = new System.Drawing.Point(167, 552);
            this.btnVisualisation.Name = "btnVisualisation";
            this.btnVisualisation.Size = new System.Drawing.Size(131, 61);
            this.btnVisualisation.TabIndex = 7;
            this.btnVisualisation.Text = "Entre en tant que visiteur";
            this.btnVisualisation.UseVisualStyleBackColor = false;
            this.btnVisualisation.Click += new System.EventHandler(this.btnVisualisation_Click);
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 656);
            this.Controls.Add(this.btnVisualisation);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnRedirectCreerUser);
            this.Controls.Add(this.btnConnexion);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "login";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnConnexion;
        private System.Windows.Forms.Button btnRedirectCreerUser;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Button btnVisualisation;
    }
}