using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DestinyCustoms.Data.Models.ApplicationModels;

namespace DestinyCustoms.Data
{
    public class DestinyCustomsDbContext : IdentityDbContext
    {
        public DestinyCustomsDbContext(DbContextOptions<DestinyCustomsDbContext> options)
            : base(options)
        {
        }
        public DbSet<Exotic> Exotics { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<ItemClass> ItemClasses { get; set; }

        public DbSet<Suggestion> Suggestions { get; set; }

    }
}
