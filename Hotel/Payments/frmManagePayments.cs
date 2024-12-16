using Hotel.Invoice;
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
using static HotelDatabase_Buisness.clsRoom;

namespace Hotel.Payments
{
    public partial class frmManagePayments : Form
    {
        DataTable _dtPaymentsList;
        public frmManagePayments()
        {
            InitializeComponent();
        }

        string _GetRealColumnNameInDB()
        {
            switch (cbFilterBy.Text)
            {
                case "Payment ID":
                    return "PaymentID";

                case "Booking ID":
                    return "BookingID";

                case "Guest Name":
                    return "GuestName";

                case "Reason":
                    return "PaymentReason";

                default:
                    return "None";
            }
        }
        void _RefreshPaymentsList()
        {
            _dtPaymentsList = clsPayment.GetAllPayments();
            dgvPaymentsList.DataSource = _dtPaymentsList;

            if(dgvPaymentsList.Rows.Count > 0)
            {
                dgvPaymentsList.Columns[0].HeaderText = "Payment ID";
                dgvPaymentsList.Columns[0].Width = 120;

                dgvPaymentsList.Columns[1].HeaderText = "Booking ID";
                dgvPaymentsList.Columns[1].Width = 120;

                dgvPaymentsList.Columns[2].HeaderText = "Guest Name";
                dgvPaymentsList.Columns[2].Width = 220;

                dgvPaymentsList.Columns[3].HeaderText = "Payment Amount";
                dgvPaymentsList.Columns[3].Width = 120;

                dgvPaymentsList.Columns[4].HeaderText = "Payment Date";
                dgvPaymentsList.Columns[4].Width = 180;

                dgvPaymentsList.Columns[5].HeaderText = "Reason";
                dgvPaymentsList.Columns[5].Width = 150;
            }
        }
        int? _GetPaymentIDFromDGV()
        {
            return (int?)dgvPaymentsList.CurrentRow.Cells["PaymentID"].Value;
        }

        private void frmManagePayments_Load(object sender, EventArgs e)
        {
            _RefreshPaymentsList();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterBy.Visible = (cbFilterBy.Text != "None" && cbFilterBy.Text != "Payment Reason");
            cbPaymentReason.Visible = (cbFilterBy.Text == "Payment Reason");

            if (txtFilterBy.Visible)
            {
                txtFilterBy.Text = "";
                txtFilterBy.Focus();
            }

            if (cbPaymentReason.Visible)
            {
                cbPaymentReason.SelectedIndex = 0;
            }
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            if (_dtPaymentsList.Rows.Count == 0)
                return;

            string ColumnName = _GetRealColumnNameInDB();

            if (string.IsNullOrWhiteSpace(txtFilterBy.Text.Trim()) ||
                cbFilterBy.Text == "None")
            {
                _dtPaymentsList.DefaultView.RowFilter = "";

                return;
            }

            if (cbFilterBy.Text != "Guest Name")
            {
                // search with numbers
                _dtPaymentsList.DefaultView.RowFilter = string.Format("[{0}] = {1}", ColumnName, txtFilterBy.Text.Trim());
            }
            else
            {
                // search with string
                _dtPaymentsList.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", ColumnName, txtFilterBy.Text.Trim());
            }
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text != "Guest Name")
                // make sure that the user can only enter the numbers
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbPaymentReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_dtPaymentsList.Rows.Count == 0)
                return;

            if(cbPaymentReason.Text == "All")
            {
                _dtPaymentsList.DefaultView.RowFilter = "";
                return;
            }

            _dtPaymentsList.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", "PaymentReason", cbPaymentReason.Text);

        }

        private void cmsShowDetails_Click(object sender, EventArgs e)
        {
            frmShowPaymentInfo frm = new frmShowPaymentInfo(_GetPaymentIDFromDGV());
            frm.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (dgvPaymentsList.RowCount <= 0)
            {
                contextMenuStrip1.Enabled = false;
                return;
            }

            cmsPrintInvoice.Text = (clsInvoice.DoesPaymentHaveAnInvoice(_GetPaymentIDFromDGV())) ? 
                "Show Invoice" : "Print Invoice";
        }

        private void cmsPrintInvoice_Click(object sender, EventArgs e)
        {
            if (cmsPrintInvoice.Text == "Print Invoice")
                _PrintInvoice();
            else
                _ShowInvoice();
        }

        private bool _AddNewInvoice(out int? InvoiceID)
        {
            InvoiceID = null;

            clsInvoice invoice = new clsInvoice();
            invoice.PaymentID = _GetPaymentIDFromDGV();

            if (!invoice.Save())
                return false;

            InvoiceID = invoice.InvoiceID;
            return true;
        }

        private void _PrintInvoice()
        {
            if(_AddNewInvoice(out int? NewInvoiceID))
            {
                _OpenPaymentInvoiceForm(_GetPaymentIDFromDGV(), NewInvoiceID);
            }
            else
            {
                MessageBox.Show("Cannot create an invoice! Please try again later.", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _OpenPaymentInvoiceForm(int? PaymentID, int? NewInvoiceID)
        {
            frmPaymentInvoice PaymentInvoice = new frmPaymentInvoice(PaymentID, NewInvoiceID);
            PaymentInvoice.ShowDialog();
        }

        private void _ShowInvoice()
        {
            clsInvoice Invoice = clsInvoice.FindByPaymentID(_GetPaymentIDFromDGV());

            if (Invoice == null)
            {
                MessageBox.Show("Cannot show the invoice! Please try again later.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _OpenPaymentInvoiceForm(Invoice.PaymentID, Invoice.InvoiceID);
        }
    }
}
