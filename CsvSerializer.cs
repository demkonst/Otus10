using System.Collections.Generic;

namespace Otus10
{
    public class CsvSerializer<T> where T: new()
    {
        private readonly char _delimiter;

        public CsvSerializer(char delimiter = ',')
        {
            _delimiter = delimiter;
        }

        public string Serialize(T obj)
        {
            var type = obj.GetType();
            var properties = type.GetProperties();

            var values = new List<string>();
            foreach (var propertyInfo in properties)
            {
                values.Add(propertyInfo.GetValue(obj)?.ToString() ?? string.Empty);
            }

            return string.Join(_delimiter, values);
        }

        public T Deserialize(string value)
        {
            var props = value.Split(_delimiter);

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
