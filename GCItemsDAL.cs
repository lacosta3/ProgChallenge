using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using Entity;

namespace DAL
{
    public class GCItemsDAL
    {
        public DataSet GetSearchParams()
        {
            string connStr = ConfigurationManager.ConnectionStrings["DBConnectSpacemgt"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            SqlCommand command = new SqlCommand();

            try
            {
                command.CommandTimeout = 300;
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GC_GetSearchParams";

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet ds = new DataSet();

                adapter.Fill(ds);
                return ds;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
                if (command != null)
                {
                    command.Dispose();
                }
            }
        }
        /// <summary>
        /// Get User Profile
        /// </summary>
        /// <returns></returns>
        public DataTable GetUserAccessRole(string _PartnerID)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DBConnectSpacemgt"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            SqlCommand command = new SqlCommand();
            try
            {
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GC_GetUserAccesRole";
                command.Parameters.AddWithValue("@User", _PartnerID);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable datatbl = new DataTable();
                adapter.Fill(datatbl);

                return datatbl;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
                if (command != null)
                {
                    command.Dispose();
                }
            }
        }
        public DataTable GetProducts(string _desc, string _dept, string _solddate)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DBConnectSpacemgt"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            SqlCommand command = new SqlCommand();
            try
            {
                command.CommandTimeout = 300;
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GC_GetProductList";
                command.Parameters.AddWithValue("@DESCRIPTION", _desc);
                command.Parameters.AddWithValue("@DEPT", _dept);
                command.Parameters.AddWithValue("@LASTSOLD", _solddate);
 
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable datatbl = new DataTable();

                adapter.Fill(datatbl);
                return datatbl;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
                if (command != null)
                {
                    command.Dispose();
                }
            }
        }
        public DataTable GetCategoryImages(string _uid, string _cat)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DBConnectSpacemgt"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            SqlCommand command = new SqlCommand();
            try
            {
                command.CommandTimeout = 300;
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GC_GetCategoryImages";
                command.Parameters.AddWithValue("@UID", _uid);
                command.Parameters.AddWithValue("@CATEGORY", _cat);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable datatbl = new DataTable();

                adapter.Fill(datatbl);
                return datatbl;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
                if (command != null)
                {
                    command.Dispose();
                }
            }
        }
        /// <summary>
        /// Update A Product
        /// </summary>
        /// <returns></returns>
        public bool UpdateProduct(Product _product)
        {
            bool isSuccuss = false;
            string connStr = ConfigurationManager.ConnectionStrings["DBConnectSpacemgt"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            SqlCommand command = new SqlCommand();
            try
            {
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GC_UpdateProduct";
                command.Parameters.AddWithValue("@ID", _product.ID);
                command.Parameters.AddWithValue("@UPDATED_BY", _product.UserName);
                command.Parameters.AddWithValue("@DESCRIPTION", _product.Description);
                command.Parameters.AddWithValue("@DEPARTMENT", _product.Department);
                command.Parameters.AddWithValue("@LASTSOLD", _product.LastSold);
                command.Parameters.AddWithValue("@SHELFLIFE", _product.ShelfLife);
                command.Parameters.AddWithValue("@PRICE", _product.Price);
                command.Parameters.AddWithValue("@UNIT", _product.Unit);
                command.Parameters.AddWithValue("@XFOR", _product.xFor);
                command.Parameters.AddWithValue("@COST", _product.Cost);

                connection.Open();
                command.ExecuteScalar();

                isSuccuss = true;
                connection.Close();
                return isSuccuss;
            }
            catch (Exception e)
            {
                isSuccuss = false;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
                if (command != null)
                {
                    command.Dispose();
                }
            }
            return isSuccuss;
        }
        /// <summary>
        /// Add A Product
        /// </summary>
        /// <returns></returns>
        public bool AddNewProduct(Product _product)
        {
            bool isSuccuss = false;
            string connStr = ConfigurationManager.ConnectionStrings["DBConnectSpacemgt"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            SqlCommand command = new SqlCommand();
            try
            {
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GC_AddNewProduct";
                command.Parameters.AddWithValue("@UPC", _product.UPC);
                command.Parameters.AddWithValue("@UPDATED_BY", _product.UserName);
                command.Parameters.AddWithValue("@PARTNERID", _product.UserID);
                command.Parameters.AddWithValue("@DESCRIPTION", _product.Description);
                command.Parameters.AddWithValue("@DEPARTMENT", _product.Department);
                command.Parameters.AddWithValue("@LASTSOLD", _product.LastSold);
                command.Parameters.AddWithValue("@SHELFLIFE", _product.ShelfLife);
                command.Parameters.AddWithValue("@PRICE", _product.Price);
                command.Parameters.AddWithValue("@UNIT", _product.Unit);
                command.Parameters.AddWithValue("@XFOR", _product.xFor);
                command.Parameters.AddWithValue("@COST", _product.Cost);

                connection.Open();
                command.ExecuteScalar();

                isSuccuss = true;
                connection.Close();
                return isSuccuss;
            }
            catch (Exception e)
            {
                isSuccuss = false;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
                if (command != null)
                {
                    command.Dispose();
                }
            }
            return isSuccuss;
        }
        /// <summary>
        /// Add A Image
        /// </summary>
        /// <returns></returns>
        public bool AddNewImage(Media _media)
        {
            bool isSuccuss = false;
            string connStr = ConfigurationManager.ConnectionStrings["DBConnectSpacemgt"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            SqlCommand command = new SqlCommand();
            try
            {
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GC_AddNewImage";
                command.Parameters.AddWithValue("@CATEGORY", _media.Category);
                command.Parameters.AddWithValue("@PARTNERID", _media.UserID);
                command.Parameters.AddWithValue("@URL", _media.URL);

                connection.Open();
                command.ExecuteScalar();

                isSuccuss = true;
                connection.Close();
                return isSuccuss;
            }
            catch (Exception e)
            {
                isSuccuss = false;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
                if (command != null)
                {
                    command.Dispose();
                }
            }
            return isSuccuss;
        }
        /// <summary>
        /// Delete a Product
        /// </summary>
        /// <returns></returns>
        public bool DelProduct(int _id, string _user)
        {
            bool isSuccuss = false;
            string connStr = ConfigurationManager.ConnectionStrings["DBConnectSpacemgt"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            SqlCommand command = new SqlCommand();
            try
            {
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GC_DeleteProduct";
                command.Parameters.AddWithValue("@ID", _id);
                command.Parameters.AddWithValue("@UPDATED_BY", _user);

                connection.Open();
                command.ExecuteScalar();

                isSuccuss = true;
                connection.Close();
                return isSuccuss;
            }
            catch (Exception e)
            {
                isSuccuss = false;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
                if (command != null)
                {
                    command.Dispose();
                }
            }
            return isSuccuss;
        }
        /// <summary>
        /// Register User Profile  
        /// </summary>
        /// <returns></returns>
        public bool AddUserProfile(DataTable _profile)
        {
            bool isSuccuss = false;
            string connStr = ConfigurationManager.ConnectionStrings["DBConnectSpacemgt"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sp_GC_RegisterNewUser";
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@tbl", _profile);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        isSuccuss = true;

                    }
                    catch (Exception e)
                    {
                        isSuccuss = false;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }

            return isSuccuss;
        }
        /// <summary>
        /// Delete A User
        /// </summary>
        /// <returns></returns>
        public string GetUserRole(string _user)
        {
            string err = "true";
            string connStr = ConfigurationManager.ConnectionStrings["DBConnectSpacemgt"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            SqlCommand command = new SqlCommand();
            try
            {
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GC_GetUserRole";
                command.Parameters.AddWithValue("@User", _user);

                connection.Open();
                string result = (string)command.ExecuteScalar();
                connection.Close();
                return result;
            }
            catch (Exception e)
            {
                return err = "false";

            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
                if (command != null)
                {
                    command.Dispose();
                }
            }
        }
        /// <summary>
        /// Add Product File 
        /// </summary>
        /// <returns></returns>
        public int AddProductFile(DataTable _dt)
        {
            int result = 0;
            string connStr = ConfigurationManager.ConnectionStrings["DBConnectSpacemgt"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sp_GC_AddProductFile";
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@tbl", _dt);
                        con.Open();

                        return result = (int?)cmd.ExecuteScalar() ?? 0;
                    }
                    catch (Exception e)
                    {
                        return -1;
                    }
                    finally
                    {
                        if (con != null)
                        {
                            con.Dispose();
                        }
                        if (cmd != null)
                        {
                            cmd.Dispose();
                        }
                    }
                }
            }
        }


    }

}