using Newtonsoft.Json;

namespace TDonation.Services.DTOs.Paypal;

public class PaypalCaptureResponse
{
    [JsonProperty("id")]
    public string Id { get; set; }
    
    [JsonProperty("amount")]
    public Money Amount { get; set; }
    
    [JsonProperty("status")]
    public string Status { get; set; }
    
    [JsonProperty("create_time")]
    public DateTime CreateTime { get; set; }
    
    [JsonProperty("update_time")]
    public DateTime UpdateTime { get; set; }

    [JsonProperty("invoice_id")]
    public string InvoiceId { get; set; }
    
    [JsonProperty("custom_id")]
    public string CustomId { get; set; }
}

public class PurchaseUnit
{
    [JsonProperty("reference_id")]
    public string ReferenceId { get; set; }
    
    [JsonProperty("amount")]
    public Money Amount { get; set; }
    
    [JsonProperty("payee")]
    public Payee Payee { get; set; }
}

public class Money
{
    [JsonProperty("currency_code")]
    public string CurrencyCode { get; set; }
    [JsonProperty("value")]
    public string Value { get; set; }
}

public class Payee
{
    [JsonProperty("email_address")]
    public string EmailAddress { get; set; }
    
    [JsonProperty("merchant_id")]
    public string Merchantid { get; set; }
}