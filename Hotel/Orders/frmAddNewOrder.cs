using Hotel.Global;
using Hotel.Items;
using Hotel.Items.Controls;
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
    public partial class frmAddNewOrder : Form
    {
        clsOrder _Order = new clsOrder();
        int? _ItemID = null;

        List<ucItemShortCardWithQuantity> _SelectedItemCards =
            new List<ucItemShortCardWithQuantity>();

        Dictionary<int?, ucItemShortCardWithQuantity> _AllItemCards =
            new Dictionary<int?, ucItemShortCardWithQuantity>();

        public frmAddNewOrder()
        {
            InitializeComponent();
        }
        void _StoreAllItemsInDictionary()
        {
            _AllItemCards.Clear();

            DataTable dtItems = clsItem.GetAllItems();
            foreach(DataRow Item in dtItems.Rows)
            {
                ucItemShortCardWithQuantity ItemCard = new ucItemShortCardWithQuantity();
                ItemCard.ItemID = (int?)Item["ItemID"];
                ItemCard.ItemName = (string)Item["ItemName"];
                ItemCard.ItemPrice = Convert.ToSingle(Item["ItemPrice"]);
                ItemCard.ItemImagePath = (Item["ItemImagePath"] != DBNull.Value) ? (string)Item["ItemImagePath"] : null;

                _AllItemCards.Add(ItemCard.ItemID, ItemCard);
            }
        }
        void _RefreshItemList()
        {
            dgvList.DataSource = clsItem.GetAllItems();

            if (dgvList.Rows.Count > 0)
            {
                dgvList.Columns[0].HeaderText = "Item ID";
                dgvList.Columns[0].Width = 184;

                dgvList.Columns[1].HeaderText = "Item Name";
                dgvList.Columns[1].Width = 220;

                dgvList.Columns[2].HeaderText = "Item Type";
                dgvList.Columns[2].Width = 220;

                dgvList.Columns[3].HeaderText = "Item Price";
                dgvList.Columns[3].Width = 220;
            }
        }
        void _ShowDiningList()
        {
            _StoreAllItemsInDictionary();
            _RefreshItemList();

            flowLayoutPanel1.Controls.Clear();
            _SelectedItemCards.Clear();
            //lblTitleOfList.Text = "Dining Menu Items:";
            btnSelectedItem.Visible = true;
            lblSelectedItemsText.Visible = true;
            flowLayoutPanel1.Visible = true;
            cmsShowItemDetails.Visible = true;
        }
        void _Reset()
        {
            btnOrder.Enabled = false;
            btnSelectedItem.Enabled = false;
            ucBookingAndReservationCardWithFilter1.FilterEnabled = false;
        }

        void _SaveOrder()
        {
            if (_Order == null)
                return;

            if(MessageBox.Show($"Total Item Price = {_CalculateTotalItemPrice()}\r\n" +
                $"Are you sure you want to continue?", 
                "Confirm",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (!_Order.Save())
                {
                    MessageBox.Show("Order Saved Failed!", "Failed",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                _Reset();
            }
            
        }

        float _CalculateTotalItemPrice()
        {
            return _SelectedItemCards.Sum(Item => Item.TotalItemPrice);
        }

        void _SaveOrderForDining()
        {
            _Order.OrderType = clsOrder.enOrderType.Dining;
            _Order.Fees = (decimal)_CalculateTotalItemPrice();
            _Order.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            _SaveOrder();
            _SaveItemsInOrderItemsTable_AndDisableChangeQuantityInItemCard();
        }

        void _SaveItemsInOrderItemsTable_AndDisableChangeQuantityInItemCard()
        {
            _SelectedItemCards.ForEach(Item =>
            {
                clsOrderItem OrderItem = new clsOrderItem();
                OrderItem.OrderID = _Order.OrderID;
                OrderItem.ItemID = Item.ItemID;
                OrderItem.Quantity = Item.ItemQuantity;
                OrderItem.PricePerItem = (decimal)Item.ItemPrice;
                OrderItem.Save();

                Item.DisableChangeQuantity();
            });
        }

        bool _IsItemAlreadySelected()
        {
            if(_SelectedItemCards.Count>0)
            {
                ucItemShortCardWithQuantity ItemFound = _SelectedItemCards.Find(Item => Item.ItemID == _ItemID);

                if(ItemFound != null)
                {
                    // The item has already been selected, so I just update the quantity
                    ItemFound.ItemQuantity++;
                    return true;
                }
            }
            return false;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tcAddNewOrder.SelectedTab = tcAddNewOrder.TabPages["tpOrderInfo"];
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucBookingAndReservationCardWithFilter1_OnBookingAndReservationSelected(object sender, Bookings.Controls.ucBookingAndReservationCardWithFilter.BookingAndReservationSelectedEventArgs e)
        {
            if(!e.BookingID.HasValue || !e.ReservationID.HasValue)
            {
                btnNext.Enabled = false;
                btnOrder.Enabled = false;
                return;
            }

            if(clsBooking.IsBookingCheckedOut(e.BookingID))
            {
                MessageBox.Show("This Booking has been completed, so you cannot request an order for it",
                   "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnNext.Enabled = false;
                btnOrder.Enabled = false;
                return;
            }

            _Order.BookingID = e.BookingID;
            _Order.RoomID = ucBookingAndReservationCardWithFilter1.ReservationInfo.RoomID;

            btnNext.Enabled = true;
            btnOrder.Enabled = true;
        }

        private void dgvList_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvList.SelectedRows.Count > 0)
            {
                _ItemID = (int?)dgvList.SelectedRows[0].Cells[0].Value;
            }
        }

        private void btnSelectedItem_Click(object sender, EventArgs e)
        {
            if (_ItemID == null)
                return;

            if(_AllItemCards.TryGetValue(_ItemID, out ucItemShortCardWithQuantity ItemCard))
            {
                if (_IsItemAlreadySelected())
                    return;

                flowLayoutPanel1.Controls.Add(ItemCard);
                _SelectedItemCards.Add(ItemCard);
            }
        }

        private void frmAddNewOrder_Activated(object sender, EventArgs e)
        {
            ucBookingAndReservationCardWithFilter1.FilterFocus();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if(_SelectedItemCards.Count == 0)
            {
                MessageBox.Show("You have to select one element at least!", "Not Allowed",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _SaveOrderForDining();
            MessageBox.Show("The Order is completed Successfully",
                   "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void cmsShowItemDetails_Click(object sender, EventArgs e)
        {

            frmShowItemInfo ShowItemInfo = new frmShowItemInfo(_ItemID, false);
            ShowItemInfo.ShowDialog();
        }

        private void frmAddNewOrder_Load(object sender, EventArgs e)
        {
            _ShowDiningList();
        }

        private void ucBookingAndReservationCardWithFilter1_Load(object sender, EventArgs e)
        {

        }

    }
}
