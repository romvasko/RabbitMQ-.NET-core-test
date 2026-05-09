using Microsoft.EntityFrameworkCore;
using Rabbit.Db.Models;

namespace Rabbit.Db
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
    }
}
