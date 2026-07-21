namespace NotificationService.API.DTOs;

public class NotificationRequest
{
    public Guid UserId { get; set; }

    public Guid OrderId { get; set; }

    public string Message { get; set; } = string.Empty;
}