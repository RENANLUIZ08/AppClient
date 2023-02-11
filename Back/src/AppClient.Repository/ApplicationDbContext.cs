using AppClient.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppClient.Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Client>? Clients { get; set; }

    }
}
