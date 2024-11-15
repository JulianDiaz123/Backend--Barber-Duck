using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeluqueriaApi.Enums;
using PeluqueriaApi.Models.Turno;
using PeluqueriaApi.Models.Turno.Dto;
using PeluqueriaApi.Services;
using PeluqueriaApi.Utils.Exceptions;

namespace PeluqueriaApi.Controllers
{
    [Route("api/turnos")]
    [Authorize(Roles = Roles.ADMIN)]
    [ApiController]
    public class TurnosController : ControllerBase
    {
        private readonly TurnoServices _turnoServices;
        public TurnosController(TurnoServices turnoServices)
        {
            _turnoServices = turnoServices;
        }

        [HttpGet]
        [Authorize(Roles = $"{Roles.ADMIN}, {Roles.MOD}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(CustomMessage), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<TurnosDTO>>> Get()
        {
            try
            {
                var turnos = await _turnoServices.GetAll();
                return Ok(turnos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new CustomMessage(ex.Message));
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = $"{Roles.ADMIN}, {Roles.MOD}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(CustomMessage), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CustomMessage), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TurnoDTO>> Get(int id)
        {
            try
            {
                var turno = await _turnoServices.GetOneById(id);
                return Ok(turno);
            }
            catch (CustomHttpException ex)
            {
                return StatusCode((int)ex.StatusCode, new CustomMessage(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new CustomMessage(ex.Message));
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CustomMessage), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(CustomMessage), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Turno>> Post([FromBody] CreateTurnoDTO createTurnoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var turno = await _turnoServices.CreateOne(createTurnoDto);

                return Created(nameof(Post), turno);
            }
            catch (CustomHttpException ex)
            {
                return StatusCode((int)ex.StatusCode, new CustomMessage(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new CustomMessage(ex.Message));
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CustomMessage), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(CustomMessage), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Turno>> Put(int id, [FromBody] UpdateTurnoDTO updateTurnoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var turno = await _turnoServices.UpdateOneById(id, updateTurnoDto);
                return Ok(turno);

            }
            catch (CustomHttpException ex)
            {
                return StatusCode((int)ex.StatusCode, new CustomMessage(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new CustomMessage(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CustomMessage), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CustomMessage), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CustomMessage), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _turnoServices.DeleteOneById(id);
                return Ok(new CustomMessage($"El turno con el Id = {id} fue eliminado!"));
            }
            catch (CustomHttpException ex)
            {
                return StatusCode((int)ex.StatusCode, new CustomMessage(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new CustomMessage(ex.Message));
            }
        }
    }
}
