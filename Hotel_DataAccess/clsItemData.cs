using System;
using System.Data;
using System.Data.SqlClient;

namespace HotelDatabase_DataAccess
{
    public class clsItemData
    {

        public static DataTable GetAllItems()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Items_GetAllItems", connection))
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

        public static bool GetItemInfoByID(int? ItemID, ref int? ItemTypeID, ref string ItemName, ref float ItemPrice, ref string Description, ref string ItemImagePath)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Items_GetItemInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ItemID", (object)ItemID ?? DBNull.Value);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // The record was found successfully !
                                isFound = true;

                                ItemTypeID = (reader["ItemTypeID"] != DBNull.Value) ? (int?)(reader["ItemTypeID"]) : null;
                                ItemName = (string)reader["ItemName"];
                                ItemPrice = Convert.ToSingle(reader["ItemPrice"]);
                                Description = (reader["Description"] != DBNull.Value) ? (string)reader["Description"] : null;
                                ItemImagePath = (reader["ItemImagePath"] != DBNull.Value) ? (string)reader["ItemImagePath"] : null;


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

        public static int? AddNewItem(int? ItemTypeID, string ItemName, float ItemPrice, string Description, string ItemImagePath)
        {
            int? ItemID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Items_AddNewItem", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ItemTypeID", (object)ItemTypeID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ItemName", ItemName);
                        command.Parameters.AddWithValue("@ItemPrice", ItemPrice);
                        command.Parameters.AddWithValue("@Description", (object)Description ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ItemImagePath", (object)ItemImagePath ?? DBNull.Value);


                        SqlParameter outputItemIDParameter = new SqlParameter("@NewItemID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputItemIDParameter);
                        command.ExecuteNonQuery();

                        ItemID = (int)outputItemIDParameter.Value;
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

            return ItemID;
        }

        public static bool UpdateItemInfo(int? ItemID, int? ItemTypeID, string ItemName, float ItemPrice, string Description, string ItemImagePath)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Items_UpdateItemInfo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ItemID", (object)ItemID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ItemTypeID", (object)ItemTypeID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ItemName", ItemName);
                        command.Parameters.AddWithValue("@ItemPrice", ItemPrice);
                        command.Parameters.AddWithValue("@Description", (object)Description ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ItemImagePath", (object)ItemImagePath ?? DBNull.Value);


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

        public static bool DeleteItem(int? ItemID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Items_DeleteItem", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ItemID", (object)ItemID ?? DBNull.Value);

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

        public static bool DoesItemExist(string ItemName)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_Items_CheckIfItemExists", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ItemName", ItemName);

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