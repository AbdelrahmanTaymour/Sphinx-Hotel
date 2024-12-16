using System;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using HotelDatabase_DataAccess;

namespace HotelDatabase_Buisness
{
    public class clsBooking
    {
        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _mode = enMode.AddNew;

        public enum enBookingStatus { Ongoing = 0, CheckedOut = 1 }

        public int? BookingID { get; private set; }
        public int? ReservationID { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal IncidentalCharges { get; set; }
        public enBookingStatus Status { get; set; }
        public int? CreatedByUserID { get; set; }

        clsUser _CreatedByUserInfo;
        clsReservation _ReservationInfo;
        public clsUser CreatedByUserInfo
        {
            get
            {
                if (_CreatedByUserInfo == null && CreatedByUserID.HasValue)
                    _CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
                return _CreatedByUserInfo;
            }
        }
        public clsReservation ReservationInfo
        {
            get
            {
                if (_ReservationInfo == null && ReservationID.HasValue)
                    _ReservationInfo = clsReservation.Find(ReservationID);
                return _ReservationInfo;
            }
        }

        public string BookingStatusName => _GetStatusBookingName();

        public clsBooking()
        {
            BookingID = null;
            ReservationID = null;
            CheckInDate = default;
            CheckOutDate = default;
            IncidentalCharges = default;
            Status = enBookingStatus.Ongoing;
            CreatedByUserID = null;

            _mode = enMode.AddNew;
        }
        private clsBooking(int? BookingID, int? ReservationID, DateTime CheckInDate, DateTime CheckOutDate, decimal IncidentalCharges, enBookingStatus Status, int? CreatedByUserID)
        {
            this.BookingID = BookingID;
            this.ReservationID = ReservationID;
            this.CheckInDate = CheckInDate;
            this.CheckOutDate = CheckOutDate;
            this.IncidentalCharges = IncidentalCharges;
            this.Status = Status;
            this.CreatedByUserID = CreatedByUserID;

            _mode = enMode.Update;
        }

        public static clsBooking Find(int? BookingID)
        {
            int? ReservationID = null;
            DateTime CheckInDate = default;
            DateTime CheckOutDate = default;
            decimal IncidentalCharges = default;
            byte Status = default;
            int? CreatedByUserID = null;

            bool isFound = clsBookingData.GetBookingInfoByID(BookingID, ref ReservationID, ref CheckInDate, ref CheckOutDate, ref IncidentalCharges, ref Status, ref CreatedByUserID);

            if (isFound)
                return new clsBooking(BookingID, ReservationID, CheckInDate, CheckOutDate, IncidentalCharges, (enBookingStatus)Status, CreatedByUserID);
            else
                return null;
        }
        public static bool DoesBookingExist(int? BookingID)
        {
            return clsBookingData.DoesBookingExist(BookingID);
        }
        
        public static int? GetBookingIDByReservationID(int? ReservationID)
        {
            return clsBookingData.GetBookingIDByReservationID(ReservationID);
        }
        public static int? GetBookingIDByRoomID(int? RoomID)
        {
            return clsBookingData.GetBookingIDByRoomID(RoomID);
        }
        string _GetStatusBookingName()
        {
            return GetStatusBookingName(this.Status);
        }
        bool _AddNewBooking()
        {
            BookingID = clsBookingData.AddNewBooking(ReservationID, CheckInDate, CheckOutDate, IncidentalCharges, (byte)Status, CreatedByUserID);
            return BookingID.HasValue;
        }
        bool _UpdateBooking()
        {
            return clsBookingData.UpdateBookingInfo(BookingID, ReservationID, CheckInDate, CheckOutDate, IncidentalCharges, (byte)Status, CreatedByUserID);
        }

        public bool Save()
        {
            switch (_mode)
            {
                case enMode.AddNew:
                    if (_AddNewBooking())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateBooking();

            }
            return false;
        }

        public static bool DeleteBooking(int? BookingID)
        {
            return clsBookingData.DeleteBooking(BookingID);
        }
        public static DataTable GetAllBookings()
        {
            return clsBookingData.GetAllBookings();
        }
        public static DataTable GetGuestBookingHistory(int? GuestID)
        {
            return clsBookingData.GetGuestBookingHistory(GuestID);
        }
        public static string GetStatusBookingName(enBookingStatus BookingStatus)
        {
            switch (BookingStatus)
            {
                case enBookingStatus.Ongoing:
                    return "Ongoing";

                case enBookingStatus.CheckedOut:
                    return "CheckedOut";

                default:
                    return "N/A";
            }
        }
        public static bool IsBookingCheckedOut(int? BookingID)
        {
            return clsBookingData.IsBookingCheckedOut(BookingID);
        }
        public (bool IsSucceed, int? PaymentID) CheckOut(int? CreatedByUserID)
        {
            int? PaymentID = clsBookingData.CheckOut(this.BookingID, CreatedByUserID);

            if (PaymentID.HasValue)
                return (true, PaymentID);

            return (false, null);
        }
    }
}