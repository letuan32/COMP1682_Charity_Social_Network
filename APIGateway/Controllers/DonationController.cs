using APIGateway.CQRS.Commands;
using APIGateway.CQRS.Queries;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedModels.Paypal;
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
    /// Disburse donation to post owner
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
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