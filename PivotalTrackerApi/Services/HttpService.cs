using DotNetPivotalTrackerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DotNetPivotalTrackerApi.Exceptions;

namespace DotNetPivotalTrackerApi.Services
{ 
    public static class HttpService
    {
        private static string _apiToken;
        public static string ApiToken
        {
            get { return _apiToken; }
        }

        private static readonly string _baseUrl = "https://www.pivotaltracker.com/services/v5/";
        private static HttpClient _httpClient = new HttpClient();

        private static StringContent CreateStringContent(string content)
        {
            return new StringContent(content, Encoding.UTF8, "application/json");
        }

        public static void SetupHttpClient(string apiToken)
        {
            _apiToken = apiToken;
            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.DefaultRequestHeaders.Add("X-TrackerToken", _apiToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        
        /// <summary>
        /// Calls a GET request on the specified path.
        /// </summary>
        /// <param name="path">URL of the path to call as a string.</param>
        /// <returns>HttpResponseMessage</returns>
        public static async Task<HttpResponseMessage> GetAsync(string path)
        {
            CheckTokenExists();
            return _httpClient.GetAsync(path).Result;
        }

        /// <summary>
        /// Calls a POST request on the specified path, posting the model data as JSON.
        /// </summary>
        /// <typeparam name="T">Type to return deserialised JSON as.</typeparam>
        /// <param name="path">URL of the path to call as a string.</param>
        /// <param name="data">Model data to be serialised as JSON.</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PostAsync<T>(string path, T data)
        {
            CheckTokenExists();
            var content = CreateStringContent(JsonService.SerializeObjectToJson(data));
            return _httpClient.PostAsync(path, content).Result;
        }

        /// <summary>
        /// Calls a POST request on the specified path, posting the model data as JSON.
        /// </summary>
        /// <typeparam name="T">Type to return deserialised JSON as.</typeparam>
        /// <param name="path">URL of the path to call as a string.</param>
        /// <param name="data">Model data to be serialised as JSON.</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PutAsync<T>(string path, T data)
        {
            CheckTokenExists();
            var content = CreateStringContent(JsonService.SerializeObjectToJson(data));
            return _httpClient.PutAsync(path, content).Result;
        }

        /// <summary>
        /// Calls a POST request on the specified path, passing data as HttpContent.
        /// </summary>
        /// <typeparam name="T">Type to return deserialised JSON as.</typeparam>
        /// <param name="path">URL of the path to call as a string.</param>
        /// <param name="data">Data to be sent in the POST request as HttpContent</param>
        /// <param name="serialiseToJson">Whether or not to serialise as JSON. Calls <see cref="PostAsync{T}(string, T)"/> if true. Default: false.</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PostContentAsync<T>(string path, T data, bool serialiseToJson = false) where T : HttpContent
        {
            CheckTokenExists();
            if (serialiseToJson)
                return await PostAsync<T>(path, data);

            return _httpClient.PostAsync(path, data).Result;
        }

        public static async Task<HttpResponseMessage> DeleteAsync(string path)
        {
            CheckTokenExists();
            return _httpClient.DeleteAsync(path).Result;
        }

        private static void CheckTokenExists()
        {
            if (string.IsNullOrEmpty(_apiToken))
                throw new PivotalHttpException("Api Token has not been set. Please set it using HttpService.SetupHttpClient(string apiToken).");
        }
    }
}
