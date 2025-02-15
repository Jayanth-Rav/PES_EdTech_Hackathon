using Dapper;
using PES_EdTech_API.Model;
using PES_EdTech_API.Model.Data;
using System.Data;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace PES_EdTech_API.Repo
{
    public class QuizRequestRepo : IQuizRequest
    {
        private readonly DapperDBContext context;
        public QuizRequestRepo(DapperDBContext context)
        {
            this.context = context;
        }
        private static readonly string apiKey = "AIzaSyDRI5oNcpVaCHAwgh2qGHhDyXtwFbggikc";
        private static readonly string apiUrl = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent?key={apiKey}";

        //static async Task Main()
        //{
        //    string inputText = "Rolls Royce.";
        //    string prompt = $"Generate a quiz with 5 multiple-choice questions based on the following text:\n\n{inputText}";

        //    string quiz = await GetGeminiResponse(prompt);
        //    Console.WriteLine("Generated Quiz:\n" + quiz);
        //}

        static async Task<List<Questions>> GetGeminiResponse(string prompt)
        {
            using (HttpClient client = new HttpClient())
            {
                var requestBody = new
                {
                    contents = new[]
                    {
                new { parts = new[] { new { text = prompt } } }
            }
                };

                string jsonRequest = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                string jsonResponse = await response.Content.ReadAsStringAsync();

                Console.WriteLine("🔹 AI Raw Response: " + jsonResponse);

                dynamic result = JsonConvert.DeserializeObject(jsonResponse);
                string aiResponse = result?.candidates[0]?.content?.parts[0]?.text ?? "{}";

                aiResponse = ExtractJson(aiResponse);

                if (!aiResponse.StartsWith("{") && !aiResponse.StartsWith("["))
                {
                    Console.WriteLine("❌ Invalid JSON response detected!");
                    return new List<Questions>();
                }

                Console.WriteLine("🔹 AI Extracted JSON: " + aiResponse);

                var parsedObject = JsonConvert.DeserializeObject<dynamic>(aiResponse);
                if (parsedObject != null && parsedObject.Questions != null)
                {
                    string quizJson = JsonConvert.SerializeObject(parsedObject.Questions);

                    Console.WriteLine("🔹 Serialized JSON Before Deserialization: " + quizJson);

                    var questionsList = JsonConvert.DeserializeObject<List<Questions>>(quizJson) ?? new List<Questions>();

                    // 🚀 Ensure AnswerId correctly maps to an existing OptionId
                    foreach (var question in questionsList)
                    {
                        Console.WriteLine($"🔹 Processing Question: {question.Question}");

                        // Ensure OptionId exists
                        var correctChoice = question.Options.FirstOrDefault(o => o.OptionId == question.AnswerId);
                        if (correctChoice == null)
                        {
                            Console.WriteLine($"⚠️ Invalid AnswerId '{question.AnswerId}' for question: {question.Question}");
                            question.AnswerId = 0;
                        }

                        Console.WriteLine($"✅ Final AnswerId for '{question.Question}': {question.AnswerId}");
                    }

                    return questionsList;
                }

                return new List<Questions>();
            }
        }

        // ✅ **Helper method to extract JSON**
        static string ExtractJson(string input)
        {
            Match match = Regex.Match(input, @"(\{.*\}|\[.*\])", RegexOptions.Singleline);
            return match.Success ? match.Value : "{}"; // Returns JSON part or "{}" if not found
        }

        public async Task<List<Questions>> GetQuizQuestions(QuizRequest quizRequest)
        {
            string inputText = quizRequest.QuizTopic;

            string prompt = $@"
Generate a JSON response **ONLY**, no extra text.
Create a quiz with {quizRequest.NumberOfQuestion} multiple-choice questions based on this passage:

{inputText}

### JSON Format:
{{
    ""Questions"": [
        {{
            ""Question"": ""Who developed the theory of relativity?"",
            ""Options"": [
                {{ ""OptionId"": 1, ""Option"": ""Isaac Newton"" }},
                {{ ""OptionId"": 2, ""Option"": ""Albert Einstein"" }},
                {{ ""OptionId"": 3, ""Option"": ""Nikola Tesla"" }},
                {{ ""OptionId"": 4, ""Option"": ""Stephen Hawking"" }}
            ],
            ""AnswerId"": 2
        }}
    ]
}}

- `OptionId` should be a unique integer (1,2,3,4).
- `Option` should be a string (e.g., ""Albert Einstein"").
- `AnswerId` should reference the correct `OptionId`.
";

            List<Questions> quiz = await GetGeminiResponse(prompt);
            return quiz ?? new List<Questions>();
        }


    }
}
