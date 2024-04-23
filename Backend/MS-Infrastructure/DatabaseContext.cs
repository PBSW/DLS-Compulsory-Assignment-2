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
        modelBuilder.Entity<Measurement>()
            .HasKey(m => m.Id)
            .HasName("PK_Id");
    }
}