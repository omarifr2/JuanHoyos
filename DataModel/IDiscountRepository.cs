using System.Collections.Generic;

namespace DataModel.DiscountRules
{
    public interface IDiscountRepository
    {
        void AddDiscount(IDiscountRule discount);
        List<IDiscountRule> GetDiscounts();
    }
}