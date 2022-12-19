using Application.Interfaces;
using Infrastructure.Repositories.Base;

namespace PKIK_Project.Configurations
{
    public static class ScrutorRegister
    {
        public static void RegisterScrutor(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssembliesOf(typeof(Repository<>))
                .AddClasses(publicOnly: true)
                .AsMatchingInterface((repository, filter) =>
                    filter.Where(implementation => implementation.Name.Equals($"I{repository.Name}", StringComparison.OrdinalIgnoreCase)))
                .WithTransientLifetime());

            services.Scan(scan => scan
                            .FromAssemblyOf<IService>()
                            .AddClasses(publicOnly: true)
                            .AsMatchingInterface((service, filter) =>
                                filter.Where(implementation => implementation.Name.Equals($"I{service.Name}", StringComparison.OrdinalIgnoreCase)))
                            .WithTransientLifetime());
        }
    }
}
