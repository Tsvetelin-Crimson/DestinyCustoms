using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DestinyCustoms.Data;
using DestinyCustoms.Data.Models;

namespace DestinyCustoms.Infrastructure
{
    public static class ApplicationBuilderExtentions
    {
        public static IApplicationBuilder SetUpDatabase(this IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();

            var db = scope.ServiceProvider.GetService<DestinyCustomsDbContext>();
            db.Database.Migrate();

            SeedItemClasses(db);

            return app;
        }

        private static void SeedItemClasses(DestinyCustomsDbContext db)
        {
            if (!db.WeaponClasses.Any())
            {
                var weaponClasses = new List<WeaponClass>() 
                {
                    new WeaponClass() { Name = "Scout Rifle" },
                    new WeaponClass() { Name = "Pulse Rifle" },
                    new WeaponClass() { Name = "Auto Rifle" },
                    new WeaponClass() { Name = "Hand Cannon" },
                    new WeaponClass() { Name = "Shotgun" },
                    new WeaponClass() { Name = "Sidearm" },
                    new WeaponClass() { Name = "Sniper Rifle" },
                    new WeaponClass() { Name = "Fusion Rifle" },
                    new WeaponClass() { Name = "Machine Gun" },
                    new WeaponClass() { Name = "Rocket Launcher" },
                    new WeaponClass() { Name = "Sword" },
                };

                db.WeaponClasses.AddRange(weaponClasses);
                db.SaveChanges();
            }
        }
    }
}
