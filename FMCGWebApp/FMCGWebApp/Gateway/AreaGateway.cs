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
    public class AreaGateway
    {
        private SqlConnection _connection = new SqlConnection(
WebConfigurationManager.ConnectionStrings["FMCG_Db"].ConnectionString);
        public int SaveArea(Area area)
        {
            string query = @"INSERT INTO [dbo].[tb_Area]
           ([AreaName]
           ,[AreaCode])
     VALUES
           ('" + area.AreaName + "', '" + area.AreaCode + "')";

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

        public bool IsAreaNameExists(Area area)
        {
            try
            {
                string Query = "SELECT * FROM tb_Area WHERE (AreaName = @AreaName and AreaCode = @AreaCode)";
                SqlCommand Command = new SqlCommand(Query, _connection);
                _connection.Open();
                Command.Parameters.Clear();
                Command.Parameters.Add("AreaName", SqlDbType.VarChar);
                Command.Parameters["AreaName"].Value = area.AreaName;
                Command.Parameters.Add("AreaCode", SqlDbType.VarChar);
                Command.Parameters["AreaCode"].Value = area.AreaCode;
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