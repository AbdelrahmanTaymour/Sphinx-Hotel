using System;
using System.Data;
using System.Text;
using HotelDatabase_DataAccess;

namespace HotelDatabase_Buisness
{
    public class clsOrder
    {

        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _mode = enMode.AddNew;

        public enum enOrderType { RoomService = 0, Dining = 1 }

        public int? OrderID { get; private set; }
        public int? BookingID { get; set; }
        public int? RoomID { get; set; }
        public enOrderType OrderType { get; set; }
        public decimal Fees { get; set; }
        public DateTime OrderDate { get; set; }
        public int? CreatedByUserID { get; set; }

        clsBooking _BookingInfo;
        clsRoom _RoomInfo;
        clsUser _CreatedByUserInfo;

        public clsBooking BookingInfo
        {
            get
            {
                if (_BookingInfo == null && BookingID.HasValue)
                    _BookingInfo = clsBooking.Find(BookingID);
                return _BookingInfo;
            }
        }
        public clsRoom RoomInfo
        {
            get
            {
                if (_RoomInfo == null && RoomID.HasValue)
                    _RoomInfo = clsRoom.Find(RoomID);
                return _RoomInfo;
            }
        }
        public clsUser CreatedByUserInfo
        {
            get
            {
                if (_CreatedByUserInfo == null && RoomID.HasValue)
                    _CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
                return _CreatedByUserInfo;
            }
        }
        public string OrderTypeName => _GetOrderTypeName(this.OrderType);

        public clsOrder()
        {
            OrderID = null;
            BookingID = null;
            RoomID = null;
            OrderType = enOrderType.Dining;
            Fees = -1M;
            OrderDate = DateTime.Now;
            CreatedByUserID = null;

            _mode = enMode.AddNew;
        }

        private clsOrder(int? OrderID, int? BookingID, int? RoomID, enOrderType OrderType, decimal Fees, DateTime OrderDate, int? CreatedByUserID)
        {
            this.OrderID = OrderID;
            this.BookingID = BookingID;
            this.RoomID = RoomID;
            this.OrderType = OrderType;
            this.Fees = Fees;
            this.OrderDate = OrderDate;
            this.CreatedByUserID = CreatedByUserID;

            _mode = enMode.Update;
        }
        string _GetOrderTypeName(enOrderType OrderType)
        {
            switch (OrderType)
            {
                case enOrderType.RoomService:
                    return "Room Service";

                case enOrderType.Dining:
                    return "Dining";

                default:
                    return "Unknown";
            }
        }
        public static clsOrder Find(int? OrderID)
        {
            int? BookingID = null;
            int? RoomID = null;
            byte OrderType = 0;
            decimal Fees = -1M;
            DateTime OrderDate = DateTime.Now;
            int? CreatedByUserID = null;

            bool isFound = clsOrderData.GetOrderInfoByID(OrderID, ref BookingID, ref RoomID, ref OrderType, ref Fees, ref OrderDate, ref CreatedByUserID);

            if (isFound)
                return new clsOrder(OrderID, BookingID, RoomID, (enOrderType)OrderType, Fees, OrderDate, CreatedByUserID);
            else
                return null;
        }

        public static bool DoesOrderExist(int? OrderID)
        {
            return clsOrderData.DoesOrderExist(OrderID);
        }

        private bool _AddNewOrder()
        {
            OrderID = clsOrderData.AddNewOrder(BookingID, RoomID, (byte)OrderType, Fees, OrderDate, CreatedByUserID);
            return OrderID.HasValue;
        }

        private bool _UpdateOrder()
        {
            return clsOrderData.UpdateOrderInfo(OrderID, BookingID, RoomID, (byte)OrderType, Fees, OrderDate, CreatedByUserID);
        }

        public bool Save()
        {
            switch (_mode)
            {
                case enMode.AddNew:
                    if (_AddNewOrder())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateOrder();

            }
            return false;
        }

        public static bool DeleteOrder(int? OrderID)
        {
            return clsOrderData.DeleteOrder(OrderID);
        }

        public static DataTable GetAllOrders()
        {
            return clsOrderData.GetAllOrders();
        }

        public static DataTable GetAllOrdersForBookingID(int? BookingID)
        {
            return clsOrderData.GetAllOrdersForBookingID(BookingID);
        }
        
    }
}