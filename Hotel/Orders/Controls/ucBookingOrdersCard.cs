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
    public partial class ucBookingOrdersCard : UserControl
    {
        DataTable _dtOrders;
        int? _BookingID;
        public ucBookingOrdersCard()
        {
            InitializeComponent();
        }
        int? _GetOrderIDFromDGV()
        {
            return (int?)dgvOrdersList.CurrentRow?.Cells["OrderID"].Value;
        }
        void _RefreshOrdersList()
        {
            _dtOrders = clsOrder.GetAllOrdersForBookingID(_BookingID);
            dgvOrdersList.DataSource = _dtOrders;

            if (dgvOrdersList.Rows.Count > 0)
            {
                dgvOrdersList.Columns[0].HeaderText = "Order ID";
                dgvOrdersList.Columns[0].Width = 120;

                dgvOrdersList.Columns[1].HeaderText = "Booking ID";
                dgvOrdersList.Columns[1].Width = 160;

                dgvOrdersList.Columns[2].HeaderText = "Room Number";
                dgvOrdersList.Columns[2].Width = 180;

                dgvOrdersList.Columns[3].HeaderText = "Order Type";
                dgvOrdersList.Columns[3].Width = 150;

                dgvOrdersList.Columns[4].HeaderText = "Fees";
                dgvOrdersList.Columns[4].Width = 150;

                dgvOrdersList.Columns[5].HeaderText = "Order Date";
                dgvOrdersList.Columns[5].Width = 180;

                dgvOrdersList.Columns[6].HeaderText = "Created By";
                dgvOrdersList.Columns[6].Width = 130;
            }
        }

        public void LoadBookingOrdersInfo(int? BookingID)
        {
            _BookingID = BookingID;

            _RefreshOrdersList();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            contextMenuStrip1.Enabled = (dgvOrdersList.RowCount > 0);
        }

        private void cmsShowOrderDetails_Click(object sender, EventArgs e)
        {
            frmShowOrderInfo frm = new frmShowOrderInfo(_GetOrderIDFromDGV());
            frm.ShowDialog();
        }

        private void dgvOrdersList_DoubleClick(object sender, EventArgs e)
        {
            frmShowOrderInfo frm = new frmShowOrderInfo(_GetOrderIDFromDGV());
            frm.ShowDialog();
        }
    }
}
