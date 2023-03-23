﻿using System.Text.Json.Serialization;
using MediatR;
using Newtonsoft.Json;
using TDonation.CQRS.ViewModels;

namespace TDonation.CQRS.Commands;

public class HandleZaloCallbackCommand : IRequest<HandleZaloCallbackResponse>
{
    public string Data { get; set; }
    
    public string Mac { get; set; }
    
    public int Type { get; set; }
    
    public ZaloPayCallbackData ParsedData  => JsonConvert.DeserializeObject<ZaloPayCallbackData>(Data);
}


public class ZaloPayCallbackData
{
    [JsonPropertyName("app_id")]
    public int AppId { get; set; }
    
    [JsonPropertyName("app_trans_id")]
    public string AppTransId { get; set; }
    
    [JsonPropertyName("app_time")]
    public long AppTime { get; set; }
    
    [JsonPropertyName("app_user")]
    public string AppUser { get; set; }
    
    [JsonPropertyName("amount")]
    public int Amount { get; set; }
    
    [JsonPropertyName("embed_data")]
    public string? EmbedData { get; set; }
    
    [JsonPropertyName("item")]
    public string? Item { get; set; }
    
    [JsonPropertyName("zp_trans_id")]
    public long ZpTransId { get; set; }
    
    [JsonPropertyName("server_time")]
    public long ServerTime { get; set; }
    
    [JsonPropertyName("channel")]
    public int Channel { get; set; }
    
    [JsonPropertyName("merchant_user_id")]
    public string MerchantUserId { get; set; }
    
    [JsonPropertyName("user_fee_amount")]
    public int UserFeeAmount { get; set; }
    
    [JsonPropertyName("discount_amount")]
    public int DiscountAmount { get; set; }
}

