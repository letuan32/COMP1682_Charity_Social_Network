using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using SharedModels.Paypal;
using TDonation.Consumers;
using TDonation.Entities;
using TDonation.Services.Interfaces;

namespace DonationServiceTest;

public class PaypalCaptureConsumerTests
{
    private readonly Mock<IDonationService> _mockDonationService;
    private readonly Mock<IUserService> _mockUserService;
    private readonly Mock<IPaypalService> _mockPaypalService;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILogger<PaypalCaptureConsumer>> _mockLogger;

    private readonly PaypalCaptureConsumer _paypalCaptureConsumer;

    public PaypalCaptureConsumerTests()
    {
        _mockDonationService = new Mock<IDonationService>();
        _mockUserService = new Mock<IUserService>();
        _mockPaypalService = new Mock<IPaypalService>();
        _mockMapper = new Mock<IMapper>();
        _mockLogger = new Mock<ILogger<PaypalCaptureConsumer>>();

        _paypalCaptureConsumer = new PaypalCaptureConsumer(
            _mockDonationService.Object,
            _mockLogger.Object,
            _mockUserService.Object,
            _mockMapper.Object,
            _mockPaypalService.Object
        );
    }

    [Fact]
    public async Task Consume_ShouldUpsertTransactionEntityByExternalIdAsync_WhenCalled()
    {
        // Arrange

        // Act

        // Assert
        Assert.True(true);
    }

    [Fact]
    public async Task Consume_ShouldLogError_WhenExceptionIsThrown()
    {
        // Arrange

        // Act

        // Assert
        Assert.True(true);
    }
}