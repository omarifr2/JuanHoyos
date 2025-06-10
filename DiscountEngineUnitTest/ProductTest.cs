using System;
using Xunit;
using DataModel;

namespace DiscountEngine.Test
{
    public class ProductTest
    {
        [Fact]
        public void Product_ShouldInitializeCorrectly()
        {
            var product = new Product("P123", "Laptop", "Electronics", 1000m);

            Assert.Equal("P123", product.SKU);
            Assert.Equal("Laptop", product.Name);
            Assert.Equal("Electronics", product.Category);
            Assert.Equal(1000m, product.Price);
        }

        [Fact]
        public void Product_ShouldAllowPropertyUpdates()
        {
            var product = new Product("P456", "Mouse", "Accessories", 50m);

            product.Name = "Wireless Mouse";
            product.Price = 60m;

            Assert.Equal("Wireless Mouse", product.Name);
            Assert.Equal(60m, product.Price);
        }

        [Fact]
        public void Product_ShouldHandleDecimalPrecision()
        {
            var product = new Product("P789", "Keyboard", "Accessories", 79.99m);

            Assert.Equal(79.99m, product.Price);
        }
    }
}