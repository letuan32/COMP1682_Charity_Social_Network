using APIGateway.ZaloPayHelper;
using AutoMapper;
using Microsoft.Extensions.Options;
using TDonation.Services.Interfaces;
using TDonation.Utils;
using Flurl.Http;
using Newtonsoft.Json;
using TDonation.CQRS.Commands;
using TDonation.CQRS.ViewModels;
using TDonation.Services.DTOs.ZaloPay;

namespace TDonation.Services;

public class ZaloPayService : IZaloPayService
{
    private readonly ZaloPayOption _zaloPayOption;
    private readonly ILogger<ZaloPayService> _logger;
    private readonly IMapper _mapper;



    public ZaloPayService(IOptions<ZaloPayOption> zaloPayOption, ILogger<ZaloPayService> logger, IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
        _zaloPayOption = zaloPayOption.Value;
    }

    public async Task<CreateTransactionResponse> CreateTransactionAsync(CreateTransactionCommand request)
    {
        var createZaloTransactionRequest = new CreateZaloPayTransactionRequest(request, _zaloPayOption);
        // var requestBody = createZaloTransactionRequest.ParseToRequestParams();

        try
        {
            var response = await _zaloPayOption.CreateOrderUrl
                .PostJsonAsync(createZaloTransactionRequest)
                .ReceiveJson<CreateZaloTransactionResponse>();

            return _mapper.Map<CreateTransactionResponse>(response);
        }
        catch (FlurlHttpException e)
        {
            _logger.LogError("Failure to create ZaloPay transaction. {Message}", e.Message);
            throw;
        }
    }
}