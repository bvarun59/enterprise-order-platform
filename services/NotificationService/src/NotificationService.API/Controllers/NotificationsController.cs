using Microsoft.AspNetCore.Mvc;
using NotificationService.API.DTOs;

namespace NotificationService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly ILogger<NotificationsController> _logger;

    public NotificationsController(ILogger<NotificationsController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Send(NotificationRequest request)
    {
        _logger.LogInformation("Notification received");

        _logger.LogInformation("UserId : {UserId}", request.UserId);

        _logger.LogInformation("OrderId : {OrderId}", request.OrderId);

        _logger.LogInformation("Message : {Message}", request.Message);

        _logger.LogInformation("Notification completed");

        return Ok(new
        {
            Status = "Notification Sent"
        });
    }
}