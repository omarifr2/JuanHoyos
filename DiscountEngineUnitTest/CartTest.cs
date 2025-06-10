using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using DataModel;

namespace DiscountEngine.Test
{
    public class CartTest
    {
        [Fact]
        public void Cart_ShouldStartWithEmptyList()
        {
            var cart = new Cart();
            Assert.Empty(cart.Items);
        }

        [Fact]
        public void AddItem_ShouldIncreaseCartSize()
        {
            var cart = new Cart();
            var product = new Product("P123", "Laptop", "Electronics", 1000m);

            cart.AddItem(product, 2);

            Assert.Single(cart.Items);
            Assert.Equal(2, cart.Items[0].Quantity);
        }

        [Fact]
        public void GetTotal_ShouldReturnCorrectAmount()
        {
            var cart = new Cart();
            var product1 = new Product("P123", "Laptop", "Electronics", 1000m);
            var product2 = new Product("P456", "Mouse", "Accessories", 50m);

            cart.AddItem(product1, 1);
            cart.AddItem(product2, 2);

            decimal expectedTotal = (1000m * 1) + (50m * 2);
            Assert.Equal(expectedTotal, cart.GetTotal());
        }

        [Fact]
        public void GetTotal_ShouldReturnZero_ForEmptyCart()
        {
            var cart = new Cart();
            Assert.Equal(0m, cart.GetTotal());
        }

        [Fact]
        public void AddItem_ShouldHandleMultipleEntries()
        {
            var cart = new Cart();
            var product = new Product("P789", "Keyboard", "Accessories", 80m);

            cart.AddItem(product, 1);
            cart.AddItem(product, 2);

            Assert.Equal(2, cart.Items.Count);
            Assert.Equal(3, cart.Items.Sum(i => i.Quantity));
        }
    }
}