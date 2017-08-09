using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DotNetPivotalTrackerApi.Portable.Services
{
    public static class JsonService
    {
        private static JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings();

        static JsonService()
        {
            //_jsonSerializerSettings.ContractResolver = new CamelCaseExceptDictionaryNamesContractResolver();
            //_jsonSerializerSettings.Converters.Add(new StringEnumConverter());
            _jsonSerializerSettings.ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            _jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        }

        public static string SerializeObjectToJson<T>(T model)
        {
            var json = JsonConvert.SerializeObject(model, Formatting.Indented, _jsonSerializerSettings);
            return json;
        }

        public static T SerializeJsonToObject<T>(string json)
        {
            var obj = JsonConvert.DeserializeObject<T>(json, _jsonSerializerSettings);
            return obj;
        }
    }

    internal class CamelCaseExceptDictionaryNamesContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonDictionaryContract CreateDictionaryContract(Type objectType)
        {
            JsonDictionaryContract contract = base.CreateDictionaryContract(objectType);

            contract.DictionaryKeyResolver = propertyName => propertyName;

            return contract;
        }
    }
}
