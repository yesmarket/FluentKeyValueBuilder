using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentKeyValueBuilder.Attributes;

namespace FluentKeyValueBuilder.Extensions
{
    public static class TypeExtensions
    {
        public static IList<Tuple<string, string>> GetPropertyNiceNames(this Type type)
        {
            var reflectedProperties =
                type
                    .GetPublicProperties()
                    .Select(
                        info =>
                            new Tuple<string, string>(info.Name,
                                info.GetCustomAttributes(typeof (NiceNameAttribute))
                                    .Cast<NiceNameAttribute>()
                                    .Select(attribute => attribute.NiceName)
                                    .FirstOrDefault()))
                    .ToList();


            return reflectedProperties;
        }

        public static PropertyInfo[] GetPublicProperties(this Type type)
        {
            if (!type.IsInterface)
            {
                return type.GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
            }

            var propertyInfos = new List<PropertyInfo>();

            var considered = new List<Type> {type};
            var queue = new Queue<Type>(new[]{type});

            while (queue.Count > 0)
            {
                var subType = queue.Dequeue();
                var subInterfaces = subType.GetInterfaces().Where(subInterface => !considered.Contains(subInterface));

                foreach (var subInterface in subInterfaces)
                {
                    considered.Add(subInterface);
                    queue.Enqueue(subInterface);
                }

                var typeProperties = subType.GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);

                var newPropertyInfos = typeProperties.Where(x => !propertyInfos.Contains(x));

                propertyInfos.InsertRange(0, newPropertyInfos);
            }

            return propertyInfos.ToArray();
        }
    }
}