using Ordering.Infrastructure.Data.Interceptors;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register infrastructure services, e.g., DbContext, repositories, etc.

            var connectionString = configuration.GetConnectionString("Database");


            // Add Services to the container.
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptors>();

            services.AddDbContext<ApplicationDbContext>((sp, options) => {

                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(connectionString);



            });
           // services.AddScoped<IApplicationDbContext, ApplicationDbContext>();



            return services;
        }
    }
}
