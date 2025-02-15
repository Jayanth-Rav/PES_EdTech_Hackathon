using Newtonsoft.Json;

namespace PES_EdTech_APP.Models
{
    public class Questions
    {
        [JsonProperty("Question")]
        public string Question { get; set; } = string.Empty;

        public List<Choice> Options { get; set; } = new List<Choice>();

        [JsonProperty("AnswerId")]  // ✅ Ensure this matches AI response
        public int AnswerId { get; set; }
    }

    public class Choice
    {
        [JsonProperty("OptionId")]
        public int OptionId { get; set; }

        [JsonProperty("Option")]
        public string Option { get; set; } = string.Empty;
    }
}
