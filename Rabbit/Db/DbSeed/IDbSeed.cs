using Rabbit.CustomDI.ServiceInterfaces;

namespace Rabbit.Db.DbSeed
{
    public interface IDbSeed: IScopedServiceInterface
    {
        public void Seed();
    }
}
