using Application.DTO;
using Application.DTO.User;
using Application.DTOs.User;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PKIK_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Returns users",
            Description = @"Returns all users.",
            OperationId = "GetAllUsers"
            )]
        [SwaggerResponse(StatusCodes.Status200OK, "The users were returned", Type = typeof(IEnumerable<UserBaseDTO>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult<IEnumerable<UserBaseDTO>>> GetAll()
        {
            return Ok(await _userService.GetAll());
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Returns specific user",
            Description = @"Returns specific user by provided user identificator.",
            OperationId = "GetUser"
            )]
        [SwaggerResponse(StatusCodes.Status200OK, "The user with provided identificator was returned", Type = typeof(UserBaseDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The user you are looking for does not exists")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult<UserBaseDTO>> Get(string id)
        {
            return Ok(await _userService.Get(id));
        }


        [HttpPost]
        [SwaggerOperation(
            Summary = "Registers new user",
            Description = @"Registers user using provided data.",
            OperationId = "Register"
            )]
        [SwaggerResponse(StatusCodes.Status201Created, "The user was registered", Type = typeof(UserBaseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The registration data is invalid")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult> Register([FromBody] UserCreateDTO registerDTO)
        {
            string id = await _userService.Create(registerDTO);
            UserBaseDTO user = await _userService.Get(id);
            return CreatedAtAction(nameof(UserController.Register), new { id }, user);
        }

        //TODO: Think if PUT is not better
        [HttpPost("ConfirmMail")]
        [SwaggerOperation(
            Summary = "Confirms account",
            Description = @"Confirms account using provided data.",
            OperationId = "ConfirmMail"
            )]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The account was confirmed")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The confirmation data is invalid")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The user you are looking for does not exists")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult> ConfirmMail([FromBody] EmailConfirmationDTO confirmationDTO)
        {
            await _userService.ConfirmEmail(confirmationDTO);
            return NoContent();
        }
    }
}
