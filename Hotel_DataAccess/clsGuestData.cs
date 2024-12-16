using System;
using System.Data;
using System.Data.SqlClient;

namespace HotelDatabase_DataAccess
{
    public class clsGuestData
    {
        public static DataTable GetAllGuests()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Guests_GetAllGuests", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsDataAccessUtilities.LogError(ex);
            }
            catch (Exception ex)
            {
                clsDataAccessUtilities.LogError(ex);
            }

            return dt;
        }

        public static bool GetGuestInfoByID(int? GuestID, ref int? PersonID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Guests_GetGuestInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@GuestID", (object)GuestID ?? DBNull.Value);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // The record was found successfully !
                                isFound = true;

                                PersonID = (reader["PersonID"] != DBNull.Value) ? (int?)reader["PersonID"] : null;

                            }
                            else
                            {
                                // The record wasn't found !
                                isFound = false;
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsDataAccessUtilities.LogError(ex);
            }
            catch (Exception ex)
            {
                clsDataAccessUtilities.LogError(ex);
            }
            return isFound;
        }

        public static int? AddNewGuest(int? PersonID)
        {
            int? GuestID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Guests_AddNewGuest", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                    
                            command.Parameters.AddWithValue("@PersonID", (object)PersonID ?? DBNull.Value);
                            

                        SqlParameter outputGuestIDParameter = new SqlParameter("@NewGuestID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputGuestIDParameter);
                        command.ExecuteNonQuery();

                        GuestID = (int)outputGuestIDParameter.Value;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsDataAccessUtilities.LogError(ex);
            }
            catch (Exception ex)
            {
                clsDataAccessUtilities.LogError(ex);
            }

            return GuestID;
        }

        public static bool UpdateGuestInfo(int? GuestID, int? PersonID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Guests_UpdateGuestInfo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@GuestID", (object)GuestID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@PersonID", (object)PersonID ?? DBNull.Value);

                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                clsDataAccessUtilities.LogError(ex);
            }
            catch (Exception ex)
            {
                clsDataAccessUtilities.LogError(ex);
            }

            return rowsAffected != 0;
        }

        public static bool DeleteGuest(int? GuestID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Guests_DeleteGuest", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@GuestID", (object)GuestID ?? DBNull.Value);

                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                clsDataAccessUtilities.LogError(ex);
            }
            catch (Exception ex)
            {
                clsDataAccessUtilities.LogError(ex);
            }

            return rowsAffected != 0;
        }

        public static bool DoesGuestExist(int? GuestID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Guests_CheckIfGuestExists", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@GuestID", (object)GuestID ?? DBNull.Value);

                        SqlParameter returnValue = new SqlParameter
                        {
                            Direction = ParameterDirection.ReturnValue
                        };

                        command.Parameters.Add(returnValue);
                        command.ExecuteScalar();

                        isFound = (int)returnValue.Value == 1;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsDataAccessUtilities.LogError(ex);
            }
            catch (Exception ex)
            {
                clsDataAccessUtilities.LogError(ex);
            }

            return isFound;
        }
        
    }
}