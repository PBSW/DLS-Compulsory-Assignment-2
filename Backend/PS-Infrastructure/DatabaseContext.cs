using Microsoft.EntityFrameworkCore;
using Shared;

namespace PS_Infrastructure;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Primary Keys
        modelBuilder.Entity<Patient>()
            .HasKey(p => p.Ssn);
        

        // Unique
        modelBuilder.Entity<Patient>()
            .HasIndex(p => p.Ssn)
            .IsUnique();
        
        modelBuilder.Entity<Patient>()
            .HasIndex(p => p.Mail)
            .IsUnique();
        
        //Properties
        modelBuilder.Entity<Patient>()
            .Property(p => p.Ssn)
            .IsRequired()
            .HasColumnType("varchar(10)");
        
        modelBuilder.Entity<Patient>()
            .Property(p => p.Mail)
            .IsRequired()
            .HasColumnType("varchar(128)");
        
        modelBuilder.Entity<Patient>()
            .Property(p => p.Name)
            .IsRequired()
            .HasColumnType("varchar(128)");
    }
    
    public DbSet<Patient> Patients { get; set; }
}