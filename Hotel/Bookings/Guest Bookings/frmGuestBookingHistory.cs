using Hotel.Payments;
using Hotel.People;
using Hotel.Reservations;
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

namespace Hotel.Bookings
{
    public partial class frmGuestBookingHistory : Form
    {
        DataTable _dtAllBookingHistory;

        int? _GuestID;
        public frmGuestBookingHistory(int? GuestID, int? PersonID)
        {
            InitializeComponent();
            _GuestID = GuestID;
            ucPersonCard1.LoadPersonInfo(PersonID);
        }
        int? _GetBookingIDFromDGV()
        {
            return (int?)dgvBookingsList.CurrentRow.Cells[0].Value;
        }
        int? _GetReservationIDFromDGV()
        {
            return (int?)dgvBookingsList.CurrentRow.Cells[1].Value;
        }
        int? _GetPaymentIDFromDGV()
        {
            return (int?)dgvBookingsList.CurrentRow.Cells[2].Value;
        }
        void _RefreshBookingsList()
        {
            _dtAllBookingHistory = clsBooking.GetGuestBookingHistory(_GuestID);
            dgvBookingsList.DataSource = _dtAllBookingHistory;

            if (dgvBookingsList.Rows.Count > 0)
            {
                dgvBookingsList.Columns[0].HeaderText = "Booking ID";
                dgvBookingsList.Columns[0].Width = 80;

                dgvBookingsList.Columns[1].HeaderText = "Reservation ID";
                dgvBookingsList.Columns[1].Width = 80;

                dgvBookingsList.Columns[2].HeaderText = "Payment ID";
                dgvBookingsList.Columns[2].Width = 80;

                dgvBookingsList.Columns[3].HeaderText = "Full Name";
                dgvBookingsList.Columns[3].Width = 180;

                dgvBookingsList.Columns[4].HeaderText = "Room";
                dgvBookingsList.Columns[4].Width = 50;

                dgvBookingsList.Columns[5].HeaderText = "Payment Amount";
                dgvBookingsList.Columns[5].Width = 110;

                dgvBookingsList.Columns[6].HeaderText = "Created By";
                dgvBookingsList.Columns[6].Width = 130;

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmGuestBookingHistory_Load(object sender, EventArgs e)
        {
            _RefreshBookingsList();
        }

        private void cmsShowBookingDetails_Click(object sender, EventArgs e)
        {

            frmShowBookingInfo frm = new frmShowBookingInfo(_GetBookingIDFromDGV());
            frm.ShowDialog();
        }

        private void cmsShowReservationDetails_Click(object sender, EventArgs e)
        {
            frmShowReservationInfo frm = new frmShowReservationInfo(_GetReservationIDFromDGV());
            frm.ShowDialog();
        }

        private void cmsShowPaymentDetails_Click(object sender, EventArgs e)
        {
            frmShowPaymentInfo frm = new frmShowPaymentInfo(_GetPaymentIDFromDGV());
            frm.ShowDialog();
        }

    }
}
