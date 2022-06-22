using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OrderProcessorApi.Controllers;

public class CartController : ControllerBase
{
    [Authorize]
    [HttpPost("/my/orders")]
    public async Task<ActionResult<Order>> PlaceOrder([FromBody] ShoppingCart request)
    {
        var user = User?.GetSub();
        var name = User?.GetPreferredUserName();
        var otherThing = User?.GetClaimFrom("nickname");
        if (user is null || name is null)
        {
            return StatusCode(403);
        }
        var order = new Order(user!, name!, DateTime.Now, request, OrderStatus.Pending);
        // TODO: save it, publish it to a queue...

        return StatusCode(201, order);

    }
}


public record ShoppingCart(string[] items, string? specialInstructions);

public record Order(string user, string name, DateTime created, ShoppingCart cart, OrderStatus status);

public enum OrderStatus { Pending, Cancelled, Fulfilled }
