using APIGateway.Controllers;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using TDonation;

namespace APIGatewayTest;

    public class DonationControllerTests
    {
        private readonly Mock<Payment.PaymentClient> _mockPaymentClient;
        private readonly Mock<ILogger<WeatherController>> _mockLogger;
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<IBus> _mockBus;
        private readonly DonationController _controller;

        public DonationControllerTests()
        {
            _mockPaymentClient = new Mock<Payment.PaymentClient>();
            _mockLogger = new Mock<ILogger<WeatherController>>();
            _mockMediator = new Mock<IMediator>();
            _mockBus = new Mock<IBus>();
            _controller = new DonationController(_mockPaymentClient.Object, _mockLogger.Object, _mockMediator.Object, _mockBus.Object);
        }

        [Fact]
        public async Task CreateTransaction_ReturnsOkResult()
        {
            // Arrange

            // Act

            // Assert
            Assert.True(true);
        }

        [Fact]
        public async Task ZaloPayCallBack_ReturnsOkResult()
        {
            // Arrange

            // Act

            // Assert
            Assert.True(true);
        }

        [Fact]
        public async Task PaypalCapture_ReturnsOkResult()
        {
            // Arrange

            // Act

            // Assert
            Assert.True(true);
        }

        [Fact]
        public async Task Disburse_ReturnsOkResult()
        {
            // Arrange

            // Act

            // Assert
            Assert.True(true);
        }

        [Fact]
        public async Task GetDonations_ReturnsOkResult()
        {
            // Arrange

            // Act

            // Assert
            Assert.True(true);
        }

        [Fact]
        public async Task GetDonationById_ReturnsNotImplementedException()
        {
            // Arrange

            // Act

            // Assert
            await Assert.ThrowsAsync<NotImplementedException>(() => _controller.GetDonationById(1));
        }

        [Fact]
        public async Task GetTotalDonationAmount_ReturnsNotImplementedException()
        {
            // Arrange

            // Act

            // Assert
            await Assert.ThrowsAsync<NotImplementedException>(() => _controller.GetTotalDonationAmount());
        }
    }