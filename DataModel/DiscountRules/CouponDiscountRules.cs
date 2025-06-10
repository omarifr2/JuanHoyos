using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.DiscountRules
{
    public class CouponDiscountRule : IDiscountRule
    {
        private string _couponCode;
        private decimal _discountPercentage;

        public CouponDiscountRule(string couponCode, decimal discountPercentage)
        {
            _couponCode = couponCode;
            _discountPercentage = discountPercentage;
        }

        public decimal ApplyDiscount(Cart cart)
        {
            decimal subtotal = cart.Items.Sum(item => item.Product.Price * item.Quantity);
            return subtotal * (1 - _discountPercentage / 100);
        }
    }
}
