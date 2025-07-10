using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Group> Groups { get; set; }
    public DbSet<Course> Courses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>()
            .Property(c => c.Description)
            .IsRequired(false)
            .HasMaxLength(200);

        modelBuilder.Entity<Course>()
            .HasMany(c => c.Groups)
            .WithOne(g => g.Course)
            .HasForeignKey(g => g.CourseId);
    }

}
