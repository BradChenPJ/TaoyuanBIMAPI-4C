using TaoyuanBIMAPI.Repository.Implement;
using TaoyuanBIMAPI.Repository.Interface;

namespace TaoyuanBIMAPI.CollectionExtension
{
    public static class RepositoryExtension
    {
        public static IServiceCollection AddRepositoryInterface(this IServiceCollection services)
        {
            services.AddScoped<IMaptoolRepository, MaptoolRepository>();
            services.AddScoped<IIdentityRepository, IdentityRepository>();

            return services;
        }
    }
}
