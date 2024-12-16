using Guna.UI2.WinForms;
using Hotel.Grobal;
using Hotel.ItemTypes;
using Hotel.Properties;
using HotelDatabase_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel.Items
{
    public partial class frmAddEditItem : Form
    {
        public Action<int?> ItemIDBack;

        enum _enMode { AddNew, Update };
        _enMode _Mode = _enMode.AddNew;

        int? _itemID = null;
        clsItem _Item = null;
        public frmAddEditItem()
        {
            InitializeComponent();

            _Mode = _enMode.AddNew;
        }

        public frmAddEditItem(int? itemID)
        {
            InitializeComponent();

            _itemID = itemID;
            _Mode = _enMode.Update;
        }

        void _FillComboBoxWithItemTypeName()
        {
            cbItemTypes.Items.Clear();
            DataTable dtItemTypesTitle = clsItemType.GetAllItemTypes();

            foreach (DataRow drTitle in dtItemTypesTitle.Rows)
            {
                cbItemTypes.Items.Add(drTitle["ItemTypeName"].ToString());
            }
        }

        void _ResetFields()
        {
            foreach (Control item in this.Controls)
            {

                if (item is Guna2TextBox txtGuna)
                    txtGuna.Clear();

                if (item is Guna2ComboBox comboGuna)
                    comboGuna.SelectedIndex = 0;
            }

            pbItemImage.Image = Resources.question_mark;
            pbItemImage.Cursor = Cursors.Default;
        }

        void _ResetDefaultValues()
        {
            _FillComboBoxWithItemTypeName();

            if (_Mode == _enMode.AddNew)
            {
                lblTitle.Text = "Add New Item";
                _Item = new clsItem();
                _ResetFields();
            }
            else
            {
                lblTitle.Text = "Update Item";
            }

            this.Text = lblTitle.Text;

            //hide/show the remove link in case there is no image for the customer
            llRemoveImage.Visible = (pbItemImage.ImageLocation != null);

            cbItemTypes.SelectedIndex = 0;
        }

        void _FillFieldsWithItemInfo()
        {
            lblItemID.Text = _Item.ItemID.ToString();
            txtItemName.Text = _Item.ItemName;
            txtItemPrice.Text = _Item.ItemPrice.ToString("C");
            txtDescription.Text = _Item.Description;

            cbItemTypes.SelectedIndex = cbItemTypes.FindString(_Item.ItemTypeInfo.ItemTypeName);
        }

        void _LoadData()
        {
            _Item = clsItem.Find(_itemID);

            if (_Item == null)
            {
                MessageBox.Show($"There is no Item with ID = {_itemID} !",
                  "Missing Item", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();
                return;
            }

            _FillFieldsWithItemInfo();

            //load Item image in case it was set.
            if (_Item.ItemImagePath != null)
            {
                pbItemImage.ImageLocation = _Item.ItemImagePath;
                pbItemImage.Cursor = Cursors.Hand;
            }
            else
            {
                pbItemImage.Image = Resources.question_mark;
                pbItemImage.Cursor = Cursors.Default;
            }

            //hide/show the remove link in case there is no image for the Item
            llRemoveImage.Visible = (_Item.ItemImagePath != null);
        }

        bool _HandleItemImage()
        {
            // this procedure will handle the item image,
            // it will take care of deleting the old image from the folder
            // in case the image changed, and it will rename the new image with guid and 
            // place it in the images folder.

            // _Item.ItemImagePath contains the old Image, we check if it changed then we copy the new image
            if (_Item.ItemImagePath != pbItemImage.ImageLocation)
            {

                if (_Item.ItemImagePath != null)
                {
                    // first we delete the old image from the folder in case there is any.
                    try
                    {
                        File.Delete(_Item.ItemImagePath);
                    }
                    catch (IOException iox)
                    {
                        clsLogger.LogError("IO Exception", iox);
                        return false;
                    }
                    catch (Exception ex)
                    {
                        clsLogger.LogError("General Exception", ex);
                        return false;
                    }
                }

                if (pbItemImage.ImageLocation != null)
                {
                    // then we copy the new image to the image folder after we rename it
                    string SourceImageFile = pbItemImage.ImageLocation.ToString();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile, clsUtil.enSourceImage.Item))
                    {
                        pbItemImage.ImageLocation = SourceImageFile;

                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            return true;
        }

        void _FillItemObjectWithFieldsData()
        {
            _Item.ItemName = txtItemName.Text.Trim();
            _Item.Description = txtDescription.Text.Trim();
            _Item.ItemTypeID = clsItemType.Find(cbItemTypes.Text).ItemTypeID;


            if (txtItemPrice.Text.Contains("$")) // to remove the $ from the beginning of price
                _Item.ItemPrice = float.Parse(txtItemPrice.Text.Trim().Substring(1));
            else
                _Item.ItemPrice = float.Parse(txtItemPrice.Text.Trim());


            if (pbItemImage.ImageLocation != null)
                _Item.ItemImagePath = pbItemImage.ImageLocation;
            else
                _Item.ItemImagePath = null;
        }

        void _SaveItem()
        {
            _FillItemObjectWithFieldsData();

            if (_Item.Save())
            {
                lblTitle.Text = "Update Item";
                this.Text = lblTitle.Text;
                lblItemID.Text = _Item.ItemID.ToString();

                // change form mode to update
                _Mode = _enMode.Update;

                MessageBox.Show("Data Saved Successfully", "Saved",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Trigger the event to send data back to the caller form
                ItemIDBack?.Invoke(_Item.ItemID);
            }
            else
            {
                MessageBox.Show("Data Saved Failed", "Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void _ShowNewItemTypeInComboBox(string NewItemTypeName)
        {
            // Refresh
            _FillComboBoxWithItemTypeName();

            cbItemTypes.SelectedIndex = cbItemTypes.FindString(NewItemTypeName);
        }

        private void frmAddEditItem_Load(object sender, EventArgs e)
        {
             _ResetDefaultValues();

            if (_Mode == _enMode.Update)
                _LoadData();
        }

        private void llAddNewItemType_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditItemType AddEditItemType = new frmAddEditItemType();
            AddEditItemType.ItemTypeNameBack += _ShowNewItemTypeInComboBox;
            AddEditItemType.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtItemName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtItemName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtItemName, "This field is required!");
                return;
            }
            else
                errorProvider1.SetError(txtItemName, null);


            if (_Item.ItemName.ToLower() != txtItemName.Text.Trim().ToLower()
                && clsItem.DoesItemExist(txtItemName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtItemName, "This item already exists!");
            }
            else
                errorProvider1.SetError(txtItemName, null);

        }

        private void txtItemPrice_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtItemPrice.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtItemPrice, "This field is required!");
            }
            else
            {
                errorProvider1.SetError(txtItemPrice, null);
            }
        }

        private void txtItemPrice_KeyPress(object sender, KeyPressEventArgs e)
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

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                pbItemImage.Load(selectedFilePath);
                llRemoveImage.Visible = true;
                pbItemImage.Cursor = Cursors.Hand;
                // ...
            }
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbItemImage.ImageLocation = null;

            pbItemImage.Image = Resources.question_mark;
            pbItemImage.Cursor = Cursors.Default;

            llRemoveImage.Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!, put the mouse over the red icon(s) to see the error",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (!_HandleItemImage())
                return;

            _SaveItem();
        }

        private void frmAddEditItem_Activated(object sender, EventArgs e)
        {
            txtItemName.Focus();
        }

        private void llUpdateCurrentItemType_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditItemType EditItemType = new frmAddEditItemType(cbItemTypes.Text.Trim());
            EditItemType.ItemTypeNameBack += _ShowNewItemTypeInComboBox;
            EditItemType.ShowDialog();
        }
    }
}
