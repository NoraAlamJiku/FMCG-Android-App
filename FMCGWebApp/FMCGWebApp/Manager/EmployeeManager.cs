using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FMCGWebApp.Gateway;
using FMCGWebApp.Models;

namespace FMCGWebApp.Manager
{
    public class EmployeeManager
    {
        private EmployeeGateway _employee = new EmployeeGateway();
        public string SaveEmployee(Employee employee)
        {
            if (_employee.IsEmailExists(employee))
            {
                return "Email Already Exists.";
            }
            if (_employee.SaveEmployee(employee) > 0)
            {
                return "Employee Saved Successfully";
            }

            return "Employee Name Save Faild";
        }

        public List<Gender> GetGenderList()
        {
            return _employee.GetGenderList();
        }

        public List<Disignation> GetDesignationList()
        {
            return _employee.GetDesignationList();
        }
    }
}