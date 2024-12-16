using System;
using System.Data;
using System.Text;
using HotelDatabase_DataAccess;

namespace HotelDatabase_Buisness
{
    public class clsItemType
    {

        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _mode = enMode.AddNew;

        public int? ItemTypeID { get; private set; }
        public string ItemTypeName { get; set; }

        public clsItemType()
        {
            ItemTypeID = null;
            ItemTypeName = string.Empty;

            _mode = enMode.AddNew;
        }

        private clsItemType(int? ItemTypeID, string ItemTypeName)
        {
            this.ItemTypeID = ItemTypeID;
            this.ItemTypeName = ItemTypeName;

            _mode = enMode.Update;
        }

        public static clsItemType Find(int? ItemTypeID)
        {
            string ItemTypeName = string.Empty;

            bool isFound = clsItemTypeData.GetItemTypeInfoByID(ItemTypeID, ref ItemTypeName);

            if (isFound)
                return new clsItemType(ItemTypeID, ItemTypeName);
            else
                return null;
        }

        public static clsItemType Find(string ItemTypeName)
        {
            int? ItemTypeID = null;

            bool isFound = clsItemTypeData.GetItemTypeInfoByName(ItemTypeName, ref ItemTypeID);

            if (isFound)
                return new clsItemType(ItemTypeID, ItemTypeName);
            else
                return null;
        }

        public static bool DoesItemTypeExist(string ItemTypeName)
        {
            return clsItemTypeData.DoesItemTypeExist(ItemTypeName);
        }

        private bool _AddNewItemType()
        {
            ItemTypeID = clsItemTypeData.AddNewItemType(ItemTypeName);
            return ItemTypeID.HasValue;
        }

        private bool _UpdateItemType()
        {
            return clsItemTypeData.UpdateItemTypeInfo(ItemTypeID, ItemTypeName);
        }

        public bool Save()
        {
            switch (_mode)
            {
                case enMode.AddNew:
                    if (_AddNewItemType())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateItemType();

            }
            return false;
        }

        public static bool DeleteItemType(int? ItemTypeID)
        {
            return clsItemTypeData.DeleteItemType(ItemTypeID);
        }

        public static DataTable GetAllItemTypes()
        {
            return clsItemTypeData.GetAllItemTypes();
        }


        
    }
}