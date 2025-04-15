namespace ApiLog.Models
{
    public class Organization
    {
        public int Id { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public List<Employee>? Employees { get; set; } = null;
    }

    public class OrganizationDTO
    {
        public string OrganizationName { get; set; } = string.Empty;
    }

    public class OrganizationGetDTO
    {
        public int Id { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public List<string>? EmployeeNames { get; set; } = null;
    }
}