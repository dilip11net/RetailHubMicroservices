namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register infrastructure services, e.g., DbContext, repositories, etc.

            var connectionString = configuration.GetConnectionString("Database");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
           // services.AddScoped<IApplicationDbContext, ApplicationDbContext>();



            return services;
        }
    }
}
