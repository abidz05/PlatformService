using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using( var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }
        private static void SeedData(AppDbContext context)
        {
            if(!context.Platforms.Any())
            {

                Console.WriteLine("-->Seeding data");
                context.Platforms.AddRange(
                    new Models.Platform(){Name="Dot Net",Publicsher="Microsoft",Cost="Free"}
                    ,new Models.Platform(){Name="Sql Server",Publicsher="Microsoft",Cost="$100"}
                    ,new Models.Platform(){Name="Oracle Db",Publicsher="Oracle",Cost="$20"}

                );
                context.SaveChanges();
            }
            else 
            {
                Console.WriteLine("-->We Already have data");
            }
        }
    }
}