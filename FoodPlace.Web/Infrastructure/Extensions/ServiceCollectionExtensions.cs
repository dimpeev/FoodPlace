namespace FoodPlace.Web.Infrastructure.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using System.Linq;
    using System.Reflection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            Assembly
                .GetAssembly(typeof(IService))
                .GetTypes()
                .Where(t => t.IsClass &&
                            t.GetInterfaces().All(i => i != typeof(IServiceSingleton)) &&
                            t.GetInterfaces().Any(i => i.Name == $"I{t.Name}"))
                .Select(t => new
                {
                    Interface = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .ToList()
                .ForEach(s => services.AddTransient(s.Interface, s.Implementation));

            return services;
        }
    }
}