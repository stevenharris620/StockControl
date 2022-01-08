using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace StockControl.API.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        private string _userId = string.Empty;

        public DbSet<Supplier>? Suppliers { get; set; }
        public DbSet<Part>? Parts { get; set; }

        public async Task SaveChangesAsync(string userId)
        {
            _userId = userId;
            await SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seeding Manager & User role to AspNetRoles table
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", Name = "Manager", NormalizedName = "MANAGER" },
                new IdentityRole { Id = "4c5e174e-3b0e-446f-86af-483d56fd7210", Name = "User", NormalizedName = "USER" }
                );


            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<ApplicationUser>();


            //Seeding the User to AspNetUsers table
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
                    Email = "andrew@cwmlabs.com",
                    NormalizedEmail = "ANDREW@CWMLABS.COM",
                    UserName = "andrew@cwmlabs.com",
                    NormalizedUserName = "ANDREW@CWMLABS.COM",
                    FirstName = "Andrew",
                    LastName = "Admin",
                    PasswordHash = hasher.HashPassword(null, "Admin.123")
                },
                new ApplicationUser
                {
                    Id = "1e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
                    Email = "uriah@cwmlabs.com",
                    NormalizedEmail = "URIAH@CWMLABS.COM",
                    UserName = "uriah@cwmlabs.com",
                    NormalizedUserName = "URIAH@CWMLABS.COM",
                    FirstName = "Uriah",
                    LastName = "User",
                    PasswordHash = hasher.HashPassword(null, "User.123")
                }
            );

            //Seeding the relation between our user and role to AspNetUserRoles table
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "4c5e174e-3b0e-446f-86af-483d56fd7210",
                    UserId = "1e445865-a24d-4543-a6c6-9443d048cdb9"
                }
            );

        }

        /// <summary>
        /// over rides save so we can log user  for changes
        /// created and modified user is centralized here
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries())
                if (item.Entity is UserRecord userRecord)
                {
                    switch (item.State)
                    {
                        case EntityState.Detached:
                            break;
                        case EntityState.Unchanged:
                            break;
                        case EntityState.Deleted:
                            break;
                        case EntityState.Modified:
                            userRecord.ModifiedDate = DateTime.UtcNow;
                            userRecord.ModifiedByUserId = _userId;
                            break;
                        case EntityState.Added:
                            userRecord.CreationDate = DateTime.UtcNow;
                            userRecord.CreatedByUserId = _userId;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
