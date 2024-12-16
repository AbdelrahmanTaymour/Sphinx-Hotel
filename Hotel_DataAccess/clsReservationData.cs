using System;
using System.Data;
using System.Data.SqlClient;

namespace HotelDatabase_DataAccess
{
    public class clsReservationData
    {

        public static DataTable GetAllReservations()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Reservations_GetAllReservations", connection))
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

        public static bool GetReservationInfoByID(int? ReservationID, ref int? GuestID, ref int? RoomID, ref DateTime ReservedForDate, ref DateTime ReservedToDate, ref int NumberOfPeople, ref byte Status, ref DateTime CreatedDate, ref int? CreatedByUserID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Reservations_GetReservationInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ReservationID", (object)ReservationID ?? DBNull.Value);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // The record was found successfully !
                                isFound = true;

                                GuestID = (reader["GuestID"] != DBNull.Value) ? (int?)reader["GuestID"] : null;
                                RoomID = (reader["RoomID"] != DBNull.Value) ? (int?)reader["RoomID"] : null;
                                ReservedForDate = (DateTime)reader["ReservedForDate"];
                                ReservedToDate = (DateTime)reader["ReservedToDate"];
                                NumberOfPeople = (int)reader["NumberOfPeople"];
                                Status = (byte)reader["Status"];
                                CreatedDate = (DateTime)reader["CreatedDate"];
                                CreatedByUserID = (reader["CreatedByUserID"] != DBNull.Value) ? (int?)reader["CreatedByUserID"] : null;

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

        public static int? AddNewReservation(int? GuestID, int? RoomID, DateTime ReservedForDate, DateTime ReservedToDate, int NumberOfPeople, int? CreatedByUserID)
        {
            int? ReservationID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Reservations_AddNewReservation", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                    
                        command.Parameters.AddWithValue("@GuestID", (object)GuestID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@RoomID", (object)RoomID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ReservedForDate", ReservedForDate);
                        command.Parameters.AddWithValue("@ReservedToDate", ReservedToDate);
                        command.Parameters.AddWithValue("@NumberOfPeople", NumberOfPeople);
                        command.Parameters.AddWithValue("@CreatedByUserID", (object)CreatedByUserID ?? DBNull.Value);

                        SqlParameter outputReservationIDParameter = new SqlParameter("@NewReservationID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputReservationIDParameter);
                        command.ExecuteNonQuery();

                        ReservationID = (int)outputReservationIDParameter.Value;
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

            return ReservationID;
        }

        public static bool UpdateReservationInfo(int? ReservationID, int? GuestID, int? RoomID, DateTime ReservedForDate, DateTime ReservedToDate, int NumberOfPeople, byte Status, int? CreatedByUserID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Reservations_UpdateReservationInfo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ReservationID", (object)ReservationID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@GuestID", (object)GuestID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@RoomID", (object)RoomID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ReservedForDate", ReservedForDate);
                        command.Parameters.AddWithValue("@ReservedToDate", ReservedToDate);
                        command.Parameters.AddWithValue("@NumberOfPeople", NumberOfPeople);
                        command.Parameters.AddWithValue("@Status", Status);
                        command.Parameters.AddWithValue("@CreatedByUserID", (object)CreatedByUserID ?? DBNull.Value);
                            
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

        public static bool DeleteReservation(int? ReservationID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Reservations_DeleteReservation", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ReservationID", (object)ReservationID ?? DBNull.Value);

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

        public static bool DoesReservationExist(int? ReservationID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Reservations_CheckIfReservationExists", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ReservationID", (object)ReservationID ?? DBNull.Value);

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
        
        public static bool IsReservationCheckedIn(int? ReservationID)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Rservations_IsReservationCheckedIn", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ReservationID", (object)ReservationID ?? DBNull.Value);

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

        public static bool SetNewStatus(int? ReservationID, byte NewStatus)
        {
            int RowAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Rservations_SetNewReservationStatus", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ReservationID", (object)ReservationID ?? DBNull.Value);
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