using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DAL
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {

        }
        public DbSet<Product> Products { get; set; }
    };
}
