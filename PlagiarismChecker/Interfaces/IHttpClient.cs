using System.Threading.Tasks;

namespace PlagiarismChecker.Interfaces
{
    public interface IHttpClient
    {
        Task<string> GetAsync(string url);
        Task<string> PostAsync(string url);
        Task<string> PutAsync(string url);
        Task<string> DeleteAsync(string url, int id);
    }
}
