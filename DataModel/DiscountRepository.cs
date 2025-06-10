using DataModel.DiscountRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class DiscountRepository
    {
        private List<IDiscountRule> _discounts = new List<IDiscountRule>();

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
