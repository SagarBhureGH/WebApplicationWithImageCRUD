using Microsoft.EntityFrameworkCore;
using WebApplication_WEBAPI.Modals;

namespace WebApplication_WEBAPI.Data
{
    public class ApplicationDbAPI : DbContext
    {
        public ApplicationDbAPI(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<RegistrationModal> registrationModals { get; set; }
    }
}
