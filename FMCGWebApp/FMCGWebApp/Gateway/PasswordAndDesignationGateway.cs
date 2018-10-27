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
    public class PasswordAndDesignationGateway
    {
        private SqlConnection _connection = new SqlConnection(
    WebConfigurationManager.ConnectionStrings["FMCG_Db"].ConnectionString);

        public List<UserType> GetUserType()
        {
            string query = @"SELECT [Id]
      ,[RoleName]
  FROM [dbo].[tb_UserRole]";
            try
            {
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<UserType> userTypes = new List<UserType>();
                while (reader.Read())
                {
                    UserType userType = new UserType();
                    userType.Id = (int)reader["Id"];
                    userType.UserTypeName = reader["RoleName"].ToString();
                    userTypes.Add(userType);
                }
                reader.Close();
                return userTypes;
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

        public bool IsPasswordSet(EmployeePassword employee)
        {
            try
            {
                string Query = "SELECT * FROM tb_EmployeePassword WHERE (EmployeeId = @EmployeeId)";
                SqlCommand Command = new SqlCommand(Query, _connection);
                _connection.Open();
                Command.Parameters.Clear();
                Command.Parameters.Add("EmployeeId", SqlDbType.Int);
                Command.Parameters["EmployeeId"].Value = employee.EmployeeId;
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

        public int SetEmployeePassword(EmployeePassword employee)
        {
            string query = @"INSERT INTO [dbo].[tb_EmployeePassword]
           ([EmployeeId]
           ,[Password])
     VALUES
           ('" + employee.EmployeeId + "','" + employee.Password + "')";
            try
            {
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                int rowAffected = command.ExecuteNonQuery();
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

        public List<Employee> GetEmployeeById(int id)
        {

            string query1 = @"Select s.Id, s.EmployeeName, s.Email, p.DesignationName
from tb_Employee s
inner join tb_Designation p on p.Id = s.DesignationId
where s.Id = '" + id + "'";
            try
            {
                SqlCommand com = new SqlCommand(query1, _connection);
                _connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                List<Employee> userInfo = new List<Employee>();
                while (reader.Read())
                {
                    Employee logins = new Employee();
                    logins.Id = (int)reader["Id"];
                    logins.EmployeeName = reader["EmployeeName"].ToString();
                    logins.Email = reader["Email"].ToString();
                    logins.FatherName = reader["DesignationName"].ToString();
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
                _connection.Close();
            }

        }

        public bool IsUserRoleSet(EmployeeUserType userType)
        {
            try
            {
                string Query = "SELECT * FROM tb_EmployeeRole WHERE (EmployeeId = @EmployeeId and UserTypeId = @UserTypeId)";
                SqlCommand Command = new SqlCommand(Query, _connection);
                _connection.Open();
                Command.Parameters.Clear();
                Command.Parameters.Add("EmployeeId", SqlDbType.Int);
                Command.Parameters["EmployeeId"].Value = userType.EmployeeId;
                Command.Parameters.Add("UserTypeId", SqlDbType.Int);
                Command.Parameters["UserTypeId"].Value = userType.UserTypeId;
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

        public int SetEmployeeUserType(EmployeeUserType employee)
        {
            string query = @"INSERT INTO [dbo].[tb_EmployeeRole]
           ([EmployeeId]
           ,[UserTypeId])
     VALUES
           ('" + employee.EmployeeId + "','" + employee.UserTypeId + "')";
            try
            {
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                int rowAffected = command.ExecuteNonQuery();
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
    }
}