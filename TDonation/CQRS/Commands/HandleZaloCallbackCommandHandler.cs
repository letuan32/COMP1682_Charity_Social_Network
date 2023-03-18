using MediatR;
using TDonation.CQRS.ViewModels;
using TDonation.Services;
using TDonation.Services.Interfaces;

namespace TDonation.CQRS.Commands;

public class HandleZaloCallbackCommandHandler : IRequestHandler<HandleZaloCallbackCommand, HandleZaloCallbackResponse>
{
    private readonly IZaloPayService _zaloPayService;

    public HandleZaloCallbackCommandHandler(IZaloPayService zaloPayService)
    {
        _zaloPayService = zaloPayService;
    }


    public async Task<HandleZaloCallbackResponse> Handle(HandleZaloCallbackCommand request, CancellationToken cancellationToken)
    {
        return await _zaloPayService.HandZaloCallbackAsync(request, cancellationToken);
    }
}