using Newtonsoft.Json;

namespace TDonation.Services.DTOs.ZaloPay;

public class CreateZaloTransactionResponse
{
    [JsonProperty("return_code")]
    public int ReturnCode { get; set; }

    [JsonProperty("return_message")]
    public string ReturnMessage { get; set; }

    [JsonProperty("sub_return_code")]
    public int SubReturnCode { get; set; }

    [JsonProperty("sub_return_message")]
    public string SubReturnMessage { get; set; }

    [JsonProperty("zp_trans_token")]
    public string ZpTransToken { get; set; }

    [JsonProperty("order_url")]
    public string OrderUrl { get; set; }

    [JsonProperty("order_token")]
    public string OrderToken { get; set; }
}