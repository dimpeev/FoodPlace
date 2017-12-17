namespace FoodPlace.Web.Infrastructure.Extensions
{
    using Data;
    using FoodPlace.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;

    public static class ApplicationBuilderExtensions
    {
        private const string AdminUsername = "admin";
        private const string AdminEmail = "admin@mysite.com";
        private const string AdminPassword = "admin123";

        private const string Owner1Username = "owner1";
        private const string Owner1Email = "owner1@mysite.com";
        private const string Owner1Password = "owner123";

        private const string Owner2Username = "owner2";
        private const string Owner2Email = "owner2@mysite.com";
        private const string Owner2Password = "owner123";

        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<FoodPlaceDbContext>().Database.Migrate();

                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

                Task
                    .Run(async () =>
                    {
                        // Seed Roles
                        var roles = new[]
                        {
                            WebConstants.AdministratorRole,
                            WebConstants.OwnerRole,
                        };

                        foreach (var role in roles)
                        {
                            var roleExists = await roleManager.RoleExistsAsync(role);

                            if (!roleExists)
                            {
                                await roleManager.CreateAsync(new IdentityRole
                                {
                                    Name = role
                                });
                            }
                        }

                        // Seed Admin User
                        var adminUser = await userManager.FindByEmailAsync(AdminEmail);

                        if (adminUser == null)
                        {
                            // Create Admin User
                            adminUser = new User
                            {
                                UserName = AdminUsername,
                                Email = AdminEmail,
                            };

                            var result = await userManager.CreateAsync(adminUser, AdminPassword);

                            // Add User to Role
                            if (result.Succeeded)
                            {
                                await userManager.AddToRoleAsync(adminUser, WebConstants.AdministratorRole);
                            }
                        }
                        else
                        {
                            // Add User to Role
                            await userManager.AddToRoleAsync(adminUser, WebConstants.AdministratorRole);
                        }

                        // Seed Owner 1 User
                        var owner1User = await userManager.FindByEmailAsync(Owner1Email);

                        if (owner1User == null)
                        {
                            // Create Owner 1 User
                            owner1User = new User
                            {
                                UserName = Owner1Username,
                                Email = Owner1Email,
                            };

                            var result = await userManager.CreateAsync(owner1User, Owner1Password);

                            // Add User to Role
                            if (result.Succeeded)
                            {
                                await userManager.AddToRoleAsync(owner1User, WebConstants.OwnerRole);
                            }
                        }
                        else
                        {
                            // Add User to Role
                            await userManager.AddToRoleAsync(owner1User, WebConstants.OwnerRole);
                        }

                        // Seed Owner 2 User
                        var owner2User = await userManager.FindByEmailAsync(Owner2Email);

                        if (owner2User == null)
                        {
                            // Create Owner 2 User
                            owner2User = new User
                            {
                                UserName = Owner2Username,
                                Email = Owner2Email,
                            };

                            var result = await userManager.CreateAsync(owner2User, Owner2Password);

                            // Add User to Role
                            if (result.Succeeded)
                            {
                                await userManager.AddToRoleAsync(owner2User, WebConstants.OwnerRole);
                            }
                        }
                        else
                        {
                            // Add User to Role
                            await userManager.AddToRoleAsync(owner2User, WebConstants.OwnerRole);
                        }
                    })
                    .GetAwaiter()
                    .GetResult();
            }

            return app;
        }
    }
}