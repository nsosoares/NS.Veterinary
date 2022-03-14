using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NS.Veterinary.Api.Data.Context;
using NS.Veterinary.Api.Extensions;
using System.Text;

namespace NS.Veterinary.Api.Configurations
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIndentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(option => 
            option.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddErrorDescriber<IdentityMessageInPortuguese>()
                .AddDefaultTokenProviders();

            //JWT
            var jwtSettingSection = configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSettingSection);

            var jwtSetting = jwtSettingSection.Get<JwtSettings>();
            var key = Encoding.ASCII.GetBytes(jwtSetting.Secret);

            services.AddAuthentication(configureOption =>
            {
                configureOption.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                configureOption.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOption => 
            { 
                configureOption.RequireHttpsMetadata = true;
                configureOption.SaveToken = true;
                configureOption.TokenValidationParameters = new TokenValidationParameters 
                { 
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = jwtSetting.Audience,
                    ValidIssuer = jwtSetting.Issuer
                };
            });

            return services;
        }
    }
}
