using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Saritasa.OMDbScrubber.Web
{
    /// <summary>
    /// Program class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// App entry point.
        /// </summary>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Web host builder.
        /// </summary>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
