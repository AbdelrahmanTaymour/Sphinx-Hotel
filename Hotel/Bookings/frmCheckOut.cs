using Hotel.Global;
using Hotel.Orders;
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
    public partial class frmCheckOut : Form
    {
        int? _ReservationID;
        int? _BookingID;
        decimal TotalAmount;

        clsReservation _Reservation;
        clsBooking _Booking;
        public frmCheckOut(int? ReservationID, int? BookingID)
        {
            InitializeComponent();

            _BookingID = BookingID;
            _ReservationID = ReservationID;
        }
        int _NumberOfExtraNights()
        {
            return (DateTime.Now - _Booking.CheckOutDate).Days;
        }

        decimal _GetPricePerNight()
        {
            return _Reservation.RoomInfo.RoomTypeInfo.PricePerNight;
        }

        void _FillData()
        {
            ucReservationsCard1.LoadReservationInfo(_ReservationID);
            ucBookingCard1.LoadBookingInfo(_BookingID);

            _Reservation = ucReservationsCard1.ReservationInfo;
            _Booking = ucBookingCard1.BookingInfo;

            decimal TotalExtraNightsAmount = 0m;
            int NumberOfExtraNights = _NumberOfExtraNights();

            if(NumberOfExtraNights > 0)
            {
                decimal PricePerNight = _GetPricePerNight();
                TotalExtraNightsAmount = (PricePerNight * 1.50m) * NumberOfExtraNights;

                lblPricePerNight.Text = PricePerNight.ToString("C");
                lblTotalExtraNights.Text = NumberOfExtraNights.ToString();
                lblPriceIncrement.Text = "+50%";
                lblExtraNightsAmount.Text = TotalExtraNightsAmount.ToString("C");
            }
            
            lblIncidentalCharges.Text = _Booking.IncidentalCharges.ToString("C");

            TotalAmount = TotalExtraNightsAmount + _Booking.IncidentalCharges;
            lblTotalAmount.Text = TotalAmount.ToString("C");

            lblCreatedByUser.Text = clsGlobal.CurrentUser.Username;

            linkLabel1.Visible = _Booking.IncidentalCharges > 0;

        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowBookingOrdersList frm = new frmShowBookingOrdersList(_BookingID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCheckOut_Load(object sender, EventArgs e)
        {
            _FillData();
        }

        DialogResult _ShowCheckOutMessage()
        {
            return MessageBox.Show("Are you sure you want to check-out?", "Confirm",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        void _ShowSuccessMessage(int? PaymentID, decimal TotalAmount)
        {
            MessageBox.Show($"Check-out completed successfully with PaymentID = {PaymentID}\nTotal Amount = {TotalAmount}!", "Confirmed",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        void _ShowFailureMessage()
        {
            MessageBox.Show("Check-out failed!", "Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            int? CreatedByUser = clsGlobal.CurrentUser.UserID;

            (bool IsSucceed, int? PaymentID) CheckOut = _Booking.CheckOut(CreatedByUser);
            if (_ShowCheckOutMessage() == DialogResult.Yes)
            {
                if(CheckOut.IsSucceed)
                {
                    _ShowSuccessMessage(CheckOut.PaymentID, TotalAmount);
                    _FillData();

                    btnPay.Enabled = false;
                    return;
                }

                _ShowFailureMessage();
            }
        }
    }
}
