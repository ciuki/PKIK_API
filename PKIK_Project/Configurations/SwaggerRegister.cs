using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace PKIK_Project.Configurations
{
    public static class SwaggerRegister
    {
        public static void RegisterSwagger(this IServiceCollection services)
        {
            var info = new OpenApiInfo()
            {
                Title = "API for PKIK Project",
                Version = "v1",
                Description = "API for PKIK Project.",
                Contact = new OpenApiContact() { Name = "Rafał Miczek", Email = "miczek.r@gmail.com" },
                License = new OpenApiLicense() { Name = "GNU General Public License", Url = new Uri("https://opensource.org/licenses/gpl-license") }
            };
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                c.SwaggerDoc("v1", info);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityDefinition("API Key", new OpenApiSecurityScheme
                {
                    Name = "X-Api-Key",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "API Key",
                    In = ParameterLocation.Header,
                    Description = "Api Key"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                        {
                        Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "Bearer",
                            Name = "Authorization",
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.ApiKey
                        },
                        new[] { "readAccess", "writeAccess" }
                    }
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "API Key" }
                            ,
                            Scheme = "API Key",
                            Name = "X-Api-Key",
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.ApiKey
                        },
                        new[] { "readAccess", "writeAccess" }
                    }
                });
            });
            services.AddSwaggerGenNewtonsoftSupport();
        }
    }
}
