using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace IocContainer
{
    public class ContainerManager
    {
        private ConcurrentDictionary<Type, Type> typeMapping = new ConcurrentDictionary<Type, Type>();

        public void Register(Type from, Type to)
        {
            typeMapping[from] = to;
        }
        public void Register<T1, T2>()
        {
            typeMapping[typeof(T1)] = typeof(T2);
        }

        public object GetService(Type serviceType)
        {
            Type type;
            if (!typeMapping.TryGetValue(serviceType, out type))
            {
                type = serviceType;
            }
            if (type.IsInterface || type.IsAbstract)
            {
                return null;
            }

            ConstructorInfo constructor = this.GetConstructor(type);
            if (null == constructor)
            {
                return null;
            }

            object[] arguments = constructor.GetParameters().Select(p => this.GetService(p.ParameterType)).ToArray();
            object service = constructor.Invoke(arguments);
            this.InitializeInjectedProperties(service);
            this.InvokeInjectedMethods(service);
            return service;
        }

        public T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }

        protected virtual ConstructorInfo GetConstructor(Type type)
        {
            ConstructorInfo[] constructors = type.GetConstructors();
            return constructors.FirstOrDefault(c => c.GetCustomAttribute<InjectionAttribute>() != null)
                ?? constructors.FirstOrDefault();
        }

        protected virtual void InitializeInjectedProperties(object service)
        {
            PropertyInfo[] properties = service.GetType().GetProperties()
                .Where(p => p.CanWrite && p.GetCustomAttribute<InjectionAttribute>() != null)
                .ToArray();
            Array.ForEach(properties, p => p.SetValue(service, this.GetService(p.PropertyType)));
        }

        protected virtual void InvokeInjectedMethods(object service)
        {
            MethodInfo[] methods = service.GetType().GetMethods()
                .Where(m => m.GetCustomAttribute<InjectionAttribute>() != null)
                .ToArray();
            Array.ForEach(methods, m =>
            {
                object[] arguments = m.GetParameters().Select(p => this.GetService(p.ParameterType)).ToArray();
                m.Invoke(service, arguments);
            });
        }

    }
    [AttributeUsage(AttributeTargets.Constructor |
                      AttributeTargets.Property |
                      AttributeTargets.Method,
                      AllowMultiple = false)]
    public class InjectionAttribute : Attribute { }
}
