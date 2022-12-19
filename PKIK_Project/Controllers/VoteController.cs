using Application.DTOs.Vote;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PKIK_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class VoteController : Controller
    {
        private readonly IVoteService _voteService;

        public VoteController(IVoteService voteService)
        {
            _voteService = voteService;
        }

        [HttpGet("{pollId}")]
        [SwaggerOperation(
            Summary = "Returns voting results for specific poll",
            Description = @"Returns voting results for specific poll by provided poll identificator.
                            If poll ResultsArePublic is false then you have to be poll creator or moderator",
            OperationId = "GetVotes"
            )]
        [SwaggerResponse(StatusCodes.Status200OK, "The votes for provided poll identificator were returned", Type = typeof(VoteInfoDTO))]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "You do not have permissions to access this resource")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The poll you are looking for does not exists")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult<VoteInfoDTO>> Get(int pollId)
        {
            return Ok(await _voteService.Get(pollId));
        }

        [HttpPost("Aggregate")]
        [SwaggerOperation(
            Summary = "Submits new votes",
            Description = @"Adds new votes to poll using provided data.",
            OperationId = "SubmitVotes"
            )]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The votes were successfully added to poll")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The voting data is invalid")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "You do not have permissions to access this resource")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The poll,question or answer you are looking for does not exists")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult> VoteAggregate(VoteAggregateDTO voteAggregateDTO)
        {
            await _voteService.VoteAggregate(voteAggregateDTO);
            return NoContent();
        }
    }
}
