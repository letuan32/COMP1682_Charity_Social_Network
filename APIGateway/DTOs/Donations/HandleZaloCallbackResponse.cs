using Newtonsoft.Json;

namespace APIGateway.DTOs.Donations;

public class HandleZaloCallbackResponse
{
    public HandleZaloCallbackResponse(int returnCode, string returnMessage)
    {
        ReturnCode = returnCode;
        ReturnMessage = returnMessage;
    }

    [JsonProperty("return_code")]
    public int ReturnCode { get; set; }
    
    [JsonProperty("return_message")]
    public string ReturnMessage { get; set; }
}