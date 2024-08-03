using AutoMapper;
using FluentAssertions;
using Moq;
using TeaShop.Application.Comparison;
using TeaShop.Application.Data;
using TeaShop.Application.DTOs.TeaType.Request.Update;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Application.Service.TeaType.Command.UpdateTeaType;
using TeaShop.Domain.Repository;
using TeaShop.Test.Configuration;
using Entities = TeaShop.Domain.Entities;

namespace TeaShop.Test.Application.TeaType.Command
{
    public sealed class UpdateTeaTypeCommandHandlerTests
    {
        private readonly Mock<ITeaTypeRepository> _teaTypeRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;

        public UpdateTeaTypeCommandHandlerTests()
        {
            _teaTypeRepositoryMock = new();
            _unitOfWorkMock = new();
            _mapper = AutoMapperConfiguration.GetMapper();

            Entities.TeaType.ComparisonDelegate = ComparisonExtensions.TeaTypeDefaultComparison;
        }

        [Fact]
        public async Task Handle_Should_ReturnFailResult_When_ValidationFails()
        {
            // Arrange
            var request = new UpdateTeaTypeRequestDto("abanent", DateTime.UtcNow, "Black tea", "Black tea, Black tea, Black tea, Black tea.");
            var command = new UpdateTeaTypeCommand(null, request);

            var handler = new UpdateTeaTypeCommandHandler(
                _teaTypeRepositoryMock.Object,
                _mapper,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            result.IsFailure.Should().Be(true);
            result.Errors.ToList().ForEach(x => x.Code.Should().Be("Validation"));
        }

        [Fact]
        public async Task Handle_Should_ReturnFailResult_When_TeaTypeNotFound()
        {
            // Arrange
            var request = new UpdateTeaTypeRequestDto("abanent", DateTime.UtcNow, "Black Tea", "Definitely Black Tea.");
            var command = new UpdateTeaTypeCommand(Guid.NewGuid(), request);

            _teaTypeRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync((Entities.TeaType)null);

            var handler = new UpdateTeaTypeCommandHandler(
                _teaTypeRepositoryMock.Object,
                _mapper,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            result.IsFailure.Should().Be(true);
            result.Errors.ToList()[0].Should().Be(TeaTypeErrors.TeaTypeNotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnOkResult_When_TeaTypeUpdated()
        {
            // Arrange
            var request = new UpdateTeaTypeRequestDto("abanent", DateTime.UtcNow, "Black Tea", "Definitely Black Tea.");
            var command = new UpdateTeaTypeCommand(Guid.NewGuid(), request);

            _teaTypeRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync(new Entities.TeaType());

            var handler = new UpdateTeaTypeCommandHandler(
                _teaTypeRepositoryMock.Object,
                _mapper,
                _unitOfWorkMock.Object);
            
            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            result.Should().BeEquivalentTo(Result.Ok());
        }

        [Fact]
        public async Task Handle_Should_CallUpdateAsync_When_TeaTypeUpdated()
        {
            // Arrange
            var request = new UpdateTeaTypeRequestDto("abanent", DateTime.UtcNow, "Black Tea", "Definitely Black Tea.");
            var command = new UpdateTeaTypeCommand(Guid.NewGuid(), request);

            _teaTypeRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync(new Entities.TeaType());

            var handler = new UpdateTeaTypeCommandHandler(
                _teaTypeRepositoryMock.Object,
                _mapper,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            _teaTypeRepositoryMock.Verify(
                x => x.UpdateAsync(
                    It.IsAny<Entities.TeaType>(),
                    It.IsAny<Entities.TeaType>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_Should_NotCallUnitOfWork_When_ValidationFails()
        {
            // Arrange
            var request = new UpdateTeaTypeRequestDto("abanent", DateTime.UtcNow, "Black Tea", "Definitely Black Tea.");
            var command = new UpdateTeaTypeCommand(null, request);

            var handler = new UpdateTeaTypeCommandHandler(
                _teaTypeRepositoryMock.Object,
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
