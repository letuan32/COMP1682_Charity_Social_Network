using APIGateway.CQRS.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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


    public DonationController(Payment.PaymentClient paymentClient, ILogger<WeatherController> logger, IMediator mediator)
    {
        _paymentClient = paymentClient;
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction(CreateDonationTransactionCommand request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost]
    [Route("zalo-callBack")]
    public async Task<IActionResult> ZaloPayCallBack(ZaloCallbackCommand request)
    {
        var processResult =await _mediator.Send(request);
        return Ok(processResult);
    }
    
}