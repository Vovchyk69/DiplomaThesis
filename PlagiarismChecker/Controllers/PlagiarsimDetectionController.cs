using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moss.Client;
using Moss.Report.Client;
using PlagiarismChecker.AmazonS3;
using PlagiarismChecker.POCO;
using System.Net.Http;
using System.Threading.Tasks;
using FileReader = System.IO.File;
namespace PlagiarismChecker.Controllers
{
    [ApiController]
    [Route("api/checker")]
    public class PlagiarsimDetectionController : ControllerBase
    {
        private readonly ILogger<PlagiarsimDetectionController> _loggers;

        private readonly IAwsConfiguration _configuration;
        private readonly IAwsStorage _awsStorage;

        public PlagiarsimDetectionController(IAwsConfiguration configuration, ILogger<PlagiarsimDetectionController> logger)
        {
            _loggers = logger;
            _configuration = configuration;
            _awsStorage = new AwsStorage(
                _configuration.AwsAccessKey,
                _configuration.AwsSecretAccessKey,
                _configuration.Region,
                _configuration.BucketName);
        }

        [HttpGet]
        [Route("chat-gpt")]
        public ChatGptResponse Get(string promt)
        {
            if (string.IsNullOrEmpty(promt))
                return new ChatGptResponse { Error = new ChatGptResponseError { Message = "Empty promt" } };

            return null;
        }

        [HttpGet]
        [Route("moss")]
        public IActionResult GetMeasureOfSimilarity(string promt)
        {
            return Ok();
        }

        [HttpPost]
        [Route("moss-report")]
        public async Task<MossReportRow[]> GetMeasureOfSimilarityReport(FilesToCompare files)
        {
            using var clientHandler = new HttpClientHandler();
            using var httpClient = new HttpClient(clientHandler);
            var reportClient = new MossReportClient(httpClient);

            var userId = 987654321;
            var client = new MossClient(userId, new MossClientOptions() { Language = MossLanguage.Cpp });

            var document1 = await _awsStorage.DownloadFileAsync(files.FileName1);
            var document2 = await _awsStorage.DownloadFileAsync(files.FileName2);

            client.AddFile(FileReader.ReadAllBytes(document1), fileDisplayName: "File 1");
            client.AddFile(FileReader.ReadAllBytes(document2), fileDisplayName: "File 2");

            var url = client.Submit();
            var report = await reportClient.GetReport(url).ConfigureAwait(false);
            return report;
        }
    }
}
