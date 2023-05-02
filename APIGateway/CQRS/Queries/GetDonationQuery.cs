using MediatR;
using SharedModels.Paging;

namespace APIGateway.CQRS.Queries;

public class GetDonationQuery : PagingParam, IRequest<List<GetDonationItemResponse>>
{
    public int? PostId { get; set; }
    public int? SenderId { get; set; }
    public int? ReceiverId { get; set; }
}

public class GetDonationQueryHandler : IRequestHandler<GetDonationQuery, List<GetDonationItemResponse>>
{
    public Task<List<GetDonationItemResponse>> Handle(GetDonationQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}


public class GetDonationItemResponse
{
    public int Id { get; set; }
    public string ExternalTransactionId { get; set; }
    public string PaymentService { get; set; }
    public string PaymentStatus { get; set; }
    public string PaymentMethod { get; set; }
    public long Amount { get; set; }    
    public long Fees { get; set; }
    public long Taxes { get; set; }
    public string Currency { get; set; }
    public string Message { get; set; }
    public string SenderId { get; set; }
    public string SenderName { get; set; }
    public string ReceiverId { get; set; }
    public string ReceiverName { get; set; }
    public int PostId { get; set; }
}