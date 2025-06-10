using DataModel.DTO;
using DataModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DataModel.DiscountRules;

[ApiController]
[Route("api/cart")]
public class CartController : ControllerBase
{
    private readonly IDiscountEngine _discountEngineCore;

    public CartController(IDiscountEngine discountEngineCore)
    {
        _discountEngineCore = discountEngineCore;
    }

    [HttpPost]
    public IActionResult ApplyDiscounts([FromBody] Cart cart)
    {
        if (cart == null || cart.Items.Count == 0)
            return BadRequest("Invalid cart data.");

        decimal originalTotal = cart.GetTotal();
        decimal discountedTotal = originalTotal;
        var discounts = new List<DiscountDetail>();

        foreach (var rule in _discountEngineCore.GetDiscounts())
        {
            decimal previousTotal = discountedTotal;
            decimal newTotal = rule.ApplyDiscount(cart);
            decimal discountAmount = previousTotal - newTotal;

            
            if (discountAmount > 0)
            {
                discounts.Add(new DiscountDetail
                {
                    Name = rule.GetType().Name.Replace("Rule", ""),
                    Amount = discountAmount
                });
            }

            discountedTotal = newTotal;
        }

        var response = new DiscountResponse
        {
            OriginalTotal = originalTotal,
            Discounts = discounts,
            FinalTotal = discountedTotal
        };

        return Ok(response);
    }

    [HttpGet("active-discounts")]
    public IActionResult GetActiveDiscounts()
    {
        var discounts = _discountEngineCore.GetDiscounts()
            .Select(d => new { Name = d.GetType().Name })
            .ToList();

        return Ok(discounts);
    }

}