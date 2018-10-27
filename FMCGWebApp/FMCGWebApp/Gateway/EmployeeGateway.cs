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
    public class EmployeeGateway
    {
        private SqlConnection _connection = new SqlConnection(
         WebConfigurationManager.ConnectionStrings["FMCG_Db"].ConnectionString);
        public int SaveEmployee(Employee employee)
        {
            string query = @"INSERT INTO [dbo].[tb_Employee]
           ([EmployeeName]
           ,[FatherName]
           ,[MotherName]
           ,[GenderId]
           ,[Address]
           ,[NID_Number]
           ,[PhoneNumber]
           ,[Email]
           ,[DesignationId])
     VALUES
           ('" + employee.EmployeeName + "', '" + employee.FatherName + "','" + employee.MotherName + "','" +
                           employee.GenderId + "','" + employee.Address + "','" + employee.NationalIdNumber + "','" +
                           employee.PhoneNumber + "','" + employee.Email + "', '" + employee.DesignationId + "')";

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

        public bool IsEmailExists(Employee employee)
        {
            try
            {
                string Query = "SELECT * FROM tb_Employee WHERE (Email = @Email)";

                SqlCommand Command = new SqlCommand(Query, _connection);
                _connection.Open();
                Command.Parameters.Clear();
                Command.Parameters.Add("Email", SqlDbType.VarChar);
                Command.Parameters["Email"].Value = employee.Email;
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

        public List<Gender> GetGenderList()
        {

            string query = @"SELECT [Id]
      ,[GenderName]
  FROM [dbo].[tb_Gender]";
            try
            {
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Gender> genderList = new List<Gender>();
                while (reader.Read())
                {
                    Gender gender = new Gender();
                    gender.Id = (int)reader["Id"];
                    gender.GenderName = reader["GenderName"].ToString();
                    genderList.Add(gender);

                }
                reader.Close();
                return genderList;
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

        public List<Disignation> GetDesignationList()
        {
            string query = @"SELECT [Id]
      ,[DesignationName]
  FROM [dbo].[tb_Designation]";
            try
            {
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Disignation> designationList = new List<Disignation>();
                while (reader.Read())
                {
                    Disignation disignation = new Disignation();
                    disignation.Id = (int)reader["Id"];
                    disignation.DisignationName = reader["DesignationName"].ToString();
                    designationList.Add(disignation);
                }
                reader.Close();
                return designationList;
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