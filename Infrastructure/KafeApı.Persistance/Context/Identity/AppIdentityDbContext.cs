using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Persistance.Context.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<AppIdentityUser,AppIdentityRole,string>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppIdentityUser>(b =>
            {
                b.ToTable("User");
            });

            builder.Entity<AppIdentityRole>(b =>
            {
                b.ToTable("Role");
            });
        }
    }
}
