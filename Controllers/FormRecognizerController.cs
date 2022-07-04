using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScannedAPI.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace ScannedAPI.Controllers
{
    [ApiController]
    [Route("api/form-recognizer")]
    public class FormRecognizerController : ControllerBase
    {
        private readonly IFormRecognizerService _formRecognizer;

        public FormRecognizerController(IFormRecognizerService formRecognizer)
        {
            _formRecognizer = formRecognizer ?? throw new ArgumentNullException(nameof(formRecognizer));
        }

        [HttpPost]
        [Route("upload/file")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var result = await _formRecognizer.AnalyzeReceipt(file);
            return Ok(result);
        }

        [HttpPost]
        [Route("upload/url")]
        public async Task<IActionResult> Upload([FromBody] string url)
        {
            var result = await _formRecognizer.AnalyzeReceipt(url);
            return Ok(result);
        }
    }
}
