using System.Linq;
using DotNetPivotalTrackerApi.Services;
using Newtonsoft.Json.Linq;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace PivotalTrackerTests.Services
{
    class TestChildObject
    {
        public int[] Array { get; set; }
    }
    class TestJsonObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TestChildObject Child { get; set; }
    }

    public class JsonServiceTests
    {
        private int _id;
        private string _name;
        private int[] _array;

        private string _testJson;
        private TestJsonObject _testJsonObject;
        public JsonServiceTests()
        {
            _id = 1;
            _name = "Test Json";
            _array = new int[] { 0, 1, 2, 3, 4, 5 };

            _testJson = $@"
                {{ 
                    ""id"": {_id}, 
                    ""name"": ""{_name}"", 
                    ""child"": {{
                        ""array"": [{string.Join(",", _array)}]
                    }} 
                }}";

            _testJsonObject = new TestJsonObject
            {
                Id = _id,
                Name = _name,
                Child = new TestChildObject { Array = _array }
            };
        }

        [Fact]
        public void Test_Serialising_Object_To_Json()
        {
            var json = JsonService.SerializeObjectToJson(_testJsonObject);
            var jObject = JObject.Parse(json);

            Assert.AreEqual((int)jObject["id"], _id);
            Assert.AreEqual((string)jObject["name"], _name);
            Assert.IsNotNull(jObject["child"]);
            Assert.AreEqual((int)jObject["child"]["array"].Count(), _array.Count());
        }

        [Fact]
        public void Test_Serialising_Json_To_Object()
        {
            var obj = JsonService.SerializeJsonToObject<TestJsonObject>(_testJson);

            Assert.IsNotNull(obj);
            Assert.AreEqual(obj.Id, _id);
            Assert.AreEqual(obj.Name, _name);
            Assert.IsNotNull(obj.Child);
            Assert.AreEqual(obj.Child.Array.Count(), _array.Count());
        }
    }
}
