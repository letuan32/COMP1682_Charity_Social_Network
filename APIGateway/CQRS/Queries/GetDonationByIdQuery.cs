using MediatR;
using SharedModels.Paging;

namespace APIGateway.CQRS.Queries;

public class GetDonationByIdQuery : PagingParam, IRequest<GetDonationItemResponse>
{
    public int? PostId { get; set; }
    public int? SenderId { get; set; }
    public int? ReceiverId { get; set; }
}

public class GetDonationByIdQueryHandler : IRequestHandler<GetDonationQuery, List<GetDonationItemResponse>>
{
    public Task<List<GetDonationItemResponse>> Handle(GetDonationQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

