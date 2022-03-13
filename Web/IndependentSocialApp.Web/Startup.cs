namespace IndependentSocialApp.Web
{
    using System.Reflection;
    using IndependentSocialApp.Data;
    using IndependentSocialApp.Data.Seeding;
    using IndependentSocialApp.Services.Mapping;
    using IndependentSocialApp.Web.Infrastructure;
    using IndependentSocialApp.Web.Infrastructure.CustomMiddlewares;
    using IndependentSocialApp.Web.ViewModels;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
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
            services
                .AddDataBaseConfigurations(this.configuration)
                .AddControllersWithFilters()
                .AddConfigurationForApiModelState()
                .AddRouting(opt => opt.LowercaseUrls = true)
                .AddSingleton(this.configuration)
                .AddDatabaseDeveloperPageExceptionFilter()
                .AddJwtAuthentication(services.GetApplicationSettings(this.configuration))
                .AddBuisnessServices();
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
