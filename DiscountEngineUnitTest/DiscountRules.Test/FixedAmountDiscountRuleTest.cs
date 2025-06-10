using System;
using Xunit;
using DataModel;
using DataModel.DiscountRules;

namespace DiscountEngine.Test
{
    public class FixedAmountDiscountRuleTest
    {
        [Fact]
        public void FixedAmountDiscountRule_ShouldApplyDiscount_WhenThresholdIsMet()
        {
            var cart = new Cart();
            var product1 = new Product("P001", "Laptop", "Electronics", 1000m);
            var product2 = new Product("P002", "Mouse", "Accessories", 50m);

            cart.AddItem(product1, 1);
            cart.AddItem(product2, 2);

            var discountRule = new FixedAmountDiscountRule(1000, 20);
            decimal discountedTotal = discountRule.ApplyDiscount(cart);

            // Original total: (1000 * 1) + (50 * 2) = 1100
            // Discount applied: 1100 - 20 = 1080
            Assert.Equal(1080m, discountedTotal);
        }

        [Fact]
        public void FixedAmountDiscountRule_ShouldNotApplyDiscount_WhenThresholdIsNotMet()
        {
            var cart = new Cart();
            var product = new Product("P003", "Headphones", "Accessories", 200m);
            cart.AddItem(product, 1);

            var discountRule = new FixedAmountDiscountRule(500, 50);
            decimal discountedTotal = discountRule.ApplyDiscount(cart);

            // No discount applied, total should remain the same
            Assert.Equal(200m, discountedTotal);
        }

        [Fact]
        public void FixedAmountDiscountRule_ShouldApplyDiscount_ExactlyAtThreshold()
        {
            var cart = new Cart();
            var product1 = new Product("P004", "Smartphone", "Electronics", 500m);
            cart.AddItem(product1, 1);

            var discountRule = new FixedAmountDiscountRule(500, 50);
            decimal discountedTotal = discountRule.ApplyDiscount(cart);

            // Original total: 500
            // Discount applied: 500 - 50 = 450
            Assert.Equal(450m, discountedTotal);
        }
    }
}