using System;
using System.Data;
using System.Text;
using HotelDatabase_DataAccess;

namespace HotelDatabase_Buisness
{
    public class clsCountry
    {

        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _mode = enMode.AddNew;

        public short? CountryID { get; private set; }
        public string CountryName { get; set; }

        public clsCountry()
        {
            CountryID = null;
            CountryName = default;

            _mode = enMode.AddNew;
        }

        private clsCountry(short? CountryID, string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;

            _mode = enMode.Update;
        }

        public static clsCountry Find(short? CountryID)
        {
            string CountryName = default;

            bool isFound = clsCountryData.GetCountryInfoByID(CountryID, ref CountryName);

            return isFound ? new clsCountry(CountryID, CountryName) : null;
        }

        public static clsCountry Find(string CountryName)
        {
            short? CountryID = null;

            bool IsFound = clsCountryData.GetCountryInfoByName(CountryName, ref CountryID);

            return IsFound ? new clsCountry(CountryID, CountryName) : null;
        }

        public static bool DoesCountryExist(short? CountryID)
        {
            return clsCountryData.DoesCountryExist(CountryID);
        }

        private bool _AddNewCountry()
        {
            CountryID = clsCountryData.AddNewCountry(CountryName);
            return CountryID.HasValue;
        }

        private bool _UpdateCountry()
        {
            return clsCountryData.UpdateCountryInfo(CountryID, CountryName);
        }

        public bool Save()
        {
            switch (_mode)
            {
                case enMode.AddNew:
                    if (_AddNewCountry())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateCountry();

            }
            return false;
        }

        public static bool DeleteCountry(short? CountryID)
        {
            return clsCountryData.DeleteCountry(CountryID);
        }

        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();
        }


        
    }
}