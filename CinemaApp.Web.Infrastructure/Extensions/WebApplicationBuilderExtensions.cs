using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CinemaApp.Web.Infrastructure.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection serviceCollection, Type repositoryType)
        {
            Assembly repositoriesAssembly = repositoryType.Assembly;
            IEnumerable<Type> repositoryInterfaces = repositoriesAssembly
                .GetTypes()
                .Where(t => t.IsInterface && t.Name.StartsWith("I") && t.Name.EndsWith("Repository"))
                .ToArray();

            foreach (Type serviceType in repositoryInterfaces)
            {
                Type implementationType = repositoriesAssembly
                    .GetTypes()
                    .Single(t => t.IsClass && !t.IsAbstract && serviceType.IsAssignableFrom(t));

                serviceCollection.AddScoped(serviceType, implementationType);
            }

            return serviceCollection;
        }

        public static IServiceCollection RegisterUserServices(this IServiceCollection serviceCollection, Type servicesType)
        {
            Assembly servicesAssembly = servicesType.Assembly;

            IEnumerable<Type> servicesInterfaces = servicesAssembly
                .GetTypes()
                .Where(t => t.IsInterface && t.Name.StartsWith("I") && t.Name.EndsWith("Service"))
                .ToArray();

            foreach (Type serviceType in servicesInterfaces)
            {
                Type implementationType = servicesAssembly
                    .GetTypes()
                    .Single(t => t.IsClass && !t.IsAbstract && serviceType.IsAssignableFrom(t));

                serviceCollection.AddScoped(serviceType, implementationType);
            }

            return serviceCollection;
        }
    }
}
