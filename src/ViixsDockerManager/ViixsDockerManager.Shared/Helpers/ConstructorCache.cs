using System.Collections.Concurrent;
using System.Reflection;
using ViixsDockerManager.Shared.Exceptions;
using ViixsDockerManager.Shared.Helpers.Models;

namespace ViixsDockerManager.Shared.Helpers;

internal static class ConstructorCache
{
    private static readonly ConcurrentDictionary<Type, ConstructorMetadata> _typeConstructorCache =
        new ConcurrentDictionary<Type, ConstructorMetadata>();

    public static ConstructorMetadata GetOrAddConstructorMetadata<TImplementationType>()
        => GetOrAddConstructorMetadata(typeof(TImplementationType));

    private static ConstructorMetadata GetOrAddConstructorMetadata(Type implementationType)
    {
        return _typeConstructorCache.GetOrAdd(implementationType, type =>
        {
            var constructors = implementationType.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
            var firstConstructor = CheckAndGetConstructor(constructors, type.Name);

            return new ConstructorMetadata(firstConstructor, firstConstructor.GetParameters());
        });
    }

    private static ConstructorInfo CheckAndGetConstructor(ConstructorInfo[] constructors, string typeName)
    {
        switch (constructors)
        {
            case { Length: 0 }:
                throw ViixsDockerManagerException.InvalidOperation(
                    $"Cannot register {typeName}: No public constructor found.");
            case { Length: > 1 }:
                throw ViixsDockerManagerException.InvalidOperation(
                    $"Cannot register {typeName}: Only a single public constructor is allowed for services in the ServiceProvider.");
        }

        return constructors[0];
    }
}
