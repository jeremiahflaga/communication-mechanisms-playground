using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace Sales.Orders.OrderCreated
{
    // When you're using ASP.NET, it has a class already built for you to run as a hosted service
    // But because we are creating a console hosted service, we have to create a simple hosted service.
    // All it does, because it is a IHostedService, is it gets added to the container. The .NET core generic
    //      host will actually start this up for us and call StartAsync() which we'll use to start out bus,
    //      passing it the cancellation token if they decide to give up on us.
    public class MassTransitConsoleHostedService : IHostedService
    {
        readonly IBusControl _bus;

        public MassTransitConsoleHostedService(IBusControl bus)
        {
            _bus = bus;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _bus.StartAsync(cancellationToken).ConfigureAwait(false);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _bus.StopAsync(cancellationToken);
        }
    }
}
