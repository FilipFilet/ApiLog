using Microsoft.EntityFrameworkCore;
using ApiLog.Models;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Organization> Organizations { get; set; } = null!;
    public DbSet<Log> Logs { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasMany(emp => emp.Employees) // An emplyee can have many employees
            .WithOne(emp => emp.Manager)    // An employee can have one manager.
            .HasForeignKey(emp => emp.ManagerId); // The foreign key is the manager id
                                                  // to clarify, HasForeignkey is still in the context of the employee entity, so it is the employee that has a foreign key to the manager.
                                                  // So we can say that HasForeignKey is in context of the one above.

        modelBuilder.Entity<Organization>()
            .HasMany(org => org.Employees) // An organization can have many employees
            .WithOne(emp => emp.Organization) // An employee/manager can have one organization
            .HasForeignKey(emp => emp.OrganizationId); // The foreign key is the organization id

        modelBuilder.Entity<Log>().ToTable("Logs"); // Maps/Binds the Log entity to the Logs table in the database.
    }
}