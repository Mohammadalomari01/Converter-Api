using Converter.Core.Models;
using Converter.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Aspose.Words;
using Aspose.Words.Drawing;

using System.IO;
using System.Diagnostics;
using System.Net.Http;

namespace Converter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversionhistoryController : ControllerBase
    {
        private readonly ICONVERSIONHISTORYService _conversionHistoryService;
        private readonly HttpClient _httpClient;
        public ConversionhistoryController(ICONVERSIONHISTORYService conversionHistoryService, HttpClient httpClient)
        {
            _conversionHistoryService = conversionHistoryService;
            _httpClient = httpClient;
        }

        [HttpGet]
        //[CheckClaims("roleid", "21")]
        public List<Conversionhistory> GetAllConversions()
        {
            return _conversionHistoryService.GetAllConversions();
        }

        [HttpGet]
        [Route("GetConversionById/{id}")]
        //[CheckClaims("roleid", "21")]
        public ActionResult<Conversionhistory> GetConversionById(int id)
        {
            var conversion = _conversionHistoryService.GetConversionById(id);
            if (conversion == null)
            {
                return NotFound($"Conversion with ID {id} not found.");
            }
            return Ok(conversion);
        }

        [HttpPost]
        [Route("CreateConversion")]
        //[CheckClaims("roleid", "21")]
        public IActionResult CreateConversion(Conversionhistory conversionHistory)
        {
            _conversionHistoryService.CreateConversion(conversionHistory);
            return CreatedAtAction(nameof(GetConversionById), new { id = conversionHistory.Conversionid }, conversionHistory);
        }

        [HttpPut]
        [Route("UpdateConversion")]
        //[CheckClaims("roleid", "21")]
        public IActionResult UpdateConversion(Conversionhistory conversionHistory)
        {
            var existingConversion = _conversionHistoryService.GetConversionById((int)conversionHistory.Conversionid);
            if (existingConversion == null)
            {
                return NotFound($"Conversion with ID {conversionHistory.Conversionid} not found.");
            }

            _conversionHistoryService.UpdateConversion(conversionHistory);
            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteConversion/{id}")]
        //[CheckClaims("roleid", "21")]
        public IActionResult DeleteConversion(int id)
        {
            var existingConversion = _conversionHistoryService.GetConversionById(id);
            if (existingConversion == null)
            {
                return NotFound($"Conversion with ID {id} not found.");
            }

            _conversionHistoryService.DeleteConversion(id);
            return NoContent();
        }
        [HttpPost("WordToPdf")]
        public async Task<IActionResult> ConvertWordToPdf(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file was provided.");
            }

            try
            {
                // Save the uploaded Word file to a temporary location
                var tempWordPath = Path.GetTempFileName();
                using (var stream = new FileStream(tempWordPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Load the Word document
                var doc = new Aspose.Words.Document(tempWordPath);

                // Remove watermarks
                RemoveWatermarks(doc);

                // Save the cleaned document to a temporary PDF file
                var tempPdfPath = Path.ChangeExtension(tempWordPath, ".pdf");
                doc.Save(tempPdfPath, Aspose.Words.SaveFormat.Pdf);

                // Return the PDF as a file stream
                var pdfStream = new FileStream(tempPdfPath, FileMode.Open, FileAccess.Read);
                return File(pdfStream, "application/pdf", "ConvertedDocument.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // Helper method to remove watermarks
        private void RemoveWatermarks(Aspose.Words.Document doc)
        {
            // Remove watermark shapes
            foreach (Aspose.Words.Drawing.Shape shape in doc.GetChildNodes(Aspose.Words.NodeType.Shape, true))
            {
                // Check if the shape is a watermark
                if (shape.Name != null && shape.Name.ToLower().Contains("watermark"))
                {
                    shape.Remove();
                }
            }

            // Remove watermarks from headers/footers
            foreach (Aspose.Words.Section section in doc.Sections)
            {
                foreach (Aspose.Words.HeaderFooter headerFooter in section.HeadersFooters)
                {
                    foreach (Aspose.Words.Drawing.Shape shape in headerFooter.GetChildNodes(Aspose.Words.NodeType.Shape, true))
                    {
                        if (shape.Name != null && shape.Name.ToLower().Contains("watermark"))
                        {
                            shape.Remove();
                        }
                    }
                }
            }
        }
        [HttpPost("convert")]
        public async Task<IActionResult> ConvertMp4ToMp3(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            // Save the uploaded MP4 file temporarily
            var filePath = Path.Combine(Path.GetTempPath(), file.FileName);
            var outputFilePath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(file.FileName) + ".mp3");

            // Save the MP4 file to disk
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Path to ffmpeg executable
            var ffmpegPath = "C:\\Users\\VICTUS\\Downloads\\ffmpeg-7.1 (1).tar.xz\\ffmpeg-7.1";  // Replace with the actual path to ffmpeg on your server

            // Set up the process to run ffmpeg and convert the file
            var ffmpegProcess = new Process();
            ffmpegProcess.StartInfo.FileName = ffmpegPath;
            ffmpegProcess.StartInfo.Arguments = $"-i \"{filePath}\" -vn -f mp3 -ab 320k \"{outputFilePath}\"";
            ffmpegProcess.StartInfo.UseShellExecute = false;
            ffmpegProcess.StartInfo.RedirectStandardOutput = true;
            ffmpegProcess.StartInfo.RedirectStandardError = true;
            ffmpegProcess.StartInfo.CreateNoWindow = true;

            // Start the conversion process
            ffmpegProcess.Start();

            // Read the standard error and output streams
            var ffmpegOutput = await ffmpegProcess.StandardError.ReadToEndAsync();
            var ffmpegResult = await ffmpegProcess.StandardOutput.ReadToEndAsync();

            // Wait for the process to exit
            await ffmpegProcess.WaitForExitAsync();

            // Check if ffmpeg process has exited successfully
            if (ffmpegProcess.ExitCode != 0)
            {
                return StatusCode(500, $"Error converting file: {ffmpegOutput}");
            }

            // Read the converted MP3 file
            byte[] mp3Bytes = System.IO.File.ReadAllBytes(outputFilePath);

            // Clean up temporary files
            System.IO.File.Delete(filePath);
            System.IO.File.Delete(outputFilePath);

            // Return the converted MP3 file as a download
            return File(mp3Bytes, "audio/mpeg", "converted.mp3");
        }

    }
}


    
