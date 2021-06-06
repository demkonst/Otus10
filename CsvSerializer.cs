using System.Collections.Generic;

namespace Otus10
{
    public static class CsvSerializer
    {
        private const char Delimiter = ',';

        public static string Serialize<T>(T obj) where T: new()
        {
            var type = obj.GetType();
            var properties = type.GetProperties();

            var values = new List<string>();
            foreach (var propertyInfo in properties)
            {
                values.Add(propertyInfo.GetValue(obj)?.ToString() ?? string.Empty);
            }

            return string.Join(Delimiter, values);
        }

        public static T Deserialize<T>(string value) where T: new()
        {
            var props = value.Split(Delimiter);

            var result = new T();
            var properties = result.GetType().GetProperties();

            var i = 0;
            foreach (var prop in props)
            {
                properties[i].SetValue(result, Parse(prop));
                i++;
            }

            return result;
        }

        private static object Parse(string value)
        {
            if (bool.TryParse(value, out var boolResult))
            {
                return boolResult;
            }

            if (int.TryParse(value, out var intResult))
            {
                return intResult;
            }

            return value;
        }
    }
}
