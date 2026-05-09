using Microsoft.EntityFrameworkCore;
using Rabbit.Db;

namespace Rabbit.Extention
{
    public static class MigrationExtention
    {
        public static void ApplyMigragtion(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using ApplicationDbContext dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

            dbContext.Database.Migrate();
        }
    }
}