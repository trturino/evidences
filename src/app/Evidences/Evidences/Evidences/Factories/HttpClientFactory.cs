using System;
using System.Net.Http;

namespace Evidences.Factories
{
    public static class HttpClientFactory
    {
        private const string API_ENDPOINT = "https://evidences-api.azurewebsites.net";

        public static HttpClient Create()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(API_ENDPOINT);
            return client;
        }
    }
}