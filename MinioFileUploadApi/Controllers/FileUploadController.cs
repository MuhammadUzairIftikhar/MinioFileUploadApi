using Microsoft.AspNetCore.Mvc;
using MinioFileUploadApi.Services; 
using System.IO;
using System.Threading.Tasks;

namespace MinioFileUploadApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileUploadController : ControllerBase
    {
        private readonly MinioService _minioService;

        public FileUploadController(MinioService minioService)
        {
            _minioService = minioService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            using (var stream = file.OpenReadStream())
            {
                await _minioService.UploadFileAsync(file.FileName, stream, file.ContentType);
            }

            return Ok("File uploaded successfully.");
        }
    }
}
