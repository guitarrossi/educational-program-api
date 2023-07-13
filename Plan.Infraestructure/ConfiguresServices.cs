using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Plan.Application.Interfaces;
using Plan.Application.Interfaces.Identity;
using Plan.Application.Interfaces.Repositories;
using Plan.Infraestructure.Context;
using Plan.Infraestructure.Identity;
using Plan.Infraestructure.Interceptors;
using Plan.Infraestructure.Repositories;
using Plan.Infraestructure.Services;

namespace Plan.Infraestructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuditableEntityInterceptors>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<PlanContext>(options =>
                options.UseNpgsql(connectionString));


            services
                .AddDefaultIdentity<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<PlanContext>();

            services.AddTransient<INow, NowService>();
            services.AddTransient<IIdentityService, IdentityService>();

            services.AddScoped<IPlanRepository, PlanRepository>();

            return services;
        }
    }
}
