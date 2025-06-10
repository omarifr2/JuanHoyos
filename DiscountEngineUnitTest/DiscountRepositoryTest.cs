using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using DataModel;
using DataModel.DiscountRules;

namespace DiscountEngine.Test
{
    public class DiscountRepositoryTest
    {
        [Fact]
        public void Should_StartWith_EmptyRepository()
        {
            var repo = new DiscountRepository();
            Assert.Empty(repo.GetDiscounts());
        }

        [Fact]
        public void Should_AddDiscountRule_Successfully()
        {
            var repo = new DiscountRepository();
            var mockDiscount = new Mock<IDiscountRule>();

            repo.AddDiscount(mockDiscount.Object);

            var stored = repo.GetDiscounts();
            Assert.Single(stored);
            Assert.Contains(mockDiscount.Object, stored);
        }

        [Fact]
        public void Should_Store_MultipleDistinctDiscountRules()
        {
            var repo = new DiscountRepository();

            for (int i = 0; i < 5; i++)
            {
                var mockRule = new Mock<IDiscountRule>();
                repo.AddDiscount(mockRule.Object);
            }

            var stored = repo.GetDiscounts();
            Assert.Equal(5, stored.Count);
        }

        [Fact]
        public void Repository_ShouldMaintainInsertionOrder()
        {
            var repo = new DiscountRepository();
            var first = new Mock<IDiscountRule>();
            var second = new Mock<IDiscountRule>();

            repo.AddDiscount(first.Object);
            repo.AddDiscount(second.Object);

            var stored = repo.GetDiscounts();
            Assert.Same(first.Object, stored[0]);
            Assert.Same(second.Object, stored[1]);
        }

        [Fact]
        public void DiscountRules_ShouldRemainUnmodifiedExternally()
        {
            var repo = new DiscountRepository();
            var rule = new Mock<IDiscountRule>();

            var list = new List<IDiscountRule>(repo.GetDiscounts());

            repo.AddDiscount(rule.Object);

            Assert.Empty(list);
        }

        [Fact]
        public void Should_HandleDuplicateDiscountReferences()
        {
            var repo = new DiscountRepository();
            var duplicateRule = new Mock<IDiscountRule>();

            repo.AddDiscount(duplicateRule.Object);
            repo.AddDiscount(duplicateRule.Object);

            var stored = repo.GetDiscounts();
            Assert.Equal(2, stored.Count);
        }

        [Fact]
        public void ShouldAccept_RandomizedMockImplementations()
        {
            var repo = new DiscountRepository();
            var random = new Random();

            for (int i = 0; i < 10; i++)
            {
                var mock = new Mock<IDiscountRule>();
                mock.Setup(r => r.ApplyDiscount(It.IsAny<Cart>()))
                    .Returns(random.Next(1, 100));

                repo.AddDiscount(mock.Object);
            }

            var result = repo.GetDiscounts();
            Assert.Equal(10, result.Count);
        }
    }
}