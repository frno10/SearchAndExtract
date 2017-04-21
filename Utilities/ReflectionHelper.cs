using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace Wpf.Frno.SearchAndExtract.Utilities
{
    public class ReflectionHelper
    {
        public static IEnumerable<Type> GetAllTypesInAssembly<TInterface>()
        {
            Type type = typeof(TInterface);
            IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));

            return types;
        }

        public static List<Dictionary<string, string>> ExtractStaticProperties(IEnumerable<Type> searchTypes)
        {
            List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();

            foreach (Type type in searchTypes)
            {
                var propertiesAndValues = GetStaticStringPropertiesValues(type);
                result.Add(propertiesAndValues);
            }
            return result;
        }

        public static List<IClass> ExtractAndMapStaticProperties<IClass>(IEnumerable<Type> searchTypes) where IClass : new()
        {
            List<IClass> result = new List<IClass>();
            List<Dictionary<string, string>> extractedStaticProperties = ExtractStaticProperties(searchTypes);
            PropertyInfo[] properties = typeof(IClass).GetProperties();

            foreach (Dictionary<string, string> extractedProperty in extractedStaticProperties)
            {
                IClass newObject = new IClass();

                foreach(PropertyInfo property in properties)
                {
                    if (property.PropertyType == typeof(string) &&
                        extractedProperty.ContainsKey(property.Name))
                    {
                        property.SetValue(newObject, extractedProperty[property.Name]);
                    }
                }
                result.Add(newObject);
            }

            return result;
        }

        public static Dictionary<string, string> GetStaticStringPropertiesValues(Type objectType)
        {
            return objectType
                      .GetProperties(BindingFlags.Public | BindingFlags.Static)
                      .Where(f => f.PropertyType == typeof(string))
                      .ToDictionary(f => f.Name,
                                    f => (string)f.GetValue(null));
        }

        public static List<IInterface> InitializeTypes<IInterface>(IEnumerable<Type> objectTypes)
        {
            List<IInterface> result = new List<IInterface>();
            foreach(var type in objectTypes.Where(x => !x.Name.StartsWith("I")))
            {
                result.Add((IInterface)Activator.CreateInstance(type, Application.Current.Dispatcher));
            }

            return result;
        }
    }
}
