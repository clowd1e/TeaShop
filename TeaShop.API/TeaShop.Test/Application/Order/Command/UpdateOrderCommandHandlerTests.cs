using AutoMapper;
using FluentAssertions;
using Moq;
using TeaShop.Application.Comparison;
using TeaShop.Application.Data;
using TeaShop.Application.DTOs.Address.Request.Update;
using TeaShop.Application.DTOs.Customer.Request.Update;
using TeaShop.Application.DTOs.Order.Request.Update;
using TeaShop.Application.DTOs.OrderDetails.Request.Update;
using TeaShop.Application.DTOs.TeaItem.Request.Update;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Application.Service.Order.Command.UpdateOrder;
using TeaShop.Domain.Enums;
using TeaShop.Domain.Repository;
using TeaShop.Test.Configuration;
using Entities = TeaShop.Domain.Entities;

namespace TeaShop.Test.Application.Order.Command
{
    public sealed class UpdateOrderCommandHandlerTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandlerTests()
        {
            _orderRepositoryMock = new();
            _unitOfWorkMock = new();
            _mapper = AutoMapperConfiguration.GetMapper();
            Entities.Order.ComparisonDelegate = ComparisonExtensions.OrderDefaultComparison;
        }

        [Fact]
        public async Task Handle_Should_ReturnFailResult_When_ValidationFails()
        {
            // Arrange
            var addressRequest = new UpdateAddressRequestDto(Country.GBR, "London", "Somewhere 2", null, "ASD ASXC2");
            var customerRequest = new UpdateCustomerRequestDto("John", "Pork", "02355876", "asdas@gmail.com", addressRequest);
            var itemsRequest = new List<UpdateTeaItemRequestDto>()
            {
                new UpdateTeaItemRequestDto(Guid.NewGuid(), null, 0)
            };
            var orderDetailsRequest = new UpdateOrderDetailsRequestDto(
                itemsRequest, DateTime.UtcNow, addressRequest, 0, Status.InProgress
                );
            var request = new UpdateOrderRequestDto(customerRequest, orderDetailsRequest);
            var command = new UpdateOrderCommand(Guid.NewGuid(), request);

            var handler = new UpdateOrderCommandHandler(
                _orderRepositoryMock.Object,
                _mapper,
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
            var addressRequest = new UpdateAddressRequestDto(Country.GBR, "London", "Somewhere 2", 3, "ASD ASXC2");
            var customerRequest = new UpdateCustomerRequestDto("John", "Pork", "02355876", "asdas@gmail.com", addressRequest);
            var itemsRequest = new List<UpdateTeaItemRequestDto>()
            {
                new UpdateTeaItemRequestDto(Guid.NewGuid(), 3, 0)
            };
            var orderDetailsRequest = new UpdateOrderDetailsRequestDto(
                itemsRequest, DateTime.UtcNow, addressRequest, 0, Status.InProgress
                );
            var request = new UpdateOrderRequestDto(customerRequest, orderDetailsRequest);
            var command = new UpdateOrderCommand(Guid.NewGuid(), request);

            var handler = new UpdateOrderCommandHandler(
                _orderRepositoryMock.Object,
                _mapper,
                _unitOfWorkMock.Object);

            _orderRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync((Entities.Order)null);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Errors.ToList()[0].Should().BeEquivalentTo(OrderErrors.OrderNotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnOkResult_When_OrderUpdated()
        {
            // Arrange
            var addressRequest = new UpdateAddressRequestDto(Country.GBR, "London", "Somewhere 2", null, "ASD ASXC2");
            var customerRequest = new UpdateCustomerRequestDto("John", "Pork", "02355876", "asdas@gmail.com", addressRequest);
            var itemsRequest = new List<UpdateTeaItemRequestDto>()
            {
                new UpdateTeaItemRequestDto(Guid.NewGuid(), 3, 0)
            };
            var orderDetailsRequest = new UpdateOrderDetailsRequestDto(
                itemsRequest, DateTime.UtcNow, addressRequest, 0, Status.InProgress
                );
            var request = new UpdateOrderRequestDto(customerRequest, orderDetailsRequest);
            var command = new UpdateOrderCommand(Guid.NewGuid(), request);

            var handler = new UpdateOrderCommandHandler(
                _orderRepositoryMock.Object,
                _mapper,
                _unitOfWorkMock.Object);

            _orderRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync(new Entities.Order());

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            result.Should().BeEquivalentTo(Result.Ok());
        }

        [Fact]
        public async Task Handle_Should_CallUpdateAsync_When_OrderUpdated()
        {
            // Arrange
            var addressRequest = new UpdateAddressRequestDto(Country.GBR, "London", "Somewhere 2", null, "ASD ASXC2");
            var customerRequest = new UpdateCustomerRequestDto("John", "Pork", "02355876", "asdas@gmail.com", addressRequest);
            var itemsRequest = new List<UpdateTeaItemRequestDto>()
            {
                new UpdateTeaItemRequestDto(Guid.NewGuid(), 3, 0)
            };
            var orderDetailsRequest = new UpdateOrderDetailsRequestDto(
                itemsRequest, DateTime.UtcNow, addressRequest, 0, Status.InProgress
                );
            var request = new UpdateOrderRequestDto(customerRequest, orderDetailsRequest);
            var command = new UpdateOrderCommand(Guid.NewGuid(), request);

            var handler = new UpdateOrderCommandHandler(
                _orderRepositoryMock.Object,
                _mapper,
                _unitOfWorkMock.Object);

            var oldOrder = _mapper.Map<Entities.Order>(request);

            _orderRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync(oldOrder);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            _orderRepositoryMock.Verify(
                x => x.UpdateAsync(
                    It.IsAny<Entities.Order>(),
                    It.IsAny<Entities.Order>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_Should_NotCallUnitOfWork_When_ValidationFails()
        {
            // Arrange
            var addressRequest = new UpdateAddressRequestDto(Country.GBR, "", "Somewhere 2", null, "ASD ASXC2");
            var customerRequest = new UpdateCustomerRequestDto("John", "", "02355876", "asdas@gmail.com", addressRequest);
            var itemsRequest = new List<UpdateTeaItemRequestDto>()
            {
                new UpdateTeaItemRequestDto(Guid.NewGuid(), null, 0)
            };
            var orderDetailsRequest = new UpdateOrderDetailsRequestDto(
                itemsRequest, DateTime.UtcNow, addressRequest, 0, Status.InProgress
                );
            var request = new UpdateOrderRequestDto(customerRequest, orderDetailsRequest);
            var command = new UpdateOrderCommand(Guid.NewGuid(), request);

            var handler = new UpdateOrderCommandHandler(
                _orderRepositoryMock.Object,
                _mapper,
                _unitOfWorkMock.Object);

            _orderRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync(new Entities.Order());

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            _unitOfWorkMock.Verify(
                x => x.SaveChangesAsync(
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }
    }
}
