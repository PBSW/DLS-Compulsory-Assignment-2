using Microsoft.EntityFrameworkCore;
using Shared;

namespace MS_Infrastructure;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Primary Keys
        modelBuilder.Entity<Measurement>()
            .HasKey(m => m.Id)
            .HasName("PK_Id");
        
        // Auto Increment
        modelBuilder.Entity<Measurement>()
            .Property(m => m.Id)
            .ValueGeneratedOnAdd();
        
        // Required
        modelBuilder.Entity<Measurement>()
            .Property(m => m.PatientSSN)
            .IsRequired();
        
        modelBuilder.Entity<Measurement>()
            .Property(m => m.PatientSSN)
            .IsRequired();
        
        // Default Value
        modelBuilder.Entity<Measurement>()
            .Property(m => m.Seen)
            .HasDefaultValue(false);
        
        // Type
        modelBuilder.Entity<Measurement>()
            .Property(m => m.date)
            .HasColumnType("date");
        
        modelBuilder.Entity<Measurement>()
            .Property(m => m.Systolic)
            .HasColumnType("int");
        
        modelBuilder.Entity<Measurement>()
            .Property(m => m.Diastolic)
            .HasColumnType("int");
    }
    
    public DbSet<Measurement> Measurements { get; set; }
}