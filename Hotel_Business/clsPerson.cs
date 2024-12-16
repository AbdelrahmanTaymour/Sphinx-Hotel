using System;
using System.Data;
using System.Text;
using HotelDatabase_DataAccess;

namespace HotelDatabase_Buisness
{
    public class clsPerson
    {
        clsCountry _countryInfo;
        public enum enGender { Male = 0, Female = 1 }
        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _mode = enMode.AddNew;

        public int? PersonID { get; private set; }
        public string NationalNo { get; set; }
        public string FullName { get; set; }
        public short? CountryID { get; set; }
        public DateTime DateOfBirth { get; set; }
        public enGender Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public clsCountry CountryInfo
        {
            get
            {
                if (_countryInfo == null && CountryID.HasValue)
                    _countryInfo = clsCountry.Find(CountryID);
                return _countryInfo;
            }
        }
        public bool IsUser => clsUser.DoesUserExistByPersonID(PersonID);
        public string GenderText => (Gender == enGender.Male) ? "Male" : "Female";
        

        public clsPerson()
        {
            PersonID = null;
            NationalNo = default;
            FullName = default;
            CountryID = null;
            DateOfBirth = DateTime.Now;
            Gender = enGender.Male;
            Address = default;
            Phone = default;
            Email = null;
            ImagePath = null;

            _mode = enMode.AddNew;
        }

        private clsPerson(int? PersonID, string NationalNo, string FullName, short? CountryID, DateTime DateOfBirth, enGender Gender,
            string Address, string Phone, string Email, string ImagePath)
        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FullName = FullName;
            this.CountryID = CountryID;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.ImagePath = ImagePath;

            _mode = enMode.Update;
        }

        public static clsPerson Find(int? PersonID)
        {
            string NationalNo = null;
            string FullName = default;
            short? CountryID =null;
            DateTime DateOfBirth = DateTime.Now;
            byte Gender = 0;
            string Address = default;
            string Phone = default;
            string Email = null;
            string ImagePath = null;

            bool isFound = clsPersonData.GetPersonInfoByID(PersonID, ref NationalNo, ref FullName, ref CountryID, ref DateOfBirth, ref Gender, ref Address, ref Phone, ref Email, ref ImagePath);

            if (isFound)
                return new clsPerson(PersonID, NationalNo, FullName, CountryID, DateOfBirth, (enGender)Gender, Address, Phone, Email, ImagePath);
            else
                return null;
        }
        public static clsPerson Find(string NationalNo)
        {
            int? PersonID = null;
            string FullName = default;
            short? CountryID = null;
            DateTime DateOfBirth = DateTime.Now;
            byte Gender = 0;
            string Address = default;
            string Phone = default;
            string Email = null;
            string ImagePath = null;

            bool isFound = clsPersonData.GetPersonInfoByNationalNo(NationalNo, ref PersonID, ref FullName, ref CountryID, ref DateOfBirth, ref Gender, ref Address, ref Phone, ref Email, ref ImagePath);

            if (isFound)
                return new clsPerson(PersonID, NationalNo, FullName, CountryID, DateOfBirth, (enGender)Gender, Address, Phone, Email, ImagePath);
            else
                return null;
        }

        public static bool DoesPersonExist(int? PersonID)
        {
            return clsPersonData.DoesPersonExist(PersonID);
        }
        public static bool DoesPersonExist(string NationalNo)
        {
            return clsPersonData.DoesPersonExist(NationalNo);
        }
        bool _AddNewPerson()
        {
            PersonID = clsPersonData.AddNewPerson(this.NationalNo, this.FullName, this.CountryID, this.DateOfBirth, (byte)this.Gender,
                this.Address, this.Phone, this.Email, this.ImagePath);
            
            return PersonID.HasValue;
        }

        bool _UpdatePerson()
        {
            return clsPersonData.UpdatePersonInfo(this.PersonID, this.NationalNo, this.FullName, this.CountryID, this.DateOfBirth,
                (byte)this.Gender, this.Address, this.Phone, this.Email, this.ImagePath);
        }

        public bool Save()
        {
            switch (_mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdatePerson();

            }
            return false;
        }

        public static bool DeletePerson(int? PersonID)
        {
            return clsPersonData.DeletePerson(PersonID);
        }

        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }


        
    }
}