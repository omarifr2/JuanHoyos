using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.DiscountRules
{
    public class PercentageDiscountRule : IDiscountRule
    {
        private string _category;
        private decimal _discountPercentage;

        public PercentageDiscountRule(decimal discountPercentage, string category = null)
        {
            _discountPercentage = discountPercentage;
            _category = category;
        }

        public decimal ApplyDiscount(Cart cart)
        {
            decimal total = 0;
            foreach (var item in cart.Items)
            {
                if (_category == null || item.Product.Category == _category)
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
}
