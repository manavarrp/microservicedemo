
using Catalog.Api.Context;

namespace Catalog.Api.HostedService
{
    public class AppHostedService : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            CatalogSeeder.Seed();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
