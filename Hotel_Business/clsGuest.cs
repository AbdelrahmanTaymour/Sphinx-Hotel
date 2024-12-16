using System;
using System.Data;
using System.Text;
using HotelDatabase_DataAccess;

namespace HotelDatabase_Buisness
{
    public class clsGuest
    {

        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _mode = enMode.AddNew;

        public int? GuestID { get; private set; }
        public int? PersonID { get; set; }

        clsPerson _personInfo;
        public clsPerson PersonInfo
        {
            get
            {
                if (_personInfo == null && PersonID.HasValue)
                    _personInfo = clsPerson.Find(PersonID);
                return _personInfo;
            }
        }

        public clsGuest()
        {
            GuestID = null;
            PersonID = null;

            _mode = enMode.AddNew;
        }

        private clsGuest(int? GuestID, int? PersonID)
        {
            this.GuestID = GuestID;
            this.PersonID = PersonID;

            _mode = enMode.Update;
        }

        public static clsGuest Find(int? GuestID)
        {
            int? PersonID = default;

            bool isFound = clsGuestData.GetGuestInfoByID(GuestID, ref PersonID);

            if (isFound)
                return new clsGuest(GuestID, PersonID);
            else
                return null;
        }

        public static bool DoesGuestExist(int? GuestID)
        {
            return clsGuestData.DoesGuestExist(GuestID);
        }

        private bool _AddNewGuest()
        {
            GuestID = clsGuestData.AddNewGuest(PersonID);
            return GuestID.HasValue;
        }

        private bool _UpdateGuest()
        {
            return clsGuestData.UpdateGuestInfo(GuestID, PersonID);
        }

        public bool Save()
        {
            switch (_mode)
            {
                case enMode.AddNew:
                    if (_AddNewGuest())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateGuest();

            }
            return false;
        }

        public static bool DeleteGuest(int? GuestID)
        {
            return clsGuestData.DeleteGuest(GuestID);
        }

        public static DataTable GetAllGuests()
        {
            return clsGuestData.GetAllGuests();
        }


        
    }
}