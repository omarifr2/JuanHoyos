using System;
using Xunit;
using DataModel;

namespace DiscountEngine.Test
{
    public class CartItemTest
    {
        [Fact]
        public void CartItem_ShouldStoreProductCorrectly()
        {
            var product = new Product("P123", "Laptop", "Electronics", 1000m);
            var cartItem = new CartItem(product, 2);

            Assert.Equal(product, cartItem.Product);
        }

        [Fact]
        public void CartItem_ShouldStoreQuantityCorrectly()
        {
            var product = new Product("P456", "Mouse", "Accessories", 50m);
            var cartItem = new CartItem(product, 3);

            Assert.Equal(3, cartItem.Quantity);
        }

        [Fact]
        public void CartItem_ShouldAllowQuantityUpdate()
        {
            var product = new Product("P789", "Keyboard", "Accessories", 80m);
            var cartItem = new CartItem(product, 1);

            cartItem.Quantity = 5;

            Assert.Equal(5, cartItem.Quantity);
        }

        [Fact]
        public void CartItem_ShouldReferenceSameProductInstance()
        {
            var product = new Product("P999", "Monitor", "Electronics", 300m);
            var cartItem1 = new CartItem(product, 1);
            var cartItem2 = new CartItem(product, 2);

            Assert.Same(cartItem1.Product, cartItem2.Product);
        }
    }
}