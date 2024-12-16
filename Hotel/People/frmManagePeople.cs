using HotelDatabase_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel.People
{
    public partial class frmManagePeople : Form
    {
        DataTable _dtPeopleList;
        void _RefreshPeopleList()
        {
            _dtPeopleList = clsPerson.GetAllPeople();
            dgvPeopleList.DataSource = _dtPeopleList;

            if (dgvPeopleList.Rows.Count > 0)
            {
                dgvPeopleList.Columns[0].HeaderText = "Person ID";
                dgvPeopleList.Columns[0].Width = 100;

                dgvPeopleList.Columns[1].HeaderText = "National No";
                dgvPeopleList.Columns[1].Width = 100;

                dgvPeopleList.Columns[2].HeaderText = "Full Name";
                dgvPeopleList.Columns[2].Width = 220;

                dgvPeopleList.Columns[3].HeaderText = "Country";
                dgvPeopleList.Columns[3].Width = 110;

                dgvPeopleList.Columns[4].HeaderText = "Date Of Birth";
                dgvPeopleList.Columns[4].Width = 120;

                dgvPeopleList.Columns[5].HeaderText = "Gender";
                dgvPeopleList.Columns[5].Width = 100;

                dgvPeopleList.Columns[6].HeaderText = "Phone";
                dgvPeopleList.Columns[6].Width = 110;

                dgvPeopleList.Columns[7].HeaderText = "Email";
                dgvPeopleList.Columns[7].Width = 160;

            }
        }
        public frmManagePeople()
        {
            InitializeComponent();
        }
        string _GetFilterColumn(string cbFilterByText)
        {
            switch (cbFilterByText)
            {
                case "Person ID":
                    return "PersonID";

                case "National No.":
                    return "NationalNo";

                case "First Name":
                    return "FirstName";

                case "Last Name":
                    return "LastName";

                case "Nationality":
                    return "CountryName";

                case "Gender":
                    return "GenderCaption";

                case "Phone":
                    return "Phone";

                case "Email":
                    return "Email";

                default:
                    return "None";
            }
        }
        int? _GetPersonIDFromDGV()
        {
            return (int?)dgvPeopleList.CurrentRow.Cells["PersonID"].Value;
        }
        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            _RefreshPeopleList();
            cbFilterBy.SelectedIndex = 0;
        }
        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson();
            frm.ShowDialog();
            frmManagePeople_Load(null, null);
        }
        private void cmsShowPersonDetail_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo((int)dgvPeopleList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmManagePeople_Load(null, null);
        }
        private void cmsAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson();
            frm.ShowDialog();
            frmManagePeople_Load(null, null);
        }
        private void cmsEditPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson((int)dgvPeopleList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmManagePeople_Load(null, null);
        }
        private void cmsDeletePerson_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete Person [" + dgvPeopleList.CurrentRow.Cells[0].Value + "]",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int? PersonID = (int)dgvPeopleList.CurrentRow.Cells[0].Value;
                string ImagePath = clsPerson.Find(PersonID).ImagePath;

                if(clsPerson.DeletePerson(PersonID))
                {
                    if(ImagePath!=null)
                        File.Delete(ImagePath);

                    MessageBox.Show("Person Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmManagePeople_Load(null, null);
                }
                else
                {
                    MessageBox.Show("This person appears to be engaged in some active transactions.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void cmsSendEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented yet!", "SendEmail", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void cmsPhoneCall_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented yet!", "Phone Call", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = _GetFilterColumn(cbFilterBy.Text);

            if (txtFilterBy.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeopleList.DefaultView.RowFilter = "";
                return;
            }

            if (FilterColumn == "PersonID")
                _dtPeopleList.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterBy.Text.Trim());
            else
                _dtPeopleList.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterBy.Text.Trim());

        }
        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID")
            {
                if (!Char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                    e.Handled = true;
            }
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Gender")
            {
                txtFilterBy.Visible = false;
                cbGander.Visible = true;
                cbGander.Focus();
                cbGander.SelectedIndex = 0;

            }
            else
            {
                txtFilterBy.Visible = (cbFilterBy.SelectedItem.ToString() != "None");

                if (txtFilterBy.Visible)
                {
                    txtFilterBy.Focus();
                    txtFilterBy.Text = "";
                }
            }
        }
        private void cbGander_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "GenderCaption";
            string FilterValue = cbGander.Text;

            if (cbGander.Text == "All")
                _dtPeopleList.DefaultView.RowFilter = "";
            else
                _dtPeopleList.DefaultView.RowFilter = string.Format("[{0}] = '{1}'", FilterColumn, FilterValue);
        }

        private void dgvPeopleList_DoubleClick(object sender, EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo(_GetPersonIDFromDGV());
            frm.ShowDialog();
            frmManagePeople_Load(null, null);
        }
    }
}
