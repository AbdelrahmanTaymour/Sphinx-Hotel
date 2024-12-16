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

namespace Hotel.Items.Controls
{
    public partial class ucItemInfoCard : UserControl
    {
        int? _ItemID = null;
        clsItem _Item = null;

        public int? ItemID => _ItemID;
        public clsItem ItemInfo => _Item;

        bool _EnableUpdateInfo = true;
        public bool EnableUpdateInfo
        {
            get => _EnableUpdateInfo;
            set => _EnableUpdateInfo = llEditItemInfo.Visible = value;
        }

        public ucItemInfoCard()
        {
            InitializeComponent();
        }

        void _LoadItemImage()
        {
            if (_Item.ItemImagePath != null)
                if (File.Exists(_Item.ItemImagePath))
                {
                    pbItemImage.ImageLocation = _Item.ItemImagePath;
                    pbItemImage.Cursor = Cursors.Hand;
                }
                else
                {
                    MessageBox.Show("Could not find this image: = " +
                        _Item.ItemImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pbItemImage.Cursor = Cursors.Default;
                }

            else
            {
                pbItemImage.Image = Resources.question_mark;
                pbItemImage.Cursor = Cursors.Default;
            }
        }

        void _FillItemData()
        {
            lblItemID.Text = _Item.ItemID.ToString();
            lblItemName.Text = _Item.ItemName;
            lblItemType.Text = _Item.ItemTypeInfo.ItemTypeName;
            lblPrice.Text = _Item.ItemPrice.ToString("C");
            lblDescription.Text = _Item.Description ?? "N/A";

            _LoadItemImage();

            llEditItemInfo.Enabled = true;
        }
        void Reset()
        {
            _ItemID = null;
            _Item = null;

            lblItemID.Text = "[????]";
            lblItemName.Text = "[????]";
            lblItemType.Text = "[????]";
            lblPrice.Text = "[????]";
            lblDescription.Text = "[????]";

            llEditItemInfo.Enabled = false;
        }

        public void Clear()
        {
            Reset();
        }

        public void LoadItemInfo(int? ItemID)
        {
            _ItemID = ItemID;

            if (!_ItemID.HasValue)
            {
                MessageBox.Show("There is no Item!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                Reset();

                return;
            }

            _Item = clsItem.Find(_ItemID);

            if (_Item == null)
            {
                MessageBox.Show($"There is no Item with ID = {_ItemID} !",
                    "Missing Item", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Reset();

                return;
            }

            _FillItemData();
        }

        public void DisableUpdateInfo()
        {
            llEditItemInfo.Enabled = false;
        }

        private void llEditItemInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditItem EditItem = new frmAddEditItem(_ItemID);
            EditItem.ShowDialog();

            // Refresh
            LoadItemInfo(_ItemID);
        }
    }
}
