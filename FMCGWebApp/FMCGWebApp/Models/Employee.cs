using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMCGWebApp.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Email { get; set; }
        public int DesignationId { get; set; }
        public int GenderId { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalIdNumber { get; set; }
        public string Address { get; set; }
    }
}