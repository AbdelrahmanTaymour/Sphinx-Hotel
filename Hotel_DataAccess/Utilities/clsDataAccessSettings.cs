using System;
using System.Diagnostics;
using System.Configuration;

namespace HotelDatabase_DataAccess
{
    public class clsDataAccessSettings
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }
}