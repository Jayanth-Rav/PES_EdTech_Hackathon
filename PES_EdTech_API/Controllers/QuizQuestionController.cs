using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PES_EdTech_API.Model;
using PES_EdTech_API.Repo;

namespace PES_EdTech_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuizQuestionController : ControllerBase
    {
        private readonly IQuizRequest repo;
        public QuizQuestionController(IQuizRequest repo)
        {
            this.repo = repo;
        }
        [HttpPost("GetQuizQuestions")]
        public async Task<IActionResult> GetQuizQuestions([FromBody] QuizRequest quizRequest)
        {
            try
            {
                var quizList = await this.repo.GetQuizQuestions(quizRequest);
                if (quizList != null)
                {
                    return Ok(quizList);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

