namespace IndependentSocialApp.Web.Infrastructure
{
    using System.Text;
    using IndependentSocialApp.Data;
    using IndependentSocialApp.Data.Common;
    using IndependentSocialApp.Data.Common.Repositories;
    using IndependentSocialApp.Data.Models;
    using IndependentSocialApp.Data.Repositories;
    using IndependentSocialApp.Services;
    using IndependentSocialApp.Services.Data;
    using IndependentSocialApp.Services.Messaging;
    using IndependentSocialApp.Web.Infrastructure.CustomFilters;
    using IndependentSocialApp.Web.Infrastructure.CustomMiddlewares;
    using IndependentSocialApp.Web.Infrastructure.NloggerExtentions;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;

    public static class ServiceCollectionsExtentions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, AppSettings appSettings)
        {
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

            return services;
        }

        public static AppSettings GetApplicationSettings(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            var applicationSettingsConfiguration = configuration.GetSection("ApplicationSettings");
            services.Configure<AppSettings>(applicationSettingsConfiguration);
            return applicationSettingsConfiguration.Get<AppSettings>();
        }

        public static IServiceCollection AddBuisnessServices(this IServiceCollection services) 
        {
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IPostsService, PostsService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<ICommentsService, CommentsService>();
            services.AddSingleton<INloggerManager, NloggerManager>();
            services.AddTransient<ExceptionHandlingMiddleware>();

            return services;
        }

        public static IServiceCollection AddConfigurationForApiModelState(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }

        public static IServiceCollection AddControllersWithFilters(this IServiceCollection services)
        {
            services.AddControllers(
                options => options.Filters.Add<ValidateModelStateFilter>());

            return services;
        }

        public static IServiceCollection AddDataBaseConfigurations
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
               options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }
    }
}
