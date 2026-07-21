using OrderService.API.Clients.Models;

namespace OrderService.API.Clients;

public interface IUserServiceClient
{
    Task<UserDto?> GetUserAsync(Guid id);
}