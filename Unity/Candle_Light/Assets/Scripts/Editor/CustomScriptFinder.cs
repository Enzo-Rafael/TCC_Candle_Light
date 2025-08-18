using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad] 
public static class CustomScriptFinder{
    public static readonly List<Type> ValidCustomScriptTypes;
    static CustomScriptFinder()
    {
        ValidCustomScriptTypes = new List<Type>();
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var assembly in assemblies){
            try{
                var types = assembly.GetTypes();
                foreach (var type in types){
                    if (!type.IsAbstract && !type.IsInterface &&
                        type.IsSubclassOf(typeof(MonoBehaviour)) &&
                        typeof(ICodeCustom).IsAssignableFrom(type)){
                        ValidCustomScriptTypes.Add(type);
                    }
                }
            }
            catch (ReflectionTypeLoadException){}
        }
        ValidCustomScriptTypes = ValidCustomScriptTypes.OrderBy(t => t.Name).ToList();
    }
}