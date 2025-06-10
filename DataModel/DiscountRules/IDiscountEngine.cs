using System.Collections.Generic;

namespace DataModel.DiscountRules
{
    public interface IDiscountEngine
    {
        decimal CalculateTotalWithDiscounts(Cart cart);
        List<IDiscountRule> GetDiscounts();
    }
}