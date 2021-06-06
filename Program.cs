using System;

namespace Otus10
{
    internal class Program
    {
        public static void Main()
        {
            var obj = new TestClass
            {
                Prop0 = "fdf",
                Prop1 = 15547,
                Prop2 = 7784,
                Prop3 = true
            };

            var csv = CsvSerializer.Serialize(obj);
            var newObj = CsvSerializer.Deserialize<TestClass>(csv);

            var deserializedObj = newObj;

            var test1 = obj.Prop0.Equals(deserializedObj.Prop0);
            var test2 = obj.Prop1.Equals(deserializedObj.Prop1);
            var test3 = obj.Prop2.Equals(deserializedObj.Prop2);
            var test4 = obj.Prop3.Equals(deserializedObj.Prop3);

            if (!(test1 && test2 && test3 && test4))
            {
                throw new ArgumentException();
            }
        }
    }
}
