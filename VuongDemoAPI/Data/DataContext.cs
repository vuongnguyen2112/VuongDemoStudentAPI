using Microsoft.EntityFrameworkCore;
using VuongDemoAPI.Models;

namespace VuongDemoAPI.Data
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Student> Students { get; set; }
    public DbSet<Class> Classes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<Student>(entity =>
      {
        entity.ToTable("Students");
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Name)
          .IsRequired()
          .HasMaxLength(60);
        entity.Property(e => e.Grade)
          .IsRequired();
        entity.Property(e => e.ClassID)
          .IsRequired();
      });
      modelBuilder.Entity<Class>(entity =>
      {
        entity.ToTable("Classes");
        entity.HasKey(e => e.ClassId);
        entity.Property(e => e.ClassName)
          .IsRequired()
          .HasMaxLength(4);
      });
    }
  }
}
