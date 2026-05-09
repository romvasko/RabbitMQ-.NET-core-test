using Rabbit.CustomDI.ServiceImplementation;
using Rabbit.CustomDI.ServiceInterfaces;
using System.Reflection;

namespace Rabbit.CustomDI
{
    public class CustomDIService : ICustomDIService
    {
        public void RegisterServices(IServiceCollection services)
        {
            var lifeTimeAttributes = new List<Tuple<Type, Type>>()
        {
            new(typeof(IScopedService), typeof(IScopedServiceInterface)),
            new(typeof(ISingletonService), typeof(ISingletonServiceInterface)),
            new(typeof(ITransientService), typeof(ITransientServiceInterface)),
        };

            lifeTimeAttributes.ForEach(x => RegisterAccordingToAttribute(services, x));
        }

        private void RegisterAccordingToAttribute(IServiceCollection services, Tuple<Type, Type> lifetimeInterfaces)
        {
            var implementations = Assembly
                .GetAssembly(lifetimeInterfaces.Item1)
                ?.GetTypes()
                .Where(type => type.GetInterfaces().Contains(lifetimeInterfaces.Item1))
                .ToList();

            foreach (Type implementation in implementations!)
            {
                Type serviceInterface = implementation.GetInterfaces()
                    .SingleOrDefault(x => x.GetInterfaces().Contains(lifetimeInterfaces.Item2));

                if (serviceInterface != null)
                {
                    if (lifetimeInterfaces.Item1 == typeof(IScopedService))
                    {
                        services.AddScoped(serviceInterface, implementation);
                    }
                    else if (lifetimeInterfaces.Item1 == typeof(ITransientService))
                    {
                        services.AddTransient(serviceInterface, implementation);
                    }
                    else
                    {
                        services.AddSingleton(serviceInterface, implementation);
                    }
                }
            }
        }
    }
}
