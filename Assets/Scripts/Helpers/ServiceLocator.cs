using System;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
    // Dictionary to store registered services
    private static readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

    // Lock object for thread safety (if using in multi-threaded scenarios)
    private static readonly object lockObj = new object();

    // Register a service, ensure it's only registered once
    public static void Register<T>(T service)
    {
        lock (lockObj)
        {
            if (services.ContainsKey(typeof(T)))
                return;

            services[typeof(T)] = service;
        }
    }

    // Unregister a service
    public static void Deregister<T>()
    {
        lock (lockObj)
        {
            if (!services.ContainsKey(typeof(T)))
            {
                Debug.LogWarning($"Service of type {typeof(T)} not found.");
                return;
            }

            services.Remove(typeof(T));
        }
    }

    // Retrieve a service, throws exception if not found
    public static T Get<T>()
    {
        lock (lockObj)
        {
            // Check if the service is registered
            if (services.ContainsKey(typeof(T)))
            {
                return (T)services[typeof(T)];
            }

            // If not found, throw an exception
            throw new Exception($"Service of type {typeof(T)} not found.");
        }
    }

    // Try to retrieve a service, returns null if not found
    public static bool TryGet<T>(out T service)
    {
        lock (lockObj)
        {
            if (services.ContainsKey(typeof(T)))
            {
                service = (T)services[typeof(T)];
                return true;
            }

            service = default;
            return false;
        }
    }

    // Clear all registered services (useful for cleanup)
    public static void Clear()
    {
        lock (lockObj)
        {
            services.Clear();
        }
    }
}