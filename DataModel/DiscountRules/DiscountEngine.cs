using System.Collections.Generic;
using System.Linq;
using DataModel;
using DataModel.DiscountRules;

public class DiscountEngineCore : IDiscountEngine
{
    private readonly IDiscountRepository _discountRepo;

    public DiscountEngineCore(IDiscountRepository discountRepo)
    {
        _discountRepo = discountRepo;
    }

    public decimal CalculateTotalWithDiscounts(Cart cart)
    {
        decimal subtotal = cart.Items.Sum(item => item.Product.Price * item.Quantity);
        decimal totalDiscount = _discountRepo.GetDiscounts()
                            .Sum(discount => subtotal - discount.ApplyDiscount(cart));

        return subtotal - totalDiscount;
    }

    public List<IDiscountRule> GetDiscounts()
    {
        return _discountRepo.GetDiscounts();
    }
}