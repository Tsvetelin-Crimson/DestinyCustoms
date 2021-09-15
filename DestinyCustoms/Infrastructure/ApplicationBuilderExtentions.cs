using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DestinyCustoms.Data;
using DestinyCustoms.Data.Models;

namespace DestinyCustoms.Infrastructure
{
    using static Common.WebConstants;
    public static class ApplicationBuilderExtentions
    {
        public static IApplicationBuilder SetUpDatabase(this IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            MigrateDatabase(serviceProvider);
            SeedItemClasses(serviceProvider);
            SeedAdminRole(serviceProvider);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetService<DestinyCustomsDbContext>();
            db.Database.Migrate();
        }

        private static void SeedItemClasses(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetService<DestinyCustomsDbContext>();

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
    
        private static void SeedAdminRole(IServiceProvider serviceProvider)
        {
            var userService = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleService = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
                {
                    if (await roleService.RoleExistsAsync(adminRoleName))
                    {
                        return;
                    }

                    await roleService.CreateAsync(new IdentityRole { Name = adminRoleName });

                    var email = "admin@dc.com";
                    var username = "Admin";
                    var pasword = "admin123";

                    var admin = new IdentityUser
                    {
                        Email = email,
                        UserName = username,
                    };

                    await userService.CreateAsync(admin, pasword);
                    await userService.AddToRoleAsync(admin, adminRoleName);
                })
            .GetAwaiter()
            .GetResult();
        }
    }
}
