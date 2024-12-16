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

namespace Hotel.ItemTypes
{
    public partial class frmAddEditItemType : Form
    {
        public Action<int?> ItemTypeIDBack;
        public Action<string> ItemTypeNameBack;

        enum _enMode { AddNew, Update };
        _enMode _Mode = _enMode.AddNew;

        int? _ItemTypeID = null;
        string _ItemTypeName = null;
        clsItemType _ItemType = null;

        public frmAddEditItemType()
        {
            InitializeComponent();
            _Mode = _enMode.AddNew;
        }

        public frmAddEditItemType(string ItemTypeName)
        {
            InitializeComponent();
            _ItemTypeName = ItemTypeName;
            _Mode = _enMode.Update;
        }

        void _ResetDefaultValues()
        {
            if (_Mode == _enMode.AddNew)
            {
                lblTitle.Text = "Add New Item Type";
                _ItemType = new clsItemType();
                txtItemTypeName.Clear();
            }
            else
            {
                lblTitle.Text = "Update Item Type";
            }

            this.Text = lblTitle.Text;
        }
        void _FillFieldsWithItemInfo()
        {
            lblItemTypeID.Text = _ItemType.ItemTypeID.ToString();
            txtItemTypeName.Text = _ItemType.ItemTypeName;
        }

        void _LoadData()
        {
            if (_ItemTypeName != null)
                _ItemType = clsItemType.Find(_ItemTypeName);
            else
                _ItemType = clsItemType.Find(_ItemTypeID);

            if (_ItemType == null)
            {
                MessageBox.Show($"There is no Item Type with ID = {_ItemTypeID} !",
                  "Missing Item Type", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();
                return;
            }

            _FillFieldsWithItemInfo();
        }

        void _SaveItemType()
        {
            _ItemType.ItemTypeName = txtItemTypeName.Text.Trim();

            if (_ItemType.Save())
            {
                lblTitle.Text = "Update Item Type";
                this.Text = lblTitle.Text;
                lblItemTypeID.Text = _ItemType.ItemTypeID.ToString();

                // change form mode to update
                _Mode = _enMode.Update;

                MessageBox.Show("Data Saved Successfully", "Saved",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Trigger the event to send data back to the caller form
                ItemTypeIDBack?.Invoke(_ItemType.ItemTypeID);
                ItemTypeNameBack?.Invoke(_ItemType.ItemTypeName);
            }
            else
            {
                MessageBox.Show("Data Saved Failed", "Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddEditItemType_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_Mode == _enMode.Update)
                _LoadData();
        }

        private void txtItemTypeName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtItemTypeName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtItemTypeName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtItemTypeName, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtItemTypeName, null);
            }

            if (_ItemType.ItemTypeName.ToLower() != txtItemTypeName.Text.Trim().ToLower() &&
                clsItemType.DoesItemTypeExist(txtItemTypeName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtItemTypeName, "This item type already exists!");
            }
            else
            {
                errorProvider1.SetError(txtItemTypeName, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!, put the mouse over the red icon(s) to see the error",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _SaveItemType();
        }
    }
}
