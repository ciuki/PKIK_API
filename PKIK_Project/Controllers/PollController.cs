using Application.DTOs.Poll;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PKIK_Project.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace PKIK_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class PollController : ControllerBase
    {
        private readonly IPollService _pollService;

        public PollController(IPollService pollService)
        {
            _pollService = pollService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Returns all public polls",
            Description = @"Returns all public polls.",
            OperationId = "GetAllPolls"
            )]
        [SwaggerResponse(StatusCodes.Status200OK, "The public polls were returned", Type = typeof(IEnumerable<PollBaseDTO>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult<IEnumerable<PollBaseDTO>>> GetAll()
        {
            return Ok(await _pollService.GetAll());
        }

        [HttpGet("{pollId}")]
        [SwaggerOperation(
            Summary = "Returns specific poll",
            Description = @"Returns specific poll by provided poll identificator.
                            The poll must be public, hidden or you must be in allowed users.
                            If AllowAnonymous is false then you have to be logged in",
            OperationId = "GetPoll"
            )]
        [SwaggerResponse(StatusCodes.Status200OK, "The poll with provided identificator was returned", Type = typeof(PollBaseDTO))]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "You do not have permissions to access this resource")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The poll you are looking for does not exists")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult<PollBaseDTO>> Get(int pollId)
        {
            return Ok(await _pollService.Get(pollId));
        }

        [HttpGet("GetInfo/{pollId}")]
        [SwaggerOperation(
            Summary = "Returns specific poll info",
            Description = @"Returns specific poll info by provided poll identificator.
                            The poll must be public, hidden or you must be in allowed users.
                            If AllowAnonymous is false then you have to be logged in",
            OperationId = "GetPollInfo"
            )]
        [Authorize]
        [SwaggerResponse(StatusCodes.Status200OK, "The poll information for provided identificator were returned", Type = typeof(PollBaseDTO))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "You must be logged in to access this resource")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "You do not have permissions to access this resource")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The poll you are looking for does not exists")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult<PollBaseDTO>> GetInfo(int pollId)
        {
            return Ok(await _pollService.GetInfo(pollId));
        }

        [HttpGet("MyPolls")]
        [SwaggerOperation(
            Summary = "Returns all polls for logged in user",
            Description = @"Returns all polls that user created or is allowed to vote in by JWT Token.
                            The poll must be protected or private and you must be in allowed users.",
            OperationId = "GetMyPolls"
            )]
        [Authorize]
        [SwaggerResponse(StatusCodes.Status200OK, "The polls for current user were returned", Type = typeof(IEnumerable<PollBaseDTO>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "You must be logged in to access this resource")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "You do not have permissions to access this resource")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult<IEnumerable<PollBaseDTO>>> GetMyPolls()
        {
            return Ok(await _pollService.GetMyPolls());
        }

        /*
         * TODO: Decide if should be removed
         * [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<PollBaseDTO>>> Put(int id, PollCreateDTO poll)
        {
            return Ok(await _pollService.Update(poll,id));
        }*/

        [HttpPut("Activate/{pollId}")]
        [SwaggerOperation(
            Summary = "Activates poll",
            Description = @"Activates poll by provided poll identificator.
                            You must be poll owner or moderator to activate it.
                            Your eligibility status is based on JWT Token.",
            OperationId = "ActivatePoll"
            )]
        [Authorize]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Successfully activated poll with provided indentificator")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The poll data is invalid")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "You must be logged in to access this resource")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "You do not have permissions to access this resource")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The poll you are looking for does not exists")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult> Activate(int pollId)
        {
            await _pollService.OpenPoll(pollId);
            return NoContent();
        }

        [HttpPut("Invite/{pollId}")]
        [SwaggerOperation(
            Summary = "Invites to poll",
            Description = @"Adds users to AllowedUsers in poll provided by poll identificator.
                            If the poll is running it also sends notification and mail.
                            If the poll is not running only notification is sent.",
            OperationId = "InviteToPoll"
            )]
        [Authorize]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Successfully invited users to poll with provided indentificator")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "You must be logged in to access this resource")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "You do not have permissions to access this resource")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The poll you are looking for does not exists")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult> InviteToPoll(int pollId, PollInviteDTO pollInviteDTO)
        {
            await _pollService.InviteUsers(pollId, pollInviteDTO);
            return NoContent();
        }

        [HttpPut("SetModerators/{pollId}")]
        [SwaggerOperation(
            Summary = "Sets poll moderators",
            Description = @"Sets new moderator list by provided userIds list in poll provided by poll identificator.
                            It also sends notification about beeing moderator",
            OperationId = "SetPollModerators"
            )]
        [Authorize]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Successfully set moderators for poll with provided indentificator")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "You must be logged in to access this resource")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "You do not have permissions to access this resource")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The poll you are looking for does not exists")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult> SetPollModerators(int pollId, PollInviteDTO pollInviteDTO)
        {
            await _pollService.SetPollModerators(pollId, pollInviteDTO);
            return NoContent();
        }

        [HttpDelete("{pollId}")]
        [SwaggerOperation(
            Summary = "Deactivates poll",
            Description = @"Deactivates poll using provided poll identificator.
                            It also sends notification that poll ended.
                            You must be poll owner or moderator to activate it.
                            Your eligibility status is based on JWT Token.",
            OperationId = "DeactivatePoll"
            )]
        [Authorize]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Successfully removed poll with provided indentificator")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "You must be logged in to access this resource")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "You do not have permissions to access this resource")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The poll you are looking for does not exists")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult> Delete(int pollId)
        {
            await _pollService.ClosePoll(pollId);
            return NoContent();
        }

        [HttpPut("OpenPolls")]
        [SwaggerOperation(
            Summary = "Opens polls",
            Description = @"Opens all polls which start date is in past and end date in future.
                            Dedicated for Service Worker, requires API Key authentication",
            OperationId = "OpenAllPolls"
            )]
        [Authorize(AuthenticationSchemes = ApiKeyAuthenticationOptions.DefaultScheme)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Successfully activated polls")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "This endpoint is designed for Service Worker")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult> OpenAllPolls()
        {
            await _pollService.OpenAllPolls();
            return NoContent();
        }

        [HttpPut("ClosePolls")]
        [SwaggerOperation(
            Summary = "Closes polls",
            Description = @"Closes all polls which end date is in past.
                            Dedicated for Service Worker, requires API Key authentication",
            OperationId = "CloseAllPolls"
            )]
        [Authorize(AuthenticationSchemes = ApiKeyAuthenticationOptions.DefaultScheme)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Successfully activated polls")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "This endpoint is designed for Service Worker")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult> CloseAllPolls()
        {
            await _pollService.CloseAllPolls();
            return NoContent();
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates new poll",
            Description = @"Creates poll using provided data.",
            OperationId = "CreatePoll"
            )]
        [SwaggerResponse(StatusCodes.Status201Created, "The poll was created", Type = typeof(PollBaseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The poll data is invalid")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "You do not have permissions to access this resource")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The poll you are looking for does not exists")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult> Post([FromBody] PollCreateDTO pollCreateDTO)
        {

            int id = await _pollService.Create(pollCreateDTO);
            PollBaseDTO poll = await _pollService.Get(id);
            return CreatedAtAction(nameof(PollController.Post), new { id }, poll);
        }

    }

}
