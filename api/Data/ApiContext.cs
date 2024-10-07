using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
  public class ApiContext(DbContextOptions<ApiContext> options) : DbContext(options)
  {
    public DbSet<User> Users { get; set; }
    public DbSet<Administrator> Administrators { get; set; }
    public DbSet<Events> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<User>()
        .HasOne(u => u.Events)
        .WithMany(e => e.Users)
        .HasForeignKey(u => u.EventID)
        .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<Administrator>()
        .HasIndex(a => a.UserName)
        .IsUnique();

      modelBuilder.Entity<Events>()
        .HasIndex(e => e.EventCode)
        .IsUnique();
    }
  }
}