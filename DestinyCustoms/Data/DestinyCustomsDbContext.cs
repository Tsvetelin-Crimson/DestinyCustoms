﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DestinyCustoms.Data.Models;

namespace DestinyCustoms.Data
{
    public class DestinyCustomsDbContext : IdentityDbContext
    {
        public DestinyCustomsDbContext(DbContextOptions<DestinyCustomsDbContext> options)
            : base(options)
        {
        }

        public DbSet<ExoticWeapon> Exotics { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<WeaponClass> ItemClasses { get; set; }

        public DbSet<Suggestion> Suggestions { get; set; }

    }
}
