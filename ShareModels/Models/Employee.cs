using System.Diagnostics.Eventing.Reader;

namespace ShareModels.Models
{
    public enum EmployeeRoles
    {
        Admin = 0,
        Approver = 1,
        ReadOnly = 2
    }
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string? FirstName { get; set; }
        public string? Surname { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public EmployeeRoles EmployeeRole { get; set; }
    }
    public class Employees
    {
        public List<Employee> EmployeeList { get; set; }
    }

}
