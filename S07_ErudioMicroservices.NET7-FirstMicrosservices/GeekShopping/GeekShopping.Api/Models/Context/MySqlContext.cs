using GeekShopping.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Models.Context
{
    public class MySqlContext : DbContext
    {
        public MySqlContext() { }

        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) {}

        public DbSet<Product> Products { get; set; }
    }
}
