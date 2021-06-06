using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Otus10
{
    internal class Program
    {
        private static CsvSerializer _csvSerializer;

        public static async Task Main()
        {
            var obj = TestClass.New();
            _csvSerializer = new CsvSerializer();
            TestCsv(obj);
            TestJson(obj);

            const string path = "data.csv";
            const int count = 1_000;
            await GenerateCsv(path, count);
            var csvLoader = new CsvLoader(_csvSerializer);
            var items = await csvLoader.LoadFromFile<TestClass>(path);
            Console.WriteLine($"CsvLoader has loaded {items.Count} items");

            Console.WriteLine();
            Console.ReadKey();
        }

        private static void TestCsv(TestClass obj)
        {
            var serializeCsvElapseResult = FuncElapser.ElapseWithResult(() =>
            {
                string csv = null;

                for (var i = 0; i < 100_000; i++)
                {
                    csv = _csvSerializer.Serialize(obj);
                }

                return csv;
            });

            var deserializeCsvElapseResult = FuncElapser.ElapseWithResult(() =>
            {
                TestClass result = null;

                for (var i = 0; i < 100_000; i++)
                {
                    result = _csvSerializer.Deserialize<TestClass>(serializeCsvElapseResult.Result);
                }

                return result;
            });

            var sw = new Stopwatch();
            sw.Start();

            Console.WriteLine("Serialize result:");
            Console.WriteLine($"csv: \"{serializeCsvElapseResult.Result}\"");
            Console.WriteLine($"Elapsed ms: {serializeCsvElapseResult.Elapsed.Milliseconds}");

            Console.WriteLine("Deserialize result:");
            Console.WriteLine($"elapsed ms: {deserializeCsvElapseResult.Elapsed.Milliseconds}");

            sw.Stop();

            Console.WriteLine($"Console.WriteLine elapsed ms: {sw.ElapsedMilliseconds}");

            var test1 = obj.Prop0.Equals(deserializeCsvElapseResult.Result.Prop0);
            var test2 = obj.Prop1.Equals(deserializeCsvElapseResult.Result.Prop1);
            var test3 = obj.Prop2.Equals(deserializeCsvElapseResult.Result.Prop2);
            var test4 = obj.Prop3.Equals(deserializeCsvElapseResult.Result.Prop3);
            Console.WriteLine($"All tests passed: {test1 && test2 && test3 && test4}");
        }

        private static void TestJson(TestClass obj)
        {
            var serializeElapseResult = FuncElapser.ElapseWithResult(() =>
            {
                string csv = null;

                for (var i = 0; i < 100_000; i++)
                {
                    csv = JsonSerializer.Serialize(obj);
                }

                return csv;
            });

            var deserializeElapseResult = FuncElapser.ElapseWithResult(() =>
            {
                TestClass result = null;

                for (var i = 0; i < 100_000; i++)
                {
                    result = JsonSerializer.Deserialize<TestClass>(serializeElapseResult.Result);
                }

                return result;
            });

            Console.WriteLine("JSON serialize result:");
            Console.WriteLine($"Elapsed ms: {serializeElapseResult.Elapsed.Milliseconds}");

            Console.WriteLine("JSON deserialize result:");
            Console.WriteLine($"elapsed ms: {deserializeElapseResult.Elapsed.Milliseconds}");
        }

        private static async Task GenerateCsv(string path, int count)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            await using var sw = File.AppendText(path);

            for (var i = 0; i < count; i++)
            {
                var line = _csvSerializer.Serialize(TestClass.New());
                await sw.WriteLineAsync(line);
            }
        }
    }
}
