using HotelDatabase_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static HotelDatabase_Buisness.clsBooking;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Hotel.Bookings
{
    public partial class frmManageBookings : Form
    {
        DataTable _dtBooking;
        public frmManageBookings()
        {
            InitializeComponent();
        }

        string _GetRealColumnNameInDB()
        {
            switch (cbFilterBy.Text)
            {
                case "Booking ID":
                    return "BookingID";

                case "Reservation ID":
                    return "ReservationID";

                case "Guest Name":
                    return "FullName";

                case "Status":
                    return "Status";

                case "Payment ID":
                    return "PaymentID";

                default:
                    return "None";
            }
        }
        void _RefreshBookingList()
        {
            _dtBooking = clsBooking.GetAllBookings();
            dgvBookingList.DataSource = _dtBooking;

            if(dgvBookingList.Rows.Count > 0)
            {
                dgvBookingList.Columns[0].HeaderText = "Booking ID";
                dgvBookingList.Columns[0].Width = 110;

                dgvBookingList.Columns[1].HeaderText = "Reservation ID";
                dgvBookingList.Columns[1].Width = 125;

                dgvBookingList.Columns[2].HeaderText = "Guest Name";
                dgvBookingList.Columns[2].Width = 280;

                dgvBookingList.Columns[3].HeaderText = "Check In Date";
                dgvBookingList.Columns[3].Width = 150;

                dgvBookingList.Columns[4].HeaderText = "Check Out Date";
                dgvBookingList.Columns[4].Width = 150;

                dgvBookingList.Columns[5].HeaderText = "IncidentalCharges";
                dgvBookingList.Columns[5].Width = 145;

                dgvBookingList.Columns[6].HeaderText = "Status";
                dgvBookingList.Columns[6].Width = 130;

                dgvBookingList.Columns[7].HeaderText = "Payment ID";
                dgvBookingList.Columns[7].Width = 110;
            }
        }
        int? _GetBookingIDFromDGV()
        {
            return (int?)dgvBookingList.CurrentRow?.Cells["BookingID"].Value;
        }
        int? _GetReservationIDFromDGV()
        {
            return (int?)dgvBookingList.CurrentRow?.Cells["ReservationID"].Value;
        }
        string _GetBookingStatusFromDGV()
        {
            return (string)dgvBookingList.CurrentRow?.Cells["Status"].Value;
        }

        private void frmManageBookings_Load(object sender, EventArgs e)
        {
            _RefreshBookingList();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterBy.Visible = (cbFilterBy.Text != "None") && (cbFilterBy.Text != "Status");

            cbBookingStatus.Visible = (cbFilterBy.Text == "Status");

            if (txtFilterBy.Visible)
            {
                txtFilterBy.Text = "";
                txtFilterBy.Focus();
            }

            if (cbBookingStatus.Visible)
            {
                cbBookingStatus.SelectedIndex = 0;
            }
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            if (_dtBooking.Rows.Count == 0)
                return;

            string ColumnName = _GetRealColumnNameInDB();

            if (string.IsNullOrWhiteSpace(txtFilterBy.Text.Trim()) || cbFilterBy.Text == "None")
            {
                _dtBooking.DefaultView.RowFilter = "";
                return;
            }

            if (cbFilterBy.Text != "Guest" && cbFilterBy.Text != "Status")
            {
                // search with numbers
                _dtBooking.DefaultView.RowFilter = string.Format("[{0}] = {1}", ColumnName, txtFilterBy.Text.Trim());
            }
            else
            {
                // search with string
                _dtBooking.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", ColumnName, txtFilterBy.Text.Trim());
            }
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text != "Guest" && cbFilterBy.Text != "Status")
            {
                // make sure that the user can only enter the numbers
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void cbBookingStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_dtBooking.Rows.Count == 0)
                return;

            if (cbBookingStatus.Text == "All")
            {
                _dtBooking.DefaultView.RowFilter = "";
                return;
            }

            _dtBooking.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", "Status", cbBookingStatus.Text);

        }

        private void cmsShowBookingDetail_Click(object sender, EventArgs e)
        {
            frmShowBookingInfo frm = new frmShowBookingInfo(_GetBookingIDFromDGV());
            frm.ShowDialog();
        }

        private void dgvBookingList_DoubleClick(object sender, EventArgs e)
        {
            frmShowBookingInfo frm = new frmShowBookingInfo(_GetBookingIDFromDGV());
            frm.ShowDialog();
        }

        private void cmsCheckOut_Click(object sender, EventArgs e)
        {
            frmCheckOut frm = new frmCheckOut(_GetReservationIDFromDGV(), _GetBookingIDFromDGV());
            frm.ShowDialog();
            _RefreshBookingList();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            cmsCheckOut.Enabled = _GetBookingStatusFromDGV() == "Checked-Out" ? false : true;
        }
    }
}
