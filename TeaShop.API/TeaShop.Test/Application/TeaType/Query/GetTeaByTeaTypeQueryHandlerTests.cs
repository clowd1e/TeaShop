using AutoMapper;
using FluentAssertions;
using Moq;
using TeaShop.Application.DTOs.Tea.Response;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Application.Service.TeaType.Query.GetTeaByTeaType;
using TeaShop.Domain.Repository;
using TeaShop.Test.Configuration;
using Entities = TeaShop.Domain.Entities;

namespace TeaShop.Test.Application.TeaType.Query
{
    public sealed class GetTeaByTeaTypeQueryHandlerTests
    {
        private readonly Mock<ITeaTypeRepository> _teaTypeRepositoryMock;
        private readonly IMapper _mapper;
        private readonly List<Entities.Tea> tea =
        [
            new Entities.Tea() { CreatedAt = DateTime.UtcNow, CreatedBy = "abanent", Id = Guid.NewGuid(), Name = "Black Tea", Description = "Definitely Black Tea.", IsInStock = true, TeaTypeId = new Guid("00000000-0000-0000-0000-000000000001"), AvailableStock = 21, Price = 32.4m },
            new Entities.Tea() { CreatedAt = DateTime.UtcNow, CreatedBy = "abanent", Id = Guid.NewGuid(), Name = "Black Tea", Description = "Could be Black Tea.", IsInStock = false, TeaTypeId = new Guid("00000000-0000-0000-0000-000000000001"), AvailableStock = 0, Price = 12.56m }
        ];
        private readonly IEnumerable<TeaResponseDto> teaMap;

        public GetTeaByTeaTypeQueryHandlerTests()
        {
            _teaTypeRepositoryMock = new();
            _mapper = AutoMapperConfiguration.GetMapper();
            teaMap = _mapper.Map<IEnumerable<TeaResponseDto>>(tea);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailResult_When_TeaTypeNotFound()
        {
            // Arrange
            var query = new GetTeaByTeaTypeQuery(Guid.NewGuid());

            _teaTypeRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync((Entities.TeaType)null);

            _teaTypeRepositoryMock.Setup(
                x => x.GetTeaByTeaTypeAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync(tea);

            var handler = new GetTeaByTeaTypeQueryHandler(
                _teaTypeRepositoryMock.Object,
                _mapper);

            // Act
            Result<IEnumerable<TeaResponseDto>> result = await handler.Handle(query, default);

            // Assert
            result.Should().BeEquivalentTo(Result<IEnumerable<TeaResponseDto>>.Fail(TeaTypeErrors.TeaTypeNotFound));
        }

        [Fact]
        public async Task Handle_Should_ReturnFailResult_When_TeaByTeaTypeNotFound()
        {
            // Arrange
            var query = new GetTeaByTeaTypeQuery(Guid.NewGuid());

            _teaTypeRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync(new Entities.TeaType());

            _teaTypeRepositoryMock.Setup(
                x => x.GetTeaByTeaTypeAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync((IEnumerable<Entities.Tea>)null);

            var handler = new GetTeaByTeaTypeQueryHandler(
                _teaTypeRepositoryMock.Object,
                _mapper);

            // Act
            Result<IEnumerable<TeaResponseDto>> result = await handler.Handle(query, default);

            // Assert
            result.Should().BeEquivalentTo(Result<IEnumerable<TeaResponseDto>>.Fail(TeaTypeErrors.TeaByTeaTypeNotFound));
        }

        [Fact]
        public async Task Handle_Should_ReturnTeaByTeaType_When_TeaByTeaTypeFound()
        {
            // Arrange
            var query = new GetTeaByTeaTypeQuery(Guid.NewGuid());

            _teaTypeRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync(new Entities.TeaType());

            _teaTypeRepositoryMock.Setup(
                x => x.GetTeaByTeaTypeAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync(tea);

            var handler = new GetTeaByTeaTypeQueryHandler(
                _teaTypeRepositoryMock.Object,
                _mapper);

            // Act
            Result<IEnumerable<TeaResponseDto>> result = await handler.Handle(query, default);

            // Assert
            result.Should().BeEquivalentTo(Result<IEnumerable<TeaResponseDto>>.Ok(teaMap));
        }

        [Fact]
        public async Task Handle_Should_CallGetTeaByTeaTypeAsync_When_TeaByTeaTypeFound()
        {
            // Arrange
            var query = new GetTeaByTeaTypeQuery(Guid.NewGuid());

            _teaTypeRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync(new Entities.TeaType());

            _teaTypeRepositoryMock.Setup(
                x => x.GetTeaByTeaTypeAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync(tea);

            var handler = new GetTeaByTeaTypeQueryHandler(
                _teaTypeRepositoryMock.Object,
                _mapper);

            // Act
            Result<IEnumerable<TeaResponseDto>> result = await handler.Handle(query, default);

            // Assert
            _teaTypeRepositoryMock.Verify(
                x => x.GetTeaByTeaTypeAsync(
                    It.IsAny<Guid>()), 
                Times.Once);
        }
    }
}
