using Microsoft.AspNetCore.Identity;
using Saritasa.UnrealEstate.Domain.EstateContext.Entities;
using System;
using System.Threading.Tasks;

namespace Saritasa.UnrealEstate.Domain.EstateContext.Services.Initializers
{
    /// <summary>
    /// Static class that stores logic for initialize roles.
    /// </summary>
    public static class RoleInitializer
    {
        /// <summary>
        /// Static method to initialize roles async.
        /// </summary>
        /// <param name="userManager">User manager.</param>
        /// <param name="roleManager">Role manager.</param>
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string password = "Admin123";

            if (await roleManager.FindByNameAsync("Regular") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Regular"));
            }

            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                var admin = new User
                {
                    UserName = "Admin",
                    Email = adminEmail,
                    PhoneNumber = "88005553535",
                    FirstName = "Admin",
                    LastName = "Adminov",
                    Birthday = new DateTime(01 / 01 / 2000),
                    Gender = 'M'
                };

                IdentityResult result = await userManager.CreateAsync(admin, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
