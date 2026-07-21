using OrderService.API.Clients;
using OrderService.API.DTOs;
using OrderService.Infrastructure.Persistence;
using OrderService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace OrderService.API.Services;

public class OrderManagementService : IOrderService
{
    private readonly IUserServiceClient _userClient;
    private readonly IProductServiceClient _productClient;
    private readonly ApplicationDbContext _dbContext;
    private readonly INotificationServiceClient _notificationClient;
    private readonly ILogger<OrderManagementService> _logger;

    public OrderManagementService(
        IUserServiceClient userClient,
        IProductServiceClient productClient,
        ApplicationDbContext dbContext,
        INotificationServiceClient notificationClient,
        ILogger<OrderManagementService> logger)
        
    {
        _userClient = userClient;
        _productClient = productClient;
        _dbContext = dbContext;
        _notificationClient = notificationClient;
        _logger = logger;
    }

    public async Task<OrderResponse> CreateOrderAsync(CreateOrderRequest request)
    {
    // 1. Validate User
    var user = await _userClient.GetUserAsync(request.UserId);

    _logger.LogInformation("Starting order creation");

    if (user == null)
        throw new Exception("User not found.");
    
    _logger.LogInformation("User validated successfully");

    // 2. Validate Product
    var product = await _productClient.GetProductAsync(request.ProductId);

    if (product == null)
        throw new Exception("Product not found.");

    _logger.LogInformation("Product validated successfully");

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
    _logger.LogInformation("Saving order to PostgreSQL");
    _dbContext.Orders.Add(order);
    await _dbContext.SaveChangesAsync();

    await _notificationClient.SendNotificationAsync(
        order.UserId,
        order.Id,
        "Your order has been placed successfully.");
    
    _logger.LogInformation("Order saved successfully. OrderId: {OrderId}", order.Id);

    // 6. Return Response
    _logger.LogInformation("Calling NotificationService");
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
    _logger.LogInformation("Notification sent successfully");
    }

public async Task<List<OrderResponse>> GetAllOrdersAsync()
{
    return await _dbContext.Orders
        .Select(o => new OrderResponse
        {
            Id = o.Id,
            UserId = o.UserId,
            ProductId = o.ProductId,
            Quantity = o.Quantity,
            UnitPrice = o.UnitPrice,
            TotalAmount = o.TotalAmount,
            Status = o.Status,
            CreatedAt = o.CreatedAt
        })
        .ToListAsync();
}    

public async Task<OrderResponse?> GetOrderByIdAsync(Guid id)
{
    return await _dbContext.Orders
        .Where(o => o.Id == id)
        .Select(o => new OrderResponse
        {
            Id = o.Id,
            UserId = o.UserId,
            ProductId = o.ProductId,
            Quantity = o.Quantity,
            UnitPrice = o.UnitPrice,
            TotalAmount = o.TotalAmount,
            Status = o.Status,
            CreatedAt = o.CreatedAt
        })
        .FirstOrDefaultAsync();
}

}