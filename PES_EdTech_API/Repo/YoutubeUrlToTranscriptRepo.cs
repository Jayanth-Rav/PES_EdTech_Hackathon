using System;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using PES_EdTech_API.Repo;
using YoutubeTranscriptApi; // Ensure this NuGet package is installed

namespace PES_EdTech_API.Repo
{
    public class YoutubeUrlToTranscriptRepo : IYoutubeUrlToTranscriptRepo
    {
        public async Task<string> GetTranscript(string videoUrl)
        {
            string videoId = ExtractVideoId(videoUrl);
            if (string.IsNullOrWhiteSpace(videoId))
            {
                return "Invalid YouTube URL.";
            }

            try
            {
                // Instantiate without using 'using' because it doesn't implement IDisposable
                var youTubeTranscriptApi = new YouTubeTranscriptApi();

                // Call GetTranscript asynchronously (wrap in Task.Run if necessary)
                //var transcriptItems = await Task.Run(() => youTubeTranscriptApi.GetTranscript(videoId, new[] { "en" }));
                var transcriptItems = youTubeTranscriptApi.GetTranscript(videoId);
                // Use Count() extension method from System.Linq
                if (transcriptItems == null || transcriptItems.Count() == 0)
                {
                    return "Transcript not available.";
                }

                var sb = new StringBuilder();
                foreach (var item in transcriptItems)
                {
                    sb.AppendLine(item.Text);
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                return $"Error retrieving transcript: {ex.Message}";
            }
        }

        /// <summary>
        /// Extracts the video ID from the given YouTube URL.
        /// </summary>
        private string ExtractVideoId(string url)
        {
            try
            {
                var uri = new Uri(url);
                var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
                return query["v"];
            }
            catch
            {
                return null;
            }
        }
    }
}
