using AutoMapper;
using FluentAssertions;
using Moq;
using TeaShop.Application.DTOs.TeaType.Response;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Application.Service.TeaType.Query.GetTeaTypeById;
using TeaShop.Domain.Repository;
using TeaShop.Test.Configuration;
using Entities = TeaShop.Domain.Entities;

namespace TeaShop.Test.Application.TeaType.Query
{
    public sealed class GetTeaTypeByIdQueryHandlerTests
    {
        private readonly Mock<ITeaTypeRepository> _teaTypeRepositoryMock;
        private readonly IMapper _mapper;

        public GetTeaTypeByIdQueryHandlerTests()
        {
            _teaTypeRepositoryMock = new();
            _mapper = AutoMapperConfiguration.GetMapper();
        }

        [Fact]
        public async Task Handle_Should_ReturnFailResult_When_TeaTypeNotFound()
        {
            // Arrange
            var query = new GetTeaTypeByIdQuery(Guid.NewGuid());

            _teaTypeRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync((Entities.TeaType)null);

            var handler = new GetTeaTypeByIdQueryHandler(
                _teaTypeRepositoryMock.Object,
                _mapper);

            // Act
            Result<TeaTypeResponseDto> result = await handler.Handle(query, default);

            // Assert
            result.Should().BeEquivalentTo(Result<TeaTypeResponseDto>.Fail(TeaTypeErrors.TeaTypeNotFound));
        }

        [Fact]
        public async Task Handle_Should_ReturnTeaType_When_TeaTypeFound()
        {
            // Arrange
            var teaType = new Entities.TeaType() { CreatedAt = DateTime.UtcNow, CreatedBy = "abanent", Id = Guid.NewGuid(), Name = "Black Tea", Description = "Definitely Black Tea." };
            var teaTypeMap = _mapper.Map<TeaTypeResponseDto>(teaType);
            var query = new GetTeaTypeByIdQuery(teaType.Id);

            _teaTypeRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync(teaType);

            var handler = new GetTeaTypeByIdQueryHandler(
                _teaTypeRepositoryMock.Object,
                _mapper);

            // Act
            Result<TeaTypeResponseDto> result = await handler.Handle(query, default);

            // Assert
            result.Should().BeEquivalentTo(Result<TeaTypeResponseDto>.Ok(teaTypeMap));
        }

        [Fact]
        public async Task Handle_Should_CallGetByIdAsync_When_TeaTypeFound()
        {
            // Arrange
            var teaType = new Entities.TeaType() { CreatedAt = DateTime.UtcNow, CreatedBy = "abanent", Id = Guid.NewGuid(), Name = "Black Tea", Description = "Definitely Black Tea." };
            var query = new GetTeaTypeByIdQuery(teaType.Id);

            _teaTypeRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync(teaType);

            var handler = new GetTeaTypeByIdQueryHandler(
                _teaTypeRepositoryMock.Object,
                _mapper);

            // Act
            Result<TeaTypeResponseDto> result = await handler.Handle(query, default);

            // Assert
            _teaTypeRepositoryMock.Verify(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()), 
                Times.Once);
        }
    }
}
