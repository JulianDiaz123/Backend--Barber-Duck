using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeluqueriaApi.Models.Auth.Dto;
using PeluqueriaApi.Models.Auth;
using PeluqueriaApi.Services;
using PeluqueriaApi.Utils.Exceptions;
using System.Net;
using PeluqueriaApi.Models.User.Dto;
using PeluqueriaApi.Models.Rol;
using PeluqueriaApi.Enums;
using Microsoft.AspNetCore.Authorization;
using PeluqueriaApi.Models.Rol.Dto;
using PeluqueriaApi.Models.User;
using System.Data;

namespace PeluqueriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthServices _authServices;
        private readonly UserServices _userServices;
        private readonly RolServices _rolServices;
        private readonly IEncoderServices _encoderServices;
        private readonly IMapper _mapper;

        public AuthController(AuthServices authServices, UserServices userServices, RolServices rolServices,IMapper mapper, IEncoderServices encoderServices)
        {
            _authServices = authServices;
            _userServices = userServices;
            _rolServices = rolServices;
            _mapper = mapper;
            _encoderServices = encoderServices;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CustomMessage), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CustomMessage), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] Login login)
        {
            try
            {
                var user = await _userServices.GetOneByUsernameOrEmail(login.Username, login.Email);
                var passwordMatch = _encoderServices.Verify(login.Password, user.password);

                if (!passwordMatch)
                {
                    throw new CustomHttpException("Invalid Credentials", HttpStatusCode.BadRequest);
                }

                var token = _authServices.GenerateJwtToken(user);
                var userMapped = _mapper.Map<UserLoginResponseDTO>(user);
                return Ok(new LoginResponseDTO { Token = token, User = userMapped });
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

        [HttpPost("register")]
        [ProducesResponseType(typeof(CustomMessage), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CustomMessage), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDTO>> Register([FromBody] CreateUserDTO register)
        {
            try
            {
                var user = await _userServices.GetOneByUsernameOrEmail(register.userName, register.email);

                if (user != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new CustomMessage("User already exists"));
                }

                var userCreated = await _userServices.CreateOne(register);

                var defaultRol = await _rolServices.GetOneByName(Roles.USER);

                await _userServices.UpdateRolesById(userCreated.id, new List<Rol> { defaultRol });

                return Created("Register User", userCreated);
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

        [HttpPut("roles/user/{id}")]
        [Authorize(Roles = Roles.ADMIN)]
        [ProducesResponseType(typeof(CustomMessage), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CustomMessage), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> Put(int id, [FromBody] UpdateUserRolesDTO updateUserRolesDto)
        {
            try
            {
                var roles = await _rolServices.GetManyByIds(updateUserRolesDto.RoleIds);
                var userUpdated = await _userServices.UpdateRolesById(id, roles);
                return Ok(userUpdated);
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
