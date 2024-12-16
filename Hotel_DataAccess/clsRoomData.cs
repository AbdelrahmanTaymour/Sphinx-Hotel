using System;
using System.Data;
using System.Data.SqlClient;

namespace HotelDatabase_DataAccess
{
    public class clsRoomData
    {
        public static DataTable GetAllRooms()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Rooms_GetAllRooms", connection))
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

        public static DataTable GetAllAvailableRooms(int? RoomType, DateTime ReservedForDate, DateTime ReservedToDate)
        {
            DataTable abailableRooms = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Rooms_GetAvailableRooms", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@RoomTypeID", (object)RoomType ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ReservedForDate", ReservedForDate);
                        command.Parameters.AddWithValue("@ReservedToDate", ReservedToDate);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                abailableRooms.Load(reader);
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

            return abailableRooms;
        }

        public static bool GetRoomInfoByID(int? RoomID, ref int? RoomTypeID, ref int RoomNumber, ref int FloorNumber, ref decimal Size, ref byte Status, ref bool IsSmokingAllowed, ref bool IsPetFriendly, ref string RoomPhone)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Rooms_GetRoomInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RoomID", (object)RoomID ?? DBNull.Value);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // The record was found successfully !
                                isFound = true;

                                RoomTypeID = (reader["RoomTypeID"] != DBNull.Value) ? (int?)reader["RoomTypeID"] : null;
                                RoomNumber = (int)reader["RoomNumber"];
                                FloorNumber = (int)reader["FloorNumber"];
                                Size = (decimal)reader["Size"];
                                Status = (byte)reader["Status"];
                                IsSmokingAllowed = (bool)reader["IsSmokingAllowed"];
                                IsPetFriendly = (bool)reader["IsPetFriendly"];
                                RoomPhone = (string)reader["RoomPhone"];

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
        public static bool GetRoomInfoByRoomNumber(int RoomNumber, ref int? RoomID, ref byte? RoomTypeID,
            ref int FloorNumber, ref decimal Size, ref byte Status, ref bool IsSmokingAllowed,
            ref bool IsPetFriendly, ref string RoomPhone)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Rooms_GetRoomInfoByRoomNumber", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@RoomNumber", (object)RoomNumber ?? DBNull.Value);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // The record was found
                                IsFound = true;

                                RoomID = (reader["RoomID"] != DBNull.Value) ? (int?)reader["RoomID"] : null;
                                RoomTypeID = (reader["RoomTypeID"] != DBNull.Value) ? (byte?)Convert.ToByte(reader["RoomTypeID"]) : null;
                                FloorNumber = (int)reader["FloorNumber"];
                                Size = (decimal)reader["Size"];
                                Status = (byte)reader["Status"];
                                IsSmokingAllowed = (bool)reader["IsSmokingAllowed"];
                                IsPetFriendly = (bool)reader["IsPetFriendly"];
                                RoomPhone = (reader["RoomPhone"] != DBNull.Value) ? (string)reader["RoomPhone"] : null;
                            }
                            else
                            {
                                // The record was not found
                                IsFound = false;
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

            return IsFound;
        }

        public static int? AddNewRoom(int? RoomTypeID, int RoomNumber, int FloorNumber, decimal Size, byte Status, bool IsSmokingAllowed, bool IsPetFriendly, string RoomPhone)
        {
            int? RoomID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Rooms_AddNewRoom", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                    
                        command.Parameters.AddWithValue("@RoomTypeID", (object)RoomTypeID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@RoomNumber", RoomNumber);
                        command.Parameters.AddWithValue("@FloorNumber", FloorNumber);
                        command.Parameters.AddWithValue("@Size", Size);
                        command.Parameters.AddWithValue("@Status", Status);
                        command.Parameters.AddWithValue("@IsSmokingAllowed", IsSmokingAllowed);
                        command.Parameters.AddWithValue("@IsPetFriendly", IsPetFriendly);
                        command.Parameters.AddWithValue("@RoomPhone", RoomPhone);
                    
                        SqlParameter outputRoomIDParameter = new SqlParameter("@NewRoomID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputRoomIDParameter);
                        command.ExecuteNonQuery();

                        RoomID = (int)outputRoomIDParameter.Value;
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

            return RoomID;
        }

        public static bool UpdateRoomInfo(int? RoomID, int? RoomTypeID, int RoomNumber, int FloorNumber, decimal Size, byte Status, bool IsSmokingAllowed, bool IsPetFriendly, string RoomPhone)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Rooms_UpdateRoomInfo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@RoomID", (object)RoomID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@RoomTypeID", (object)RoomTypeID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@RoomNumber", RoomNumber);
                        command.Parameters.AddWithValue("@FloorNumber", FloorNumber);
                        command.Parameters.AddWithValue("@Size", Size);
                        command.Parameters.AddWithValue("@Status", Status);
                        command.Parameters.AddWithValue("@IsSmokingAllowed", IsSmokingAllowed);
                        command.Parameters.AddWithValue("@IsPetFriendly", IsPetFriendly);
                        command.Parameters.AddWithValue("@RoomPhone", RoomPhone);
                            
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

        public static bool DeleteRoom(int? RoomID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Rooms_DeleteRoom", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RoomID", (object)RoomID ?? DBNull.Value);

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

        public static bool DoesRoomExist(int? RoomID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Rooms_CheckIfRoomExists", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RoomID", (object)RoomID ?? DBNull.Value);

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

        public static bool DoesRoomnNumberExist(int RoomNumber)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Rooms_CheckIfRoomExists", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RoomNumber", RoomNumber);

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

        public static bool CheckRoomAvailability(int RoomNumber, DateTime ReservedForDate, DateTime ReservedToDate)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Rooms_CheckRoomAvailability", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@RoomNumber", RoomNumber);
                        command.Parameters.AddWithValue("@ReservedForDate", ReservedForDate);
                        command.Parameters.AddWithValue("@ReservedToDate", ReservedToDate);

                        SqlParameter returnParameter = new SqlParameter
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnParameter);
                        command.ExecuteScalar();

                        IsFound = (int)returnParameter.Value == 1;
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

            return IsFound;
        }

        public static int GetRoomsCount()
        {
            int Count = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Rooms_GetRoomsCount", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int Value))
                        {
                            Count = Value;
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

            return Count;
        }

        public static int GetRoomsCountByRoomTypeID(int? RoomTypeID)
        {
            int Count = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Rooms_GetRoomsCountByRoomTypeID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("RoomTypeID", (object)RoomTypeID ?? DBNull.Value);

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int Value))
                        {
                            Count = Value;
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

            return Count;
        }

        public static bool ChangeRoomStatus(int? RoomID, byte NewStatus)
        {
            int RowAffected = 0;
            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using(SqlCommand command = new SqlCommand("SP_Rooms_ChangeRoomStatus", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RoomID", (object)RoomID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@NewStatus", NewStatus);

                        RowAffected = command.ExecuteNonQuery();
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

            return (RowAffected > 0);
        }

    }
}