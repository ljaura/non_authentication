using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UserService.Infrastructure.Configuration
{
    public static class DataServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContextPool<UserContext>(options =>
            {
                string connectionString = Configuration.GetConnectionString("ConnectionString");

                options.UseMySql(connectionString,
                    ServerVersion.AutoDetect(connectionString));
            });

            return services;
        }

    }
}
