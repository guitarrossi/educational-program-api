using Microsoft.AspNetCore.Mvc;
using Plan.Application.Interfaces;
using Plan.Infraestructure.Context;
using Plan.WebApi.Services;

namespace Plan.WebApi
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection services)
        {
            services.AddScoped<ILoggedUser, LoggedUser>();

            services.AddHttpContextAccessor();

            services.AddHealthChecks()
                .AddDbContextCheck<PlanContext>();

            services.AddFastEndpoints();

            services.AddSwaggerGen();
            services.SwaggerDocument();

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);

            services.AddEndpointsApiExplorer();

            //services.AddOpenApiDocument(configure =>
            //    configure.Title = "Educational Program Plan API");

            return services;
        }
    }
}
