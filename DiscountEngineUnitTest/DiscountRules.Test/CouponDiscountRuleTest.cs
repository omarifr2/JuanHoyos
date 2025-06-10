using System;
using Xunit;
using DataModel;
using DataModel.DiscountRules;

namespace DiscountEngine.Test
{
    public class CouponDiscountRuleTest
    {
        [Fact]
        public void CouponDiscountRule_ShouldApplyDiscountCorrectly()
        {
            var cart = new Cart();
            var product1 = new Product("P001", "Laptop", "Electronics", 1000m);
            var product2 = new Product("P002", "Mouse", "Accessories", 50m);

            cart.AddItem(product1, 1);
            cart.AddItem(product2, 2);

            var discountRule = new CouponDiscountRule("WELCOME10", 10);
            decimal discountedTotal = discountRule.ApplyDiscount(cart);

            // Original total: (1000 * 1) + (50 * 2) = 1100
            // Discount applied: 1100 * 0.9 = 990
            Assert.Equal(990m, discountedTotal);
        }

        [Fact]
        public void CouponDiscountRule_ShouldNotAlterTotal_WhenDiscountIsZero()
        {
            var cart = new Cart();
            var product = new Product("P003", "Headphones", "Accessories", 200m);
            cart.AddItem(product, 1);

            var discountRule = new CouponDiscountRule("NO_DISCOUNT", 0);
            decimal discountedTotal = discountRule.ApplyDiscount(cart);

            // No discount applied, total should remain the same
            Assert.Equal(200m, discountedTotal);
        }

        [Fact]
        public void CouponDiscountRule_ShouldApplyLargeDiscountProperly()
        {
            var cart = new Cart();
            var product = new Product("P004", "Smartphone", "Electronics", 800m);
            cart.AddItem(product, 1);

            var discountRule = new CouponDiscountRule("BIGSALE50", 50);
            decimal discountedTotal = discountRule.ApplyDiscount(cart);

            // Original total: 800
            // Discount applied: 800 * 0.5 = 400
            Assert.Equal(400m, discountedTotal);
        }
    }
}