namespace AniTrack.Web.Infrastructure.Extensions
{
    using AniTrack.Data.Repository;
    using AniTrack.Data.Repository.Interface;
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

    public static class ServiceCollectionExtensions
    {
        private static readonly string ServiceTypeSuffix = "Service";
        private static readonly string ProjectInterfacePrefix = "I";
        public static IServiceCollection AddUserDefinedServices(this IServiceCollection serviceCollection, Assembly serviceAssembly)
        {
            Type[] serviceClasses = serviceAssembly
                .GetTypes()
                .Where(t => !t.IsInterface &&
                             t.Name.EndsWith(ServiceTypeSuffix))
                .ToArray();
            foreach (Type serviceClass in serviceClasses)
            {
                Type? serviceInterface = serviceClass
                    .GetInterfaces()
                    .FirstOrDefault(i => i.Name == $"{ProjectInterfacePrefix}{serviceClass.Name}");
                if(serviceInterface == null)
                {
                    throw new ArgumentException();
                }
                serviceCollection.AddScoped(serviceInterface, serviceClass);
            }
            return serviceCollection;
        }
    }
}
