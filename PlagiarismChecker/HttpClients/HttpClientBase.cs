using PlagiarismChecker.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlagiarismChecker.HttpClients
{
    public class HttpClientBase : IHttpClient
    {
        private readonly HttpClient _httpClient;

        public HttpClientBase(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetAsync(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public Task<string> DeleteAsync(string url, int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PostAsync(string url)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PutAsync(string url)
        {
            throw new System.NotImplementedException();
        }
    }
}
