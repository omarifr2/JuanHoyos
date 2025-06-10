using System.Collections.Generic;
using Xunit;
using Moq;
using DataModel;
using DataModel.DiscountRules;

namespace DiscountEngine.Test
{
    public class DiscountEngineCoreTest
    {
        [Fact]
        public void CalculateTotalWithDiscounts_ShouldApplyAllDiscounts()
        {
            var mockRepo = new Mock<IDiscountRepository>();
            var mockDiscount1 = new Mock<IDiscountRule>();
            var mockDiscount2 = new Mock<IDiscountRule>();

            var cart = new Cart();
            var product = new Product("P001", "Laptop", "Electronics", 1000m);
            cart.AddItem(product, 1);

            mockDiscount1.Setup(d => d.ApplyDiscount(cart)).Returns(900m); // 10% off
            mockDiscount2.Setup(d => d.ApplyDiscount(cart)).Returns(800m); // Additional $100 off

            mockRepo.Setup(repo => repo.GetDiscounts()).Returns(new List<IDiscountRule> { mockDiscount1.Object, mockDiscount2.Object });

            var discountEngine = new DiscountEngineCore(mockRepo.Object);
            decimal finalTotal = discountEngine.CalculateTotalWithDiscounts(cart);

            // Expected calculation: 1000 - ((1000 - 900) + (1000 - 800)) = 700
            Assert.Equal(700m, finalTotal);
        }

        [Fact]
        public void CalculateTotalWithDiscounts_ShouldReturnOriginalTotal_IfNoDiscounts()
        {
            var mockRepo = new Mock<IDiscountRepository>();
            var cart = new Cart();
            var product = new Product("P002", "Mouse", "Accessories", 50m);
            cart.AddItem(product, 1);

            mockRepo.Setup(repo => repo.GetDiscounts()).Returns(new List<IDiscountRule>());

            var discountEngine = new DiscountEngineCore(mockRepo.Object);
            decimal finalTotal = discountEngine.CalculateTotalWithDiscounts(cart);

            Assert.Equal(50m, finalTotal); // No discounts, total remains the same
        }

        [Fact]
        public void GetDiscounts_ShouldReturnStoredDiscounts()
        {
            var mockRepo = new Mock<IDiscountRepository>();
            var mockDiscount = new Mock<IDiscountRule>();

            mockRepo.Setup(repo => repo.GetDiscounts()).Returns(new List<IDiscountRule> { mockDiscount.Object });

            var discountEngine = new DiscountEngineCore(mockRepo.Object);
            var discounts = discountEngine.GetDiscounts();

            Assert.Single(discounts);
        }
    }
}