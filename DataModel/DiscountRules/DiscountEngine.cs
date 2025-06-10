using DataModel;
using DataModel.DiscountRules;

public class DiscountEngineCore
{
    private DiscountRepository _discountRepo;

    public DiscountEngineCore(DiscountRepository discountRepo)
    {
        _discountRepo = discountRepo;
    }

    public decimal CalculateTotalWithDiscounts(Cart cart)
    {
        decimal total = cart.Items.Sum(item => item.Product.Price * item.Quantity);
        foreach (var rule in _discountRepo.GetDiscounts())
        {
            total = rule.ApplyDiscount(cart);
        }
        return total;
    }

    public List<IDiscountRule> GetDiscounts()
    {
        return _discountRepo.GetDiscounts(); // Correctly retrieves stored discounts
    }

}