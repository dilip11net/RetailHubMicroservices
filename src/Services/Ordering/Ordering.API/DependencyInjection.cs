using Carter;

namespace Ordering.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection services)
        {
            // Register API services, e.g., controllers, Swagger, HealthChecks, etc.
            services.AddCarter();
            return services;
        }

        public static WebApplication UseAddWebApiServices(this WebApplication app)
        {
            // Configure the HTTP request pipeline, e.g., Swagger, HealthChecks, etc.
            app.MapCarter();
            return app;
        }
    }
}
