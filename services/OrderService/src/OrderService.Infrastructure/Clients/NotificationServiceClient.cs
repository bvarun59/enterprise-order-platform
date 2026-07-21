using System.Net.Http.Json;

public class NotificationServiceClient : INotificationServiceClient
{
    private readonly HttpClient _httpClient;

    public NotificationServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task SendNotificationAsync(Guid userId,
                                            Guid orderId,
                                            string message)
    {
        var request = new
        {
            userId,
            orderId,
            message
        };

        await _httpClient.PostAsJsonAsync(
            "/api/Notifications",
            request);
    }
}