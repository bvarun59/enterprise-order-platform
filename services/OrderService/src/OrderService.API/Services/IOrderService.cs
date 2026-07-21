using OrderService.API.DTOs;

namespace OrderService.API.Services;

public interface IOrderService
{
    Task<OrderResponse> CreateOrderAsync(CreateOrderRequest request);
}