using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TaoyuanBIMAPI.Model.Data;
using TaoyuanBIMAPI.Model.Identity;

namespace TaoyuanBIMAPI.CollectionExtension
{
    public static class DbExtension
    {
        public static IServiceCollection AddDbConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TaoyuanBimContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<TaoyuanBimIdentityContext>(options => options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));
            
            return services;
        }
    }
}
