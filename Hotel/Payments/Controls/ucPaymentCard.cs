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

namespace Hotel.Payments.Controls
{
    public partial class ucPaymentCard : UserControl
    {
        int? _PaymentID = null;
        clsPayment _Payment = null;

        public int? PaymentID => _PaymentID;
        public clsPayment Payment => _Payment;


        public ucPaymentCard()
        {
            InitializeComponent();
        }

        bool _DoesPaymentExist(int? PaymentID)
        {
            if (!PaymentID.HasValue)
            {
                MessageBox.Show("There is no Payment!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                Reset();

                return false;
            }

            _Payment = clsPayment.Find(PaymentID);

            if (_Payment == null)
            {
                MessageBox.Show($"There is no Payment with ID = {PaymentID} !",
                    "Missing Payment", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Reset();

                return false;
            }

            return true;
        }

        void _HideIcons()
        {
            icGuest.Visible = false;
            icPhone.Visible = false;
            icEmail.Visible = false;
            icAddress.Visible = false;
            icPaymentID.Visible = false;
            icBookingID.Visible = false;
            icPaymnetDate.Visible = false;
            icPaidAmount.Visible = false;
        }

        void _FillPaymentData()
        {
            lblPaymentID.Text = _Payment.PaymentID.ToString();
            lblGuestName.Text = _Payment.BookingInfo.ReservationInfo.GuestInfo.PersonInfo.FullName;
            lblPhone.Text = _Payment.BookingInfo.ReservationInfo.GuestInfo.PersonInfo.Phone;
            lblEmail.Text = _Payment.BookingInfo.ReservationInfo.GuestInfo.PersonInfo.Email ?? "N/A";
            lblAddress.Text = _Payment.BookingInfo.ReservationInfo.GuestInfo.PersonInfo.Address;
            lblBookingID.Text = _Payment.BookingID.ToString();

            lblPaymentDate.Text = clsFormat.DateToShort(_Payment.PaymentDate);

            lblPaidAmount.Text = _Payment.PaymentAmount.ToString("C");
        }

        public void Reset()
        {
            _PaymentID = null;
            _Payment = null;

            lblGuestName.Text = "[????]";
            lblPhone.Text = "[????]";
            lblEmail.Text = "[????]";
            lblAddress.Text = "[????]";
            lblPaymentID.Text = "[????]";
            lblBookingID.Text = "[????]";
            lblPaymentDate.Text = "[????]";
            lblPaidAmount.Text = "[????]";
        }

        public void LoadPaymentInfo(int? PaymentID, bool hideIcons = false)
        {
            _PaymentID = PaymentID;

            if (!_DoesPaymentExist(PaymentID))
                return;

            _FillPaymentData();

            if (hideIcons)
                _HideIcons();

        }
    }
}
