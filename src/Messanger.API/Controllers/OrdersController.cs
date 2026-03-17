using Messanger.API.Dtos.Orders;
using Messanger.API.Mapping;
using Messanger.Application.Dtos.Orders;
using Messanger.Application.Services;
using Messanger.Application.Services.Message;
using Microsoft.AspNetCore.Mvc;
using Messanger.Application.Abstractions.Services.Orders;
using Messanger.Application.Abstractions.Services.MessageOrder;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Messanger.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService orderService;
    private readonly IMessageOrderService messageOrderService;
    public OrdersController (IMessageOrderService messageOrderService, IOrderService orderService)
    {
        this.messageOrderService = messageOrderService;
        this.orderService = orderService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderRequest request)
    {
        CreateOrderDto dto = OrderMapper.toCreateDto(request);
        await this.orderService.CreateAsync(dto);
        await this.messageOrderService.ExecuteAsync(dto);
        return Accepted();
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> List()
    {
        List<OrderDto> listOrderDto = await this.orderService.ListAsync();
        return Ok(listOrderDto);
    }
}
