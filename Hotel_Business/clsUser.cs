using System;
using System.Data;
using System.Text;
using HotelDatabase_DataAccess;

namespace HotelDatabase_Buisness
{
    public class clsUser
    {
        enum enMode { AddNew = 0, Update = 1 };
        enMode _mode = enMode.AddNew;

        public int? UserID { get; private set; }
        public int? PersonID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        clsPerson _PersonInfo;
        public clsPerson PersonInfo
        {
            get
            {
                if (_PersonInfo == null && PersonID > 0)
                {
                    _PersonInfo = clsPerson.Find(PersonID);
                }
                return _PersonInfo;
            }
        }

        public clsUser()
        {
            UserID = null;
            PersonID = null;
            Username = string.Empty;
            Password = string.Empty;
            IsActive = default;

            _mode = enMode.AddNew;
        }
        private clsUser(int? UserID, int? PersonID, string Username, string Password, bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.Username = Username;
            this.Password = Password;
            this.IsActive = IsActive;

            _mode = enMode.Update;
        }

        private bool _AddNewUser()
        {
            UserID = clsUserData.AddNewUser(this.PersonID, this.Username, this.Password, this.IsActive);
            return UserID.HasValue;
        }
        private bool _UpdateUser()
        {
            return clsUserData.UpdateUserInfo(this.UserID, this.PersonID, this.Username, this.Password, this.IsActive);
        }
        public bool Save()
        {
            switch (_mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateUser();

            }
            return false;
        }
        
        public static clsUser FindByUserID(int? UserID)
        {
            int? PersonID = null;
            string Username = string.Empty;
            string Password = string.Empty;
            bool IsActive = default;

            bool isFound = clsUserData.GetUserInfoByUserID(UserID, ref PersonID, ref Username, ref Password, ref IsActive);

            if (isFound)
                return new clsUser(UserID, PersonID, Username, Password, IsActive);
            else
                return null;
        }
        public static clsUser FindByPersonID(int? PersonID)
        {
            int? UserID = null;
            string Username = string.Empty;
            string Password = string.Empty;
            bool IsActive = false;

            bool IsFound = clsUserData.GetUserInfoByPersonID(PersonID, ref UserID, ref Username, ref Password, ref IsActive);

            return (IsFound) ? (new clsUser(UserID, PersonID, Username, Password, IsActive)) : null;
        }
        public static clsUser FindByUsername(string Username)
        {
            int? UserID = null;
            int? PersonID = null;
            string Password = string.Empty;
            bool IsActive = false;

            bool IsFound = clsUserData.GetUserInfoByUsername(Username, ref UserID, ref PersonID, ref Password, ref IsActive);

            return (IsFound) ? (new clsUser(UserID, PersonID, Username, Password, IsActive)) : null;
        }
        public static clsUser FindByUsernameAndPassword(string Username, string Password)
        {
            int? UserID = null;
            int? PersonID = null;
            bool IsActive = false;

            bool IsFound = clsUserData.GetUserInfoByUsernameAndPassword(Username, Password, ref UserID, ref PersonID, ref IsActive);

            return (IsFound) ? (new clsUser(UserID, PersonID, Username, Password, IsActive)) : null;
        }

        public static bool DeleteUser(int? UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }
        public static bool DoesUserExistByUserID(int? UserID)
        {
            return clsUserData.DoesUserExistByUserID(UserID);
        }
        public static bool DoesUserExistByPersonID(int? PersonID)
        {
            return clsUserData.DoesUserExistByPersonID(PersonID);
        }
        public static bool DoesUserExistByUsername(string Username)
        {
            return clsUserData.DoesUserExistByUsername(Username);
        }
        public static bool DoesUserExistByUsernameAndPassword(string Username, string Password)
        {
            return clsUserData.DoesUserExistByUsernameAndPassword(Username, Password);
        }

        public bool ChangePassword(string NewPassword)
        {
            return ChangePassword(this.UserID, NewPassword);
        }
        public static bool ChangePassword(int? UserID, string NewPassword)
        {
            return clsUserData.ChangePassword(UserID, NewPassword);
        }
        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }



    }
}