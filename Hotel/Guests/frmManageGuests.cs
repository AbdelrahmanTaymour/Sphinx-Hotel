using Hotel.Bookings;
using Hotel.People;
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

namespace Hotel.Guests
{
    public partial class frmManageGuests : Form
    {
        DataTable _dtGuestsList;
        public frmManageGuests()
        {
            InitializeComponent();
        }

        void _FillCountryComboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();

            foreach (DataRow Country in dtCountries.Rows)
            {
                cbCountries.Items.Add(Country["CountryName"].ToString());
            }
        }
        void _RefreashGuestsList()
        {
            _dtGuestsList = clsGuest.GetAllGuests();
            dgvGuestsList.DataSource = _dtGuestsList;

            if(_dtGuestsList.Rows.Count > 0)
            {
                dgvGuestsList.Columns[0].HeaderText = "Guest ID";
                dgvGuestsList.Columns[0].Width = 110;

                dgvGuestsList.Columns[1].HeaderText = "Person ID";
                dgvGuestsList.Columns[1].Width = 110;

                dgvGuestsList.Columns[2].HeaderText = "National Number";
                dgvGuestsList.Columns[2].Width = 125;

                dgvGuestsList.Columns[3].HeaderText = "Full Name";
                dgvGuestsList.Columns[3].Width = 180;

                dgvGuestsList.Columns[4].HeaderText = "Gender";
                dgvGuestsList.Columns[4].Width = 150;

                dgvGuestsList.Columns[5].HeaderText = "Country";
                dgvGuestsList.Columns[5].Width = 150;

                dgvGuestsList.Columns[6].HeaderText = "Phone";
                dgvGuestsList.Columns[6].Width = 130;
            }

            cbFilterBy.SelectedIndex = 0;
        }
        int? _GetGuestIDFromDGV()
        {
            return (int?)dgvGuestsList.CurrentRow.Cells[0].Value;
        }
        int? _GetPersonIDFromDGV()
        {
            return (int?)dgvGuestsList.CurrentRow.Cells[1].Value;
        }
        string _GetRealColumnNameInDB()
        {
            switch (cbFilterBy.Text)
            {
                case "Guest ID":
                    return "GuestID";

                case "Person ID":
                    return "PersonID";

                case "National Number":
                    return "NationalNo";

                case "Name":
                    return "FullName";

                case "Gender":
                    return "Gender";

                case "Phone":
                    return "Phone";

                case "Nationality":
                    return "CountryName";

                default:
                    return "None";
            }
        }
        private void frmManageGuests_Load(object sender, EventArgs e)
        {
            _RefreashGuestsList();
            _FillCountryComboBox();
        }

        private void cmsShowPersonInfo_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo(_GetPersonIDFromDGV());
            frm.ShowDialog();
        }

        private void EdittoolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson(_GetPersonIDFromDGV());
            frm.ShowDialog();

            frmManageGuests_Load(null, null);
        }

        private void cmsShowBookingHistory_Click(object sender, EventArgs e)
        {
            frmGuestBookingHistory frm = new frmGuestBookingHistory(_GetGuestIDFromDGV(), _GetPersonIDFromDGV());
            frm.ShowDialog();
        }

        private void cmsPhoneCall_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented yet!", "Phone Call", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvGuestsList_DoubleClick(object sender, EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo(_GetPersonIDFromDGV());
            frm.ShowDialog();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterBy.Visible = (cbFilterBy.Text != "None") &&
                                  (cbFilterBy.Text != "Gender") &&
                                  (cbFilterBy.Text != "Nationality");

            cbCountries.Visible = (cbFilterBy.Text == "Nationality");
            cbGender.Visible = (cbFilterBy.Text == "Gender");

            if(txtFilterBy.Visible)
            {
                txtFilterBy.Text = "";
                txtFilterBy.Focus();
            }

            if (cbCountries.Visible)
                cbCountries.SelectedIndex = 0;

            if (cbGender.Visible)
                cbGender.SelectedIndex = 0;
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            if (_dtGuestsList.Rows.Count == 0)
                return;

            string ColumnName = _GetRealColumnNameInDB();

            if (string.IsNullOrWhiteSpace(txtFilterBy.Text.Trim()) || cbFilterBy.Text == "None")
            {
                _dtGuestsList.DefaultView.RowFilter = "";
                return;
            }

            if (cbFilterBy.Text == "Guest ID" || cbFilterBy.Text == "Person ID")
                _dtGuestsList.DefaultView.RowFilter = string.Format("[{0}] = {1}", ColumnName, txtFilterBy.Text.Trim());
            else
                _dtGuestsList.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", ColumnName, txtFilterBy.Text.Trim());
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Guest ID" || cbFilterBy.Text == "Person ID")
                // make sure that the user can only enter the numbers
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_dtGuestsList.Rows.Count == 0)
                return;

            if(cbGender.Text == "All")
            {
                _dtGuestsList.DefaultView.RowFilter = "";
                return;
            }

            _dtGuestsList.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", "Gender", cbGender.Text);

        }

        private void cbCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_dtGuestsList.Rows.Count == 0)
                return;

            if (cbCountries.Text == "All")
            {
                _dtGuestsList.DefaultView.RowFilter = "";
                return;
            }

            _dtGuestsList.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", "CountryName", cbCountries.Text);

        }
    }
}
