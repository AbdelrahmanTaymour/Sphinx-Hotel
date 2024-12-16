using Hotel.Grobal;
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

namespace Hotel.Bookings.Controls
{
    public partial class ucBookingCard : UserControl
    {
        int? _BookingID = null;
        clsBooking _Booking = null;

        public int? BookingID => _BookingID;
        public clsBooking BookingInfo => _Booking;

        public ucBookingCard()
        {
            InitializeComponent();
        }

        void _FillBookingData()
        {
            lblBookingID.Text = _Booking.BookingID.ToString();
            lblStatus.Text = _Booking.BookingStatusName;
            lblCreatedByUser.Text = _Booking.CreatedByUserInfo.Username;
            lblCheckInDate.Text = clsFormat.DateToShort(_Booking.CheckInDate);
            lblCheckOutDate.Text = clsFormat.DateToShort(_Booking.CheckOutDate);
            lblPaymentID.Text = clsPayment.FindByBookingID(_BookingID)?.PaymentID.ToString();
        }

        public void Reset()
        {
            _BookingID = null;
            _Booking = null;

            lblBookingID.Text = "[????]";
            lblStatus.Text = "[????]";
            lblCreatedByUser.Text = "[????]";
            lblCheckInDate.Text = "[????]";
            lblCheckOutDate.Text = "[????]";
            lblPaymentID.Text = "[????]";
        }

        public void LoadBookingInfo(int? BookingID)
        {
            _BookingID = BookingID;

            if (!_BookingID.HasValue)
            {
                MessageBox.Show("There is no BookingID!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                Reset();

                return;
            }

            _Booking = clsBooking.Find(_BookingID);

            if (_Booking == null)
            {
                MessageBox.Show($"There is no BookingID with ID = {_BookingID} !",
                    "Missing Booking", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Reset();

                return;
            }

            _FillBookingData();
        }
    }
}
