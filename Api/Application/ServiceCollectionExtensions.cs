using Application.Managments.PostProcessorProgram;
using Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection service)
        {
            service.AddMediatR(typeof(PostProcessorProgram));
            service.AddScoped<IGenerateMemoryMapService, GenerateMemoryMapService>();
        }
    }
}
