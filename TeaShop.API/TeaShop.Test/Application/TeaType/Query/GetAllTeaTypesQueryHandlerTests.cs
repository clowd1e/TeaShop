using AutoMapper;
using FluentAssertions;
using Moq;
using TeaShop.Application.DTOs.TeaType.Response;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Application.Service.TeaType.Query.GetAllTeaTypes;
using TeaShop.Domain.Repository;
using TeaShop.Test.Configuration;
using Entities = TeaShop.Domain.Entities;

namespace TeaShop.Test.Application.TeaType.Query
{
    public sealed class GetAllTeaTypesQueryHandlerTests
    {
        private readonly Mock<ITeaTypeRepository> _teaTypeRepositoryMock;
        private readonly IMapper _mapper;
        private readonly List<Entities.TeaType> teaTypes =
        [
            new Entities.TeaType() { CreatedAt = DateTime.UtcNow, CreatedBy = "abanent", Id = Guid.NewGuid(), Name = "Black Tea", Description = "Definitely Black Tea." },
            new Entities.TeaType() { CreatedAt = DateTime.UtcNow, CreatedBy = "abanent", Id = Guid.NewGuid(), Name = "White Tea", Description = "Definitely White Tea." },
            new Entities.TeaType() { CreatedAt = DateTime.UtcNow, CreatedBy = "abanent", Id = Guid.NewGuid(), Name = "Green Tea", Description = "Definitely Green Tea." }
        ];
        private readonly IEnumerable<TeaTypeResponseDto> teaTypesMap;

        public GetAllTeaTypesQueryHandlerTests()
        {
            _teaTypeRepositoryMock = new();
            _mapper = AutoMapperConfiguration.GetMapper();
            teaTypesMap = _mapper.Map<IEnumerable<TeaTypeResponseDto>>(teaTypes);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailResult_When_TeaTypesNotFound()
        {
            // Arrange
            _teaTypeRepositoryMock.Setup(
                    x => x.GetAllAsync())
                .ReturnsAsync((List<Entities.TeaType>)null);

            var handler = new GetAllTeaTypesQueryHandler(
                _teaTypeRepositoryMock.Object,
                _mapper);

            // Act
            Result<IEnumerable<TeaTypeResponseDto>> result = await handler.Handle(new GetAllTeaTypesQuery(), default);

            // Assert
            result.Should().BeEquivalentTo(Result<IEnumerable<TeaTypeResponseDto>>.Fail(TeaTypeErrors.TeaTypeNotFound));
        }

        [Fact]
        public async Task Handle_Should_ReturnAllTeaTypes_When_TeaTypesFound()
        {
            // Arrange
            _teaTypeRepositoryMock.Setup(
                    x => x.GetAllAsync())
                .ReturnsAsync(teaTypes);

            var handler = new GetAllTeaTypesQueryHandler(
                _teaTypeRepositoryMock.Object,
                _mapper);

            // Act
            Result<IEnumerable<TeaTypeResponseDto>> result = await handler.Handle(new GetAllTeaTypesQuery(), default);

            // Assert
            result.Should().BeEquivalentTo(Result<IEnumerable<TeaTypeResponseDto>>.Ok(teaTypesMap));
        }

        [Fact]
        public async Task Handle_Should_CallGetAllAsync_When_TeaTypesFound()
        {
            // Arrange
            _teaTypeRepositoryMock.Setup(
                    x => x.GetAllAsync())
                .ReturnsAsync(teaTypes);

            var handler = new GetAllTeaTypesQueryHandler(
                _teaTypeRepositoryMock.Object,
                _mapper);

            // Act
            Result<IEnumerable<TeaTypeResponseDto>> result = await handler.Handle(new GetAllTeaTypesQuery(), default);

            // Assert
            _teaTypeRepositoryMock.Verify(
                x => x.GetAllAsync(),
                Times.Once);
        }
    }
}
