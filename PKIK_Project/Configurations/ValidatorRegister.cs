using Application.Validators;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Newtonsoft.Json.Converters;

namespace PKIK_Project.Configurations
{
    public static class ValidatorRegister
    {
        public static void RegisterValidators(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
        options.SerializerSettings.Converters.Add(new StringEnumConverter())).AddFluentValidation(c =>
            {
                c.DisableDataAnnotationsValidation = true;
                c.RegisterValidatorsFromAssemblyContaining<IValidator>();
                // Optionally set validator factory if you have problems with scope resolve inside validators.
                c.ValidatorFactoryType = typeof(HttpContextServiceProviderValidatorFactory);
            });

            services.AddFluentValidationRulesToSwagger();
        }
    }
}
