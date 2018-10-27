using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FMCGWebApp.Gateway;
using FMCGWebApp.Models;

namespace FMCGWebApp.Manager
{
    public class PasswordAndDesignationManager
    {
        private PasswordAndDesignationGateway _passwordAndDesignation = new PasswordAndDesignationGateway();
        private ShopInfoGateway _shopInfoGateway = new ShopInfoGateway();
        public List<Employee> ListOfEmployee()
        {
            return _shopInfoGateway.GetAllEmployees();
        }

        public List<UserType> GetUserType()
        {
            return _passwordAndDesignation.GetUserType();
        }

        public string SetEmployeePassword(EmployeePassword employee)
        {
            if (_passwordAndDesignation.IsPasswordSet(employee))
            {
                return "Password set alrady.";
            }
            if (_passwordAndDesignation.SetEmployeePassword(employee) > 0)
            {
                return "Password Saved Successfully!";
            }

            return "Password Save Faild";
           // return _passwordAndDesignation.SetEmployeePassword(employee);
        }

        public List<Employee> GetEmployeeById(int id)
        {
            return _passwordAndDesignation.GetEmployeeById(id);
        }

        public string SetEmployeeUserType(EmployeeUserType employee)
        {
            if (_passwordAndDesignation.IsUserRoleSet(employee))
            {
                return "User Role Set alrady.";
            }
            if (_passwordAndDesignation.SetEmployeeUserType(employee) > 0)
            {
                return "User Type Save Successfully!";
            }

            return "User Type Save Faild";

        }
    }
}