using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Saritasa.Tools.Messages.Abstractions;
using Saritasa.Tools.Messages.Commands;
using Saritasa.Tools.Messages.Common;
using Saritasa.Tools.Messages.Common.PipelineMiddlewares;
using Saritasa.Tools.Messages.Common.Repositories;
using Saritasa.Tools.Messages.Queries;
using Saritasa.UnrealEstate.DataAccess.DbContexts;
using Saritasa.UnrealEstate.DataAccess.DbPlaceholders.EstateDb;
using Saritasa.UnrealEstate.DataAccess.Repositories;
using Saritasa.UnrealEstate.Domain.EstateContext.Abstract;
using Saritasa.UnrealEstate.Domain.EstateContext.Entities;
using Saritasa.UnrealEstate.Domain.EstateContext.Repositories;
using Saritasa.UnrealEstate.Domain.EstateContext.Services.Generators;
using Saritasa.UnrealEstate.Domain.EstateContext.Services.Mappings;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Saritasa.UnrealEstate.Web
{
    /// <summary>
    /// Class that stores the app settings.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructor of the <seealso cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">App configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// App configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configure services.
        /// </summary>
        /// <param name="services">Service collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // DB services.
            services.AddDbContext<EstateDbContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("UnrealEstateDB")));

            // Generators services.
            services.AddScoped<ITokenGenerator, JwtTokenGenerator>();

            // Repositories services.
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IListingRepository, ListingRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();

            // Pipeline services.
            var jsonFileMessageRepository = new JsonFileMessageRepository(Environment.CurrentDirectory + @"\Messages");
            var repositoryMiddleware = new RepositoryMiddleware(jsonFileMessageRepository);

            var pipelineContainer = new DefaultMessagePipelineContainer();
            services.AddSingleton<IMessagePipelineContainer>(pipelineContainer);
            services.AddScoped<IMessagePipelineService, DefaultMessagePipelineService>();
            pipelineContainer.AddCommandPipeline()
                .AddStandardMiddlewares(o => o.SetAssemblies(typeof(IUserRepository).Assembly))
                .AddMiddleware(repositoryMiddleware);
            pipelineContainer.AddQueryPipeline().AddStandardMiddlewares().AddMiddleware(repositoryMiddleware);

            // Add Identity.
            services.AddIdentity<User, IdentityRole>(
                options =>
                {
                    options.Password.RequiredLength = 8;
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequiredUniqueChars = default;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<EstateDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            // Add authentication.
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.Issuer,

                        ValidateAudience = true,
                        ValidAudience = AuthOptions.Audience,
                        ValidateLifetime = true,

                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };
                });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
            });

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfiles(new List<Profile> { new DtoMappingProfile(), new CommandMappingProfile() });
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Add swagger.
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Unreal Estate API",
                    TermsOfService = "None"
                });

                options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    In = "header",
                    Name = "Authorization",
                    Description = "JWT Authorization header",
                    Type = "apiKey"
                });

                options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });

                var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                options.IncludeXmlComments(xmlPath);
            });

            // Add MVC.
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        /// <summary>
        /// Configure the app.
        /// </summary>
        /// <param name="app">App builder.</param>
        /// <param name="env">Hosting environment.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.ShowExtensions();
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "API Documentation");
            });

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseMvc();

            //FakeDataEstateDbPlaceholder.FillListingsAsync().GetAwaiter().GetResult();
            //FakeDataEstateDbPlaceholder.FillListingPhotosAsync().GetAwaiter().GetResult();
            //FakeDataEstateDbPlaceholder.FillListingNotesAsync().GetAwaiter().GetResult();
            //FakeDataEstateDbPlaceholder.FillBidsAsync().GetAwaiter().GetResult();
            //FakeDataEstateDbPlaceholder.FillCommentsAsync().GetAwaiter().GetResult();
        }
    }
}
