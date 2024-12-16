using Hotel.ItemTypes;
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

namespace Hotel.Items
{
    public partial class frmManageItems : Form
    {
        DataTable _dtItem;
        public frmManageItems()
        {
            InitializeComponent();
        }

        void _FillComboBoxWithItemTypeName()
        {
            cbItemTypes.Items.Clear();
            DataTable dtItemTypesTitle = clsItemType.GetAllItemTypes();

            cbItemTypes.Items.Add("All");
            foreach (DataRow drTitle in dtItemTypesTitle.Rows)
            {
                cbItemTypes.Items.Add(drTitle["ItemTypeName"].ToString());
            }
        }

        string _GetRealColumnNameInDB()
        {
            switch (cbFilterBy.Text)
            {
                case "Item ID":
                    return "ItemID";

                case "Item Name":
                    return "ItemName";

                case "Item Type":
                    return "ItemTypeName";

                default:
                    return "None";
            }
        }

        void _RefreshItemList()
        {
            _dtItem = clsItem.GetAllItems();
            dgvItemsList.DataSource = _dtItem;

            if (dgvItemsList.Rows.Count > 0)
            {
                dgvItemsList.Columns[0].HeaderText = "Item ID";
                dgvItemsList.Columns[0].Width = 110;

                dgvItemsList.Columns[1].HeaderText = "Item Name";
                dgvItemsList.Columns[1].Width = 190;

                dgvItemsList.Columns[2].HeaderText = "Item Type";
                dgvItemsList.Columns[2].Width = 130;

                dgvItemsList.Columns[3].HeaderText = "Item Price";
                dgvItemsList.Columns[3].Width = 130;

                dgvItemsList.Columns[4].HeaderText = "Item Image Path";
                dgvItemsList.Columns[4].Width = 130;

                // Hide the last column (Item Image Path) because I don't want to show it, but I need its value
                dgvItemsList.Columns[dgvItemsList.Columns.Count - 1].Visible = false;
            }
        }

        int? _GetItemIDFromDGV()
        {
            return (int?)dgvItemsList.CurrentRow.Cells["ItemID"].Value;
        }

        string _GetItemTypeIDByName(string ItemTypeName)
        {
            return clsItemType.Find(ItemTypeName).ItemTypeName;
        }

        private void frmManageItems_Load(object sender, EventArgs e)
        {
            _RefreshItemList();
            _FillComboBoxWithItemTypeName();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterBy.Visible = (cbFilterBy.Text != "None") && (cbFilterBy.Text != "Item Type");
            cbItemTypes.Visible = (cbFilterBy.Text == "Item Type");

            if (txtFilterBy.Visible)
            {
                txtFilterBy.Text = "";
                txtFilterBy.Focus();
            }

            if (cbItemTypes.Visible)
                cbItemTypes.SelectedIndex = 0;
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            if (_dtItem.Rows.Count == 0)
                return;


            string ColumnName = _GetRealColumnNameInDB();

            if (string.IsNullOrWhiteSpace(txtFilterBy.Text.Trim()) || cbFilterBy.Text == "None")
            {
                _dtItem.DefaultView.RowFilter = "";
                return;
            }


            if (cbFilterBy.Text == "Item ID")
                // search with numbers
                _dtItem.DefaultView.RowFilter = string.Format("[{0}] = {1}", ColumnName, txtFilterBy.Text.Trim());
            else
                // search with string
                _dtItem.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", ColumnName, txtFilterBy.Text.Trim());

        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Item ID")
                // make sure that the user can only enter the numbers
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbItemTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_dtItem.Rows.Count == 0)
                return;

            if (cbItemTypes.Text == "All")
            {
                _dtItem.DefaultView.RowFilter = "";
                return;
            }

            _dtItem.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", "ItemTypeName", cbItemTypes.Text);

        }

        private void cmsShowItemDetails_Click(object sender, EventArgs e)
        {
            frmShowItemInfo frm = new frmShowItemInfo(_GetItemIDFromDGV());
            frm.ShowDialog();

            _RefreshItemList();
        }
        private void btnAddNewItem_Click(object sender, EventArgs e)
        {
            frmAddEditItem frm = new frmAddEditItem();
            frm.ShowDialog();

            _RefreshItemList();
        }

        private void cmsAddNewItem_Click(object sender, EventArgs e)
        {
            frmAddEditItem frm = new frmAddEditItem();
            frm.ShowDialog();

            _RefreshItemList();
        }

        private void cmsEditItem_Click(object sender, EventArgs e)
        {
            frmAddEditItem frm = new frmAddEditItem(_GetItemIDFromDGV());
            frm.ShowDialog();

            _RefreshItemList();
        }

        private void cmsAddNewItemType_Click(object sender, EventArgs e)
        {
            frmAddEditItemType frm = new frmAddEditItemType();
            frm.ShowDialog();
        }

        private void cmsEditItemType_Click(object sender, EventArgs e)
        {
            string ItemTypeName = (string)dgvItemsList.CurrentRow.Cells["ItemTypeName"].Value;

            frmAddEditItemType frm = new frmAddEditItemType(ItemTypeName);
            frm.ShowDialog();
        }

        private void dgvItemsList_DoubleClick(object sender, EventArgs e)
        {
            frmShowItemInfo frm = new frmShowItemInfo(_GetItemIDFromDGV());
            frm.ShowDialog();

            _RefreshItemList();
        }

        private void cmsDeleteItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this item?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                == DialogResult.No)
                return;

            if (clsItem.DeleteItem(_GetItemIDFromDGV()))
            {
                MessageBox.Show("Item Deleted Successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                _RefreshItemList();
            }
            else
            {
                MessageBox.Show("The item cannot be deleted because it is linked to other tables!",
                    "Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
