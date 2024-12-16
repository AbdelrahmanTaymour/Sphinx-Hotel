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

namespace Hotel.Users
{
    public partial class frmManageUsers : Form
    {
        DataTable _dtUsersList;
        public frmManageUsers()
        {
            InitializeComponent();
        }

        void _RefreshUsersList()
        {
            _dtUsersList = clsUser.GetAllUsers();
            dgvUsersList.DataSource = _dtUsersList;

            if (dgvUsersList.Rows.Count > 0)
            {
                dgvUsersList.Columns[0].HeaderText = "User ID";
                dgvUsersList.Columns[0].Width = 90;

                dgvUsersList.Columns[1].HeaderText = "Person ID";
                dgvUsersList.Columns[1].Width = 110;

                dgvUsersList.Columns[2].HeaderText = "Full Name";
                dgvUsersList.Columns[2].Width = 250;

                dgvUsersList.Columns[3].HeaderText = "UserName";
                dgvUsersList.Columns[3].Width = 120;

                dgvUsersList.Columns[4].HeaderText = "Is Active";
                dgvUsersList.Columns[4].Width = 120;
            }
        }
        string _GetRealColumnNameInDB()
        {
            switch (cbFilterBy.Text)
            {
                case "User ID":
                    return "UserID";

                case "Person ID":
                    return "PersonID";

                case "Name":
                    return "FullName";

                case "Username":
                    return "Username";

                case "Is Active":
                    return "IsActive";

                default:
                    return "None";
            }
        }
        int? _GetUserIDFromDGV()
        {
            return (int?)dgvUsersList.CurrentRow.Cells["UserID"].Value;
        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            _RefreshUsersList();
            cbFilterBy.SelectedIndex = 0;
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterBy.Visible = (cbFilterBy.Text != "None") && (cbFilterBy.Text != "Is Active");

            cbIsActive.Visible = (cbFilterBy.Text == "Is Active");

            if (txtFilterBy.Visible)
            {
                txtFilterBy.Text = "";
                txtFilterBy.Focus();
            }

            if (cbIsActive.Visible)
            {
                cbIsActive.SelectedIndex = 0;
            }
        }
        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            if (_dtUsersList.Rows.Count <= 0)
                return;

            string ColumnName = _GetRealColumnNameInDB();

            if (string.IsNullOrWhiteSpace(txtFilterBy.Text.Trim()) || cbFilterBy.Text == "None")
            {
                _dtUsersList.DefaultView.RowFilter = "";
                return;
            }

            if (cbFilterBy.Text == "User ID" || cbFilterBy.Text == "Person ID")
                // search with numbers
                _dtUsersList.DefaultView.RowFilter = string.Format("[{0}] = {1}", ColumnName, txtFilterBy.Text.Trim());
            else
                // search with string
                _dtUsersList.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", ColumnName, txtFilterBy.Text.Trim());
        }
        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_dtUsersList.Rows.Count <= 0)
                return;

            if (cbIsActive.Text == "All")
            {
                _dtUsersList.DefaultView.RowFilter = "";
                return;
            }

            _dtUsersList.DefaultView.RowFilter = string.Format("[{0}] = {1}", "IsActive", (cbIsActive.Text == "Yes"));
        }
        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "User ID" || cbFilterBy.Text == "Person ID")
                // make sure that the user can only enter the numbers
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser();
            frm.ShowDialog();
            frmManageUsers_Load(null, null);
        }
        private void dgvUsersList_DoubleClick(object sender, EventArgs e)
        {
            frmShowUserInfo frm = new frmShowUserInfo(_GetUserIDFromDGV());
            frm.ShowDialog();
            frmManageUsers_Load(null, null);
        }
       
        private void cmsShowUserDetails_Click(object sender, EventArgs e)
        {
            frmShowUserInfo frm = new frmShowUserInfo(_GetUserIDFromDGV());
            frm.ShowDialog();
            frmManageUsers_Load(null, null);
        }
        private void cmsAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser();
            frm.ShowDialog();
            frmManageUsers_Load(null, null);
        }
        private void cmsEditUser_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser(_GetUserIDFromDGV());
            frm.ShowDialog();
            frmManageUsers_Load(null, null);
        }
        private void cmsDeleteUser_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this user?", "Confirm", MessageBoxButtons.YesNo,
               MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                if (clsUser.DeleteUser(_GetUserIDFromDGV()))
                {
                    MessageBox.Show("Deleted Done Successfully", "Deleted",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _RefreshUsersList();
                }
                else
                {
                    MessageBox.Show("Deleted Failed", "Failed",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void cmsCahngePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(_GetUserIDFromDGV());
            frm.ShowDialog();
            frmManageUsers_Load(null, null);
        }
        private void cmsSendEmai_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented yet!", "Send Email", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void cmsPhoneCall_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented yet!", "Phone Call", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
