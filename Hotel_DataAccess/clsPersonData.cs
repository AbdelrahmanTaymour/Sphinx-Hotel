using System;
using System.Data;
using System.Data.SqlClient;

namespace HotelDatabase_DataAccess
{
    public class clsPersonData
    {

        public static DataTable GetAllPeople()
        {
        DataTable dt = new DataTable();

        try
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_People_GetAllPeople", connection))
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

        public static bool GetPersonInfoByID(int? PersonID, ref string NationalNo, ref string FullName, ref short? CountryID, 
            ref DateTime DateOfBirth, ref byte Gender, ref string Address, ref string Phone, ref string Email, ref string ImagePath)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_People_GetPersonInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", (object)PersonID ?? DBNull.Value);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // The record was found successfully !
                                isFound = true;

                                NationalNo = (string)reader["NationalNo"];
                                FullName = (string)reader["FullName"];
                                CountryID = (reader["CountryID"] != DBNull.Value) ? (short?)reader["CountryID"] : null;
                                DateOfBirth = (DateTime)reader["DateOfBirth"];
                                Gender = (byte)reader["Gender"];
                                Address = (string)reader["Address"];
                                Phone = (string)reader["Phone"];
                                Email = (reader["Email"] != DBNull.Value) ? (string)reader["Email"] : null;
                                ImagePath = (reader["ImagePath"] != DBNull.Value) ? (string)reader["ImagePath"] : null;
                            

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

        public static bool GetPersonInfoByNationalNo(string NationalNo, ref int? PersonID, ref string FullName, ref short? CountryID,
            ref DateTime DateOfBirth, ref byte Gender, ref string Address, ref string Phone, ref string Email, ref string ImagePath)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_People_GetPersonInfoByNationalNo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NationalNo", NationalNo);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // The record was found successfully !
                                isFound = true;

                                PersonID = (reader["PersonID"] != DBNull.Value) ? (int?)reader["PersonID"] : null;
                                FullName = (string)reader["FullName"];
                                CountryID = (reader["CountryID"] != DBNull.Value) ? (short?)reader["CountryID"] : null;
                                DateOfBirth = (DateTime)reader["DateOfBirth"];
                                Gender = (byte)reader["Gender"];
                                Address = (string)reader["Address"];
                                Phone = (string)reader["Phone"];
                                Email = (reader["Email"] != DBNull.Value) ? (string)reader["Email"] : null;
                                ImagePath = (reader["ImagePath"] != DBNull.Value) ? (string)reader["ImagePath"] : null;


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

        public static int? AddNewPerson(string NationalNo, string FullName, short? CountryID, DateTime DateOfBirth, byte Gender,
            string Address, string Phone, string Email, string ImagePath)
        {
            int? PersonID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_People_AddNewPerson", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                    
                        command.Parameters.AddWithValue("@NationalNo", NationalNo);
                        command.Parameters.AddWithValue("@FullName", FullName);
                        command.Parameters.AddWithValue("@CountryID", (Object)CountryID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                        command.Parameters.AddWithValue("@Gender", Gender);
                        command.Parameters.AddWithValue("@Address", Address);
                        command.Parameters.AddWithValue("@Phone", Phone);
                        command.Parameters.AddWithValue("@Email", (object)Email ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ImagePath", (object)ImagePath ?? DBNull.Value);
                            

                        SqlParameter outputPersonIDParameter = new SqlParameter("@NewPersonID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputPersonIDParameter);
                        command.ExecuteNonQuery();

                        PersonID = (int)outputPersonIDParameter.Value;
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

            return PersonID;
        }

        public static bool UpdatePersonInfo(int? PersonID, string NationalNo, string FullName, short? CountryID, DateTime DateOfBirth,
            byte Gender, string Address, string Phone, string Email, string ImagePath)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_People_UpdatePersonInfo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PersonID", (object)PersonID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@NationalNo", NationalNo);
                        command.Parameters.AddWithValue("@FullName", FullName);
                        command.Parameters.AddWithValue("@CountryID", (Object)CountryID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                        command.Parameters.AddWithValue("@Gender", Gender);
                        command.Parameters.AddWithValue("@Address", Address);
                        command.Parameters.AddWithValue("@Phone", Phone);
                        command.Parameters.AddWithValue("@Email", (object)Email ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ImagePath", (object)ImagePath ?? DBNull.Value);
                            
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

        public static bool DeletePerson(int? PersonID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_People_DeletePerson", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
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

        public static bool DoesPersonExist(int? PersonID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_People_CheckIfPersonExistsByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", (object)PersonID ?? DBNull.Value);

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
        
        public static bool DoesPersonExist(string NationalNo)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_People_CheckIfPersonExistsByNationalNo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NationalNo", NationalNo);

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