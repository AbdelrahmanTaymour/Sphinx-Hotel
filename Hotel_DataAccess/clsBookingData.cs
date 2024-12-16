using System;
using System.Data;
using System.Data.SqlClient;

namespace HotelDatabase_DataAccess
{
    public class clsBookingData
    {

        public static DataTable GetAllBookings()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Bookings_GetAllBookings", connection))
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

        public static bool GetBookingInfoByID(int? BookingID, ref int? ReservationID, ref DateTime CheckInDate, ref DateTime CheckOutDate, ref decimal IncidentalCharges, ref byte Status, ref int? CreatedByUserID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Bookings_GetBookingInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BookingID", (object)BookingID ?? DBNull.Value);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // The record was found successfully !
                                isFound = true;

                                ReservationID = (reader["ReservationID"] != DBNull.Value) ? (int?)reader["ReservationID"] : null;
                                CheckInDate = (DateTime)reader["CheckInDate"];
                                CheckOutDate = (DateTime)reader["CheckOutDate"];
                                IncidentalCharges = (decimal)reader["IncidentalCharges"];
                                Status = (byte)reader["Status"];
                                CreatedByUserID = (reader["CreatedByUserID"] != DBNull.Value) ? (int?)reader["CreatedByUserID"]:null;
                            

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

        public static int? GetBookingIDByReservationID(int? ReservationID)
        {
            int? BookingID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Bookings_GetBookingIDByReservationID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ReservationID", (object)ReservationID ?? DBNull.Value);

                        SqlParameter outputIdParam = new SqlParameter("@BookingID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);

                        command.ExecuteNonQuery();

                        BookingID = (int?)outputIdParam.Value;
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

            return BookingID;
        }

        public static int? GetBookingIDByRoomID(int? RoomID)
        {
            int? BookingID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Bookings_GetBookingIDByRoomID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@RoomID", (object)RoomID ?? DBNull.Value);

                        SqlParameter outputIdParam = new SqlParameter("@BookingID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);
                        command.ExecuteNonQuery();

                        BookingID = (int?)outputIdParam.Value;
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

            return BookingID;
        }

        public static int? AddNewBooking(int? ReservationID, DateTime CheckInDate, DateTime CheckOutDate, decimal IncidentalCharges, byte Status, int? CreatedByUserID)
        {
            int? BookingID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Bookings_AddNewBooking", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                    
                        command.Parameters.AddWithValue("@ReservationID", (object)ReservationID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CheckInDate", CheckInDate);
                        command.Parameters.AddWithValue("@CheckOutDate", CheckOutDate);
                        command.Parameters.AddWithValue("@IncidentalCharges", IncidentalCharges);
                        command.Parameters.AddWithValue("@Status", Status);
                        command.Parameters.AddWithValue("@CreatedByUserID", (object)CreatedByUserID ?? DBNull.Value);
                            

                        SqlParameter outputBookingIDParameter = new SqlParameter("@BookingID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputBookingIDParameter);
                        command.ExecuteNonQuery();

                        BookingID = (int)outputBookingIDParameter.Value;
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

            return BookingID;
        }

        public static bool UpdateBookingInfo(int? BookingID, int? ReservationID, DateTime CheckInDate, DateTime CheckOutDate, decimal IncidentalCharges, byte Status, int? CreatedByUserID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Bookings_UpdateBookingInfo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@BookingID", (object)BookingID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ReservationID", (object)ReservationID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CheckInDate", CheckInDate);
                        command.Parameters.AddWithValue("@CheckOutDate", CheckOutDate);
                        command.Parameters.AddWithValue("@IncidentalCharges", IncidentalCharges);
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

        public static bool DeleteBooking(int? BookingID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Bookings_DeleteBooking", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BookingID", (object)BookingID ?? DBNull.Value);

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

        public static bool DoesBookingExist(int? BookingID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Bookings_CheckIfBookingExists", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BookingID", (object)BookingID ?? DBNull.Value);

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

        public static bool IsBookingCheckedOut(int? BookingID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Bookings_IsBookingCheckedOut", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BookingID", (object)BookingID ?? DBNull.Value);

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

        public static (bool IsBooked, int? NewBookingID, int? NewPaymentID) CheckIn(int? ReservationID, DateTime CheckOutDate, int? CreatedByUserID)
        {
            bool isBooked = false;
            int? newBookingID = null;
            int? newPaymentID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_Booking_CheckIn", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ReservationID", (object)ReservationID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CheckOutDate", CheckOutDate);
                        command.Parameters.AddWithValue("@CreatedByUserID", (object)CreatedByUserID ?? DBNull.Value);

                        SqlParameter bookingIdParam = new SqlParameter("@NewBookingID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(bookingIdParam);


                        SqlParameter paymentIdParam = new SqlParameter("@NewPaymentID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(paymentIdParam);
                        command.ExecuteNonQuery();

                        isBooked = true;
                        newBookingID = (int?)bookingIdParam.Value;
                        newPaymentID = (int?)paymentIdParam.Value;
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

            return (isBooked, newBookingID, newPaymentID);
        }

        public static int? CheckOut(int? BookingID, int? CreatedByUserID)
        {
            int? newPaymentID = null;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using(SqlCommand command = new SqlCommand("SP_Booking_CheckOut", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@BookingID", (object)BookingID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedByUserID", (object)CreatedByUserID ?? DBNull.Value);

                        SqlParameter paymentIdParam = new SqlParameter("@NewPaymentID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(paymentIdParam);
                        command.ExecuteNonQuery();

                        newPaymentID = (int?)paymentIdParam.Value;
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

            return newPaymentID;
        }

        public static DataTable GetGuestBookingHistory(int? GuestID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Bookings_GetGuestBookingHistory", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@GuestID", (object)GuestID ?? DBNull.Value);

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