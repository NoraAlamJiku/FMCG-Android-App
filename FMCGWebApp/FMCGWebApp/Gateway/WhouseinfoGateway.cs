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
    public class WhouseinfoGateway
    {
        private SqlConnection _connection = new SqlConnection(
          WebConfigurationManager.ConnectionStrings["FMCG_Db"].ConnectionString);
        public int SaveWhouse(W_h_info wHInfo)
        {
            string query = @"INSERT INTO [dbo].[tb_WH_Info]
           ([WhName]
           ,[Location]
           ,[Capacity]
           ,[EmployeeId])
     VALUES
           ('" + wHInfo.WhName + "', '" + wHInfo.Location + "', '" + wHInfo.Capacity + "', '" + wHInfo.EmployeeId + "')";

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

        public bool IsWhouseExists(W_h_info wHInfo)
        {
            try
            {
                string Query = "SELECT * FROM tb_WH_Info WHERE (WhName = @WhName and Location = @Location)";
                SqlCommand Command = new SqlCommand(Query, _connection);
                _connection.Open();
                Command.Parameters.Clear();
                Command.Parameters.Add("WhName", SqlDbType.VarChar);
                Command.Parameters["WhName"].Value = wHInfo.WhName;
                Command.Parameters.Add("Location", SqlDbType.VarChar);
                Command.Parameters["Location"].Value = wHInfo.Location;
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