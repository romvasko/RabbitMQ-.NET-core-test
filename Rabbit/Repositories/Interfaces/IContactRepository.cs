using Rabbit.CustomDI.ServiceImplementation;
using Rabbit.CustomDI.ServiceInterfaces;
using Rabbit.Db.Models;

namespace Rabbit.Repositories.Interfaces
{
    public interface IContactRepository : IBaseRepository<Contact>, IScopedServiceInterface
    {
    }
}
