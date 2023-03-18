using APIGateway.CQRS.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TDonation;


namespace APIGateway.Controllers;

[ApiController]
// [Authorize]
[Route("[controller]")]
public class DonationController : ControllerBase
{
    private readonly Payment.PaymentClient _paymentClient;
    private readonly ILogger<WeatherController> _logger;
    private readonly IMediator _mediator;


    public DonationController(Payment.PaymentClient paymentClient, ILogger<WeatherController> logger, IMediator mediator)
    {
        _paymentClient = paymentClient;
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction(CreateDonationTransactionCommand request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost]
    [Route("zalo-callBack")]
    public async Task<IActionResult> CallBack(ZaloCallbackCommand cbdata)
    {
        // var result = new Dictionary<string, object>();
        // string key2 = "trMrHtvjo6myautxDUiAcYsVtaeQ8nhf";
        //
        // try {
        //     var dataStr = Convert.ToString(cbdata.data);
        //     var reqMac = Convert.ToString(cbdata.mac);
        //
        //     var mac = HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, key2, dataStr);
        //
        //     Console.WriteLine("mac = {0}", mac);
        //
        //     // kiểm tra callback hợp lệ (đến từ ZaloPay server)
        //     if (!reqMac.Equals(mac)) {
        //         // callback không hợp lệ
        //         result["return_code"] = -1;
        //         result["return_message"] = "mac not equal";
        //     }
        //     else {
        //         // thanh toán thành công
        //         // merchant cập nhật trạng thái cho đơn hàng
        //         var dataJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(dataStr);
        //         Console.WriteLine("update order's status = success where app_trans_id = {0}", dataJson["app_trans_id"]);
        //
        //         result["return_code"] = 1;
        //         result["return_message"] = "success";
        //     }
        // } catch (Exception ex) {
        //     result["return_code"] = 0; // ZaloPay server sẽ callback lại (tối đa 3 lần)
        //     result["return_message"] = ex.Message;
        // }

        // thông báo kết quả cho ZaloPay server
        // return Ok(result); 
        throw new NotImplementedException();

    }
    
    public class BankDTO {
        public string bankcode { get; set; }
        public string name { get; set; }
        public int displayorder { get; set; }
        public int pmcid { get; set; }
    }

    class BankListResponse {
        public string returncode { get; set; }
        public string returnmessage { get; set; }
        public Dictionary<string, List<BankDTO>> banks { get; set; }
    }
}