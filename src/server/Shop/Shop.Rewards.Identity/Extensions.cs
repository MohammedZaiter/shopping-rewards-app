
namespace Microsoft.Extensions.Identity
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Shop.Rewards.Identity;
    using System.Text;

    public static class Extensions
    {
        public static void AddIdentity(this IServiceCollection services, IConfigurationManager configuration)
        {
            services.AddIdentityServer();

            //fetch and register jwt token app settings configuration.
            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<JwtOptions>>().Value);

            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppIdentityDbContext>()
                    .AddDefaultTokenProviders();

            var databseConnectioString = configuration.GetConnectionString("DatabseConnectionString");

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(databseConnectioString));

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
            .AddJwtBearer((options) =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var jwtOptions = serviceProvider.GetRequiredService<IOptions<JwtOptions>>().Value;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
                };
            });
        }
    }
}