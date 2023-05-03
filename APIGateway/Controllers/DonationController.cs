using APIGateway.CQRS.Commands;
using APIGateway.CQRS.Queries;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedModels.Enums;
using SharedModels.Paypal;
using SharedModels.Post;
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
    private readonly IBus _bus;



    public DonationController(Payment.PaymentClient paymentClient, ILogger<WeatherController> logger, IMediator mediator, IBus bus)
    {
        _paymentClient = paymentClient;
        _logger = logger;
        _mediator = mediator;
        _bus = bus;
    }

    /// <summary>
    /// Creates a new donation
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authorize(Roles = "user")]
    [HttpPost]
    public async Task<IActionResult> CreateTransaction(CreateDonationTransactionCommand request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    /// <summary>
    /// Receive ZaloPay callback
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("zalo-callBack")]
    public async Task<IActionResult> ZaloPayCallBack(ZaloCallbackCommand request)
    {
        var processResult =await _mediator.Send(request);
        return Ok(processResult);
    }
    
    /// <summary>
    /// Receive Paypal Payment Capture callback
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("paypal-capture")]
    public async Task<IActionResult> PaypalCapture([FromBody] PaypalPaymentCaptureMessage request)
    {
        var endpoint = await _bus.GetSendEndpoint(new Uri("rabbitmq://localhost/paypal-capture"));
        var headers = new Dictionary<string, object>();
        headers["Authorization"] = "Bearer myAccessToken";
        await endpoint.Send(request, context =>
        {
            foreach (var keyValuePair in headers)
            {
                context.Headers.Set(keyValuePair.Key, keyValuePair.Value);
            }
        });
        return Ok();
    }
    
    /// <summary>
    /// Receive Paypal Payment Capture callback
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("paypal-event")]
    public async Task<IActionResult> PaypalCapture([FromBody] PayPalEvent request)
    {
        if (request.event_type != "PAYMENT.PAYOUTSBATCH.SUCCESS") return Ok();
            
        var endpoint = await _bus.GetSendEndpoint(new Uri("rabbitmq://localhost/post-approve-status"));
        var postId = request.resource.batch_header.sender_batch_header.sender_batch_id.Split('_')[1];
        var message = new UpdatePostApproveStatusMessage()
        {
            PostId = int.Parse(postId),
            PostApproveStatusEnum = PostApproveStatusEnum.Disbursed,
        };
        var headers = new Dictionary<string, object>();
        headers["Authorization"] = "Bearer myAccessToken";
        await endpoint.Send(message, context =>
        {
            foreach (var keyValuePair in headers)
            {
                context.Headers.Set(keyValuePair.Key, keyValuePair.Value);
            }
        });
        return Ok();
    }
    
    /// <summary>
    /// Disburse donation to post owner
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "admin,manager")]
    [Route("disburse")]
    public async Task<IActionResult> Disburse([FromBody] DisburseCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok();
    }
    
    /// <summary>
    /// Retrieves a list of donations
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetDonations([FromQuery] GetDonationQuery request)
    {
        var result = await _mediator.Send(request);
        return Ok();
    }
    
    /// <summary>
    /// Retrieves the details of a single donation by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDonationById(int id)
    {
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Retrieves the total amount of donations
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet("totalAmount")]
    public async Task<IActionResult> GetTotalDonationAmount()
    {
        throw new NotImplementedException();
    }
}

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Amount
{
    public string currency { get; set; }
    public string value { get; set; }
}

public class BatchHeader
{
    public string payout_batch_id { get; set; }
    public string batch_status { get; set; }
    public DateTime time_created { get; set; }
    public DateTime time_completed { get; set; }
    public SenderBatchHeader sender_batch_header { get; set; }
    public Amount amount { get; set; }
    public Fees fees { get; set; }
    public int payments { get; set; }
}

public class Fees
{
    public string currency { get; set; }
    public string value { get; set; }
}

public class Link
{
    public string? href { get; set; }
    public string? rel { get; set; }
    public string? method { get; set; }
    public string? encType { get; set; }
}

public class Resource
{
    public BatchHeader batch_header { get; set; }
    public List<Link> links { get; set; }
}

public class PayPalEvent
{
    public string id { get; set; }
    public DateTime create_time { get; set; }
    public string resource_type { get; set; }
    public string event_type { get; set; }
    public string summary { get; set; }
    public Resource resource { get; set; }
    public List<Link> links { get; set; }
    public string event_version { get; set; }
}

public class SenderBatchHeader
{
    public string sender_batch_id { get; set; }
}

