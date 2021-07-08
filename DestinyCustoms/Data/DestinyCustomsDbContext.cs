using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DestinyCustoms.Data
{
    public class DestinyCustomsDbContext : IdentityDbContext
    {
        public DestinyCustomsDbContext(DbContextOptions<DestinyCustomsDbContext> options)
            : base(options)
        {
        }
    }
}
