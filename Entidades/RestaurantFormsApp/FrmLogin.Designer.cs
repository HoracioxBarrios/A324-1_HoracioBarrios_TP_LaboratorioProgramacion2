namespace RestaurantFormsApp
{
    partial class FrmLogin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            btnLogin = new Button();
            txtPassword = new TextBox();
            txtUser = new TextBox();
            labelUser = new Label();
            SuspendLayout();
            // 
            // btnLogin
            // 
            btnLogin.BackColor = SystemColors.HighlightText;
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.Font = new Font("Candara", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnLogin.ForeColor = SystemColors.WindowFrame;
            btnLogin.Location = new Point(157, 433);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(168, 34);
            btnLogin.TabIndex = 0;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(140, 359);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.PlaceholderText = "Contraseña";
            txtPassword.Size = new Size(211, 23);
            txtPassword.TabIndex = 1;
            txtPassword.TextAlign = HorizontalAlignment.Center;
            // 
            // txtUser
            // 
            txtUser.Location = new Point(140, 298);
            txtUser.Name = "txtUser";
            txtUser.PlaceholderText = "Usuario";
            txtUser.Size = new Size(211, 23);
            txtUser.TabIndex = 2;
            txtUser.TextAlign = HorizontalAlignment.Center;
            // 
            // labelUser
            // 
            labelUser.AutoSize = true;
            labelUser.BackColor = Color.Transparent;
            labelUser.Font = new Font("Cambria", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            labelUser.ForeColor = SystemColors.ButtonFace;
            labelUser.Location = new Point(157, 138);
            labelUser.Name = "labelUser";
            labelUser.Size = new Size(194, 32);
            labelUser.TabIndex = 3;
            labelUser.Text = "Restaurant App";
            labelUser.TextAlign = ContentAlignment.TopCenter;
            // 
            // FrmLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(492, 593);
            Controls.Add(labelUser);
            Controls.Add(txtUser);
            Controls.Add(txtPassword);
            Controls.Add(btnLogin);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Restaurant App";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnLogin;
        private TextBox txtPassword;
        private TextBox txtUser;
        private Label labelUser;
    }
}
