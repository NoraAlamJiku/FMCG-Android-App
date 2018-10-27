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
    public class StockInGateway
    {
        private SqlConnection _connection = new SqlConnection(
    WebConfigurationManager.ConnectionStrings["FMCG_Db"].ConnectionString);

        public int SaveStockin(StockIn stockIn)
        {
            string query = @"INSERT INTO [dbo].[tb_StockIn]
           ([CategoryId]
           ,[ItemId]
           ,[Quentity]
           ,[EntryDate]
           ,[EmployeeId])
     VALUES
           ('" + stockIn.CategoryId + "','" + stockIn.ItemId + "','" + stockIn.TotalQuantity + "','" +
                           stockIn.EntryDate + "','" + stockIn.EmployeeId + "')";

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

        public bool IsItemNameExists(StockIn stockIn)
        {
            try
            {
                string Query = "SELECT * FROM tb_StockIn WHERE (CategoryId = @CategoryId and ItemId = @ItemId)";
                SqlCommand Command = new SqlCommand(Query, _connection);
                _connection.Open();
                Command.Parameters.Clear();
                Command.Parameters.Add("CategoryId", SqlDbType.Int);
                Command.Parameters["CategoryId"].Value = stockIn.CategoryId;
                Command.Parameters.Add("ItemId", SqlDbType.Int);
                Command.Parameters["ItemId"].Value = stockIn.ItemId;
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