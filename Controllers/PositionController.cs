using System.Threading.Tasks;
using LogroconAPI.ModelsDTO;
using LogroconAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogroconAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/position/")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        readonly IDtoService<PositionDto> service;
        public PositionController(IDtoService<PositionDto> service)
        {
            this.service = service;
        }

        /// <summary>
        /// Поличить информацию о должности
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get(int id)
        {
            var position = await service.Get(id);
            if(position != null)
            {
                return Ok(position);
            }
            return NotFound();
        }

        /// <summary>
        /// Создать новую должность
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("new")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create(PositionDto request)
        {
            if (request == null)
                return BadRequest();
            var position = await service.Create(request);
            if (position == null)
                return StatusCode(StatusCodes.Status500InternalServerError);
            return CreatedAtAction(position.Name, position);
        }

        /// <summary>
        /// Обновить информацию о должности
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(PositionDto request)
        {
            if (request == null)
                return BadRequest();
            await service.Update(request);
            return Ok();
        }

        /// <summary>
        /// Удалить должность
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            if (await service.Delete(id))
                return NoContent();

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}