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
    public class ShopInfoGateway
    {
        private SqlConnection _connection = new SqlConnection(
           WebConfigurationManager.ConnectionStrings["FMCG_Db"].ConnectionString);
        public int SaveShopInfo(ShopInfo shopInfo)
        {
            string query = "INSERT INTO tb_ShopInfo (ShopName,ShopkeeperName,AreaId,EmployeeId,PhoneNumber) Values('" + shopInfo.ShopName + "','" + shopInfo.ShopkeeperName + "','" + shopInfo.AreaId + "','" + shopInfo.EmployeeId + "','" + shopInfo.PhoneNumber + "')";
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

        public List<Area> GetAreaList()
        {
            string query = "SELECT * FROM tb_Area";
            try
            {
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader dr = command.ExecuteReader();
                List<Area> arealist = new List<Area>();
                while (dr.Read())
                {
                    Area area = new Area();
                    area.Id = (int)dr["Id"];
                    area.AreaName = dr["AreaName"].ToString();
                    arealist.Add(area);
                }
                dr.Close();
                return arealist;
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


        public List<Employee> GetAllEmployees()
        {
            string query = "SELECT *FROM tb_Employee";
            try
            {
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                var listOfEmployee = new List<Employee>();
                while (reader.Read())
                {
                    var employee = new Employee
                    {
                        Id = (int)reader["Id"],
                        EmployeeName = reader["EmployeeName"].ToString()
                    };
                    listOfEmployee.Add(employee);
                }
                reader.Close();
                return listOfEmployee;
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
        public bool IsShopNameExists(ShopInfo shop)
        {
            try
            {
                string Query = "SELECT * FROM tb_ShopInfo WHERE (ShopName = @ShopName)";
                SqlCommand Command = new SqlCommand(Query, _connection);
                _connection.Open();
                Command.Parameters.Clear();
                Command.Parameters.Add("ShopName", SqlDbType.VarChar);
                Command.Parameters["ShopName"].Value = shop.ShopName;
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