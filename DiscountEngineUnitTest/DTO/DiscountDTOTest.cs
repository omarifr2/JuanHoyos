using System;
using System.Collections.Generic;
using Xunit;
using DataModel.DTO;

namespace DiscountEngine.Test
{
    public class DiscountDTOTest
    {
        [Fact]
        public void DiscountDetail_ShouldStoreCorrectValues()
        {
            var discount = new DiscountDetail
            {
                Name = "WELCOME10",
                Amount = 10.00m
            };

            Assert.Equal("WELCOME10", discount.Name);
            Assert.Equal(10.00m, discount.Amount);
        }

        [Fact]
        public void DiscountResponse_ShouldStoreOriginalTotal()
        {
            var response = new DiscountResponse
            {
                OriginalTotal = 150.00m
            };

            Assert.Equal(150.00m, response.OriginalTotal);
        }

        [Fact]
        public void DiscountResponse_ShouldStoreFinalTotal()
        {
            var response = new DiscountResponse
            {
                OriginalTotal = 150.00m,
                FinalTotal = 135.00m
            };

            Assert.Equal(135.00m, response.FinalTotal);
        }

        [Fact]
        public void DiscountResponse_ShouldStoreMultipleDiscounts()
        {
            var response = new DiscountResponse
            {
                OriginalTotal = 200.00m,
                Discounts = new List<DiscountDetail>
                {
                    new DiscountDetail { Name = "Books 15% Off", Amount = 6.00m },
                    new DiscountDetail { Name = "WELCOME10 Coupon", Amount = 11.40m }
                },
                FinalTotal = 182.60m
            };

            Assert.Equal(200.00m, response.OriginalTotal);
            Assert.Equal(182.60m, response.FinalTotal);
            Assert.Equal(2, response.Discounts.Count);
            Assert.Contains(response.Discounts, d => d.Name == "Books 15% Off");
            Assert.Contains(response.Discounts, d => d.Name == "WELCOME10 Coupon");
        }
    }
}