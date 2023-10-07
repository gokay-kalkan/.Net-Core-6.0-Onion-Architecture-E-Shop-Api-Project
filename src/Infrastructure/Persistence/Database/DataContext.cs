
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Database
{
    public class DataContext:IdentityDbContext<User,Role,string>
    {
       
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DataContext()
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Cart { get; set; }
       
     
        public DbSet<Role> Roles { get; set; }
    }
}
