using Hotel.Global;
using HotelDatabase_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel.Reservations
{
    public partial class frmCheckIn : Form
    {
        int? BookingID = null;
        int? PaymentID = null;

        int? _ReservationID = null;
        clsReservation _Reservation;
        public frmCheckIn(int? ReservationID)
        {
            InitializeComponent();

            _ReservationID = ReservationID;
        }

        int _GetNumberOfTotalNights()
        {
            return Math.Abs((_Reservation.ReservedToDate - _Reservation.ReservedForDate).Days);
        }

        decimal _GetPricePerNight()
        {
            return _Reservation.RoomInfo.RoomTypeInfo.PricePerNight;
        }

        void Clear()
        {
            ucReservationsCard1.Clear();

            lblBookingID.Text = "[????]";
            lblPaymentID.Text = "[????]";
            lblNightsNo.Text = "[????]";
            lblPricePerNight.Text = "[????]";
            lblTotalAmount.Text = "[????]";
            lblCreatedByUser.Text = "[????]";
        }
        void FillData()
        {
            ucReservationsCard1.LoadReservationInfo(_ReservationID);
            _Reservation = ucReservationsCard1.ReservationInfo;
            btnPay.Enabled = true;

            int NumberOfNights = _GetNumberOfTotalNights();
            decimal PricePerNight = _GetPricePerNight();

            lblBookingID.Text = (BookingID.HasValue) ? BookingID.ToString() : "[????]";
            lblPaymentID.Text = (PaymentID.HasValue) ? PaymentID.ToString() : "[????]";

            lblNightsNo.Text = NumberOfNights.ToString();
            lblPricePerNight.Text = "$" + PricePerNight.ToString();
            lblTotalAmount.Text = "$" + (NumberOfNights * PricePerNight).ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.Username;
        }

        private void frmCheckIn_Load(object sender, EventArgs e)
        {
            FillData();
        }

        DialogResult _ShowCheckInReservationMessage()
        {
            return MessageBox.Show("Are you sure you want to check-in this reservation?", "Confirm",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        void _ShowSuccessMessage(int? BookingID, int? PaymentID)
        {
            MessageBox.Show($"Check-in completed successfully with \nBookinID = {BookingID}, and PaymentID = {PaymentID}!", "Confirmed",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        void _ShowFailureMessage()
        {
            MessageBox.Show("Check-in failed!", "Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void btnPay_Click(object sender, EventArgs e)
        {
            int? CreatedByUser = clsGlobal.CurrentUser.UserID;

            if(_ShowCheckInReservationMessage() == DialogResult.Yes)
            {
                (bool IsBooked, int? BookingID, int? PaymentID) Booking = _Reservation.CheckIn(CreatedByUser);

                if (Booking.IsBooked)
                {
                    BookingID = Booking.BookingID;
                    PaymentID = Booking.PaymentID;

                    _ShowSuccessMessage(BookingID, PaymentID);
                    FillData();

                    btnPay.Enabled = false;
                    return;
                }

                _ShowFailureMessage();
            }

            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
