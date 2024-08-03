using FluentAssertions;
using Moq;
using TeaShop.Application.Data;
using TeaShop.Application.DTOs.Order.Request.Remove;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Application.Service.Order.Command.RemoveOrder;
using TeaShop.Domain.Repository;
using Entities = TeaShop.Domain.Entities;

namespace TeaShop.Test.Application.Order.Command
{
    public sealed class RemoveOrderCommandHandlerTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public RemoveOrderCommandHandlerTests()
        {
            _orderRepositoryMock = new();
            _unitOfWorkMock = new();
        }

        [Fact]
        public async Task Handle_Should_ReturnFailResult_When_ValidationFails()
        {
            // Arrange
            var request = new RemoveOrderRequestDto(null);
            var command = new RemoveOrderCommand(request);

            var handler = new RemoveOrderCommandHandler(
                _orderRepositoryMock.Object,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Errors.ToList().ForEach(x => x.Code.Should().Be("Validation"));
        }

        [Fact]
        public async Task Handle_Should_ReturnFailResult_When_OrderNotFound()
        {
            // Arrange
            var request = new RemoveOrderRequestDto(Guid.NewGuid());
            var command = new RemoveOrderCommand(request);

            _orderRepositoryMock.Setup(
                               x => x.GetByIdAsync(
                                                      It.IsAny<Guid>()))
                .ReturnsAsync((Entities.Order)null);

            var handler = new RemoveOrderCommandHandler(
                _orderRepositoryMock.Object,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Errors.ToList()[0].Should().BeEquivalentTo(OrderErrors.OrderNotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnOkResult_When_OrderFound()
        {
            // Arrange
            var request = new RemoveOrderRequestDto(Guid.NewGuid());
            var command = new RemoveOrderCommand(request);

            _orderRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync(new Entities.Order());

            var handler = new RemoveOrderCommandHandler(
                _orderRepositoryMock.Object,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            result.Should().BeEquivalentTo(Result.Ok());
        }

        [Fact]
        public async Task Handle_Should_CallDeleteAsync_When_OrderFound()
        {
            // Arrange
            var request = new RemoveOrderRequestDto(Guid.NewGuid());
            var command = new RemoveOrderCommand(request);

            var order = new Entities.Order();
            _orderRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync(order);

            var handler = new RemoveOrderCommandHandler(
                _orderRepositoryMock.Object,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            _orderRepositoryMock.Verify(
                x => x.DeleteAsync(order),
                Times.Once);
        }

        [Fact]
        public async Task Handle_Should_NotCallUnitOfWork_When_ValidationFails()
        {
            // Arrange
            var request = new RemoveOrderRequestDto(null);
            var command = new RemoveOrderCommand(request);

            var handler = new RemoveOrderCommandHandler(
                _orderRepositoryMock.Object,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            _unitOfWorkMock.Verify(
                x => x.SaveChangesAsync(default),
                Times.Never);
        }
    }
}
