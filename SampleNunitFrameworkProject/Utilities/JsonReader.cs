using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SampleNunitFrameworkProject.Utilities
{
    public class JsonReader
    {
        private JToken jsonObject;
        public JsonReader()
        {
            string testData = File.ReadAllText("TestData/TestData.json");
            jsonObject = JToken.Parse(testData);
        }

        public string ExtractData(String tokenName)
        {
            return jsonObject.SelectToken(tokenName).Value<string>();
        }
        public List<string> ExtractArrayData(String tokenName)
        {
            return jsonObject.SelectToken(tokenName).Values<string>().ToList();
        }

        public string ExtractDatatest(String tokenName)
        {
            string testData = File.ReadAllText("TestData/TestData.json");
            var jsonObject = JsonSerializer.Deserialize<Dictionary<string, string>>(testData);
            return jsonObject.GetValueOrDefault(tokenName);
        }
    }
}
