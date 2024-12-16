using HotelDatabase_DataAccess;
using System;
using System.Data;

namespace HotelDatabase_Buisness
{
    public class clsRoom
    {

        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _mode = enMode.AddNew;

        public enum enRoomStatus
        {
            Available = 0,
            Booked = 1,
            UnderMaintenance = 2
        }

        public enum enRoomTypes
        {
            Single = 1,
            Double = 2,
            DeluxeSuite = 3,
            FamilyRoom = 4
        }

        public int? RoomID { get; private set; }
        public enRoomTypes RoomTypeID { get; set; }
        public int RoomNumber { get; set; }
        public int FloorNumber { get; set; }
        public decimal Size { get; set; }
        public enRoomStatus Status { get; set; }
        public bool IsSmokingAllowed { get; set; }
        public bool IsPetFriendly { get; set; }
        public string RoomPhone { get; set; }
        public string RoomTypeName => _RoomTypeName(this.RoomTypeID);
        public string RoomStatusName => _RoomStatusName(this.Status);



        clsRoomType _roomType;
        public clsRoomType RoomTypeInfo
        {
            get
            {
                if (_roomType == null)
                {
                    _roomType = clsRoomType.Find((byte?)RoomTypeID);
                }
                return _roomType;
            }
        }

        public clsRoom()
        {
            RoomID = null;
            RoomTypeID = enRoomTypes.Single;
            RoomNumber = default;
            FloorNumber = default;
            Size = default;
            Status = enRoomStatus.Available;
            IsSmokingAllowed = default;
            IsPetFriendly = default;
            RoomPhone = string.Empty;

            _mode = enMode.AddNew;
        }
        private clsRoom(int? RoomID, enRoomTypes RoomTypeID, int RoomNumber, int FloorNumber, decimal Size, enRoomStatus Status, bool IsSmokingAllowed, bool IsPetFriendly, string RoomPhone)
        {
            this.RoomID = RoomID;
            this.RoomTypeID = RoomTypeID;
            this.RoomNumber = RoomNumber;
            this.FloorNumber = FloorNumber;
            this.Size = Size;
            this.Status = Status;
            this.IsSmokingAllowed = IsSmokingAllowed;
            this.IsPetFriendly = IsPetFriendly;
            this.RoomPhone = RoomPhone;

            _mode = enMode.Update;
        }

        string _RoomTypeName(enRoomTypes RoomType)
        {
            switch (RoomType)
            {
                case enRoomTypes.Single:
                    return "Single";

                case enRoomTypes.Double:
                    return "Double";

                case enRoomTypes.DeluxeSuite:
                    return "Deluxe Suite";

                case enRoomTypes.FamilyRoom:
                    return "Family Room";

                default:
                    return "Unknown";
            }

        }
        string _RoomStatusName(enRoomStatus RoomStatus)
        {
            switch (RoomStatus)
            {
                case enRoomStatus.Available:
                    return "Available";

                case enRoomStatus.Booked:
                    return "Booked";

                case enRoomStatus.UnderMaintenance:
                    return "Under Maintenance";

                default:
                    return "Unknown";
            }
        }
        bool _AddNewRoom()
        {
            RoomID = clsRoomData.AddNewRoom((int?)RoomTypeID, RoomNumber, FloorNumber, Size, (byte)Status, IsSmokingAllowed, IsPetFriendly, RoomPhone);
            return RoomID.HasValue;
        }
        bool _UpdateRoom()
        {
            return clsRoomData.UpdateRoomInfo(RoomID, (int?)RoomTypeID, RoomNumber, FloorNumber, Size, (byte)Status, IsSmokingAllowed, IsPetFriendly, RoomPhone);
        }
        public bool Save()
        {
            switch (_mode)
            {
                case enMode.AddNew:
                    if (_AddNewRoom())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateRoom();

            }
            return false;
        }

        public static clsRoom Find(int? RoomID)
        {
            int? RoomTypeID = null;
            int RoomNumber = default;
            int FloorNumber = default;
            decimal Size = default;
            byte Status = default;
            bool IsSmokingAllowed = default;
            bool IsPetFriendly = default;
            string RoomPhone = null;

            bool isFound = clsRoomData.GetRoomInfoByID(RoomID, ref RoomTypeID, ref RoomNumber, ref FloorNumber, ref Size, ref Status, ref IsSmokingAllowed, ref IsPetFriendly, ref RoomPhone);

            if (isFound)
                return new clsRoom(RoomID, (enRoomTypes)RoomTypeID, RoomNumber, FloorNumber, Size, (enRoomStatus)Status, IsSmokingAllowed, IsPetFriendly, RoomPhone);
            else
                return null;
        }
        public static clsRoom FindByRoomNumber(int RoomNumber)
        {
            int? RoomID = null;
            byte? RoomTypeID = null;
            int FloorNumber = 0;
            decimal Size = -1M;
            byte Status = 0;
            bool IsSmokingAllowed = false;
            bool IsPetFriendly = false;
            string RoomPhone = string.Empty;

            bool IsFound = clsRoomData.GetRoomInfoByRoomNumber(RoomNumber, ref RoomID, ref RoomTypeID,
                ref FloorNumber, ref Size, ref Status, ref IsSmokingAllowed,
                 ref IsPetFriendly, ref RoomPhone);

            return IsFound
                ? new clsRoom(RoomID, (enRoomTypes)RoomTypeID, RoomNumber, FloorNumber, Size,
                    (enRoomStatus)Status, IsSmokingAllowed, IsPetFriendly, RoomPhone)
                : null;
        }

        public static bool DoesRoomExist(int? RoomID)
        {
            return clsRoomData.DoesRoomExist(RoomID);
        }

        public static bool IsRoomNumberExists(int RoomNumber)
        {
            return clsRoomData.DoesRoomnNumberExist(RoomNumber);
        }

        public static bool DeleteRoom(int? RoomID)
        {
            return clsRoomData.DeleteRoom(RoomID);
        }

        public static DataTable GetAllRooms()
        {
            return clsRoomData.GetAllRooms();
        }

        public static int GetRoomsCount()
        {
            return clsRoomData.GetRoomsCount();
        }
        public static bool CheckRoomAvailability(int RoomNumber, DateTime ReservedForDate, DateTime ReservedToDate)
        {
            return clsRoomData.CheckRoomAvailability(RoomNumber, ReservedForDate, ReservedToDate);
        }

        public bool IsRoomBooked()
        {
            return this.Status == enRoomStatus.Booked;
        }
        public static DataTable GetAllAvailableRooms(int? RoomTypeID, DateTime ReservedForDate, DateTime ReservedToDate)
        {
            return clsRoomData.GetAllAvailableRooms(RoomTypeID, ReservedForDate, ReservedToDate);
        }
        
        public static bool ChangeRoomStatus(int? RoomID, enRoomStatus NewStatus)
        {
            return clsRoomData.ChangeRoomStatus(RoomID, (byte)NewStatus);
        }
    }
}