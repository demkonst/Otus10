using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Otus10
{
    public class CsvLoader<T> where T: new()
    {
        private readonly CsvSerializer<T> _csvSerializer;

        public CsvLoader(CsvSerializer<T> csvSerializer)
        {
            _csvSerializer = csvSerializer;
        }

        public async Task<List<T>> LoadFromFile(string path)
        {
            var result = new List<T>();

            using var sr = new StreamReader(path);
            while (!sr.EndOfStream)
            {
                var line = await sr.ReadLineAsync();
                result.Add(_csvSerializer.Deserialize(line));
            }

            return result;
        }
    }
}
