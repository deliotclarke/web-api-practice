using Microsoft.EntityFrameworkCore;

namespace my_web_api.Models
{
  public class MyWebApiContext : DbContext
  {
    public MyWebApiContext(DbContextOptions<MyWebApiContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
  }
}