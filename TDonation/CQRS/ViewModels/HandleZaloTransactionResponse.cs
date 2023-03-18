namespace TDonation.CQRS.ViewModels;

public class HandleZaloCallbackResponse
{
    public HandleZaloCallbackResponse(int returnCode, string returnMessage)
    {
        ReturnCode = returnCode;
        ReturnMessage = returnMessage;
    }

    public int ReturnCode { get; set; }
    public string ReturnMessage { get; set; }
}