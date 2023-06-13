using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NichiOnlineTest.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Core Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Core Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "NIS_USERS");
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "NIS_ROLES");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("NIS_USER_ROLES");
                //in case you chagned the TKey type
                //  entity.HasKey(key => new { key.UserId, key.RoleId });
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("NIS_USER_CLAIMS");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("NIS_USER_LOGINS");
                //in case you chagned the TKey type
                //  entity.HasKey(key => new { key.ProviderKey, key.LoginProvider });       
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("NIS_ROLE_CLAIMS");

            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("NIS_USERTOKENS");
                //in case you chagned the TKey type
                // entity.HasKey(key => new { key.UserId, key.LoginProvider, key.Name });
            });
        }
    }
}
