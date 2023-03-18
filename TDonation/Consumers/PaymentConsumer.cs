using MassTransit;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SharedModels;
using TDonation.Services.ZaloPayHelper.Crypto;
using TDonation.Utils;

namespace TDonation.Consumers;

public class PaymentConsumer : IConsumer<ZaloCallbackMessage>
{
    private readonly ZaloPayOption _zaloPayOption;

    public PaymentConsumer(IOptions<ZaloPayOption> zaloPayOption)
    {
        _zaloPayOption = zaloPayOption.Value;
    }

    public Task Consume(ConsumeContext<ZaloCallbackMessage> context)
    {
        var contextMessage = context.Message;
        
        var result = new Dictionary<string, object>();
        string key2 = "trMrHtvjo6myautxDUiAcYsVtaeQ8nhf";
        
        try {
            var dataStr = Convert.ToString(contextMessage.Data);
            var reqMac = Convert.ToString(contextMessage.Mac);
        
            var mac = HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, key2, dataStr);
            
        
            // kiểm tra callback hợp lệ (đến từ ZaloPay server)
            if (!reqMac.Equals(mac)) {
                // callback không hợp lệ
                result["return_code"] = -1;
                result["return_message"] = "mac not equal";
            }
            else {
                // thanh toán thành công
                // merchant cập nhật trạng thái cho đơn hàng
                var dataJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(dataStr);
                Console.WriteLine("update order's status = success where app_trans_id = {0}", dataJson["app_trans_id"]);
        
                result["return_code"] = 1;
                result["return_message"] = "success";
            }
        } catch (Exception ex) {
            result["return_code"] = 0; // ZaloPay server sẽ callback lại (tối đa 3 lần)
            result["return_message"] = ex.Message;
        }

        // thông báo kết quả cho ZaloPay server
        // return Ok(result); 
        throw new NotImplementedException();
    }
}