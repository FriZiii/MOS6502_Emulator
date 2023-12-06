using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Processor
{
    public static class ServiceCollectionExtensions
    {
        public static void AddProcessor(this IServiceCollection service)
        {
            service.AddScoped<IProcessor, Processor>();
        }
    }
}
