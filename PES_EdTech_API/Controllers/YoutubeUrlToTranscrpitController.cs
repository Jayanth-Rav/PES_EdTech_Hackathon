using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PES_EdTech_API.Repo;

namespace PES_EdTech_API.Controllers
{
    [ApiController]
    [Route("youtubeapi/[Controller]")]
    public class YoutubeUrlToTranscriptController : Controller
    {
        private readonly IYoutubeUrlToTranscriptRepo _transcriptRepository;

        public YoutubeUrlToTranscriptController(IYoutubeUrlToTranscriptRepo transcriptRepository)
        {
            _transcriptRepository = transcriptRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTranscript([FromQuery] string videoUrl)
        {
            string transcript = await _transcriptRepository.GetTranscript(videoUrl);
            return string.IsNullOrEmpty(transcript) ? NotFound("Transcript not available.") : Content(transcript, "text/plain");
        }
    }
}
