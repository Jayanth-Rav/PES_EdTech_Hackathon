using System.Threading.Tasks;

namespace PES_EdTech_API.Repo
{
    public interface IYoutubeUrlToTranscriptRepo
    {
        Task<string> GetTranscript(string videoUrl);
    }
}
