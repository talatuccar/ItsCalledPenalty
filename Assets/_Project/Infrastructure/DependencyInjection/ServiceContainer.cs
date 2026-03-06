using System;
using System.Collections.Generic;

namespace Project.Infrastructure.DependencyInjection
{
    public class ServiceContainer
    {
        private readonly Dictionary<Type, IService> services = new();

        public void Register<T>(T service) where T : IService
        {
            var type = typeof(T);

            if (services.ContainsKey(type))
            {
                throw new Exception($"Service already registered: {type}");
            }

            services[type] = service;
        }

        public T Get<T>() where T : IService
        {
            var type = typeof(T);

            if (services.TryGetValue(type, out var service))
            {
                return (T)service;
            }

            throw new Exception($"Service not found: {type}");
        }
    }
}