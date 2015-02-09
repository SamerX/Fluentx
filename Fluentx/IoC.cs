using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Fluentx
{
    /// <summary>
    /// Represents the life cycle of a registered container entry
    /// </summary>
    public enum LifeCycle
    {
        /// <summary>
        /// IoC container resolves by creating a new instance on every resolve
        /// </summary>
        Transient,
        /// <summary>
        /// IoC container resolves by creating a new instance for the first time then reuse that value for the afterwards calls
        /// </summary>
        Singleton

    }
    /// <summary>
    /// Used with auto registration of classes
    /// </summary>
    public enum AutoRegisterMode
    {
        /// <summary>
        /// If resolve type interface not found then the concreteType is used as a resolve type
        /// </summary>
        Flexable,
        /// <summary>
        /// If resolve type interface not found then no registration happens
        /// </summary>
        Strict
    }
    /// <summary>
    /// Its a simple Inversion of Control resolver that help in registering resolve types against 
    /// concrete types with support for simple DI (Dependency Injection)
    /// </summary>
    public static class IoC
    {
        private class ContainerEntry
        {
            public ContainerEntry(Type resolveType, Type concreteType, LifeCycle lifeCycle)
            {
                ResolveType = resolveType;
                ConcreteType = concreteType;
                LifeCycle = lifeCycle;
            }
            public Type ResolveType { get; private set; }
            public Type ConcreteType { get; private set; }
            public LifeCycle LifeCycle { get; private set; }
            public object Instance { get; private set; }

            public void CreateInstance(params object[] args)
            {
                this.Instance = Activator.CreateInstance(this.ConcreteType, args);
            }
        }
        /// <summary>
        /// Based on Jimmy Bogard's implementation for simple IoC container
        /// </summary>
        private class IoCContainer
        {
            private readonly IList<ContainerEntry> entries = new List<ContainerEntry>();

            public void Register<TResolveType, TConcretType>()
            {
                Register<TResolveType, TConcretType>(LifeCycle.Singleton);
            }

            public void Register<TResolveType, TConcretType>(LifeCycle lifeCycle)
            {
                if (!entries.Any(x => x.ResolveType == typeof(TResolveType)))
                {
                    entries.Add(new ContainerEntry(typeof(TResolveType), typeof(TConcretType), lifeCycle));
                }
            }
            /// <summary>
            /// Registers the specified resolve and concerete types in the container, null values are ignored and not registered.
            /// </summary>
            /// <param name="resolveType"></param>
            /// <param name="concreteType"></param>
            public void Register(Type resolveType, Type concreteType)
            {
                Register(resolveType, concreteType, LifeCycle.Singleton);
            }
            /// <summary>
            /// Registers the specified resolve and concerete types in the container, null values are ignored and not registered.
            /// </summary>
            /// <param name="resolveType"></param>
            /// <param name="concreteType"></param>
            /// <param name="lifeCycle"></param>
            public void Register(Type resolveType, Type concreteType, LifeCycle lifeCycle)
            {
                if (resolveType != null && concreteType != null && !entries.Any(x => x.ResolveType == resolveType))
                {
                    entries.Add(new ContainerEntry(resolveType, concreteType, lifeCycle));
                }
            }
            /// <summary>
            /// Returns a concrete instance of the specified resolve type
            /// </summary>
            /// <typeparam name="TResolveType"></typeparam>
            /// <returns></returns>
            public TResolveType Resolve<TResolveType>()
            {
                return (TResolveType)ResolveObject(typeof(TResolveType));
            }
            /// <summary>
            /// Returns a concrete instance of the specified resolve type
            /// </summary>
            /// <param name="resolveType"></param>
            /// <returns></returns>
            public object Resolve(Type resolveType)
            {
                return ResolveObject(resolveType);
            }

            private object ResolveObject(Type resolveType)
            {
                var entry = entries.FirstOrDefault(x => x.ResolveType == resolveType);

                if (entry == null)
                {
                    throw new TypeNotRegisteredException(string.Format("The type {0} has not been registered in IoC container.", resolveType.Name));
                }
                return GetInstance(entry);
            }

            private object GetInstance(ContainerEntry entry)
            {
                if (entry.Instance == null || entry.LifeCycle == LifeCycle.Transient)
                {
                    var parameters = ResolveConstructorParameters(entry);
                    entry.CreateInstance(parameters.ToArray());
                }
                return entry.Instance;
            }

            private IEnumerable<object> ResolveConstructorParameters(ContainerEntry entry)
            {
                var constructorInfo = entry.ConcreteType.GetConstructors().First();

                foreach (var parameter in constructorInfo.GetParameters())
                {
                    yield return ResolveObject(parameter.ParameterType);
                }
            }


        }

        private static IoCContainer _container;

        private static IoCContainer Container
        {
            get
            {
                if (_container == null)
                {
                    _container = new IoCContainer();
                }
                return _container;
            }
        }
        /// <summary>
        /// Registers the specified resolveType and concrete type in the container
        /// </summary>
        /// <typeparam name="TResolveType"></typeparam>
        /// <typeparam name="TConcretType"></typeparam>
        public static void Register<TResolveType, TConcretType>()
        {
            Container.Register<TResolveType, TConcretType>();
        }
        /// <summary>
        /// Registers the specified resolveType and concrete type in the container
        /// </summary>
        /// <typeparam name="TResolveType"></typeparam>
        /// <typeparam name="TConcretType"></typeparam>
        /// <param name="lifeCycle"></param>
        public static void Register<TResolveType, TConcretType>(LifeCycle lifeCycle)
        {
            Container.Register<TResolveType, TConcretType>(lifeCycle);
        }
        /// <summary>
        /// Tries to resolve the specified resolveType by retrieving instance of the 
        /// registered concrete type in the container.
        /// </summary>
        /// <typeparam name="TResolveType"></typeparam>
        /// <returns></returns>
        public static TResolveType Resolve<TResolveType>()
        {
            return Container.Resolve<TResolveType>();
        }
        /// <summary>
        /// Tries to register interfaces only from the collection of types provided by 
        /// having the interface as ResolveType and interface name without "I" as concreteType.
        /// e.g ISomething => Something
        /// </summary>
        /// <param name="types">Only interfaces will be looked up for auto registeration</param>
        public static void AutoRegisterByInterfaces(IEnumerable<Type> types)
        {
            if (types != null)
            {
                foreach (var type in types)
                {
                    if (type.IsInterface)
                    {
                        var concreteTypeExpectedName = type.Name.Substring(1);
                        var concreteType = GetType(concreteTypeExpectedName);
                        Container.Register(type, concreteType);
                    }
                }
            }
        }

        /// <summary>
        /// Tries to register classes only from the collection of types provided by having
        /// the class as a ConcreteType and the ConcreteType prefixed with "I" as the 
        /// Resolve Type, if no interface found the concreteType it self is used only in case AutoRegistrationMode is set to Flexable.
        /// </summary>
        /// <param name="types">Only classes will be lookup for auto registration</param>
        public static void AutoRegisterByClasses(IEnumerable<Type> types, AutoRegisterMode autoRegisterMode = AutoRegisterMode.Strict)
        {
            if (types != null)
            {
                foreach (var type in types)
                {
                    if (type.IsClass)
                    {
                        var resolveTypeExpectedName = "I" + type.Name;
                        var resolveType = GetType(resolveTypeExpectedName);

                        if (resolveType != null)
                        {
                            Container.Register(resolveType, type);
                        }
                        else
                        {
                            if (autoRegisterMode == AutoRegisterMode.Flexable)
                            {
                                Container.Register(type, type);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Tries to register classes only from the collection of types provided by 
        /// having the resolve type and concrete type the same type provided
        /// </summary>
        /// <param name="typesToAutoRegister"></param>
        public static void AutoRegisterByClassesAsIs(IEnumerable<Type> typesToAutoRegister)
        {
            if (typesToAutoRegister != null)
            {
                foreach (var type in typesToAutoRegister)
                {
                    if (type.IsClass)
                    {
                        Container.Register(type, type);
                    }
                }
            }
        }

        private static Type GetType(string typeName)
        {
            return Type.GetType(typeName) ?? AppDomain.CurrentDomain.GetAssemblies()
                .Select(x => x.DefinedTypes.FirstOrDefault(t => t.Name == typeName))
                .FirstOrDefault(x => x != null);
        }
    }
}