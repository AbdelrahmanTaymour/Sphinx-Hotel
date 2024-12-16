using System;
using System.Data;
using System.Data.SqlClient;

namespace HotelDatabase_DataAccess
{
    public class clsPaymentData
    {

        public static DataTable GetAllPayments()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Payments_GetAllPayments", connection))
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

        public static bool GetPaymentInfoByID(int? PaymentID, ref int? BookingID, ref DateTime PaymentDate, ref decimal PaymentAmount, ref byte PaymentReason, ref int? CreatedByUserID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Payments_GetPaymentInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PaymentID", (object)PaymentID ?? DBNull.Value);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // The record was found successfully !
                                isFound = true;

                                BookingID = (reader["BookingID"] != DBNull.Value) ? (int?)reader["BookingID"] : null;
                                PaymentDate = (DateTime)reader["PaymentDate"];
                                PaymentAmount = (decimal)reader["PaymentAmount"];
                                PaymentReason = (byte)reader["PaymentReason"];
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
        public static bool GetPaymentInfoByBookingID(int? BookingID, ref int? PaymentID, ref DateTime PaymentDate, ref decimal PaymentAmount, ref byte PaymentReason, ref int? CreatedByUserID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Payments_GetPaymentInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BookingID", (object)BookingID ?? DBNull.Value);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // The record was found successfully !
                                isFound = true;

                                PaymentID = (reader["PaymentID"] != DBNull.Value) ? (int?)reader["PaymentID"] : null;
                                PaymentDate = (DateTime)reader["PaymentDate"];
                                PaymentAmount = (decimal)reader["PaymentAmount"];
                                PaymentReason = (byte)reader["PaymentReason"];
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
        public static int? AddNewPayment(int? BookingID, DateTime PaymentDate, decimal PaymentAmount, byte PaymentReason, int? CreatedByUserID)
        {
            int? PaymentID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Payments_AddNewPayment", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@BookingID", (object)BookingID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@PaymentDate", PaymentDate);
                        command.Parameters.AddWithValue("@PaymentAmount", PaymentAmount);
                        command.Parameters.AddWithValue("@PaymentReason", PaymentReason);
                        command.Parameters.AddWithValue("@CreatedByUserID", (object)CreatedByUserID ?? DBNull.Value);

                        SqlParameter outputPaymentIDParameter = new SqlParameter("@PaymentID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputPaymentIDParameter);
                        command.ExecuteNonQuery();

                        PaymentID = (int?)outputPaymentIDParameter.Value;
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

            return PaymentID;
        }

        public static bool UpdatePaymentInfo(int? PaymentID, int? BookingID, DateTime PaymentDate, decimal PaymentAmount, byte PaymentReason, int? CreatedByUserID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Payments_UpdatePaymentInfo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PaymentID", (object)PaymentID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@BookingID", (object)BookingID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@PaymentDate", PaymentDate);
                        command.Parameters.AddWithValue("@PaymentAmount", PaymentAmount);
                        command.Parameters.AddWithValue("@PaymentReason", PaymentReason);
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


        public static bool DeletePayment(int? PaymentID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Payments_DeletePayment", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PaymentID", (object)PaymentID ?? DBNull.Value);

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

        public static bool DoesPaymentExist(int? PaymentID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Payments_CheckIfPaymentExists", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PaymentID", (object)PaymentID ?? DBNull.Value);

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