using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Otus10
{
    public class CsvLoader
    {
        private readonly CsvSerializer _csvSerializer;

        public CsvLoader(CsvSerializer csvSerializer)
        {
            _csvSerializer = csvSerializer;
        }

        public async Task<List<T>> LoadFromFile<T>(string path) where T: new()
        {
            var result = new List<T>();

            using var sr = new StreamReader(path);
            while (!sr.EndOfStream)
            {
                var line = await sr.ReadLineAsync();
                result.Add(_csvSerializer.Deserialize<T>(line));
            }

            return result;
        }
    }
}
