using System;
using System.Data;
using System.Text;
using HotelDatabase_DataAccess;

namespace HotelDatabase_Buisness
{
    public class clsPayment
    {

        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _mode = enMode.AddNew;

        public enum enPaymentReason { CheckIn = 0, CheckOut = 1 };

        public int? PaymentID { get; private set; }
        public int? BookingID { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public enPaymentReason PaymentReason { get; set; }
        public int? CreatedByUserID { get; set; } 

        clsBooking _BookingInfo;
        public clsBooking BookingInfo
        {
            get
            {
                if (_BookingInfo == null && BookingID.HasValue)
                    _BookingInfo = clsBooking.Find(BookingID.Value);
                return _BookingInfo;
            }
        }

        public clsPayment()
        {
            PaymentID = null;
            BookingID = null;
            PaymentDate = default;
            PaymentAmount = default;
            PaymentReason = default;
            CreatedByUserID = null;

            _mode = enMode.AddNew;
        }

        private clsPayment(int? PaymentID, int? BookingID, DateTime PaymentDate, decimal PaymentAmount, enPaymentReason PaymentReason, int? CreatedByUserID)
        {
            this.PaymentID = PaymentID;
            this.BookingID = BookingID;
            this.PaymentDate = PaymentDate;
            this.PaymentAmount = PaymentAmount;
            this.PaymentReason = PaymentReason;
            this.CreatedByUserID = CreatedByUserID;

            _mode = enMode.Update;
        }

        public static clsPayment Find(int? PaymentID)
        {
            int? BookingID = default;
            DateTime PaymentDate = default;
            decimal PaymentAmount = default;
            byte PaymentReason = default;
            int? CreatedByUserID = null;

            bool isFound = clsPaymentData.GetPaymentInfoByID(PaymentID, ref BookingID, ref PaymentDate, ref PaymentAmount, ref PaymentReason, ref CreatedByUserID);

            if (isFound)
                return new clsPayment(PaymentID, BookingID, PaymentDate, PaymentAmount, (enPaymentReason)PaymentReason, CreatedByUserID);
            else
                return null;
        }

        public static clsPayment FindByBookingID(int? BookingID)
        {
            int? PaymentID = null;
            DateTime PaymentDate = default;
            decimal PaymentAmount = default;
            byte PaymentReason = default;
            int? CreatedByUserID = null;

            bool isFound = clsPaymentData.GetPaymentInfoByID(BookingID, ref PaymentID, ref PaymentDate, ref PaymentAmount, ref PaymentReason, ref CreatedByUserID);

            if (isFound)
                return new clsPayment(PaymentID, BookingID, PaymentDate, PaymentAmount, (enPaymentReason)PaymentReason, CreatedByUserID);
            else
                return null;
        }

        public static bool DoesPaymentExist(int? PaymentID)
        {
            return clsPaymentData.DoesPaymentExist(PaymentID);
        }

        private bool _AddNewPayment()
        {
            PaymentID = clsPaymentData.AddNewPayment(BookingID, PaymentDate, PaymentAmount, (byte)PaymentReason, CreatedByUserID);
            return PaymentID.HasValue;
        }

        private bool _UpdatePayment()
        {
            return clsPaymentData.UpdatePaymentInfo(PaymentID, BookingID, PaymentDate, PaymentAmount, (byte)PaymentReason, CreatedByUserID);
        }

        public bool Save()
        {
            switch (_mode)
            {
                case enMode.AddNew:
                    if (_AddNewPayment())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdatePayment();

            }
            return false;
        }

        public static bool DeletePayment(int? PaymentID)
        {
            return clsPaymentData.DeletePayment(PaymentID);
        }

        public static DataTable GetAllPayments()
        {
            return clsPaymentData.GetAllPayments();
        }


        
    }
}