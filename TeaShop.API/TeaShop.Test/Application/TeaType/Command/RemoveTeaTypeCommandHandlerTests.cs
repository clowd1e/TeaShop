using FluentAssertions;
using Moq;
using TeaShop.Application.Data;
using TeaShop.Application.DTOs.TeaType.Request.Remove;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Application.Service.TeaType.Command.RemoveTeaType;
using TeaShop.Domain.Repository;
using Entities = TeaShop.Domain.Entities;

namespace TeaShop.Test.Application.TeaType.Command
{
    public sealed class RemoveTeaTypeCommandHandlerTests
    {
        private readonly Mock<ITeaTypeRepository> _teaTypeRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public RemoveTeaTypeCommandHandlerTests()
        {
            _teaTypeRepositoryMock = new();
            _unitOfWorkMock = new();
        }

        [Fact]
        public async Task Handle_Should_ReturnFailResult_When_ValidationFails()
        {
            // Arrange
            var request = new RemoveTeaTypeRequestDto(null);
            var command = new RemoveTeaTypeCommand(request);

            var handler = new RemoveTeaTypeCommandHandler(
                _teaTypeRepositoryMock.Object,
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
            var request = new RemoveTeaTypeRequestDto(Guid.NewGuid());
            var command = new RemoveTeaTypeCommand(request);

            _teaTypeRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync((Entities.TeaType)null);

            var handler = new RemoveTeaTypeCommandHandler(
                _teaTypeRepositoryMock.Object,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            result.IsFailure.Should().Be(true);
            result.Errors.ToList()[0].Should().Be(TeaTypeErrors.TeaTypeNotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnOkResult_When_TeaTypeRemoved()
        {
            // Arrange
            var request = new RemoveTeaTypeRequestDto(Guid.NewGuid());
            var command = new RemoveTeaTypeCommand(request);

            _teaTypeRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync(new Entities.TeaType());

            var handler = new RemoveTeaTypeCommandHandler(
                _teaTypeRepositoryMock.Object,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            result.Should().BeEquivalentTo(Result.Ok());
        }

        [Fact]
        public async Task Handle_Should_CallDeleteAsync_When_ValidationIsSuccessfull_And_TeaTypeIsUnique()
        {
            // Arrange
            var request = new RemoveTeaTypeRequestDto(Guid.NewGuid());
            var command = new RemoveTeaTypeCommand(request);

            _teaTypeRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync(new Entities.TeaType());

            var handler = new RemoveTeaTypeCommandHandler(
                _teaTypeRepositoryMock.Object,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            _teaTypeRepositoryMock.Verify(
                x => x.DeleteAsync(
                    It.IsAny<Entities.TeaType>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_Should_NotCallUnitOfWork_When_ValidationFails()
        {
            // Arrange
            var request = new RemoveTeaTypeRequestDto(null);
            var command = new RemoveTeaTypeCommand(request);

            var handler = new RemoveTeaTypeCommandHandler(
                _teaTypeRepositoryMock.Object,
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
