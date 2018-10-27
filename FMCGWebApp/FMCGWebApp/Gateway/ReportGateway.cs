using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using FMCGWebApp.ViewModels;

namespace FMCGWebApp.Gateway
{
    public class ReportGateway
    {
        private SqlConnection _connection = new SqlConnection(
    WebConfigurationManager.ConnectionStrings["FMCG_Db"].ConnectionString);

        public List<CompareOrderandStock> GetNeedandStockInfo()
        {

            string query1 = @"SELECT i.ItemName
,c.CategoryName
,LastStock = s.Stock- isnull(e.sell,0)
,n.Need
FROM tb_Stock s
LEFT JOIN tb_Need n on n.ItemId = s.ItemId
Full Outer JOIN tb_Sell e on e.ItemId = s.ItemId
Full OUTER JOIN tb_Item i on i.Id = s.ItemId
Full Outer JOIN tb_category c on c.Id = i.CategoryId
ORDER BY n.Need DESC";
            try
            {
                SqlCommand command = new SqlCommand(query1, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<CompareOrderandStock> compareOrderandStocks = new List<CompareOrderandStock>();
                int number = 1;
                while (reader.Read())
                {
                    CompareOrderandStock compareOrderandStock = new CompareOrderandStock();
                    compareOrderandStock.Id = number;
                    compareOrderandStock.CategoryName = reader["CategoryName"].ToString();
                    compareOrderandStock.ItemName = reader["ItemName"].ToString();
                    compareOrderandStock.Need = reader["Need"].ToString();
                    compareOrderandStock.Stock = reader["LastStock"].ToString();

                    compareOrderandStocks.Add(compareOrderandStock);

                    number++;

                }
                reader.Close();
                return compareOrderandStocks;
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