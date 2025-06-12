using DataModel;
using DataModel.DiscountRules;
using DataModel.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public class ApplyDiscountsHandler : IRequestHandler<ApplyDiscountsRequest, DiscountResponse>
{
    private readonly ICartValidator _cartValidator;
    private readonly IDiscountEngine _discountEngineCore;


    public ApplyDiscountsHandler(ICartValidator cartValidator, IDiscountEngine discountEngineCore)
    {
        _cartValidator = cartValidator;
        _discountEngineCore = discountEngineCore;
    }

    public async Task<DiscountResponse> Handle(ApplyDiscountsRequest request, CancellationToken cancellationToken)
    {
        if (request.Cart == null || request.Cart.Items.Count == 0)
            return new DiscountResponse { Error = "Invalid cart data" }; // Return error inside response object

        decimal originalTotal = request.Cart.GetTotal();
        decimal discountedTotal = originalTotal;
        var discounts = new List<DiscountDetail>();

        //assuming those apply
        foreach (var rule in _discountEngineCore.GetDiscounts())
        {
            decimal previousTotal = discountedTotal;
            decimal newTotal = rule.ApplyDiscount(request.Cart);
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

        return new DiscountResponse
        {
            OriginalTotal = originalTotal,
            Discounts = discounts,
            FinalTotal = discountedTotal
        };
    }
}