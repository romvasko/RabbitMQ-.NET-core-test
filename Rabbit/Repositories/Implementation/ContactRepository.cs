using Rabbit.CustomDI.ServiceImplementation;
using Rabbit.Db;
using Rabbit.Db.Models;
using Rabbit.Repositories.Interfaces;

namespace Rabbit.Repositories.Implementation
{
    public class ContactRepository : BaseRepository<Contact>, IContactRepository, IScopedService
    {
        public ContactRepository(ApplicationDbContext context) : base(context) { }
    }
}
