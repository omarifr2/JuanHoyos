using System;
using Xunit;
using DataModel;
using DataModel.DiscountRules;

namespace DiscountEngine.Test
{
    public class BuyXGetYFreeRuleTest
    {
        [Fact]
        public void BuyXGetYFreeRule_ShouldApplyDiscountCorrectly()
        {
            var cart = new Cart();
            var product = new Product("T123", "T-Shirt", "Clothing", 20m);

            cart.AddItem(product, 3); // Buying 3, eligible for 1 free item with Buy 2 Get 1 Free

            var discountRule = new BuyXGetYFreeRule("T123", 2, 1);
            decimal discountedTotal = discountRule.ApplyDiscount(cart);

            // Original total: 3 * 20 = 60
            // Discount applied: 1 free item -> 2 * 20 = 40
            Assert.Equal(40m, discountedTotal);
        }

        [Fact]
        public void BuyXGetYFreeRule_ShouldNotApplyDiscount_IfConditionNotMet()
        {
            var cart = new Cart();
            var product = new Product("T123", "T-Shirt", "Clothing", 20m);

            cart.AddItem(product, 1); // Buying only 1 (doesn't meet Buy 2 Get 1 Free)

            var discountRule = new BuyXGetYFreeRule("T123", 2, 1);
            decimal discountedTotal = discountRule.ApplyDiscount(cart);

            // No discount applied, total should remain the same
            Assert.Equal(20m, discountedTotal);
        }

        [Fact]
        public void BuyXGetYFreeRule_ShouldApplyMultipleDiscounts_WhenQuantityIncreases()
        {
            var cart = new Cart();
            var product = new Product("T123", "T-Shirt", "Clothing", 20m);

            cart.AddItem(product, 6); // Buying 6, eligible for 2 free items (Buy 2 Get 1 Free applies twice)

            var discountRule = new BuyXGetYFreeRule("T123", 2, 1);
            decimal discountedTotal = discountRule.ApplyDiscount(cart);

            // Original total: 6 * 20 = 120
            // Discount applied: 2 free items -> 4 * 20 = 80
            Assert.Equal(80m, discountedTotal);
        }
    }
}