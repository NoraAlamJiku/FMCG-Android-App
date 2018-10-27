using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using FMCGWebApp.Models;

namespace FMCGWebApp.Gateway
{
    public class CategoryGateway
    {
        private SqlConnection _connection = new SqlConnection(
            WebConfigurationManager.ConnectionStrings["FMCG_Db"].ConnectionString);
        public int SaveCategory(Category category)
        {
            string query = @"INSERT INTO [dbo].[tb_Category]
           ([CategoryName])
     VALUES
           ('" + category.CategoryName + "')";

            try
            {
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                int rowAffected = command.ExecuteNonQuery();
                _connection.Close();

                return rowAffected;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                _connection.Close();
            }


        }

        public bool IsCategoryNameExists(Category category)
        {
            try
            {
                string Query = "SELECT * FROM tb_Category WHERE (CategoryName = @CategoryName)";
                SqlCommand Command = new SqlCommand(Query, _connection);
                _connection.Open();
                Command.Parameters.Clear();
                Command.Parameters.Add("CategoryName", SqlDbType.VarChar);
                Command.Parameters["CategoryName"].Value = category.CategoryName;
                SqlDataReader Reader = Command.ExecuteReader();
                Reader.Read();
                bool isExist = Reader.HasRows;
                Reader.Close();
                return isExist;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                _connection.Close();
            }

        }

    }
}