using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaoyuanBIMAPI.Model.Data;
using TaoyuanBIMAPI.Model.Identity;

namespace TaoyuanBIMAPI.CollectionExtension
{
    public static class AuthExtension
    {
        public static IServiceCollection AddAuthService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<TaoyuanBIMUser, TaoyuanBIMRole>(option =>
            {
                option.Password.RequireDigit = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequiredLength = 4;
            }).AddEntityFrameworkStores<TaoyuanBimIdentityContext>().AddDefaultTokenProviders();

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    // get "roles" for [Authorize]
                    RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,  // Token Expires
                    ValidateIssuerSigningKey = true,  // SignKey
                    ValidIssuer = configuration.GetValue<string>("JwtSettings:Issuer"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JwtSettings:SignKey")))
                };
            });
            services.AddAuthorization();

            return services;
        }
    }
}
