using OrderService.API.Clients.Models;

namespace OrderService.API.Clients;

public interface IProductServiceClient
{
    Task<ProductDto?> GetProductAsync(Guid id);
}