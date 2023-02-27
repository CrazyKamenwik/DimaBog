using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using SkyDrive.DAL;
using SkyDrive.Tests.Initialize;

namespace SkyDrive.Tests.IntegrationTests
{
    public class TestHttpClientFactory<T> : WebApplicationFactory<T> where T : class
    {
        private static InMemoryDatabaseRoot? _root;
        private static InMemoryDatabaseRoot Root => _root ??= new InMemoryDatabaseRoot();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor =
                    services.SingleOrDefault(descriptor =>
                        descriptor.ServiceType == typeof(DbContextOptions<ApplicationContext>));

                if (dbContextDescriptor != null)
                    services.Remove(dbContextDescriptor);

                services.AddDbContext<ApplicationContext>(options =>
                    options.UseInMemoryDatabase(nameof(ApplicationContext)));

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();

                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ApplicationContext>();

                InitializeDb.Initialize(db);
            });
        }
    }
}
