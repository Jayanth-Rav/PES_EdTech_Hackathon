using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PES_EdTech_API.Model;
using PES_EdTech_API.Repo;

namespace PES_EdTech_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepo repo;
        public EmployeeController(IEmployeeRepo repo)
        {
            this.repo = repo;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var _list = await this.repo.GetAll();
                if (_list != null)
                {
                    return Ok(_list);
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

        [HttpGet("GetbyCode/{emp_id}")]
        public async Task<IActionResult> GetbyCode(int emp_id)
        {
            try
            {
                var _list = await this.repo.Getbycode(emp_id);
                if (_list != null)
                {
                    return Ok(_list);
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

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] Employee employee)
        {
            try
            {
                var _result = await this.repo.Insert(employee);
                return Ok(_result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] Employee employee)
        {
            try
            {

                var _result = await this.repo.Update(employee);
                return Ok(_result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete/{emp_id}")]
        public async Task<IActionResult> Delete(int emp_id)
        {
            try
            {
                var _result = await this.repo.Delete(emp_id);
                return Ok(_result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
