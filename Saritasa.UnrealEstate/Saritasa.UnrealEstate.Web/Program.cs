using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Saritasa.UnrealEstate.Domain.EstateContext.Entities;
using Saritasa.UnrealEstate.Domain.EstateContext.Services.Initializers;
using System;
using System.Threading.Tasks;

namespace Saritasa.UnrealEstate.Web
{
    /// <summary>
    /// Program class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// App entry point.
        /// </summary>
        public static async Task Main(string[] args)
        {
            IWebHost host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;

                UserManager<User> userManager = serviceProvider.GetRequiredService<UserManager<User>>();
                RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                await RoleInitializer.InitializeAsync(userManager, roleManager);
            }

            host.Run();
        }

        /// <summary>
        /// Web host builder.
        /// </summary>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
