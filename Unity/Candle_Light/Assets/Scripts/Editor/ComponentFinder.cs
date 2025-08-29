using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class ComponentFinder
{
    private static readonly Dictionary<Type, List<Type>> s_CachedTypes = new Dictionary<Type, List<Type>>();


    public static List<Type> GetTypes(Type interfaceType)
    {
        if (s_CachedTypes.ContainsKey(interfaceType))
        {
            return s_CachedTypes[interfaceType];
        }
        var validTypes = new List<Type>();
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var assembly in assemblies)
        {
            try
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (!type.IsAbstract && !type.IsInterface &&
                        type.IsSubclassOf(typeof(MonoBehaviour)) &&
                        interfaceType.IsAssignableFrom(type))
                    {
                        validTypes.Add(type);
                    }
                }
            }
            catch (ReflectionTypeLoadException) {}
        }

        s_CachedTypes[interfaceType] = validTypes;
        return validTypes;
    }
}