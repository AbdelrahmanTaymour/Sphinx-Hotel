using System;
using System.Data;
using System.Text;
using HotelDatabase_DataAccess;

namespace HotelDatabase_Buisness
{
    public class clsReservation
    {

        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _mode = enMode.AddNew;

        public enum enReservationStatus {
            Pending = 0,
            Confirmed = 1,
            Canceled = 2,
            Booked = 3,
            Invalid = 4
        }

        public int? ReservationID { get; private set; }
        public int? GuestID { get; set; }
        public int? RoomID { get; set; }
        public DateTime ReservedForDate { get; set; }
        public DateTime ReservedToDate { get; set; }
        public int NumberOfPeople { get; set; }
        public enReservationStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedByUserID { get; set; }

        clsGuest _GuestInfo;
        clsRoom _RoomInfo;
        clsUser _CreatedByUserInfo;

        public clsGuest GuestInfo
        {
            get
            {
                if (_GuestInfo == null && GuestID.HasValue)
                    _GuestInfo = clsGuest.Find(GuestID);
                return _GuestInfo;
            }
        }
        public clsRoom RoomInfo
        {
            get
            {
                if(_RoomInfo == null && RoomID.HasValue)
                    _RoomInfo = clsRoom.Find(RoomID);
                return _RoomInfo;
            }
        }
        public clsUser CreatedByUserInfo
        {
            get
            {
                if (_CreatedByUserInfo == null && CreatedByUserID.HasValue)
                    _CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
                return _CreatedByUserInfo;
            }
        }
        public string ReservationStatusName => _ReservationStatusName(this.Status);
        public bool IsCheckedIn => IsReservationCheckedIn(this.ReservationID);


        string _ReservationStatusName(enReservationStatus ReservationStatus)
        {
            switch (ReservationStatus)
            {
                case enReservationStatus.Pending:
                    return "Pending";

                case enReservationStatus.Confirmed:
                    return "Confirmed";

                case enReservationStatus.Canceled:
                    return "Canceled";

                case enReservationStatus.Booked:
                    return "Booked";

                case enReservationStatus.Invalid:
                    return "Invalid";

                default:
                    return "Unknown";
            }
        }
        public clsReservation()
        {
            ReservationID = null;
            GuestID = null;
            RoomID = null;
            ReservedForDate = DateTime.Now;
            ReservedToDate = DateTime.Now.AddDays(5);
            NumberOfPeople = default;
            Status = default;
            CreatedDate = default;
            CreatedByUserID = null;

            _mode = enMode.AddNew;
        }
        private clsReservation(int? ReservationID, int? GuestID, int? RoomID, DateTime ReservedForDate, DateTime ReservedToDate, int NumberOfPeople, enReservationStatus Status, DateTime CreatedDate, int? CreatedByUserID)
        {
            this.ReservationID = ReservationID;
            this.GuestID = GuestID;
            this.RoomID = RoomID;
            this.ReservedForDate = ReservedForDate;
            this.ReservedToDate = ReservedToDate;
            this.NumberOfPeople = NumberOfPeople;
            this.Status = Status;
            this.CreatedDate = CreatedDate;
            this.CreatedByUserID = CreatedByUserID;

            _mode = enMode.Update;
        }

        public static clsReservation Find(int? ReservationID)
        {
            int? GuestID = null;
            int? RoomID = null;
            DateTime ReservedForDate = default;
            DateTime ReservedToDate = default;
            int NumberOfPeople = default;
            byte Status = 0;
            DateTime CreatedDate = default;
            int? CreatedByUserID = null;

            bool isFound = clsReservationData.GetReservationInfoByID(ReservationID, ref GuestID, ref RoomID, ref ReservedForDate, ref ReservedToDate, ref NumberOfPeople, ref Status, ref CreatedDate, ref CreatedByUserID);

            if (isFound)
                return new clsReservation(ReservationID, GuestID, RoomID, ReservedForDate, ReservedToDate, NumberOfPeople, (enReservationStatus)Status, CreatedDate, CreatedByUserID);
            else
                return null;
        }

        public static bool DoesReservationExist(int? ReservationID)
        {
            return clsReservationData.DoesReservationExist(ReservationID);
        }

        private bool _AddNewReservation()
        {
            ReservationID = clsReservationData.AddNewReservation(GuestID, RoomID, ReservedForDate, ReservedToDate, NumberOfPeople, CreatedByUserID);
            return ReservationID.HasValue;
        }

        private bool _UpdateReservation()
        {
            return clsReservationData.UpdateReservationInfo(ReservationID, GuestID, RoomID, ReservedForDate, ReservedToDate, NumberOfPeople, (byte)Status, CreatedByUserID);
        }

        public bool Save()
        {
            switch (_mode)
            {
                case enMode.AddNew:
                    if (_AddNewReservation())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateReservation();

            }
            return false;
        }

        public static bool DeleteReservation(int? ReservationID)
        {
            return clsReservationData.DeleteReservation(ReservationID);
        }

        public static DataTable GetAllReservations()
        {
            return clsReservationData.GetAllReservations();
        }

        public static bool IsReservationCheckedIn(int? ReservationID)
        {
            return clsReservationData.IsReservationCheckedIn(ReservationID);
        }

        public bool SetNewStatus(enReservationStatus NewStatus)
        {
            return SetNewStatus(this.ReservationID, NewStatus);
        }

        public static bool SetNewStatus(int? ReservationID, enReservationStatus NewStatus)
        {
            return clsReservationData.SetNewStatus(ReservationID, (byte)NewStatus);
        }

        //public bool CheckIn(int? ReservationID, DateTime CheckOutDate, int? CreatedByUserID)
        //{
        //    this.BookingID = clsBookingData.CheckIn(ReservationID, CheckOutDate, CreatedByUserID);

        //    return this.BookingID.HasValue;

        //}
        public (bool IsBooked, int? BookingID, int? PaymentID) CheckIn(int? CreatedByUserID)
        {
            return clsBookingData.CheckIn(this.ReservationID, this.ReservedToDate, CreatedByUserID);


        }

    }
}