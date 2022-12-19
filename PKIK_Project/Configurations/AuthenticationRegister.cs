using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PKIK_Project.Helpers;
using System.Text;

namespace PKIK_Project.Configurations
{
    public static class AuthenticationRegister
    {
        public static void RegisterAuthentication(this IServiceCollection services, ConfigurationManager configuration)
        {
            byte[] Key = Encoding.UTF8.GetBytes(configuration["JWT:Secret"]);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddApiKeySupport(options => { })
                .AddJwtBearer(o =>
                    {

                        o.SaveToken = true;
                        o.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Key)
                        };
                    });
        }
    }
}
