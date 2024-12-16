using Guna.UI2.WinForms;
using Hotel.Global;
using HotelDatabase_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void ValidatingOfTextBoxes(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(((Guna2TextBox)sender).Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(((Guna2TextBox)sender), "This field is required!");
            }
            else
            {
                errorProvider1.SetError(((Guna2TextBox)sender), null);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string username = ""; string password = "";

            if (clsGlobal.GetStoredCredential(out username, out password))
            {
                txtUsername.Text = username;
                txtPassword.Text = clsGlobal.Decrypt(password);
                tsRememberMe.Checked = true;
            }
            else
                tsRememberMe.Checked = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Fields are required!", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string HashedPassword = clsGlobal.ComputeHash(txtPassword.Text.Trim());

            clsUser user = clsUser.FindByUsernameAndPassword(txtUsername.Text.Trim(), HashedPassword);

            if(user != null)
            {
                if (!user.IsActive)
                {
                    txtUsername.Focus();
                    MessageBox.Show("Your account is not active, Please contact your admin!", "Denied", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (tsRememberMe.Checked)
                    clsGlobal.RememberUsernameAndPassword(txtUsername.Text.Trim(), clsGlobal.Encrypt(txtPassword.Text.Trim()));
                else
                    clsGlobal.RemoveStoredCredential();

                clsGlobal.CurrentUser = user;
                this.Hide();
                frmMainForm frm = new frmMainForm();
                frm.ApplicationClose += _HandleApplcaitionClosing;
                frm.ShowDialog();
            }
            else
            {
                txtUsername.Focus();
                MessageBox.Show("Invalid Username/Password.", "Wrong Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _HandleApplcaitionClosing(bool CloseAllApplication)
        {
            if (CloseAllApplication)
                this.Close();
            else
                this.Show();
        }
    }
}
