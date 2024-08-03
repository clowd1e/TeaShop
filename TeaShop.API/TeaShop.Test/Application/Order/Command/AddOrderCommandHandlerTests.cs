using AutoMapper;
using FluentAssertions;
using Moq;
using TeaShop.Application.Data;
using TeaShop.Application.DTOs.Address.Request.Add;
using TeaShop.Application.DTOs.Customer.Request.Add;
using TeaShop.Application.DTOs.Order.Request.Add;
using TeaShop.Application.DTOs.OrderDetails.Request.Add;
using TeaShop.Application.DTOs.TeaItem.Request.Add;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Application.Service.Order.Command.AddOrder;
using TeaShop.Domain.Enums;
using TeaShop.Domain.Repository;
using TeaShop.Test.Configuration;
using Entities = TeaShop.Domain.Entities;

namespace TeaShop.Test.Application.Order.Command
{
    public sealed class AddOrderCommandHandlerTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<ITeaRepository> _teaRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;

        public AddOrderCommandHandlerTests()
        {
            _orderRepositoryMock = new();
            _teaRepositoryMock = new();
            _unitOfWorkMock = new();
            _mapper = AutoMapperConfiguration.GetMapper();
        }

        [Fact]
        public async Task Handle_Should_ReturnFailResult_When_ValidationFails()
        {
            // Arrange
            var addressRequest = new AddAddressRequestDto(Country.GBR, "", "Somewhere 2", null, "ASD ASXC2");
            var customerRequest = new AddCustomerRequestDto("John", "", "02355876", "asdas@gmail.com", addressRequest);
            var itemsRequest = new List<AddTeaItemRequestDto>()
            {
                new AddTeaItemRequestDto(Guid.NewGuid(), null, 0)
            };
            var orderDetailsRequest = new AddOrderDetailsRequestDto(
                itemsRequest, null, addressRequest, 0, Status.New
                );
            var orderRequest = new AddOrderRequestDto(customerRequest, orderDetailsRequest);
            var command = new AddOrderCommand(orderRequest);

            var handler = new AddOrderCommandHandler(
                _orderRepositoryMock.Object,
                _teaRepositoryMock.Object,
                _mapper,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Errors.ToList().ForEach(x => x.Code.Should().Be("Validation"));
        }

        [Fact]
        public async Task Handle_Should_ReturnFailResult_When_TeaNotFound()
        {
            // Arrange
            var addressRequest = new AddAddressRequestDto(Country.GBR, "London", "Somewhere 2", null, "ASD ASXC2");
            var customerRequest = new AddCustomerRequestDto("John", "Pork", "02355876", "asdas@gmail.com", addressRequest);
            var itemsRequest = new List<AddTeaItemRequestDto>()
            {
                new AddTeaItemRequestDto(Guid.NewGuid(), 3, 0)
            };
            var orderDetailsRequest = new AddOrderDetailsRequestDto(
                itemsRequest, DateTime.UtcNow, addressRequest, 0, Status.New
                );
            var orderRequest = new AddOrderRequestDto(customerRequest, orderDetailsRequest);
            var command = new AddOrderCommand(orderRequest);

            _teaRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync((Entities.Tea)null);

            var handler = new AddOrderCommandHandler(
                _orderRepositoryMock.Object,
                _teaRepositoryMock.Object,
                _mapper,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Errors.ToList()[0].Should().Be(TeaErrors.TeaNotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnOkResult_When_OrderAdded()
        {
            // Arrange
            var addressRequest = new AddAddressRequestDto(Country.GBR, "London", "Somewhere 2", null, "ASD ASXC2");
            var customerRequest = new AddCustomerRequestDto("John", "Pork", "02355876", "asdas@gmail.com", addressRequest);
            var itemsRequest = new List<AddTeaItemRequestDto?>()
            {
                new AddTeaItemRequestDto(Guid.NewGuid(), 3, 0)
            };
            var orderDetailsRequest = new AddOrderDetailsRequestDto(
                itemsRequest, DateTime.UtcNow, addressRequest, 0, Status.New
                );
            var orderRequest = new AddOrderRequestDto(customerRequest, orderDetailsRequest);
            var command = new AddOrderCommand(orderRequest);

            _teaRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync(new Entities.Tea());

            var handler = new AddOrderCommandHandler(
                _orderRepositoryMock.Object,
                _teaRepositoryMock.Object,
                _mapper,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            result.Should().BeEquivalentTo(Result.Ok());
        }

        [Fact]
        public async Task Handle_Should_CallAddAsync_When_ValidationIsSuccessfull()
        {
            // Arrange
            var addressRequest = new AddAddressRequestDto(Country.GBR, "London", "Somewhere 2", null, "ASD ASXC2");
            var customerRequest = new AddCustomerRequestDto("John", "Pork", "02355876", "asdas@gmail.com", addressRequest);
            var itemsRequest = new List<AddTeaItemRequestDto?>()
            {
                new AddTeaItemRequestDto(Guid.NewGuid(), 3, 0)
            };
            var orderDetailsRequest = new AddOrderDetailsRequestDto(
                itemsRequest, DateTime.UtcNow, addressRequest, 0, Status.New
                );
            var orderRequest = new AddOrderRequestDto(customerRequest, orderDetailsRequest);
            var command = new AddOrderCommand(orderRequest);

            _teaRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync(new Entities.Tea());

            var handler = new AddOrderCommandHandler(
                _orderRepositoryMock.Object,
                _teaRepositoryMock.Object,
                _mapper,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            _orderRepositoryMock.Verify(
                x => x.AddAsync(
                    It.IsAny<Entities.Order>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_Should_NotCallUnitOfWork_When_ValidationFails()
        {
            // Arrange
            var addressRequest = new AddAddressRequestDto(Country.GBR, "", "Somewhere 2", null, "ASD ASXC2");
            var customerRequest = new AddCustomerRequestDto("John", "", "", "asdas@gmail.com", addressRequest);
            var itemsRequest = new List<AddTeaItemRequestDto?>()
            {
                new AddTeaItemRequestDto(Guid.NewGuid(), null, 0)
            };
            var orderDetailsRequest = new AddOrderDetailsRequestDto(
                itemsRequest, DateTime.UtcNow, addressRequest, 0, Status.New
                );
            var orderRequest = new AddOrderRequestDto(customerRequest, orderDetailsRequest);
            var command = new AddOrderCommand(orderRequest);

            _teaRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync(new Entities.Tea());

            var handler = new AddOrderCommandHandler(
                _orderRepositoryMock.Object,
                _teaRepositoryMock.Object,
                _mapper,
                _unitOfWorkMock.Object);

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
