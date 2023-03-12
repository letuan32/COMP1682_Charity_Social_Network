using APIGateway.Enums;
using Microsoft.AspNetCore.Mvc;
using TDonation;


namespace APIGateway.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController : ControllerBase
{
    private readonly Payment.PaymentClient _paymentClient;
    private readonly ILogger<WeatherController> _logger;

    public PaymentController(Payment.PaymentClient paymentClient, ILogger<WeatherController> logger)
    {
        _paymentClient = paymentClient;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction()
        {
        var response = await _paymentClient.CreateTransactionAsync(new CreateTransactionRequest()
        {
            UserId = Guid.NewGuid().ToString(),
            UserEmail = "Email@gmail.com",
            Amount = 10000,
            Description = "Ung ho",
            BankingType = (int)BankingTypeEnum.Visa,
            CallbackUrl = string.Empty,
            PaymentService = (int)PaymentServiceEnum.ZaloPay
        });

        return Ok(response);
    }
    
    // [HttpGet]
    // [Route("banks")]
    // public async IAsyncEnumerable<BankDTO> GetBanks()
    // {
        // string appid = "2554";
        // string key1 = "sdngKKJmqEMzvh5QQcdD2A9XBSKUNaYn";
        // string key2 = "trMrHtvjo6myautxDUiAcYsVtaeQ8nhf";
        // string createOrderUrl = "https://sb-openapi.zalopay.vn/v2/create";
        // string getBankListUrl = "https://sbgateway.zalopay.vn/api/getlistmerchantbanks";
        // var reqtime = Utils.GetTimeStamp().ToString();
        //
        // Dictionary<string, string> param = new Dictionary<string, string>();
        // param.Add("appid", appid); 
        // param.Add("reqtime", reqtime); 
        // param.Add("mac", HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, key1, appid+"|"+reqtime));
        //
        // var result = await HttpHelper.PostFormAsync<BankListResponse>(getBankListUrl, param);
        //
        // foreach(var entry in result.banks) {
        //     var pmcid = entry.Key;
        //     var banklist = entry.Value;
        //     foreach (var bank in banklist)
        //     {
        //         yield return bank;
        //     }
        // }
    //     throw new NotImplementedException();
    // }

    [HttpPost]
    public async Task<IActionResult> CallBack(CallbackData cbdata)
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
    
    public class CallbackData {
        public string data { get; set; }
        public string mac { get; set; }
        public int type { get; set; }
        public Dictionary<string, object>? item { get; set; }
        // add more properties as needed
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