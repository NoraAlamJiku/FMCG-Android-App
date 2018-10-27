using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using FMCGWebApp.Models;
using FMCGWebApp.ViewModels;

namespace FMCGWebApp.Gateway
{
    public class StockOutGateway
    {
        private SqlConnection _connection = new SqlConnection(
    WebConfigurationManager.ConnectionStrings["FMCG_Db"].ConnectionString);

        public int ConfirmSellOrder(Stockout stockout)
        {
            string query = @"UPDATE [dbo].[tb_StockOut] SET [Status] = '" + stockout.Status + "'WHERE Id = '" +
                stockout.Id + "'";

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

        public List<Stockout> SellOrderList()
        {

            string query1 = @"SELECT [Id] FROM [dbo].[tb_StockOut] Where Status = '" + "Ordered" + "'";
            try
            {
                SqlCommand command = new SqlCommand(query1, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Stockout> stockouts = new List<Stockout>();
                while (reader.Read())
                {
                    Stockout stockout = new Stockout();
                    stockout.Id = (int)reader["Id"];
                    stockouts.Add(stockout);

                }
                reader.Close();
                return stockouts;
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
        public List<SellOrderInfo> GetAllSellOrder(int id)
        {

            string query1 = @"SELECT s.[Id]
      ,s.Quentity
      ,c.CategoryName
	  ,i.ItemName
	  ,h.ShopName
	  ,a.AreaName
  FROM [FMCG_Db].[dbo].[tb_StockOut] s
  inner join tb_Category c on s.CategoryId = c.Id
  inner join tb_Item i on s.ItemId = i.Id
  inner join tb_ShopInfo h on s.ShopId = h.Id
  inner join tb_Area a on s.AreaId = a.Id
  Where s.Id = '" + id + "'";
            try
            {
                SqlCommand command = new SqlCommand(query1, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<SellOrderInfo> sellorders = new List<SellOrderInfo>();
                while (reader.Read())
                {
                    SellOrderInfo sellorder = new SellOrderInfo();
                    sellorder.Id = (int)reader["Id"];
                    sellorder.CategoryName = reader["CategoryName"].ToString();
                    sellorder.ItemName = reader["ItemName"].ToString();
                    sellorder.Quentity = (int)reader["Quentity"];
                    sellorder.ShopName = reader["ShopName"].ToString();
                    sellorder.AreaName = reader["AreaName"].ToString();
                    sellorders.Add(sellorder);

                }
                reader.Close();
                return sellorders;
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