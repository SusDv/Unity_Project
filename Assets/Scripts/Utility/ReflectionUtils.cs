using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Utility
{
    public static class ReflectionUtils
    {
        public static List<T> GetConcreteInstances<T>()
            where T : class
        {
            var assembly = Assembly.GetAssembly(typeof(T));

            var concreteClasses = assembly.GetTypes()
                .Where(t => typeof(T).IsAssignableFrom(t) && !t.IsAbstract);

            return concreteClasses.Select(concreteClass => 
                Activator.CreateInstance(concreteClass) as T).ToList();
        }
    }
}