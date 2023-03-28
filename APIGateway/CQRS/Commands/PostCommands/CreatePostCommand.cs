using MediatR;
using SharedModels.Enums;

namespace APIGateway.CQRS.Commands.PostCommands;

public class CreatePostCommand : IRequest<bool>
{
    public string Content { get; set; }
    public string Location { get; set; }
    public List<string>? MediaUrls { get; set; }
    public List<string>? DocumentUrls { get; set; }
    public long ExpectedAmount { get; set; }
    public DateTime ExpectedReceivedDate { get; set; }
    public PostCategoryEnum PostCategoryEnum { get; set; }
    public CurrencyEnum CurrencyEnum { get; set; }
}