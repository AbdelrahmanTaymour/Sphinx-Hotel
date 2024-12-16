using System;
using System.Data;
using System.Data.SqlClient;

namespace HotelDatabase_DataAccess
{
    public class clsRoomTypeData
    {

        public static DataTable GetAllRoomTypes()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_RoomTypes_GetAllRoomTypes", connection))
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

        public static bool GetRoomTypeInfoByID(int? RoomTypeID, ref string RoomTypeTitle, ref byte Capacity, ref decimal PricePerNight, ref string Description)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_RoomTypes_GetRoomTypeInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RoomTypeID", (object)RoomTypeID ?? DBNull.Value);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // The record was found successfully !
                                isFound = true;

                                RoomTypeID = (reader["RoomTypeID"] != DBNull.Value) ? (int?)reader["RoomTypeID"] : null;
                                RoomTypeTitle = (string)reader["RoomTypeTitle"];
                                Capacity = (byte)reader["Capacity"];
                                PricePerNight = (decimal)reader["PricePerNight"];
                                Description = (reader["Description"] != DBNull.Value) ? (string)reader["Description"] : null;

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

        public static bool GetRoomTypeInfoByTitle(string RoomTypeTitle, ref int? RoomTypeID, ref byte Capacity, ref decimal PricePerNight, ref string Description)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_RoomTypes_GetRoomTypeInfoByTitle", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RoomTypeTitle", RoomTypeTitle);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // The record was found successfully !
                                isFound = true;

                                RoomTypeID = (reader["RoomTypeID"] != DBNull.Value) ? (int?)reader["RoomTypeID"] : null;
                                Capacity = (byte)reader["Capacity"];
                                PricePerNight = (decimal)reader["PricePerNight"];
                                Description = (reader["Description"] != DBNull.Value) ? (string)reader["Description"] : null;

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

        public static int? AddNewRoomType(string RoomTypeTitle, byte Capacity, decimal PricePerNight, string Description)
        {
            int? RoomTypeID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_RoomTypes_AddNewRoomType", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                    
                        command.Parameters.AddWithValue("@RoomTypeTitle", RoomTypeTitle);
                        command.Parameters.AddWithValue("@Capacity", Capacity);
                        command.Parameters.AddWithValue("@PricePerNight", PricePerNight);
                        command.Parameters.AddWithValue("@Description", (object)Description ?? DBNull.Value);

                        SqlParameter outputRoomTypeIDParameter = new SqlParameter("@RoomTypeID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputRoomTypeIDParameter);
                        command.ExecuteNonQuery();

                        RoomTypeID = (int?)outputRoomTypeIDParameter.Value;
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

            return RoomTypeID;
        }

        public static bool UpdateRoomTypeInfo(int? RoomTypeID, string RoomTypeTitle, byte Capacity, decimal PricePerNight, string Description)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_RoomTypes_UpdateRoomTypeInfo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@RoomTypeID", (object)RoomTypeID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@RoomTypeTitle", RoomTypeTitle);
                        command.Parameters.AddWithValue("@Capacity", Capacity);
                        command.Parameters.AddWithValue("@PricePerNight", PricePerNight);
                        command.Parameters.AddWithValue("@Description", (object)Description ?? DBNull.Value);
                            
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

        public static bool DeleteRoomType(int? RoomTypeID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_RoomTypes_DeleteRoomType", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RoomTypeID", (object)RoomTypeID ?? DBNull.Value);

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

        public static bool DoesRoomTypeExist(int? RoomTypeID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_RoomTypes_CheckIfRoomTypeExists", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RoomTypeID", (object)RoomTypeID ?? DBNull.Value);

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

        public static DataTable GetAllRoomTypesTitle()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_RoomTypes_GetAllRoomTypesTitle", connection))
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

    }
}