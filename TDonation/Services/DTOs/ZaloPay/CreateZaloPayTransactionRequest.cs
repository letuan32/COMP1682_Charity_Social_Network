using System.Reflection;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using TDonation.CQRS.Commands;
using TDonation.Enums;
using TDonation.Services.ZaloPayHelper.Crypto;
using TDonation.Utils;

namespace TDonation.Services.DTOs.ZaloPay;

public class CreateZaloPayTransactionRequest
{
    
    [JsonProperty("app_id")]
    public int AppId { get; set; }

    [JsonProperty("app_trans_id")]
    public string AppTransId { get; set; }

    [JsonProperty("app_user")]
    public string AppUser { get; set; }

    [JsonProperty("app_time")]
    public long AppTime { get; set; }

    [JsonProperty("item")] 
    public string Item { get; set; }

    [JsonProperty("embed_data")]
    public string EmbedData { get; set; }

    [JsonProperty("amount")]
    public long Amount { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("bank_code")]
    public string BankCode { get; set; }
    
    [JsonProperty("callback_url")]
    public string? CallbackUrl { get; set; }

    [JsonProperty("mac")]
    public string Mac { get; set; }

    private readonly ZaloPayOption _option;
    public CreateZaloPayTransactionRequest(CreateTransactionCommand request,ZaloPayOption option)
    {
        _option = option;
        AppId = option.Appid;
        AppTime = long.Parse(ZaloPayHelper.Utils.GetTimeStamp().ToString());
        AppTransId = request.InternalTransactionId;
        AppUser = request.UserId;
        BankCode = GetBankCode(request.BankingType);
        EmbedData = GetEmbedData();
        Item = JsonConvert.SerializeObject(new List<string>());
        Amount = request.Amount;
        Description = request.Description;
        CallbackUrl = option.CallBackUrl;
        Mac = GenerateMac();
    }

    public Dictionary<string, string> ParseToRequestParams()
    {
        return typeof(CreateZaloPayTransactionRequest).GetProperties()
            .Where(p => p.GetCustomAttribute<JsonPropertyAttribute>() is not null)
            .ToDictionary(
                p => p.GetCustomAttribute<JsonPropertyAttribute>()?.PropertyName ?? p.Name,
                p => p.GetValue(this)?.ToString() ?? string.Empty);
    }

    private string GetBankCode(BankingTypeEnum bankingType)
    {
        switch (bankingType)
        {
            case BankingTypeEnum.ATM:
                return string.Empty;
            case BankingTypeEnum.Visa:
                return "CC";
            case BankingTypeEnum.EWallet:
                return "zalopayapp";
            default: return string.Empty;
        }
    }
    
    private string GetEmbedData()
    {
        return !string.IsNullOrEmpty(BankCode) 
            ? JsonConvert.SerializeObject(new{})
            : JsonConvert.SerializeObject(new {bankgroup = "ATM" });
    }
    
    private string GenerateMac()
    {
        var data = AppId + "|" + AppTransId + "|" + AppUser + "|" + Amount + "|" 
                   + AppTime + "|" +  EmbedData + "|" + JsonConvert.DeserializeObject(Item);

        return HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, _option.Key1, data);
    }
}