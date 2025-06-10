using System.Collections.Generic;
using DataModel.DiscountRules;

namespace DataModel
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly List<IDiscountRule> _discounts = new List<IDiscountRule>();

        public void AddDiscount(IDiscountRule discount)
        {
            _discounts.Add(discount);
        }

        public List<IDiscountRule> GetDiscounts()
        {
            return _discounts;
        }
    }
}