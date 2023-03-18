namespace SharedModels;

public class ZaloCallbackResponse
{
    public string Data { get; set; }
    public string Mac { get; set; }
    public int Type { get; set; }
    public Dictionary<string, object>? Item { get; set; }
}