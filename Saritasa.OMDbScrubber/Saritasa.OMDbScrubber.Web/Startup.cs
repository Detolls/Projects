using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Saritasa.OMDbScrubber.DataAccess.Context;
using Saritasa.OMDbScrubber.Domain.OMDb.Repositories;
using Saritasa.OMDbScrubber.Infrastructure.DI.Implementations.Sql;
using Saritasa.Tools.Messages.Abstractions;
using Saritasa.Tools.Messages.Commands;
using Saritasa.Tools.Messages.Common;
using Saritasa.Tools.Messages.Common.PipelineMiddlewares;
using Saritasa.Tools.Messages.Common.Repositories;
using Saritasa.Tools.Messages.Queries;
using System;

namespace Saritasa.OMDbScrubber.Web
{
    /// <summary>
    /// Class that stores the app settings.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configuration the app.
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Constructor of the <seealso cref="Startup"/> class.
        /// </summary>
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Configure services.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<OMDbScrubberContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IMoviesService, EFCoreMoviesService>();

            var jsonFileMessageRepository = new JsonFileMessageRepository(Environment.CurrentDirectory + @"\Messages");
            var repositoryMiddleware = new RepositoryMiddleware(jsonFileMessageRepository);

            var pipelineContainer = new DefaultMessagePipelineContainer();
            services.AddSingleton<IMessagePipelineContainer>(pipelineContainer);
            services.AddScoped<IMessagePipelineService, DefaultMessagePipelineService>();
            pipelineContainer.AddCommandPipeline().AddStandardMiddlewares(o => o.SetAssemblies(typeof(IMoviesService).Assembly)).AddMiddleware(repositoryMiddleware);
            pipelineContainer.AddQueryPipeline().AddStandardMiddlewares().AddMiddleware(repositoryMiddleware);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        /// <summary>
        /// Configure the app.
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });
        }
    }
}
