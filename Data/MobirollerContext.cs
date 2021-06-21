using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mobiroller.Data.Entity;
using System.Reflection;

namespace Mobiroller.Data
{
    public class MobirollerContext : IdentityDbContext<IdentityUser>
    {
        public MobirollerContext(DbContextOptions<MobirollerContext> options) : base(options)
        {
        }

        public DbSet<Incident> Incidents { get; set; }

        public DbSet<Locale> Locales { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}