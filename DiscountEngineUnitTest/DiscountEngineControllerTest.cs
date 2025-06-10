using DataModel;
using DataModel.DiscountRules;
using DataModel.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DiscountEngine.Test
{
    public class DiscountEngineControllerTest
    {
        private readonly Mock<IDiscountEngine> _mockDiscountEngine;
        private readonly CartController _controller;

        public DiscountEngineControllerTest()
        {
            _mockDiscountEngine = new Mock<IDiscountEngine>();
            _controller = new CartController(_mockDiscountEngine.Object);
        }

        [Fact]
        public void ApplyDiscounts_ShouldReturnBadRequest_WhenCartIsNull()
        {
            var result = _controller.ApplyDiscounts(null) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Invalid cart data.", result.Value);
        }

        [Fact]
        public void ApplyDiscounts_ShouldReturnBadRequest_WhenCartIsEmpty()
        {
            var cart = new Cart();
            var result = _controller.ApplyDiscounts(cart) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Invalid cart data.", result.Value);
        }

        [Fact]
        public void ApplyDiscounts_ShouldApplyDiscounts_Correctly()
        {
            var cart = new Cart();
            var product = new Product("P001", "Laptop", "Electronics", 1000m);
            cart.AddItem(product, 1);

            var mockDiscount = new Mock<IDiscountRule>();
            mockDiscount.Setup(d => d.ApplyDiscount(cart)).Returns(900m);
            _mockDiscountEngine.Setup(e => e.GetDiscounts()).Returns(new List<IDiscountRule> { mockDiscount.Object });

            var result = _controller.ApplyDiscounts(cart) as OkObjectResult;
            var response = result?.Value as DiscountResponse;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(response);
            Assert.Equal(1000m, response.OriginalTotal);
            Assert.Single(response.Discounts);
            Assert.Equal(900m, response.FinalTotal);
        }

        [Fact]
        public void GetActiveDiscounts_ShouldReturnListOfDiscounts()
        {
            // Create a concrete discount rule instance
            var discountRule = new PercentageDiscountRule(10, "Books");

            // Ensure the mock returns a non-empty list
            _mockDiscountEngine.Setup(e => e.GetDiscounts()).Returns(new List<IDiscountRule> { discountRule });

            var result = _controller.GetActiveDiscounts() as OkObjectResult;

            // Validate response is not null
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}