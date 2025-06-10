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
        decimal discountAmount = cart.Items
            .Where(item => item.Product.Category == _category)
            .Sum(item => item.Product.Price * item.Quantity * (_discountPercentage / 100));

        return cart.Items.Sum(item => item.Product.Price * item.Quantity) - discountAmount; // Proper discount subtraction
    }

}