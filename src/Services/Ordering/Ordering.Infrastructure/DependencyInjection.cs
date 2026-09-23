using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register infrastructure services, e.g., DbContext, repositories, etc.

            var connectionString = configuration.GetConnectionString("OrderingDatabase");

            //services.AddDbContext<IApplicationDbContext>(options => options.UseSqlServer(connectionString));
            //services.AddScoped<IApplicationDbContext, ApplicationDbContext>();



            return services;
        }
    }
}
