using Microsoft.EntityFrameworkCore;
using WebApplication_WithImage.Models;

namespace WebApplication_WithImage.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<DetailsModal> detailsModals { get; set; }
    }
}
