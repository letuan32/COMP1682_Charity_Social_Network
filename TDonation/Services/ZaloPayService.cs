using AutoMapper;
using Microsoft.Extensions.Options;
using TDonation.Services.Interfaces;
using TDonation.Utils;
using Flurl.Http;
using Newtonsoft.Json;
using TDonation.CQRS.Commands;
using TDonation.CQRS.ViewModels;
using TDonation.Services.DTOs.ZaloPay;
using TDonation.Services.ZaloPayHelper.Crypto;

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

        try
        {
            var response = await _zaloPayOption.CreateOrderUrl
                .PostJsonAsync(createZaloTransactionRequest)
                .ReceiveJson<CreateZaloTransactionResponse>();
            if (response.ReturnCode == 2)
                return new CreateTransactionResponse(null, null,
                    $"Failure to create transaction. {response.ReturnMessage}");
            // TODO: Save transaction to db
            return _mapper.Map<CreateTransactionResponse>(response);
        }
        catch (FlurlHttpException e)
        {
            _logger.LogError("Failure to create ZaloPay transaction. {Message}", e.Message);
            throw;
        }
    }

    public Task<HandleZaloCallbackResponse> HandZaloCallbackAsync(HandleZaloCallbackCommand request,
        CancellationToken cancellationToken)
    {
        try {
            var mac = HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, _zaloPayOption.Key2, request.Data);

            // kiểm tra callback hợp lệ (đến từ ZaloPay server)
            if (!request.Mac.Equals(mac)) {
                // callback không hợp lệ
                return Task.FromResult(new HandleZaloCallbackResponse(-1, "mac not equal"));
            }

            // thanh toán thành công
            // merchant cập nhật trạng thái cho đơn hàng
            var data = request.ParsedData;

            // TODO: Save to database
            // thông báo kết quả cho ZaloPay server
            // return Ok(result);            
            return Task.FromResult(new HandleZaloCallbackResponse(1, "success"));
        } catch (Exception ex) {
            // ZaloPay server sẽ callback lại (tối đa 3 lần)
            return Task.FromResult(new HandleZaloCallbackResponse(0, ex.Message));
        }
    }
}