using DataModel;
using DataModel.DiscountRules;

public class CategoryDiscountRule : IDiscountRule
{
    private string _category;
    private decimal _discountPercentage;

    public CategoryDiscountRule(string category, decimal discountPercentage)
    {
        _category = category;
        _discountPercentage = discountPercentage;
    }

    public decimal ApplyDiscount(Cart cart)
    {
        decimal total = 0;
        foreach (var item in cart.Items)
        {
            if (item.Product.Category == _category)
            {
                total += item.Product.Price * item.Quantity * (1 - _discountPercentage / 100);
            }
            else
            {
                total += item.Product.Price * item.Quantity;
            }
        }
        return total;
    }
}