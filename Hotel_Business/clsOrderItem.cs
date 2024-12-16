using System;
using System.Data;
using System.Text;
using HotelDatabase_DataAccess;

namespace HotelDatabase_Buisness
{
    public class clsOrderItem
    {

        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _mode = enMode.AddNew;

        public int? OrderItemsID { get; private set; }
        public int? OrderID { get; set; }
        public int? ItemID { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerItem { get; set; }
        public decimal? TotalItemPrice { get; set; }

        public clsOrderItem()
        {
            OrderItemsID = null;
            OrderID = null;
            ItemID = null;
            Quantity = -1;
            PricePerItem = -1M;
            TotalItemPrice = -1M;

            _mode = enMode.AddNew;
        }

        private clsOrderItem(int? OrderItemsID, int? OrderID, int? ItemID, int Quantity, decimal PricePerItem, decimal? TotalItemPrice)
        {
            this.OrderItemsID = OrderItemsID;
            this.OrderID = OrderID;
            this.ItemID = ItemID;
            this.Quantity = Quantity;
            this.PricePerItem = PricePerItem;
            this.TotalItemPrice = TotalItemPrice;

            _mode = enMode.Update;
        }

        public static clsOrderItem Find(int? OrderItemsID)
        {
            int? OrderID = default;
            int? ItemID = default;
            int Quantity = -1;
            decimal PricePerItem = -1M;
            decimal? TotalItemPrice = -1M;

            bool isFound = clsOrderItemData.GetOrderItemInfoByID(OrderItemsID, ref OrderID, ref ItemID, ref Quantity, ref PricePerItem, ref TotalItemPrice);

            if (isFound)
                return new clsOrderItem(OrderItemsID, OrderID, ItemID, Quantity, PricePerItem, TotalItemPrice);
            else
                return null;
        }

        public static bool DoesOrderItemExist(int? OrderItemsID)
        {
            return clsOrderItemData.DoesOrderItemExist(OrderItemsID);
        }

        private bool _AddNewOrderItem()
        {
            OrderItemsID = clsOrderItemData.AddNewOrderItem(OrderID, ItemID, Quantity, PricePerItem);
            return OrderItemsID.HasValue;
        }

        private bool _UpdateOrderItem()
        {
            return clsOrderItemData.UpdateOrderItemInfo(OrderItemsID, OrderID, ItemID, Quantity, PricePerItem);
        }

        public bool Save()
        {
            switch (_mode)
            {
                case enMode.AddNew:
                    if (_AddNewOrderItem())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateOrderItem();

            }
            return false;
        }

        public static bool DeleteOrderItem(int? OrderItemsID)
        {
            return clsOrderItemData.DeleteOrderItem(OrderItemsID);
        }

        public static DataTable GetAllOrderItems()
        {
            return clsOrderItemData.GetAllOrderItems();
        }

        public static DataTable GetAllOrderItemsByOrderID(int? OrderID)
        {
            return clsOrderItemData.GetAllOrderItemsByOrderID(OrderID);
        }


    }
}