using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace DiContainer
{
    public class DIContainer
    {
        private readonly Dictionary<Type, List<object>> _instances = new();
        
        public T RegisterNew<T>(bool single = false)
        {
            Type type = typeof(T);
            T instance;
            if (typeof(Component).IsAssignableFrom(type))
                instance = CreateMono<T>();
            else
                instance = Create<T>();
            RegisterInstance<T>(instance, single);
            return instance;
        }

        public void RegisterInstance<T>(T instance, bool single = false)
        {
            Type type = typeof(T);
            if (!_instances.ContainsKey(type))
                _instances[type] = new List<object>();
            if(single)
                _instances[type].Clear();
            _instances[type].Add(instance);
        }
        
        public T Resolve<T>()
        {
            Type type = typeof(T);
            T instance;
            if(TryGetInstance(type, out object rawInstance))
            {
                instance = (T)rawInstance;
            }
            else
            {
                instance = RegisterNew<T>();
            }
            return instance;
        }

        public void Inject<T>(T instance)
        {
            InjectMono<T>(instance);
        }
        
        private T Create<T>()
        {
            Type type = typeof(T);
            var constructors = type.GetConstructors()
                .OrderByDescending(c => c.GetParameters().Length);
            foreach (var constructor in constructors)
            {
                var parameters = constructor.GetParameters();
                var args = new object[parameters.Length];
                bool canResolve = true;
                for (int i = 0; i < parameters.Length; i++)
                {
                    Type paramType = parameters[i].ParameterType;
                    if (TryGetInstance(paramType, out object dependency))
                    {
                        args[i] = dependency;
                    }
                    else
                    {
                        canResolve = false;
                        break;
                    }
                }
                if (canResolve)
                {
                    return (T)constructor.Invoke(args);
                }
            }
            throw new Exception($"Can't create instance {nameof(type)}");
        }

        private T CreateMono<T>()
        {
            Type type = typeof(T);
            GameObject obj = new GameObject(type.Name);
            T instance = (T)(object)obj.AddComponent(type);
            InjectMono<T>(instance);
            return instance;
        }

        private void InjectMono<T>(T instance)
        {
            Type type = typeof(T);
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.GetCustomAttribute<MonoConstructorAttribute>() != null)
                .ToArray();
            if (methods.Length > 0)
            {
                MethodInfo method = methods[0];
                ParameterInfo[] parameters = method.GetParameters();
                var args = new object[parameters.Length];
                for (int i = 0; i < parameters.Length; i++)
                {
                    Type paramType = parameters[i].ParameterType;
                    if (TryGetInstance(paramType, out var dependency))
                    {
                        args[i] = dependency;
                    }
                    else
                    {
                        throw new ArgumentException("The dependencies could not be resolved.");
                    }
                }
                method.Invoke(instance, args);
            }
        }
        
        private bool TryGetInstance(Type type, out object instance)
        {
            if (_instances.TryGetValue(type, out var instances))
            {
                instance = instances.FirstOrDefault(x => x != null);
                if (instance != null)
                    return true;
            }
            foreach (var kvp in _instances)
            {
                Type registeredType = kvp.Key;
                if (type.IsAssignableFrom(registeredType))
                {
                    instance = kvp.Value.FirstOrDefault(x => x != null);
                    if (instance != null)
                        return true;
                }
            }
            instance = null;
            return false;
        }
    }
}

