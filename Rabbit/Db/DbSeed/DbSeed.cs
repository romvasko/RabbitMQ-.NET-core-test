using Rabbit.CustomDI.ServiceImplementation;
using Rabbit.Db.Models;
using Rabbit.Repositories.Interfaces;

namespace Rabbit.Db.DbSeed
{
    public class DbSeed : IDbSeed, IScopedService
    {
        private readonly IContactRepository _contractRepository;

        public DbSeed(IContactRepository contactRepository)
        {
            _contractRepository = contactRepository;
        }
        public void Seed()
        {
            if (!_contractRepository.Any())
            {
                var testContact1 = new Contact()
                {
                    Name = "Jonh",
                    JobTitle = "Dev",
                    MobilePhone = "111111111111",
                    BirthDate = DateTime.Now.ToShortDateString()
                };
                var testContact2 = new Contact()
                {
                    Name = "Dave",
                    JobTitle = "support",
                    MobilePhone = "11132223333",
                    BirthDate = DateTime.Now.ToShortDateString()
                };

                _contractRepository.Add(testContact1);
                _contractRepository.Add(testContact2);
            }
        }
    }
}
