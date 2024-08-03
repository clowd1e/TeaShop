using AutoMapper;
using FluentAssertions;
using Moq;
using TeaShop.Application.DTOs.Customer.Response;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Application.Service.Customer.Query.GetAllCustomers;
using TeaShop.Domain.Enums;
using TeaShop.Domain.Repository;
using TeaShop.Domain.ValueObjects;
using TeaShop.Test.Configuration;
using Entities = TeaShop.Domain.Entities;

namespace TeaShop.Test.Application.Customer.Query
{
    public sealed class GetAllCustomersQueryHandlerTests
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly IMapper _mapper;
        private readonly List<Entities.Customer> customers =
        [
            new Entities.Customer() { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Email = "", Phone = "", Address = new Address() { Country = Country.GBR, City = "London", Street = "Somewhere 65", PostalCode = "ASDASaSD" } },
            new Entities.Customer() { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Doe", Email = "", Phone = "", Address = new Address() { Country = Country.GBR, City = "London", Street = "Somewhere 43", PostalCode = "ASDASaSD" } },
            new Entities.Customer() { Id = Guid.NewGuid(), FirstName = "Oleksandr", LastName = "Smith", Email = "", Phone = "", Address = new Address() { Country = Country.GBR, City = "London", Street = "Somewhere 23", PostalCode = "ASDASaSD" } },
            new Entities.Customer() { Id = Guid.NewGuid(), FirstName = "John", LastName = "Pork", Email = "", Phone = "", Address = new Address() { Country = Country.GBR, City = "London", Street = "Somewhere 78", PostalCode = "ASDASaSD" } },
        ];
        private readonly IEnumerable<CustomerResponseDto> customersMap;

        public GetAllCustomersQueryHandlerTests()
        {
            _customerRepositoryMock = new();
            _mapper = AutoMapperConfiguration.GetMapper();
            customersMap = _mapper.Map<IEnumerable<CustomerResponseDto>>(customers);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailResult_When_CustomersNotFound()
        {
            // Arrange
            _customerRepositoryMock.Setup(
                    x => x.GetAllAsync())
                .ReturnsAsync((List<Entities.Customer>)null);

            var handler = new GetAllCustomersQueryHandler(
                _customerRepositoryMock.Object,
                _mapper);

            // Act
            Result<IEnumerable<CustomerResponseDto>> result = await handler.Handle(new GetAllCustomersQuery(), default);

            // Assert
            result.Should().BeEquivalentTo(Result<IEnumerable<CustomerResponseDto>>.Fail(CustomerErrors.CustomersNotFound));
        }

        [Fact]
        public async Task Handle_Should_ReturnAllCustomers_When_CustomersFound()
        {
            // Arrange
            _customerRepositoryMock.Setup(
                    x => x.GetAllAsync())
                .ReturnsAsync(customers);

            var handler = new GetAllCustomersQueryHandler(
                _customerRepositoryMock.Object,
                _mapper);

            // Act
            Result<IEnumerable<CustomerResponseDto>> result = await handler.Handle(new GetAllCustomersQuery(), default);

            // Assert
            result.Should().BeEquivalentTo(Result<IEnumerable<CustomerResponseDto>>.Ok(customersMap));
        }

        [Fact]
        public async Task Handle_Should_CallGetAllAsync_When_CustomersFound()
        {
            // Arrange
            _customerRepositoryMock.Setup(
                                   x => x.GetAllAsync())
                .ReturnsAsync(customers);

            var handler = new GetAllCustomersQueryHandler(
                _customerRepositoryMock.Object,
                _mapper);

            // Act
            Result<IEnumerable<CustomerResponseDto>> result = await handler.Handle(new GetAllCustomersQuery(), default);

            // Assert
            _customerRepositoryMock.Verify(
                x => x.GetAllAsync(), 
                Times.Once);
        }
    }
}
