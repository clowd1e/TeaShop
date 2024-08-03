using AutoMapper;
using FluentAssertions;
using Moq;
using TeaShop.Application.Data;
using TeaShop.Application.DTOs.Address.Request.Add;
using TeaShop.Application.DTOs.Customer.Request.Add;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Application.Service.Customer.Command.AddCustomer;
using TeaShop.Domain.Enums;
using TeaShop.Domain.Repository;
using TeaShop.Test.Configuration;
using Entities = TeaShop.Domain.Entities;

namespace TeaShop.Test.Application.Customer.Command
{
    public sealed class AddCustomerCommandHandlerTests
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;

        public AddCustomerCommandHandlerTests()
        {
            _customerRepositoryMock = new();
            _unitOfWorkMock = new();
            _mapper = AutoMapperConfiguration.GetMapper();
        }

        [Fact]
        public async Task Handle_Should_ReturnFailResult_When_ValidationFails()
        {
            // Arrange
            var addressRequest = new AddAddressRequestDto(Country.GBR, "", "Ohio 21", null, "ASD CV32");
            var request = new AddCustomerRequestDto("John", "", "0234534546", "asdasda@adfs.com", addressRequest);
            var command = new AddCustomerCommand(request);

            var handler = new AddCustomerCommandHandler(
                _customerRepositoryMock.Object,
                _mapper,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Errors.ToList().ForEach(x => x.Code.Should().Be("Validation"));
        }

        [Fact]
        public async Task Handle_Should_ReturnFailResult_When_CustomerAlreadyExists()
        {
            // Arrange
            var addressRequest = new AddAddressRequestDto(Country.GBR, "London", "Ohio 21", null, "ASD CV32");
            var request = new AddCustomerRequestDto("John", "Doe", "0234534546", "asdad@gmail.com", addressRequest);
            var command = new AddCustomerCommand(request);

            _customerRepositoryMock.Setup(
                x => x.ExistsAsync(
                    It.IsAny<Entities.Customer>()))
                .ReturnsAsync(true);

            var handler = new AddCustomerCommandHandler(
                _customerRepositoryMock.Object,
                _mapper,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Errors.ToList()[0].Should().Be(CustomerErrors.CustomerAlreadyExists);
        }

        [Fact]
        public async Task Handle_Should_ReturnOkResult_When_CustomerAdded()
        {
            // Arrange
            var addressRequest = new AddAddressRequestDto(Country.GBR, "London", "Ohio 21", null, "ASD CV32");
            var request = new AddCustomerRequestDto("John", "Doe", "0234534546", "asfs@gmail.com", addressRequest);
            var command = new AddCustomerCommand(request);

            _customerRepositoryMock.Setup(
                x => x.ExistsAsync(
                    It.IsAny<Entities.Customer>()))
                .ReturnsAsync(false);

            var handler = new AddCustomerCommandHandler(
                _customerRepositoryMock.Object,
                _mapper,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            result.Should().BeEquivalentTo(Result.Ok());
        }

        [Fact]
        public async Task Handle_Should_CallAddAsync_When_ValidationIsSuccessfull_And_CustomerIsUnique()
        {
            // Arrange
            var addressRequest = new AddAddressRequestDto(Country.GBR, "London", "Ohio 21", null, "ASD CV32");
            var request = new AddCustomerRequestDto("John", "Doe", "0234534546", "asfs@gmail.com", addressRequest);
            var command = new AddCustomerCommand(request);

            _customerRepositoryMock.Setup(
                x => x.ExistsAsync(
                    It.IsAny<Entities.Customer>()))
                .ReturnsAsync(false);

            var handler = new AddCustomerCommandHandler(
                _customerRepositoryMock.Object,
                _mapper,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            _customerRepositoryMock.Verify(
                x => x.AddAsync(
                    It.IsAny<Entities.Customer>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_Should_NotCallUnitOfWork_When_ValidationFails()
        {
            var addressRequest = new AddAddressRequestDto(Country.GBR, "", "Ohio 21", null, "ASD CV32");
            var request = new AddCustomerRequestDto("John", "", "13243564", "", addressRequest);
            var command = new AddCustomerCommand(request);

            _customerRepositoryMock.Setup(
                x => x.ExistsAsync(
                    It.IsAny<Entities.Customer>()))
                .ReturnsAsync(false);

            var handler = new AddCustomerCommandHandler(
                _customerRepositoryMock.Object,
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
