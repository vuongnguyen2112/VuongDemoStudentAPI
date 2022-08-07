using Microsoft.EntityFrameworkCore;
using VuongDemoAPI.Models;

namespace VuongDemoAPI.Data
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Student> Students { get; set; }
    public DbSet<Class> Classes { get; set; }
  }
}
