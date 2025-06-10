using System;
using Xunit;
using DataModel;
using DataModel.DiscountRules;

namespace DiscountEngine.Test
{
    public class CategoryDiscountRuleTest
    {
        [Fact]
        public void CategoryDiscountRule_ShouldApplyDiscountCorrectly()
        {
            var cart = new Cart();
            var product1 = new Product("B001", "Book A", "Books", 30m);
            var product2 = new Product("B002", "Book B", "Books", 40m);
            var product3 = new Product("E001", "Laptop", "Electronics", 1000m);

            cart.AddItem(product1, 1);
            cart.AddItem(product2, 2);
            cart.AddItem(product3, 1); // Should not get a category discount

            var discountRule = new CategoryDiscountRule("Books", 15);
            decimal discountedTotal = discountRule.ApplyDiscount(cart);

            // Original total:
            // (30 * 1) + (40 * 2) + (1000 * 1) = 1110
            // Discount applied:
            // (30 * 0.85) + (40 * 2 * 0.85) + (1000 * 1) = 1093.5

            Assert.Equal(1093.5m, discountedTotal);
        }

        [Fact]
        public void CategoryDiscountRule_ShouldNotApplyDiscount_WhenCategoryDoesNotMatch()
        {
            var cart = new Cart();
            var product = new Product("E001", "Laptop", "Electronics", 1000m);
            cart.AddItem(product, 1);

            var discountRule = new CategoryDiscountRule("Books", 15);
            decimal discountedTotal = discountRule.ApplyDiscount(cart);

            // No discount applied, total should remain the same
            Assert.Equal(1000m, discountedTotal);
        }

        [Fact]
        public void CategoryDiscountRule_ShouldApplyDiscountToMultipleItems()
        {
            var cart = new Cart();
            var product1 = new Product("B001", "Book A", "Books", 30m);
            var product2 = new Product("B002", "Book B", "Books", 40m);

            cart.AddItem(product1, 2);
            cart.AddItem(product2, 3);

            var discountRule = new CategoryDiscountRule("Books", 20);
            decimal discountedTotal = discountRule.ApplyDiscount(cart);

            // Original total: (30 * 2) + (40 * 3) = 180
            // Discount applied: (30 * 2 * 0.8) + (40 * 3 * 0.8) = 144
            Assert.Equal(144m, discountedTotal);
        }
    }
}