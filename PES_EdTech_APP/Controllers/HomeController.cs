using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PES_EdTech_APP.Models;
using System.Diagnostics;
using System.Text;

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
                Questions question = new Questions();
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/QuizQuestion/GetQuizQuestions/GetQuizQuestions", content);
                if (response.IsSuccessStatusCode)
                {
                    string questionData = await response.Content.ReadAsStringAsync();
                    question = JsonConvert.DeserializeObject<Questions>(questionData)!;
                    return RedirectToAction("QuizPage");
                }
                return View(question);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        public IActionResult QuizPage()
        {
            return View();
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
