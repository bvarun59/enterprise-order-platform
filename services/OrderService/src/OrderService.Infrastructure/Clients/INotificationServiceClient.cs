public interface INotificationServiceClient
{
    Task SendNotificationAsync(Guid userId, Guid orderId, string message);
}