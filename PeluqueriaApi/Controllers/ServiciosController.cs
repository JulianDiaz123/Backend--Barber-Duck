using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeluqueriaApi.Enums;
using PeluqueriaApi.Models.Servicio;
using PeluqueriaApi.Models.Servicio.Dto;
using PeluqueriaApi.Services;
using PeluqueriaApi.Utils.Exceptions;

namespace PeluqueriaApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.ADMIN)]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        private readonly ServicioServices _servicioServices;

        public ServiciosController(ServicioServices servicioServices)
        {
            _servicioServices = servicioServices;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CustomMessage), StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<List<ServiciosDTO>>> Get()
        {
            try
            {
                var servicios = await _servicioServices.GetAll();
                return Ok(servicios);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new CustomMessage(ex.Message));
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CustomMessage), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CustomMessage), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ServicioDTO>> Get(int id)
        {
            try
            {
                var servicio = await _servicioServices.GetOneById(id);
                return Ok(servicio);
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
        [Authorize(Roles = $"{Roles.ADMIN}, {Roles.MOD}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CustomMessage), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(CustomMessage), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Servicio>> Post([FromBody] CreateServicioDTO createServicioDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var servicio = await _servicioServices.CreateOne(createServicioDto);

                return Created(nameof(Post), servicio);
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
        [Authorize(Roles = $"{Roles.ADMIN}, {Roles.MOD}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CustomMessage), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(CustomMessage), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Servicio>> Put(int id, [FromBody] UpdateServicioDTO updateServicioDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var servicio = await _servicioServices.UpdateOneById(id, updateServicioDto);
                return Ok(servicio);

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
        [Authorize(Roles = $"{Roles.ADMIN}, {Roles.MOD}")]
        [ProducesResponseType(typeof(CustomMessage), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(CustomMessage), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _servicioServices.DeleteOneById(id);
                return Ok(new CustomMessage($"El Servicio con el Id = {id} fue eliminado!"));
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
