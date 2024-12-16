using Guna.UI2.WinForms;
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

namespace Hotel.RoomTypes
{
    public partial class frmManageRoomTypes : Form
    {
        DataTable _dtRoomTypes;
        public frmManageRoomTypes()
        {
            InitializeComponent();
        }

        string _GetRealColumnNameInDB()
        {
            switch (cbFilterBy.Text)
            {
                case "Room Type ID":
                    return "RoomTypeID";

                case "Room Type Title":
                    return "RoomTypeTitle";

                case "Capacity":
                    return "Capacity";

                case "Price Per Night":
                    return "PricePerNight";

                default:
                    return "None";
            }
        }

        void _RefreshRoomTypesList()
        {
            _dtRoomTypes = clsRoomType.GetAllRoomTypes();
            dgvRoomTypes.DataSource = _dtRoomTypes;

            if (dgvRoomTypes.Rows.Count > 0)
            {
                dgvRoomTypes.Columns[0].HeaderText = "Room Type ID";
                dgvRoomTypes.Columns[0].Width = 150;

                dgvRoomTypes.Columns[1].HeaderText = "Room Type Title";
                dgvRoomTypes.Columns[1].Width = 190;

                dgvRoomTypes.Columns[2].HeaderText = "Capacity";
                dgvRoomTypes.Columns[2].Width = 150;

                dgvRoomTypes.Columns[3].HeaderText = "Price Per Night";
                dgvRoomTypes.Columns[3].Width = 180;
            }
        }

        int? _GetRoomIDFromDGV()
        {
            return (int?)dgvRoomTypes.CurrentRow.Cells["RoomTypeID"].Value;
        }

        private void frmManageRoomTypes_Load(object sender, EventArgs e)
        {
            _RefreshRoomTypesList();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterBy.Visible = (cbFilterBy.Text != "None");

            if (txtFilterBy.Visible)
            {
                txtFilterBy.Text = "";
                txtFilterBy.Focus();
            }
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            if (_dtRoomTypes.Rows.Count == 0)
                return;

            string ColumnName = _GetRealColumnNameInDB();

            if (string.IsNullOrWhiteSpace(txtFilterBy.Text.Trim()) ||
                cbFilterBy.Text == "None")
            {
                _dtRoomTypes.DefaultView.RowFilter = "";
                return;
            }

            if (cbFilterBy.Text != "Room Type Title")
            {
                // search with numbers
                _dtRoomTypes.DefaultView.RowFilter = string.Format("[{0}] = {1}", ColumnName, txtFilterBy.Text.Trim());
            }
            else
            {
                // search with string
                _dtRoomTypes.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", ColumnName, txtFilterBy.Text.Trim());
            }
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text != "Room Type Title")
            {
                char inputChar = e.KeyChar;

                // Allow digits, the decimal point, and the backspace.
                bool isDigit = Char.IsDigit(inputChar);
                bool isDecimalPoint = (inputChar == '.');
                bool isBackspace = (inputChar == '\b');

                // If the input character is not a digit, decimal point, or backspace, suppress it.
                if (!isDigit && !isDecimalPoint && !isBackspace)
                {
                    e.Handled = true;
                }

                // Make sure there is only one decimal point in the input.
                if (isDecimalPoint && ((sender as Guna2TextBox).Text.Contains(".") || (sender as Guna2TextBox).Text.Length == 0))
                {
                    e.Handled = true;
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            contextMenuStrip1.Enabled = dgvRoomTypes.Rows.Count > 0;
        }

        private void dgvRoomTypes_DoubleClick(object sender, EventArgs e)
        {
            frmShowRoomTypeInfo ShowRoomTypeInfo = new frmShowRoomTypeInfo(_GetRoomIDFromDGV());
            ShowRoomTypeInfo.ShowDialog();

            frmManageRoomTypes_Load(null, null);
        }

        private void ShowDetailstoolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowRoomTypeInfo ShowRoomTypeInfo = new frmShowRoomTypeInfo(_GetRoomIDFromDGV());
            ShowRoomTypeInfo.ShowDialog();

            frmManageRoomTypes_Load(null, null);
        }

        private void EdittoolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditRoomType EditRoomType = new frmEditRoomType(_GetRoomIDFromDGV());
            EditRoomType.ShowDialog();

            frmManageRoomTypes_Load(null, null);
        }
    }
}
