using Dapper;
using PES_EdTech_API.Model;
using PES_EdTech_API.Model.Data;
using System.Data;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

        static async Task Main()
        {
            string inputText = "Rolls Royce.";
            string prompt = $"Generate a quiz with 5 multiple-choice questions based on the following text:\n\n{inputText}";

            string quiz = await GetGeminiResponse(prompt);
            Console.WriteLine("Generated Quiz:\n" + quiz);
        }

        static async Task<string> GetGeminiResponse(string prompt)
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

                dynamic result = JsonConvert.DeserializeObject(jsonResponse) ?? new object();
                return result?.candidates[0]?.content?.parts[0]?.text ?? "No response";
            }
        }

        public async Task<string> GetQuizQuestions(QuizRequest quizRequest)
        { 
            string prompt = $"Generate a quiz with { quizRequest.NumberOfQuestion } multiple-choice questions based on the following text:\n\n{ quizRequest.QuizTopic }";
            string quiz = await GetGeminiResponse(prompt);
            Console.WriteLine("Generated Quiz:\n" + quiz);
            return quiz;
        }        
    }
}
