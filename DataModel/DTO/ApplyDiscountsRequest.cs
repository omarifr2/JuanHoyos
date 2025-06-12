using DataModel.DTO;
using DataModel;
using MediatR;

public class ApplyDiscountsRequest : IRequest<DiscountResponse>
{
    public Cart Cart { get; set; }
    public string? DiscountCode { get; set; }

    // Parameterless constructor needed for MediatR serialization
    public ApplyDiscountsRequest() { }

    public ApplyDiscountsRequest(Cart cart, string discountCode = null)
    {
        Cart = cart ?? throw new ArgumentNullException(nameof(cart));
        DiscountCode = discountCode;
    }
}