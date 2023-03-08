using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PlagiarismChecker.POCO;

namespace PlagiarismChecker.Controllers
{
    [ApiController]
    [Route("api/chat-gpt")]
    public class PlagiarsimDetectionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<PlagiarsimDetectionController> _loggers;

        public PlagiarsimDetectionController(IConfiguration configuration, ILogger<PlagiarsimDetectionController> logger)
        {
            _loggers = logger;
        }

        [HttpGet]
        public ChatGptResponse Get(string promt)
        {
            if (string.IsNullOrEmpty(promt))
                return new ChatGptResponse { Error = new ChatGptResponseError { Message = "Empty promt" } };
        
            var gpt = new OpenAIService(new OpenAiOptions { ApiKey = })
        }
    }
}
