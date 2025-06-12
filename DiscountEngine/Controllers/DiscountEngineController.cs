using DataModel.DTO;
using DataModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DataModel.DiscountRules;

[ApiController]
[Route("api/cart")]
public class CartController : ControllerBase
{
    private readonly IMediator _mediator;

   

    public CartController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> ApplyDiscounts([FromBody] Cart cart)
    {
        if (cart == null || cart.Items.Count == 0)
            return BadRequest("Invalid cart data.");

        var response = await _mediator.Send(new ApplyDiscountsRequest(cart));
        return Ok(response);
    }

    [HttpGet("active-discounts")]
    public async Task<IActionResult> GetActiveDiscounts()
    {
        var discounts = await _mediator.Send(new GetActiveDiscountsQuery());
        return Ok(discounts);
    }

}