using System;
using System.Data;
using System.Text;
using HotelDatabase_DataAccess;

namespace HotelDatabase_Buisness
{
    public class clsItem
    {

        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _mode = enMode.AddNew;

        public int? ItemID { get; private set; }
        public int? ItemTypeID { get; set; }
        public string ItemName { get; set; }
        public float ItemPrice { get; set; }
        public string Description { get; set; }
        public string ItemImagePath { get; set; }

        clsItemType _itemTypeInfo;
        public clsItemType ItemTypeInfo
        {
            get
            {
                if (_itemTypeInfo == null && ItemTypeID.HasValue)
                    _itemTypeInfo = clsItemType.Find(ItemTypeID);
                return _itemTypeInfo;
            }
        }

        public clsItem()
        {
            ItemID = null;
            ItemTypeID = null;
            ItemName = string.Empty;
            ItemPrice = 0f;
            Description = null;
            ItemImagePath = null;

            _mode = enMode.AddNew;
        }

        private clsItem(int? ItemID, int? ItemTypeID, string ItemName, float ItemPrice, string Description, string ItemImagePath)
        {
            this.ItemID = ItemID;
            this.ItemTypeID = ItemTypeID;
            this.ItemName = ItemName;
            this.ItemPrice = ItemPrice;
            this.Description = Description;
            this.ItemImagePath = ItemImagePath;

            _mode = enMode.Update;
        }

        public static clsItem Find(int? ItemID)
        {
            int? ItemTypeID = null;
            string ItemName = string.Empty;
            float ItemPrice = 0f;
            string Description = null;
            string ItemImagePath = null;

            bool isFound = clsItemData.GetItemInfoByID(ItemID, ref ItemTypeID, ref ItemName, ref ItemPrice, ref Description, ref ItemImagePath);

            if (isFound)
                return new clsItem(ItemID, ItemTypeID, ItemName, ItemPrice, Description, ItemImagePath);
            else
                return null;
        }

        public static bool DoesItemExist(string ItemName)
        {
            return clsItemData.DoesItemExist(ItemName);
        }

        private bool _AddNewItem()
        {
            ItemID = clsItemData.AddNewItem(ItemTypeID, ItemName, ItemPrice, Description, ItemImagePath);
            return ItemID.HasValue;
        }

        private bool _UpdateItem()
        {
            return clsItemData.UpdateItemInfo(ItemID, ItemTypeID, ItemName, ItemPrice, Description, ItemImagePath);
        }

        public bool Save()
        {
            switch (_mode)
            {
                case enMode.AddNew:
                    if (_AddNewItem())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateItem();

            }
            return false;
        }

        public static bool DeleteItem(int? ItemID)
        {
            return clsItemData.DeleteItem(ItemID);
        }

        public static DataTable GetAllItems()
        {
            return clsItemData.GetAllItems();
        }


        
    }
}