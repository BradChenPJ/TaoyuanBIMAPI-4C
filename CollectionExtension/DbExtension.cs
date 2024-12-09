using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TaoyuanBIMAPI.Model;

namespace TaoyuanBIMAPI.CollectionExtension
{
    public static class DbExtension
    {
        public static IServiceCollection AddDbConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TaoyuanBimContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConnectionString")));

            return services;
        }
    }
}
