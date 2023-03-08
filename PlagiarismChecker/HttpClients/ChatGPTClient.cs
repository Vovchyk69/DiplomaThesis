using PlagiarismChecker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlagiarismChecker.HttpClients
{
    public class ChatGPTClient
    {
        private readonly IHttpClient _httpClient;

        public ChatGPTClient(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetSomeData()
        {
            return await _httpClient.GetAsync("https://example.com/api/data");
        }
    }
}
