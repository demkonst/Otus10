using Bogus;

namespace Otus10
{
    public class TestClass
    {
        public string Prop0 { get; set; }
        public int Prop1 { get; set; }
        public int Prop2 { get; set; }
        public bool Prop3 { get; set; }
        public string Prop4 { get; set; }
        public string Prop5 { get; set; }
        public string Prop6 { get; set; }
        public string Prop7 { get; set; }
        public string Prop8 { get; set; }

        public static TestClass New()
        {
            return new Faker<TestClass>()
                .RuleForType(typeof(bool), faker => faker.Random.Bool())
                .RuleForType(typeof(int), faker => faker.Random.Int())
                .RuleForType(typeof(string), faker => faker.Random.String2(10))
                .Generate();
        }
    }
}
