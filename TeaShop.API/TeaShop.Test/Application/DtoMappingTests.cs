using AutoMapper;
using FluentAssertions;
using TeaShop.Application.DTOs.Address.Request.Add;
using TeaShop.Application.DTOs.Address.Response;
using TeaShop.Application.Mapper;
using TeaShop.Domain.Enums;
using TeaShop.Domain.ValueObjects;
using TeaShop.Test.Configuration;

namespace TeaShop.Test.Application
{
    public sealed class DtoMappingTests
    {
        private readonly IMapper _mapper;

        public DtoMappingTests()
        {
            _mapper = AutoMapperConfiguration.GetMapper();
        }

        [Fact]
        public void Mapper_Should_Convert_AddAddressRequestDto_To_Address()
        {
            // Arrange
            var addAddressRequestDto = new AddAddressRequestDto(
                Country.GBR, "London", "Somewhere 32", 1, "ACD FS32");

            // Act
            var address = _mapper.Map<Address>(addAddressRequestDto);

            // Assert

            address.Should().BeOfType<Address>();
            address.Country.Should().Be(addAddressRequestDto.Country);
            address.City.Should().Be(addAddressRequestDto.City);
            address.Street.Should().Be(addAddressRequestDto.Street);
            address.HouseNumber.Should().Be(addAddressRequestDto.HouseNumber);
            address.PostalCode.Should().Be(addAddressRequestDto.PostalCode);
        }

        [Fact]
        public void Mapper_Should_Convert_Address_To_AddressResponseDto()
        {
            // Arrange
            var address = new Address()
            {
                Country = Country.GBR,
                City = "London",
                Street = "Somewhere 32",
                HouseNumber = 1,
                PostalCode = "ACD FS32"
            };

            // Act
            var addressResponseDto = _mapper.Map<AddressResponseDto>(address);

            // Assert
            addressResponseDto.Should().BeOfType<AddressResponseDto>();
            addressResponseDto.Country.Should().Be(address.Country);
            addressResponseDto.City.Should().Be(address.City);
            addressResponseDto.Street.Should().Be(address.Street);
            addressResponseDto.HouseNumber.Should().Be(address.HouseNumber);
            addressResponseDto.PostalCode.Should().Be(address.PostalCode);
        }
    }
}
