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

namespace Hotel.Orders
{
    public partial class frmMangeOrders : Form
    {
        DataTable _dtOrders;
        public frmMangeOrders()
        {
            InitializeComponent();
        }
        string _GetRealColumnNameInDB()
        {
            switch (cbFilterBy.Text)
            {
                case "Order ID":
                    return "OrderID";

                case "Booking ID":
                    return "BookingID";

                case "Room Number":
                    return "RoomNumber";

                case "Order Type":
                    return "OrderType";

                case "Payment ID":
                    return "PaymentID";

                default:
                    return "None";
            }
        }

        void _RefreshOrdersList()
        {
            _dtOrders = clsOrder.GetAllOrders();
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
                dgvOrdersList.Columns[5].Width = 300;

                dgvOrdersList.Columns[6].HeaderText = "Created By";
                dgvOrdersList.Columns[6].Width = 130;
            }
        }
        int? _GetOrderIDFromDGV()
        {
            return (int?)dgvOrdersList.CurrentRow?.Cells["OrderID"].Value;
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterBy.Visible = (cbFilterBy.Text != "None") && (cbFilterBy.Text != "Order Type");

            cbOrderTypes.Visible = (cbFilterBy.Text == "Order Type");

            if (txtFilterBy.Visible)
            {
                txtFilterBy.Text = "";
                txtFilterBy.Focus();
            }

            if (cbOrderTypes.Visible)
            {
                cbOrderTypes.SelectedIndex = 0;
            }
        }

        private void frmMangeOrders_Load(object sender, EventArgs e)
        {
            _RefreshOrdersList();
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            if (_dtOrders.Rows.Count == 0)
                return;

            string ColumnName = _GetRealColumnNameInDB();

            if (string.IsNullOrWhiteSpace(txtFilterBy.Text.Trim()) ||
                cbFilterBy.Text == "None")
            {
                _dtOrders.DefaultView.RowFilter = "";

                return;
            }

            _dtOrders.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", ColumnName, txtFilterBy.Text.Trim());
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text != "Guest Name")
                // make sure that the user can only enter the numbers
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbOrderTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_dtOrders.Rows.Count == 0)
                return;

            if (cbOrderTypes.Text == "All")
            {
                _dtOrders.DefaultView.RowFilter = "";
                return;
            }

            _dtOrders.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", "OrderType", cbOrderTypes.Text);

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            contextMenuStrip1.Enabled = (dgvOrdersList.RowCount > 0);
        }

        private void btnAddNewOrder_Click(object sender, EventArgs e)
        {
            frmAddNewOrder frm = new frmAddNewOrder();
            frm.ShowDialog();

            _RefreshOrdersList();
        }

        private void cmsShowOrderDetails_Click(object sender, EventArgs e)
        {
            frmShowOrderInfo frm = new frmShowOrderInfo(_GetOrderIDFromDGV());
            frm.ShowDialog();
        }
    }
}
