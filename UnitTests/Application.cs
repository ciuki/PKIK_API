
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;

namespace UnitTests
{
    public class Application : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var root = new InMemoryDatabaseRoot();

            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<IdentityDbContext>));
                services.AddDbContext<IdentityDbContext>(options =>
                    options.UseInMemoryDatabase("Testing", root));


                ServiceProvider sp = services.BuildServiceProvider();

                using (IServiceScope scope = sp.CreateScope())
                {
                    IServiceProvider scopedServices = scope.ServiceProvider;
                    IdentityDbContext db = scopedServices.GetRequiredService<IdentityDbContext>();

                    db.Database.EnsureCreated();


                    Utilities.InitializeDbForTests(db);

                }

            });



            return base.CreateHost(builder);
        }
    }
}
