using System;
using Xunit;
using DataModel;
using DataModel.DiscountRules;

namespace DiscountEngine.Test
{
    public class PercentageDiscountRuleTest
    {
        [Fact]
        public void PercentageDiscountRule_ShouldApplyDiscountToAllProducts()
        {
            var cart = new Cart();
            var product1 = new Product("P001", "Laptop", "Electronics", 1000m);
            var product2 = new Product("P002", "Mouse", "Accessories", 50m);

            cart.AddItem(product1, 1);
            cart.AddItem(product2, 2);

            var discountRule = new PercentageDiscountRule(10);
            decimal discountedTotal = discountRule.ApplyDiscount(cart);

            // Original total: (1000 * 1) + (50 * 2) = 1100
            // Discount applied: 1100 * 0.9 = 990
            Assert.Equal(990m, discountedTotal);
        }

        [Fact]
        public void PercentageDiscountRule_ShouldApplyDiscountToSpecificCategory()
        {
            var cart = new Cart();
            var product1 = new Product("P003", "Headphones", "Electronics", 200m);
            var product2 = new Product("P004", "Book A", "Books", 30m);

            cart.AddItem(product1, 1);
            cart.AddItem(product2, 2);

            var discountRule = new PercentageDiscountRule(15, "Books");
            decimal discountedTotal = discountRule.ApplyDiscount(cart);

            // Original total: (200 * 1) + (30 * 2) = 260
            // Discount applied only to Books category: (200 * 1) + (30 * 2 * 0.85) = 251
            Assert.Equal(251m, discountedTotal);
        }

        [Fact]
        public void PercentageDiscountRule_ShouldNotApplyDiscount_WhenCategoryDoesNotMatch()
        {
            var cart = new Cart();
            var product1 = new Product("P005", "Smartphone", "Electronics", 800m);
            var product2 = new Product("P006", "Table", "Furniture", 150m);

            cart.AddItem(product1, 1);
            cart.AddItem(product2, 1);

            var discountRule = new PercentageDiscountRule(20, "Books");
            decimal discountedTotal = discountRule.ApplyDiscount(cart);

            // No discount applied, total should remain the same
            Assert.Equal(950m, discountedTotal);
        }
    }
}