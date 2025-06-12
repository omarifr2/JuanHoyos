using DataModel.DTO;
using MediatR;

public class GetActiveDiscountsQuery : IRequest<List<DiscountDetail>>
{
}
