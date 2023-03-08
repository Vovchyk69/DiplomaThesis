using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlagiarismChecker.AmazonS3;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlagiarismChecker.Controller
{
    [ApiController]
    [Route("api/documents")]
    [Produces("application/json")]
    public class AwsS3Controller : ControllerBase
    {
        private readonly IAwsConfiguration _configuration;
        private readonly IAwsStorage _awsStorage;

        public AwsS3Controller(IAwsConfiguration configuration)
        {
            _configuration = configuration;
            _awsStorage = new AwsStorage(
                _configuration.AwsAccessKey,
                _configuration.AwsSecretAccessKey,
                _configuration.Region,
                _configuration.BucketName);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult> GetAllDocuments()
        {
            var documents = await _awsStorage.GetFilesAsync();

            return Ok(documents.Select(x => x.Key));
        }


        [HttpPost]
        [Route("filter")]
        public async Task<ActionResult<List<string>>> GetFilesFromS3(IEnumerable<string> documentNames)
        {
            var results = new List<string>();
            foreach (var documentName in documentNames)
            {
                var doc = await _awsStorage.DownloadFileAsync(documentName);
                var file = File(doc, "application/octet-stream", documentName);
                results.Add(doc);
            }

            return Ok(results);

        }

        [HttpPost]
        public async Task<ActionResult<bool>> UploadDocumentToS3(IFormFile file)
        {
            if (file is null || file.Length <= 0)
                return BadRequest("File is required for upload... Input not valid");

            var result = await _awsStorage.UploadFileAsync(file);
            return Ok(result);
        }

        [HttpPost("list")]
        public async Task<ActionResult<bool>> UploadDocumentsToS3(IFormFile[] files)
        {
            if (files is null || files.Length <= 0)
                return BadRequest("File is required for upload... Input not valid");

            foreach (var file in files)
                await _awsStorage.UploadFileAsync(file);

            return Ok(true);
        }

        [HttpDelete("{documentName}")]
        public async Task<ActionResult> DeleteDocumentFromS3(string documentName)
        {
            if (string.IsNullOrEmpty(documentName))
                return BadRequest("File name is required ...");

            await _awsStorage.DeleteFileAsync(documentName);

            return Ok();

        }
    }
}