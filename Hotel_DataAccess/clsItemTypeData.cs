using System;
using System.Data;
using System.Data.SqlClient;

namespace HotelDatabase_DataAccess
{
    public class clsItemTypeData
    {

        public static DataTable GetAllItemTypes()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_ItemTypes_GetAllItemTypes", connection))
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

        public static bool GetItemTypeInfoByID(int? ItemTypeID, ref string ItemTypeName)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_ItemTypes_GetItemTypeInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ItemTypeID", (object)ItemTypeID ?? DBNull.Value);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // The record was found successfully !
                                isFound = true;

                                ItemTypeName = (string)reader["ItemTypeName"];

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

        public static bool GetItemTypeInfoByName(string ItemTypeName, ref int? ItemTypeID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_ItemTypes_GetItemTypeInfoByName", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ItemTypeName", (object)ItemTypeName ?? DBNull.Value);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // The record was found successfully !
                                isFound = true;

                                ItemTypeID = (reader["ItemTypeID"] != DBNull.Value) ? (byte?)Convert.ToByte(reader["ItemTypeID"]) : null;

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

        public static int? AddNewItemType(string ItemTypeName)
        {
            int? ItemTypeID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_ItemTypes_AddNewItemType", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                    
                            command.Parameters.AddWithValue("@ItemTypeName", ItemTypeName);
                            

                        SqlParameter outputItemTypeIDParameter = new SqlParameter("@NewItemTypeID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputItemTypeIDParameter);
                        command.ExecuteNonQuery();

                        ItemTypeID = (int)outputItemTypeIDParameter.Value;
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

            return ItemTypeID;
        }

        public static bool UpdateItemTypeInfo(int? ItemTypeID, string ItemTypeName)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_ItemTypes_UpdateItemTypeInfo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ItemTypeID", (object)ItemTypeID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ItemTypeName", ItemTypeName);

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

        public static bool DeleteItemType(int? ItemTypeID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_ItemTypes_DeleteItemType", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ItemTypeID", (object)ItemTypeID ?? DBNull.Value);

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

        public static bool DoesItemTypeExist(string ItemTypeName)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_ItemTypes_CheckIfItemTypeExistsByName", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ItemTypeName", ItemTypeName);

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