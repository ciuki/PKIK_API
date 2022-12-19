using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PKIK_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost("/login")]
        [SwaggerOperation(
            Summary = "Logs user in",
            Description = @"Logs user in using provided data.
                            Returns JWT token with its expiration date",
            OperationId = "Login"
            )]
        [SwaggerResponse(StatusCodes.Status200OK, "Login success", Type = typeof(LoginResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "User data is invalid, he is not activated or he is locked in")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] LoginDTO loginDTO)
        {
            return Ok(await _authenticationService.Login(loginDTO));
        }



    }
}
