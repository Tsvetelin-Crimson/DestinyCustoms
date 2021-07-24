using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DestinyCustoms.Data.Models;

namespace DestinyCustoms.Data
{
    public class DestinyCustomsDbContext : IdentityDbContext
    {
        public DestinyCustomsDbContext(DbContextOptions<DestinyCustomsDbContext> options)
            : base(options)
        {
        }

        public DbSet<ExoticWeapon> Weapons { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<WeaponClass> WeaponClasses { get; set; }
    }
}
