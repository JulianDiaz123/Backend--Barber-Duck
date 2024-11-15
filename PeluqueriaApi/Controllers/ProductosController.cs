using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeluqueriaApi.Enums;
using PeluqueriaApi.Models.Producto;
using PeluqueriaApi.Models.Producto.Dto;
using PeluqueriaApi.Models.Servicio;
using PeluqueriaApi.Services;
using PeluqueriaApi.Utils.Exceptions;

namespace PeluqueriaApi.Controllers
{
    [Route("api/productos")]
    [Authorize(Roles = Roles.ADMIN)]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ProductoServices _productoServices;

        public ProductosController(ProductoServices productoServices)
        {
            _productoServices = productoServices;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CustomMessage), StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<List<ProductosDTO>>> Get()
        {
            try
            {
                var productos = await _productoServices.GetAll();
                return Ok(productos);
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
        public async Task<ActionResult<ProductoDTO>> Get(int id)
        {
            try
            {
                var producto = await _productoServices.GetOneById(id);
                return Ok(producto);
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
        public async Task<ActionResult<Producto>> Post([FromBody] CreateProductoDTO createProductoDto)
        {
            try
            {
                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }
                var producto = await _productoServices.CreateOne(createProductoDto);
 
                return Created(nameof(Post), producto);
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
        public async Task<ActionResult<Producto>> Put(int id, [FromBody] UpdateProductoDTO updateProductoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var producto = await _productoServices.UpdateOneById(id, updateProductoDto);
                return Ok(producto);

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
        [ProducesResponseType(typeof(CustomMessage), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CustomMessage), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(CustomMessage), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _productoServices.DeleteOneById(id);
                return Ok(new CustomMessage($"El Producto con el Id = {id} fue eliminado!"));
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
