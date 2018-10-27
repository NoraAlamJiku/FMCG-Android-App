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
    public class ItemGateway
    {
        private SqlConnection _connection = new SqlConnection(
WebConfigurationManager.ConnectionStrings["FMCG_Db"].ConnectionString);
        public int SaveItem(Item item)
        {
            string query = @"INSERT INTO [dbo].[tb_Item]
           ([ItemName]
           ,[CategoryId])
     VALUES
           ('" + item.ItemName + "', '" + item.CategoryId + "')";

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

        public bool IsItemNameExists(Item item)
        {
            try
            {
                string Query = "SELECT * FROM tb_Item WHERE (CategoryId = @CategoryId and ItemName = @ItemName )";
                SqlCommand Command = new SqlCommand(Query, _connection);
                _connection.Open();
                Command.Parameters.Clear();
                Command.Parameters.Add("CategoryId", SqlDbType.Int);
                Command.Parameters["CategoryId"].Value = item.CategoryId;
                Command.Parameters.Add("ItemName", SqlDbType.VarChar);
                Command.Parameters["ItemName"].Value = item.ItemName;
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

        public List<Category> GetAllCategory()
        {

            string query1 = @"SELECT [Id]
      ,[CategoryName]
  FROM [dbo].[tb_Category]";
            try
            {
                SqlCommand command = new SqlCommand(query1, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Category> categoryList = new List<Category>();
                while (reader.Read())
                {
                    Category category = new Category();
                    category.Id = (int)reader["Id"];
                    category.CategoryName = reader["CategoryName"].ToString();
                    categoryList.Add(category);

                }
                reader.Close();
                return categoryList;
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