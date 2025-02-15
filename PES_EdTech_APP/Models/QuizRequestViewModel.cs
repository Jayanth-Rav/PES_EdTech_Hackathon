namespace PES_EdTech_APP.Models
{
    public class QuizRequestViewModel
    {
        public bool TopicGeneral { get; set; }
        public bool MCQChoice { get; set; }
        public bool Share { get; set; }
        public int NumberOfQuestion { get; set; }
        public bool QuizMode { get; set; }
        public string QuizTopic { get; set; } = String.Empty;
    }
}
