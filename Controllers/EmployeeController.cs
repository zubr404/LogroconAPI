using System.Threading.Tasks;
using LogroconAPI.ModelsDTO;
using LogroconAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogroconAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/employee/")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        readonly IDtoService<EmployeeDto> service;
        public EmployeeController(IDtoService<EmployeeDto> service)
        {
            this.service = service;
        }

        /// <summary>
        /// Получить информацию о сотруднике
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeDto>> Get(int id)
        {
            var employee = await service.Get(id);
            if (employee != null)
            {
                return Ok(employee);
            }
            return NotFound();
        }
        
        /// <summary>
        /// Создать нового сотрудника
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("new")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create(EmployeeDto request)
        {
            if (request == null)
                return BadRequest();
            var employee = await service.Create(request);
            return CreatedAtAction(employee.FullName, employee);
        }

        /// <summary>
        /// Обновить информацию о сотруднике
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(EmployeeDto request)
        {
            if (request == null)
                return BadRequest();
            await service.Update(request);
            return Ok();
        }
        
        /// <summary>
        /// Удалить сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<ActionResult> Delete(int id)
        {
            if (await service.Delete(id))
                return NoContent();

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}