using AutoMapper;
using FluentAssertions;
using Moq;
using TeaShop.Application.DTOs.Customer.Response;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Application.Service.Customer.Query.GetCustomerById;
using TeaShop.Domain.Enums;
using TeaShop.Domain.Repository;
using TeaShop.Domain.ValueObjects;
using TeaShop.Test.Configuration;
using Entities = TeaShop.Domain.Entities;

namespace TeaShop.Test.Application.Customer.Query
{
    public sealed class GetCustomerByIdQueryHandlerTests
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly IMapper _mapper;

        public GetCustomerByIdQueryHandlerTests()
        {
            _customerRepositoryMock = new();
            _mapper = AutoMapperConfiguration.GetMapper();
        }

        [Fact]
        public async Task Handle_Should_ReturnFailResult_When_CustomerNotFound()
        {
            // Arrange
            _customerRepositoryMock.Setup(
                    x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Entities.Customer)null);

            var handler = new GetCustomerByIdQueryHandler(
                _customerRepositoryMock.Object,
                _mapper);

            // Act
            Result<CustomerResponseDto> result = await handler.Handle(new GetCustomerByIdQuery(Guid.NewGuid()), default);

            // Assert
            result.Should().BeEquivalentTo(Result<CustomerResponseDto>.Fail(CustomerErrors.CustomerNotFound));
        }

        [Fact]
        public async Task Handle_Should_ReturnCustomer_When_CustomerFound()
        {
            // Arrange
            var customer = new Entities.Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "",
                Phone = "",
                Address = new Address()
                {
                    Country = Country.GBR,
                    City = "London",
                    Street = "Somewhere 65",
                    PostalCode = "ASDASaSD"
                }
            };

            var customerMap = _mapper.Map<CustomerResponseDto>(customer);

            _customerRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync(customer);

            var handler = new GetCustomerByIdQueryHandler(
                _customerRepositoryMock.Object,
                _mapper);

            // Act
            Result<CustomerResponseDto> result = await handler.Handle(new GetCustomerByIdQuery(customer.Id), default);

            // Assert
            result.Should().BeEquivalentTo(Result<CustomerResponseDto>.Ok(customerMap));
        }

        [Fact]
        public async Task Handle_Should_CallGetByIdAsync_When_CustomerFound()
        {
            // Arrange
            var customer = new Entities.Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "",
                Phone = "",
                Address = new Address()
                {
                    Country = Country.GBR,
                    City = "London",
                    Street = "Somewhere 65",
                    PostalCode = "ASDASaSD"
                }
            };

            _customerRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync(customer);

            var handler = new GetCustomerByIdQueryHandler(
                _customerRepositoryMock.Object,
                _mapper);

            // Act
            await handler.Handle(new GetCustomerByIdQuery(customer.Id), default);

            // Assert
            _customerRepositoryMock.Verify(
                x => x.GetByIdAsync(customer.Id),
                Times.Once);
        }
    }
}
