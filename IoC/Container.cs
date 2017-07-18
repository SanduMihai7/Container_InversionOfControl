using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using IoC.Interfaces;
using IoC.Exceptions;
using IoC.Services;

namespace IoC
{
    public class Container
    {
        private Dictionary<Type, Type> _map = new Dictionary<Type, Type>();

        public ContainerBuilder For<TSource>()
        {
            return For(typeof(TSource));
        }

        public ContainerBuilder For(Type sourceType)
		{
            return new ContainerBuilder(this, sourceType);
		}

        public TSource Resolve<TSource>()
        {
            return (TSource)Resolve(typeof(TSource));
        }

        public object Resolve(Type sourceType)
		{
            Type typeToInstantiate;
            if (_map.ContainsKey(sourceType))
            {
                typeToInstantiate = _map[sourceType];
            } 
            else if(sourceType.GetTypeInfo().IsGenericType && _map.ContainsKey(sourceType.GetGenericTypeDefinition()))
            {
                var destination = _map[sourceType.GetGenericTypeDefinition()];
                typeToInstantiate = destination.MakeGenericType(sourceType.GenericTypeArguments);
            }
            else if(!sourceType.GetTypeInfo().IsAbstract)
            {
                typeToInstantiate = sourceType;
            }
            else{
                throw new TypeNotRegisteredException(sourceType.FullName);
            }

            return CreateInstance(typeToInstantiate);
		}

        private object CreateInstance(Type destinationType)
        {
            var parameters = destinationType.GetTypeInfo().DeclaredConstructors
                                            .OrderByDescending(c => c.GetParameters().Count())
                                            .First()
                                            .GetParameters()
                                            .Select(p => Resolve(p.ParameterType))
                                            .ToArray();
            return Activator.CreateInstance(destinationType, parameters);
        }

        public class ContainerBuilder
        {
            Container _container;
            Type _sourceType;

            public ContainerBuilder(Container container, Type sourceType)
            {
                _container = container;
                _sourceType = sourceType;
            }

			public ContainerBuilder Use<TDestination>()
			{
                return Use(typeof(TDestination));
			}

			public ContainerBuilder Use(Type destinationType)
			{
                _container._map.Add(_sourceType, destinationType);
                return this;
			}
        }
    }
}
