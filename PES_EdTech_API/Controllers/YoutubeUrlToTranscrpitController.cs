using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PES_EdTech_API.Model;
using PES_EdTech_API.Repo;

namespace PES_EdTech_API.Controllers
{
    [ApiController]
    [Route("api/youtubeapi/[Controller]")]
    public class YoutubeUrlToTranscriptController : Controller
    {
        private readonly IYoutubeUrlToTranscriptRepo _transcriptRepository;

        public YoutubeUrlToTranscriptController(IYoutubeUrlToTranscriptRepo transcriptRepository)
        {
            _transcriptRepository = transcriptRepository;
        }

        [HttpPost]
        public async Task<IActionResult> GetTranscript([FromBody] TranscriptRequest request)
        {
            string transcript = await _transcriptRepository.GetTranscript(request.VideoUrl);
            if (string.IsNullOrEmpty(transcript))
            {
                return NotFound(new { error = "Transcript not available." });
            }
            // Return JSON containing the transcript in the response body.
            return Ok(new { transcript = transcript });
        }
    }
}
