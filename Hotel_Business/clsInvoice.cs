using System;
using System.Data;
using System.Text;
using HotelDatabase_DataAccess;

namespace HotelDatabase_Buisness
{
    public class clsInvoice
    {

        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _mode = enMode.AddNew;

        public int? InvoiceID { get; private set; }
        public int? PaymentID { get; set; }
        public DateTime InvoiceDate { get; set; }

        public clsInvoice()
        {
            InvoiceID = null;
            PaymentID = null;
            InvoiceDate = default;

            _mode = enMode.AddNew;
        }

        private clsInvoice(int? InvoiceID, int? PaymentID, DateTime InvoiceDate)
        {
            this.InvoiceID = InvoiceID;
            this.PaymentID = PaymentID;
            this.InvoiceDate = InvoiceDate;

            _mode = enMode.Update;
        }

        public static clsInvoice FindByID(int? InvoiceID)
        {
            int? PaymentID = null;
            DateTime InvoiceDate = default;

            bool isFound = clsInvoiceData.GetInvoiceInfoByID(InvoiceID, ref PaymentID, ref InvoiceDate);

            if (isFound)
                return new clsInvoice(InvoiceID, PaymentID, InvoiceDate);
            else
                return null;
        }

        public static clsInvoice FindByPaymentID(int? PaymentID)
        {
            int? InvoiceID = null;
            DateTime InvoiceDate = default;

            bool isFound = clsInvoiceData.GetInvoiceInfoByPaymentID(PaymentID, ref InvoiceID, ref InvoiceDate);

            if (isFound)
                return new clsInvoice(InvoiceID, PaymentID, InvoiceDate);
            else
                return null;
        }

        public static bool DoesInvoiceExist(int? InvoiceID)
        {
            return clsInvoiceData.DoesInvoiceExist(InvoiceID);
        }

        private bool _AddNewInvoice()
        {
            InvoiceID = clsInvoiceData.AddNewInvoice(PaymentID);
            return InvoiceID.HasValue;
        }

        private bool _UpdateInvoice()
        {
            return clsInvoiceData.UpdateInvoiceInfo(InvoiceID, PaymentID);
        }

        public bool Save()
        {
            switch (_mode)
            {
                case enMode.AddNew:
                    if (_AddNewInvoice())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateInvoice();

            }
            return false;
        }

        public static bool DeleteInvoice(int? InvoiceID)
        {
            return clsInvoiceData.DeleteInvoice(InvoiceID);
        }

        public static DataTable GetAllInvoices()
        {
            return clsInvoiceData.GetAllInvoices();
        }

        public static bool DoesPaymentHaveAnInvoice(int? paymentID)
        {
            return clsInvoiceData.DoesPaymentHaveAnInvoice(paymentID);
        }

    }
}