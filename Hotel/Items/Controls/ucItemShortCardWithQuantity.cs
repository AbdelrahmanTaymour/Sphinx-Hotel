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
    public partial class ucItemShortCardWithQuantity : UserControl
    {
        private string _itemImagePath;
        private string _itemName = "Item Name";
        private float _itemPrice;
        private int? _itemID;
        private clsItem _item;

        public string ItemImagePath
        {
            get => _itemImagePath;
            set
            {
                _itemImagePath = value;
                _LoadItemImage();
            }
        }

        public string ItemName
        {
            get => _itemName;
            set
            {
                _itemName = value;
                lblItemName.Text = value;
            }
        }

        public float ItemPrice
        {
            get => _itemPrice;
            set
            {
                _itemPrice = value;
                lblItemPrice.Text = _itemPrice.ToString("C");
            }
        }

        public float TotalItemPrice => _itemPrice * (float)numaricQuantity.Value;

        public short ItemQuantity
        {
            get => (short)numaricQuantity.Value;
            set => numaricQuantity.Value = value;
        }

        public int? ItemID
        {
            get => _itemID;
            set => _itemID = value;
        }

        public ucItemShortCardWithQuantity()
        {
            InitializeComponent();
        }

        private bool _DoesItemExist()
        {
            if (!_itemID.HasValue)
            {
                _ShowError("There is no Item!");
                ResetFields();
                return false;
            }

            _item = clsItem.Find(_itemID);
            if (_item == null)
            {
                _ShowError($"There is no Item with ID = {_itemID} !");
                ResetFields();
                return false;
            }

            return true;
        }

        private void _LoadItemImage()
        {
            if (!string.IsNullOrEmpty(_itemImagePath) && File.Exists(_itemImagePath))
            {
                pbItemImage.ImageLocation = _itemImagePath;
                pbItemImage.Cursor = Cursors.Hand;
            }
            else
            {
                pbItemImage.Image = Resources.question_mark;
                pbItemImage.Cursor = Cursors.Default;

                if (string.IsNullOrEmpty(_itemImagePath))
                    return;

                _ShowError($"Could not find this image: {_itemImagePath}");
            }
        }

        private void _FillItemData()
        {
            ItemName = _item.ItemName;
            ItemPrice = _item.ItemPrice;
            _LoadItemImage();
        }

        public void DisableChangeQuantity()
        {
            numaricQuantity.Enabled = false;
        }

        public void LoadItemInfo(int? itemID)
        {
            ItemID = itemID;

            if (!_DoesItemExist())
                return;

            _FillItemData();
        }

        public void ResetFields()
        {
            ItemImagePath = null;
            ItemName = "[????]";
            ItemPrice = 0.00f;

            pbItemImage.Image = Resources.question_mark;
        }

        private void _ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
