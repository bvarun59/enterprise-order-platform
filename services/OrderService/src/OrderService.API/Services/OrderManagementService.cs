using OrderService.API.Clients;
using OrderService.API.DTOs;
using OrderService.Infrastructure.Persistence;
using OrderService.Domain.Entities;

namespace OrderService.API.Services;

public class OrderManagementService : IOrderService
{
    private readonly IUserServiceClient _userClient;
    private readonly IProductServiceClient _productClient;
    private readonly ApplicationDbContext _dbContext;

    public OrderManagementService(
        IUserServiceClient userClient,
        IProductServiceClient productClient,
        ApplicationDbContext dbContext)
    {
        _userClient = userClient;
        _productClient = productClient;
        _dbContext = dbContext;
    }

    public async Task<OrderResponse> CreateOrderAsync(CreateOrderRequest request)
    {
    // 1. Validate User
    var user = await _userClient.GetUserAsync(request.UserId);

    if (user == null)
        throw new Exception("User not found.");

    // 2. Validate Product
    var product = await _productClient.GetProductAsync(request.ProductId);

    if (product == null)
        throw new Exception("Product not found.");

    // 3. Calculate pricing
    var unitPrice = product.Price;
    var totalAmount = unitPrice * request.Quantity;

    // 4. Create Order
    var order = new Order
    {
        UserId = request.UserId,
        ProductId = request.ProductId,
        Quantity = request.Quantity,
        UnitPrice = unitPrice,
        TotalAmount = totalAmount,
        Status = "Pending"
    };

    // 5. Save to Database
    _dbContext.Orders.Add(order);
    await _dbContext.SaveChangesAsync();

    // 6. Return Response
    return new OrderResponse
    {
        Id = order.Id,
        UserId = order.UserId,
        ProductId = order.ProductId,
        Quantity = order.Quantity,
        UnitPrice = order.UnitPrice,
        TotalAmount = order.TotalAmount,
        Status = order.Status,
        CreatedAt = order.CreatedAt
    };
    }
}