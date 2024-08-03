using AutoMapper;
using FluentAssertions;
using Moq;
using TeaShop.Application.DTOs.Tea.Response;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Application.Service.Tea.Query.GetAllTea;
using TeaShop.Domain.Repository;
using TeaShop.Test.Configuration;
using Entities = TeaShop.Domain.Entities;

namespace TeaShop.Test.Application.Tea.Query
{
    public sealed class GetAllTeaQueryHandlerTests
    {
        private readonly Mock<ITeaRepository> _repositoryMock;
        private readonly IMapper _mapper;
        private readonly List<Entities.Tea> tea = 
        [
            new Entities.Tea() { CreatedAt = DateTime.UtcNow, CreatedBy = "abanent", Id = Guid.NewGuid(), Name = "Black Tea", Description = "Definitely Black Tea." },
            new Entities.Tea() { CreatedAt = DateTime.UtcNow, CreatedBy = "abanent", Id = Guid.NewGuid(), Name = "White Tea", Description = "Definitely White Tea." },
            new Entities.Tea() { CreatedAt = DateTime.UtcNow, CreatedBy = "abanent", Id = Guid.NewGuid(), Name = "Green Tea", Description = "Definitely Green Tea." }
        ];
        private readonly IEnumerable<TeaResponseDto> teaMap;

        public GetAllTeaQueryHandlerTests()
        {
            _repositoryMock = new Mock<ITeaRepository>();
            _mapper = AutoMapperConfiguration.GetMapper();
            teaMap = _mapper.Map<IEnumerable<TeaResponseDto>>(tea);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailResult_When_TeaNotFound()
        {
            // Arrange
            _repositoryMock.Setup(
                    x => x.GetAllAsync())
                .ReturnsAsync((List<Entities.Tea>)null);

            var handler = new GetAllTeaQueryHandler(
                _repositoryMock.Object,
                _mapper);

            // Act
            Result<IEnumerable<TeaResponseDto>> result = await handler.Handle(new GetAllTeaQuery(), default);

            // Assert
            result.Should().BeEquivalentTo(Result<IEnumerable<TeaResponseDto>>.Fail(TeaErrors.TeaNotFound));
        }

        [Fact]
        public async Task Handle_Should_ReturnOkResult_When_TeaFound()
        {
            // Arrange
            _repositoryMock.Setup(
                    x => x.GetAllAsync())
                .ReturnsAsync(tea);

            var handler = new GetAllTeaQueryHandler(
                _repositoryMock.Object,
                _mapper);

            // Act
            Result<IEnumerable<TeaResponseDto>> result = await handler.Handle(new GetAllTeaQuery(), default);

            // Assert
            result.Should().BeEquivalentTo(Result<IEnumerable<TeaResponseDto>>.Ok(teaMap));
        }

        [Fact]
        public async Task Handle_Should_CallGetAllAsync_When_TeaFound()
        {
            // Arrange
            _repositoryMock.Setup(
                    x => x.GetAllAsync())
                .ReturnsAsync(tea);

            var handler = new GetAllTeaQueryHandler(
                _repositoryMock.Object,
                _mapper);

            // Act
            Result<IEnumerable<TeaResponseDto>> result = await handler.Handle(new GetAllTeaQuery(), default);

            // Assert
            _repositoryMock.Verify(
                x => x.GetAllAsync(),
                Times.Once);
        }
    }
}
