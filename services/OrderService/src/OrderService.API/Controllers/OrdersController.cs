using Microsoft.AspNetCore.Mvc;
using OrderService.API.Clients;
using OrderService.API.DTOs;
using OrderService.API.Services;

namespace OrderService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IUserServiceClient _userServiceClient;
    private readonly IProductServiceClient _productServiceClient;
    private readonly IOrderService _orderService;

    public OrdersController(
        IUserServiceClient userServiceClient,
        IProductServiceClient productServiceClient,
        IOrderService orderService)
    {
        _userServiceClient = userServiceClient;
        _productServiceClient = productServiceClient;
        _orderService = orderService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Order Service is working");
    }

    [HttpGet("test-user/{id}")]
    public async Task<IActionResult> TestUser(Guid id)
    {
        var user = await _userServiceClient.GetUserAsync(id);

        if (user == null)
            return NotFound();

        return Ok(user);
    }
    [HttpGet("test-product/{id}")]
    public async Task<IActionResult> TestProduct(Guid id)
    {
    var product = await _productServiceClient.GetProductAsync(id);

    if (product == null)
        return NotFound();

    return Ok(product);
    }
    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
    {
    var order = await _orderService.CreateOrderAsync(request);

    return CreatedAtAction(
        nameof(Get),
        new { id = order.Id },
        order);
    }
}