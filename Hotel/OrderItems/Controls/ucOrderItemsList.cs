﻿using Hotel.Items;
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

namespace Hotel.OrderItems.Controls
{
    public partial class ucOrderItemsList : UserControl
    {
        int? _OrderID = null;
        public ucOrderItemsList()
        {
            InitializeComponent();
        }

        void _RefreshOrderItemsList()
        {
            dgvOrderItemsList.DataSource = clsOrderItem.GetAllOrderItemsByOrderID(_OrderID);

            if (dgvOrderItemsList.Rows.Count > 0)
            {
                dgvOrderItemsList.Columns[0].HeaderText = "Order Item ID";
                dgvOrderItemsList.Columns[0].Width = 120;

                dgvOrderItemsList.Columns[1].HeaderText = "Order ID";
                dgvOrderItemsList.Columns[1].Width = 120;

                dgvOrderItemsList.Columns[2].HeaderText = "Item ID";
                dgvOrderItemsList.Columns[2].Width = 120;

                dgvOrderItemsList.Columns[3].HeaderText = "Item Name";
                dgvOrderItemsList.Columns[3].Width = 180;

                dgvOrderItemsList.Columns[4].HeaderText = "Quantity";
                dgvOrderItemsList.Columns[4].Width = 120;

                dgvOrderItemsList.Columns[5].HeaderText = "Price";
                dgvOrderItemsList.Columns[5].Width = 160;
            }
        }

        public void LoadOrderItemsInfo(int? OrderID)
        {
            _OrderID = OrderID;

            _RefreshOrderItemsList();
        }

        private void dgvOrderItemsList_DoubleClick(object sender, EventArgs e)
        {
            frmShowItemInfo ShowItemInfo = new frmShowItemInfo((int)dgvOrderItemsList.CurrentRow.Cells["ItemID"].Value, false);
            ShowItemInfo.ShowDialog();
        }

        private void ShowItemInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowItemInfo ShowItemInfo = new frmShowItemInfo((int)dgvOrderItemsList.CurrentRow.Cells["ItemID"].Value, false);
            ShowItemInfo.ShowDialog();
        }

        private void cmsEditProfile_Opening(object sender, CancelEventArgs e)
        {
            cmsEditProfile.Enabled = (dgvOrderItemsList.RowCount > 0);
        }
    }
}
