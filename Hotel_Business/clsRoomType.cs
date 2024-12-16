using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using HotelDatabase_DataAccess;

namespace HotelDatabase_Buisness
{
    public class clsRoomType
    {

        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _mode = enMode.AddNew;

        public int? RoomTypeID { get; private set; }
        public string RoomTypeTitle { get; set; }
        public byte Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public string Description { get; set; }

        public static readonly Dictionary<clsRoom.enRoomTypes, byte> RoomTypeCapacities = new Dictionary<clsRoom.enRoomTypes, byte>()
        {
            { clsRoom.enRoomTypes.Single, 1 },
            { clsRoom.enRoomTypes.Double, 2 },
            { clsRoom.enRoomTypes.DeluxeSuite, 4 },
            { clsRoom.enRoomTypes.FamilyRoom, 6 }
        };
        public clsRoomType()
        {
            RoomTypeID = null;
            RoomTypeTitle = string.Empty;
            Capacity = default;
            PricePerNight = default;
            Description = null;

            _mode = enMode.AddNew;
        }

        private clsRoomType(int? RoomTypeID, string RoomTypeTitle, byte Capacity, decimal PricePerNight, string Description)
        {
            this.RoomTypeID = RoomTypeID;
            this.RoomTypeTitle = RoomTypeTitle;
            this.Capacity = Capacity;
            this.PricePerNight = PricePerNight;
            this.Description = Description;

            _mode = enMode.Update;
        }

        public static clsRoomType Find(int? RoomTypeID)
        {
            string RoomTypeTitle = default;
            byte Capacity = default;
            decimal PricePerNight = default;
            string Description = default;

            bool isFound = clsRoomTypeData.GetRoomTypeInfoByID(RoomTypeID, ref RoomTypeTitle, ref Capacity, ref PricePerNight, ref Description);

            if (isFound)
                return new clsRoomType(RoomTypeID, RoomTypeTitle, Capacity, PricePerNight, Description);
            else
                return null;
        }
        public static clsRoomType Find(string RoomTypeTitle)
        {
            int? RoomTypeID = null;
            byte Capacity = default;
            decimal PricePerNight = default;
            string Description = default;

            bool isFound = clsRoomTypeData.GetRoomTypeInfoByTitle(RoomTypeTitle, ref RoomTypeID, ref Capacity, ref PricePerNight, ref Description);

            if (isFound)
                return new clsRoomType(RoomTypeID, RoomTypeTitle, Capacity, PricePerNight, Description);
            else
                return null;
        }
        public static bool DoesRoomTypeExist(int? RoomTypeID)
        {
            return clsRoomTypeData.DoesRoomTypeExist(RoomTypeID);
        }

        private bool _AddNewRoomType()
        {
            RoomTypeID = clsRoomTypeData.AddNewRoomType(RoomTypeTitle, Capacity, PricePerNight, Description);
            return RoomTypeID.HasValue;
        }

        private bool _UpdateRoomType()
        {
            return clsRoomTypeData.UpdateRoomTypeInfo(RoomTypeID, RoomTypeTitle, Capacity, PricePerNight, Description);
        }

        public bool Save()
        {
            switch (_mode)
            {
                case enMode.AddNew:
                    if (_AddNewRoomType())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateRoomType();

            }
            return false;
        }

        public static bool DeleteRoomType(int? RoomTypeID)
        {
            return clsRoomTypeData.DeleteRoomType(RoomTypeID);
        }

        public static DataTable GetAllRoomTypes()
        {
            return clsRoomTypeData.GetAllRoomTypes();
        }

        public static DataTable GetAllRoomTypesTitle()
        {
            return clsRoomTypeData.GetAllRoomTypesTitle();
        }

        public static int GetRoomsCountByRoomTypeID(int? RoomTypeID)
        {
            return clsRoomData.GetRoomsCountByRoomTypeID(RoomTypeID);
        }
        
        public static int? GetRoomTypeIDByTitle(string RoomTypeTitle)
        {
            switch (RoomTypeTitle)
            {
                case "Single":
                    return 1;

                case "Double":
                    return 2;

                case "Deluxe Suite":
                    return 3;

                case "Family Room":
                    return 4;

                default:
                    return null;
            }
        }

    }
}