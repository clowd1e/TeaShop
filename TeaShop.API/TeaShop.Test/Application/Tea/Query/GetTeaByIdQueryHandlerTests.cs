using AutoMapper;
using FluentAssertions;
using Moq;
using TeaShop.Application.DTOs.Tea.Response;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Application.Service.Tea.Query.GetTeaById;
using TeaShop.Domain.Repository;
using TeaShop.Test.Configuration;
using Entities = TeaShop.Domain.Entities;

namespace TeaShop.Test.Application.Tea.Query
{
    public sealed class GetTeaByIdQueryHandlerTests
    {
        private readonly Mock<ITeaRepository> _repositoryMock;
        private readonly IMapper _mapper;

        public GetTeaByIdQueryHandlerTests()
        {
            _repositoryMock = new Mock<ITeaRepository>();
            _mapper = AutoMapperConfiguration.GetMapper();
        }

        [Fact]
        public async Task Handle_Should_ReturnFailResult_When_TeaNotFound()
        {
            // Arrange
            _repositoryMock.Setup(
                    x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Entities.Tea)null);

            var handler = new GetTeaByIdQueryHandler(
                _repositoryMock.Object,
                _mapper);

            // Act
            Result<TeaResponseDto> result = await handler.Handle(new GetTeaByIdQuery(Guid.NewGuid()), default);

            // Assert
            result.Should().BeEquivalentTo(Result<TeaResponseDto>.Fail(TeaErrors.TeaNotFound));
        }

        [Fact]
        public async Task Handle_Should_ReturnOkResult_When_TeaFound()
        {
            // Arrange
            var tea = new Entities.Tea() { CreatedAt = DateTime.UtcNow, CreatedBy = "abanent", Id = Guid.NewGuid(), Name = "Black Tea", Description = "Definitely Black Tea.", AvailableStock = 0, IsInStock = false, Price = 5.66m, TeaTypeId = Guid.NewGuid() };
            var teaMap = _mapper.Map<TeaResponseDto>(tea);

            _repositoryMock.Setup(
                    x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(tea);

            var handler = new GetTeaByIdQueryHandler(
                _repositoryMock.Object,
                _mapper);

            // Act
            Result<TeaResponseDto> result = await handler.Handle(new GetTeaByIdQuery(Guid.NewGuid()), default);

            // Assert
            result.Should().BeEquivalentTo(Result<TeaResponseDto>.Ok(teaMap));
        }

        [Fact]
        public async Task Handle_Should_CallGetByIdAsync_When_TeaFound()
        {
            // Arrange
            var tea = new Entities.Tea() { CreatedAt = DateTime.UtcNow, CreatedBy = "abanent", Id = Guid.NewGuid(), Name = "Black Tea", Description = "Definitely Black Tea.", AvailableStock = 0, IsInStock = false, Price = 5.66m, TeaTypeId = Guid.NewGuid() };
            var teaMap = _mapper.Map<TeaResponseDto>(tea);

            _repositoryMock.Setup(
                    x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(tea);

            var handler = new GetTeaByIdQueryHandler(
                _repositoryMock.Object,
                _mapper);

            // Act
            Result<TeaResponseDto> result = await handler.Handle(new GetTeaByIdQuery(Guid.NewGuid()), default);

            // Assert
            _repositoryMock.Verify(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()),
                Times.Once);
        }
    }
}
