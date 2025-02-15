using PES_EdTech_API.Model;

namespace PES_EdTech_API.Repo
{
    public interface IQuizRequest
    {
        Task<List<Questions>> GetQuizQuestions(QuizRequest quizRequest);
    }
}
