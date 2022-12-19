using Application.DTOs.Notification;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PKIK_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Returns user notifications",
            Description = @"Returns all user notifications based on JWT token.",
            OperationId = "GetAllNotifications"
            )]
        [SwaggerResponse(StatusCodes.Status200OK, "The notifications for current user were returned", Type = typeof(IEnumerable<NotificationBaseDTO>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "You must be logged in to access this resource")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "You do not have permissions to access this resource")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult<IEnumerable<NotificationBaseDTO>>> GetAll()
        {
            return Ok(await _notificationService.GetAll());
        }

        [HttpGet("{notificationId}")]
        [SwaggerOperation(
            Summary = "Returns specific notification",
            Description = @"Returns specific notification by provided notification identificator.
                            Notification must be targeted to loged in user.",
            OperationId = "GetNotification"
            )]
        [SwaggerResponse(StatusCodes.Status200OK, "The notification with provided identificator was returned", Type = typeof(NotificationBaseDTO))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "You must be logged in to access this resource")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "You do not have permissions to access this resource")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The notification you are looking for does not exists")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult<NotificationBaseDTO>> Get(int notificationId)
        {
            return Ok(await _notificationService.Get(notificationId));
        }

        [HttpPut("SetAsSeen/{notificationId}")]
        [SwaggerOperation(
            Summary = "Sets notification status as seen",
            Description = @"Sets notification status as seen.
                            You must be a target of this notification to change status.",
            OperationId = "SetNotificationAsSeen"
            )]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Successfully set the seen status for notification with provided indentificator")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "You must be logged in to access this resource")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "You do not have permissions to access this resource")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The notification you are looking for does not exists")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]

        public async Task<ActionResult> SetAsSeen(int notificationId)
        {
            await _notificationService.SetAsSeen(notificationId);
            return NoContent();
        }
        [HttpDelete("{notificationId}")]
        [SwaggerOperation(
            Summary = "Removes notification",
            Description = @"Remove notification using provided notification identificator.
                            You must be a target of this notification to remove it.",
            OperationId = "DeleteNotification"
            )]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Successfully removed notification with provided indentificator")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "You must be logged in to access this resource")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "You do not have permissions to access this resource")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The notification you are looking for does not exists")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult> Delete(int notificationId)
        {
            await _notificationService.Delete(notificationId);
            return NoContent();
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates new notification",
            Description = @"Creates notification using provided data.",
            OperationId = "CreateNotification"
            )]
        [AllowAnonymous]
        [SwaggerResponse(StatusCodes.Status201Created, "The notification was created", Type = typeof(NotificationBaseDTO))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult> Post([FromBody] NotificationCreateDTO notificationCreateDTO)
        {
            int id = await _notificationService.Create(notificationCreateDTO);
            NotificationBaseDTO notification = await _notificationService.GetPrivate(id);
            return CreatedAtAction(nameof(NotificationController.Post), new { id }, notification);
        }

    }
}
