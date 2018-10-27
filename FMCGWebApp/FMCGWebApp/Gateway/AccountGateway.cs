using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using FMCGWebApp.ViewModels;

namespace FMCGWebApp.Gateway
{
    public class AccountGateway
    {
        private SqlConnection con = new SqlConnection(
    WebConfigurationManager.ConnectionStrings["FMCG_Db"].ConnectionString);

        public List<LoginInfo> AdminLogin(LoginInfo employee)
        {

            employee.UserTypeId = 1;
            string query1 = @"SELECT s.Id, s.EmployeeName, s.Email, p.Password, e.UserTypeId
  FROM tb_Employee s
  INNER JOIN tb_EmployeePassword p on p.EmployeeId = s.Id
  INNER JOIN tb_EmployeeRole e on e.EmployeeId = s.Id
  where Email = '" + employee.Email + "' and Password = '" + employee.Password + "' and UserTypeId = '" + employee.UserTypeId + "'";
            try
            {
                SqlCommand com = new SqlCommand(query1, con);
                con.Open();
                SqlDataReader reader = com.ExecuteReader();
                List<LoginInfo> userInfo = new List<LoginInfo>();
                while (reader.Read())
                {
                    LoginInfo logins = new LoginInfo();
                    logins.Id = (int)reader["Id"];
                    logins.EmployeeName = reader["EmployeeName"].ToString();
                    logins.Email = reader["Email"].ToString();
                    logins.UserTypeId = (int)reader["UserTypeId"];
                    userInfo.Add(logins);

                }
                reader.Close();
                con.Close();
                return userInfo;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                con.Close();
            }

        }

        public List<LoginInfo> AssistantLogin(LoginInfo employee)
        {

            employee.UserTypeId = 2;
            string query1 = @"SELECT s.Id, s.EmployeeName, s.Email, p.Password, e.UserTypeId
  FROM tb_Employee s
  INNER JOIN tb_EmployeePassword p on p.EmployeeId = s.Id
  INNER JOIN tb_EmployeeRole e on e.EmployeeId = s.Id
  where Email = '" + employee.Email + "' and Password = '" + employee.Password + "' and UserTypeId = '" + employee.UserTypeId + "'";
            try
            {
                SqlCommand com = new SqlCommand(query1, con);
                con.Open();
                SqlDataReader reader = com.ExecuteReader();
                List<LoginInfo> userInfo = new List<LoginInfo>();
                while (reader.Read())
                {
                    LoginInfo logins = new LoginInfo();
                    logins.Id = (int)reader["Id"];
                    logins.EmployeeName = reader["EmployeeName"].ToString();
                    logins.Email = reader["Email"].ToString();
                    logins.UserTypeId = (int)reader["UserTypeId"];
                    userInfo.Add(logins);

                }
                reader.Close();
                con.Close();
                return userInfo;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                con.Close();
            }

        }

        public List<LoginInfo> WhInchargeLogin(LoginInfo employee)
        {

            employee.UserTypeId = 3;
            string query1 = @"SELECT s.Id, s.EmployeeName, s.Email, p.Password, e.UserTypeId
  FROM tb_Employee s
  INNER JOIN tb_EmployeePassword p on p.EmployeeId = s.Id
  INNER JOIN tb_EmployeeRole e on e.EmployeeId = s.Id
  where Email = '" + employee.Email + "' and Password = '" + employee.Password + "' and UserTypeId = '" + employee.UserTypeId + "'";
            try
            {
                SqlCommand com = new SqlCommand(query1, con);
                con.Open();
                SqlDataReader reader = com.ExecuteReader();
                List<LoginInfo> userInfo = new List<LoginInfo>();
                while (reader.Read())
                {
                    LoginInfo logins = new LoginInfo();
                    logins.Id = (int)reader["Id"];
                    logins.EmployeeName = reader["EmployeeName"].ToString();
                    logins.Email = reader["Email"].ToString();
                    logins.UserTypeId = (int)reader["UserTypeId"];
                    userInfo.Add(logins);

                }
                reader.Close();
                con.Close();
                return userInfo;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                con.Close();
            }

        }

        public List<LoginInfo> SellForceLogin(LoginInfo employee)
        {

            employee.UserTypeId = 4;
            string query1 = @"SELECT s.Id, s.EmployeeName, s.Email, p.Password, e.UserTypeId
  FROM tb_Employee s
  INNER JOIN tb_EmployeePassword p on p.EmployeeId = s.Id
  INNER JOIN tb_EmployeeRole e on e.EmployeeId = s.Id
  where Email = '" + employee.Email + "' and Password = '" + employee.Password + "' and UserTypeId = '" + employee.UserTypeId + "'";
            try
            {
                SqlCommand com = new SqlCommand(query1, con);
                con.Open();
                SqlDataReader reader = com.ExecuteReader();
                List<LoginInfo> userInfo = new List<LoginInfo>();
                while (reader.Read())
                {
                    LoginInfo logins = new LoginInfo();
                    logins.Id = (int)reader["Id"];
                    logins.EmployeeName = reader["EmployeeName"].ToString();
                    logins.Email = reader["Email"].ToString();
                    logins.UserTypeId = (int)reader["UserTypeId"];
                    userInfo.Add(logins);

                }
                reader.Close();
                con.Close();
                return userInfo;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                con.Close();
            }

        }

        public List<LoginInfo> GetUserRole(int id)
        {

            string query1 = @"SELECT s.Id, s.EmployeeName, s.Email, p.Password, e.UserTypeId
  FROM tb_Employee s
  INNER JOIN tb_EmployeePassword p on p.EmployeeId = s.Id
  INNER JOIN tb_EmployeeRole e on e.EmployeeId = s.Id
  where s.Id = '" + id + "'";
            try
            {
                SqlCommand com = new SqlCommand(query1, con);
                con.Open();
                SqlDataReader reader = com.ExecuteReader();
                List<LoginInfo> userInfo = new List<LoginInfo>();
                while (reader.Read())
                {
                    LoginInfo logins = new LoginInfo();
                    logins.Id = (int)reader["Id"];
                    logins.EmployeeName = reader["EmployeeName"].ToString();
                    logins.Email = reader["Email"].ToString();
                    logins.UserTypeId = (int)reader["UserTypeId"];
                    userInfo.Add(logins);

                }
                reader.Close();
                return userInfo;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                con.Close();
            }

        }
    }
}