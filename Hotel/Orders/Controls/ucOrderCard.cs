using Hotel.Grobal;
using Hotel.OrderItems;
using Hotel.Properties;
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

namespace Hotel.Orders.Controls
{
    public partial class ucOrderCard : UserControl
    {
        int? _OrderID = null;
        clsOrder _Order = null;

        public int? OrderID => _OrderID;
        public clsOrder Order => _Order;

        public ucOrderCard()
        {
            InitializeComponent();
        }

        void _FillOrderData()
        {
            llShowOrderItemsInfo.Visible = (_Order.OrderType == clsOrder.enOrderType.Dining);

            lblOrderID.Text = _Order.OrderID.ToString();
            lblOrderedBy.Text = _Order.BookingInfo.ReservationInfo.GuestInfo.PersonInfo.FullName;
            lblRoomNumber.Text = _Order.RoomInfo.RoomNumber.ToString();
            lblOrderDate.Text = clsFormat.DateToShort(_Order.OrderDate);
            lblOrderType.Text = _Order.OrderTypeName;
            lblBookingID.Text = _Order.BookingID.ToString();
            lblTotalAmount.Text = _Order.Fees.ToString("C");
            lblCreatedByUser.Text = _Order.CreatedByUserInfo.Username;

            pbGender.Image = (_Order.BookingInfo.ReservationInfo.GuestInfo.PersonInfo.Gender == clsPerson.enGender.Male) ?
                              Resources.gender_male : Resources.gender_female;
        }

        public void Clear()
        {
            _OrderID = null;
            _Order = null;

            llShowOrderItemsInfo.Visible = false;

            lblOrderID.Text = "[????]";
            lblRoomNumber.Text = "[????]";
            lblOrderType.Text = "[????]";
            lblOrderDate.Text = "[????]";
            lblOrderedBy.Text = "[????]";
            lblBookingID.Text = "[????]";
            lblCreatedByUser.Text = "[????]";
            lblTotalAmount.Text = "[????]";

            pbGender.Image = Resources.gender_male;
        }
        public void LoadOrderInfo(int? OrderID)
        {
            _OrderID = OrderID;

            if (!_OrderID.HasValue)
            {
                MessageBox.Show("There is no Order!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Clear();
                return;
            }

            _Order = clsOrder.Find(_OrderID);

            if (_Order == null)
            {
                MessageBox.Show($"There is no Order with ID = {_OrderID} !", "Missing Order", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Clear();
                return;
            }

            _FillOrderData();
        }

        private void llShowOrderItemsInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowOrderItemsInfo ShowOrderItemsInfo = new frmShowOrderItemsInfo(_Order.OrderID);
            ShowOrderItemsInfo.ShowDialog();
        }
    }
}
