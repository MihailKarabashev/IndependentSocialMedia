namespace IndependentSocialApp.Web
{
    using System.Reflection;
    using IndependentSocialApp.Data;
    using IndependentSocialApp.Data.Common;
    using IndependentSocialApp.Data.Common.Repositories;
    using IndependentSocialApp.Data.Models;
    using IndependentSocialApp.Data.Repositories;
    using IndependentSocialApp.Data.Seeding;
    using IndependentSocialApp.Services.Data;
    using IndependentSocialApp.Services.Mapping;
    using IndependentSocialApp.Services.Messaging;
    using IndependentSocialApp.Web.Infrastructure;
    using IndependentSocialApp.Web.Infrastructure.CustomFilters;
    using IndependentSocialApp.Web.Infrastructure.CustomMiddlewares;
    using IndependentSocialApp.Web.Infrastructure.NloggerExtentions;
    using IndependentSocialApp.Web.ViewModels;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllers(
                options => options.Filters.Add<ValidateModelStateFilter>());

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddRouting(opt => opt.LowercaseUrls = true);

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton(this.configuration);

            services.AddJwtAuthentication(services.GetApplicationSettings(this.configuration));

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IPostsService, PostsService>();
            services.AddSingleton<INloggerManager, NloggerManager>();


            services.AddTransient<ExceptionHandlingMiddleware>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllers();
                    });
        }
    }
}
