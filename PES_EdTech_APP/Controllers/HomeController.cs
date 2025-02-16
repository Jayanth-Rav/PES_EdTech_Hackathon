using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PES_EdTech_APP.Models;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.Json;

namespace PES_EdTech_APP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        Uri baseAddress = new Uri("https://localhost:7047/api");
        private readonly HttpClient _client;

        public HomeController(ILogger<HomeController> logger)
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult QuizPreference(string inputText)
        {
            ViewBag.InputText = inputText;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetQuestions(QuizRequestViewModel model)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(model.QuizTopic) &&
                (model.QuizTopic.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                 model.QuizTopic.StartsWith("https://", StringComparison.OrdinalIgnoreCase)))
                {
                    // Create the request model and serialize it as JSON using System.Text.Json.
                    var requestModel = new { VideoUrl = model.QuizTopic };
                    var jsondata = System.Text.Json.JsonSerializer.Serialize(requestModel);

                    // Rename the variable to 'requestContent' to avoid conflict.
                    var requestContent = new StringContent(jsondata, Encoding.UTF8, "application/json");

                    // Make a POST request.
                    HttpResponseMessage transcriptResponse = await _client.PostAsync(
                        _client.BaseAddress + "/youtubeapi/YoutubeUrlToTranscript", requestContent);

                    if (transcriptResponse != null && transcriptResponse.IsSuccessStatusCode)
                    {
                        // Optionally, deserialize the response JSON to get the transcript.
                        var jsonResponse = await transcriptResponse.Content.ReadAsStringAsync();
                        using (JsonDocument doc = JsonDocument.Parse(jsonResponse))
                        {
                            if (doc.RootElement.TryGetProperty("transcript", out JsonElement transcriptElement))
                            {
                                model.QuizTopic = transcriptElement.GetString();
                            }
                        }
                    }
                }

                List<Questions> question = new List<Questions>();
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                // Send request to external API to get quiz questions
                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/QuizQuestion/GetQuizQuestions/GetQuizQuestions", content);

                if (response.IsSuccessStatusCode)
                {
                    // Read response and deserialize into a list of questions
                    string questionData = await response.Content.ReadAsStringAsync();
                    List<Questions> questions = JsonConvert.DeserializeObject<List<Questions>>(questionData)!;

                    // Store questions in TempData to persist across redirection
                    TempData["QuizQuestions"] = JsonConvert.SerializeObject(questions);

                    // Redirect to QuizPage
                    return RedirectToAction("QuizPage");
                }

                TempData["errorMessage"] = "Failed to fetch questions.";
                return RedirectToAction("QuizPage");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("QuizPage");
            }
        }

        public IActionResult QuizPage()
        {
            if (TempData["QuizQuestions"] != null)
            {
                string questionData = TempData["QuizQuestions"]!.ToString()!;
                List<Questions> questions = JsonConvert.DeserializeObject<List<Questions>>(questionData)!;
                return View(questions);
            }

            return View(new List<Questions>()); // Empty list if no data
        }

        public IActionResult QuizResult(float answerCount)
        {
            return View(answerCount);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
