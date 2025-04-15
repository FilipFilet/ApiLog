using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLog.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public bool IsManager { get; set; }

        public int? ManagerId { get; set; } = null;
        public Employee? Manager { get; set; } = null; // Manager of this employee
        public List<Employee>? Employees { get; set; } = null; // Employees managed by this employee
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; } // Organization of this employee

    }

    public class EmployeeDTO
    {
        public string EmployeeName { get; set; } = string.Empty;
        public bool IsManager { get; set; }
        public int? ManagerId { get; set; } = null;
        public int OrganizationId { get; set; }
    }

    public class EmployeeGetDTO
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public bool IsManager { get; set; }
        public string? ManagerName { get; set; } = string.Empty; // Manager of this employee
        public string OrganizationName { get; set; } = string.Empty; // Organization of this employee
        public List<SubEmployeeGetDTO>? Employees { get; set; } = null; // Employees managed by this employee
    }

    public class SubEmployeeGetDTO
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public bool IsManager { get; set; }
        public string? ManagerName { get; set; } = string.Empty; // Manager of this employee
        public string OrganizationName { get; set; } = string.Empty; // Organization of this employee
    }
}