using Rabbit.Db;
using Rabbit.Db.DbSeed;

namespace Rabbit.Extention
{
    public static class DbSeedExtention
    {
        public static void Seeding(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();


                var dbSeed = scope.ServiceProvider.GetService<IDbSeed>();
                dbSeed?.Seed();
        }
    }
}
