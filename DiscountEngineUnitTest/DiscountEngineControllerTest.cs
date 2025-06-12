using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using DataModel.DTO;
using DataModel;

public class DiscountEngineControllerTest
{
    private readonly Mock<IMediator> _mockMediator;
    private readonly CartController _controller;

    public DiscountEngineControllerTest()
    {
        _mockMediator = new Mock<IMediator>();
        _controller = new CartController(_mockMediator.Object);
    }

    [Fact]
    public async Task ApplyDiscounts_ShouldReturnBadRequest_WhenCartIsNull()
    {
        var cart = (Cart)null; 

        _mockMediator.Setup(m => m.Send(It.IsAny<ApplyDiscountsRequest>(), CancellationToken.None))
                     .ReturnsAsync(new DiscountResponse { Error = "Invalid cart data" });

        var result = await _controller.ApplyDiscounts(cart) as BadRequestObjectResult;

        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal("Invalid cart data", result.Value);
    }
    [Fact]
    public async Task ApplyDiscounts_ShouldApplyDiscounts_Correctly()
    {
        var cart = new Cart();
        var product = new Product("P001", "Laptop", "Electronics", 1000m);
        cart.AddItem(product, 1);

        var response = new DiscountResponse
        {
            OriginalTotal = 1000m,
            Discounts = new List<DiscountDetail>
        {
            new DiscountDetail { Name = "Electronics10", Amount = 100m }
        },
            FinalTotal = 900m
        };

        _mockMediator.Setup(m => m.Send(It.IsAny<ApplyDiscountsRequest>(), CancellationToken.None))
                     .ReturnsAsync(response);  // Ensure Mediator receives an ApplyDiscountsRequest

        var result = await _controller.ApplyDiscounts(cart) as OkObjectResult;  // Pass only Cart

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        var responseData = result.Value as DiscountResponse;
        Assert.NotNull(responseData);
        Assert.Equal(1000m, responseData.OriginalTotal);
        Assert.Single(responseData.Discounts);
        Assert.Equal(900m, responseData.FinalTotal);
    }

    [Fact]
    public async Task GetActiveDiscounts_ShouldReturnListOfDiscounts()
    {
        var request = new GetActiveDiscountsQuery();
        var expectedDiscounts = new List<DiscountDetail>
        {
            new DiscountDetail { Name = "Books15", Amount = 15m }
        };

        _mockMediator.Setup(m => m.Send(request, CancellationToken.None)).ReturnsAsync(expectedDiscounts);

        var result = await _controller.GetActiveDiscounts() as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        var responseData = result.Value as List<DiscountDetail>;
        Assert.NotNull(responseData);
        Assert.Single(responseData);
        Assert.Equal("Books15", responseData[0].Name);
    }
}