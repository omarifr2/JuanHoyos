using DataModel.DiscountRules;
using DataModel.DTO;
using MediatR;

public class GetActiveDiscountsHandler : IRequestHandler<GetActiveDiscountsQuery, List<DiscountDetail>>
{
    private readonly IDiscountEngine _discountEngineCore;

    public GetActiveDiscountsHandler(IDiscountEngine discountEngineCore)
    {
        _discountEngineCore = discountEngineCore;
    }

    public Task<List<DiscountDetail>> Handle(GetActiveDiscountsQuery request, CancellationToken cancellationToken)
    {
        var discounts = _discountEngineCore.GetDiscounts()?
            .Select(d => new DiscountDetail { Name = d.GetType().Name.Replace("Rule", "") })
            .ToList() ?? new List<DiscountDetail>();

        return Task.FromResult(discounts);
    }
}