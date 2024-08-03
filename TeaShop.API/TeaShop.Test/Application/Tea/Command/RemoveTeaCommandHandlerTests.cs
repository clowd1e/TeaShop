using FluentAssertions;
using Moq;
using TeaShop.Application.Data;
using TeaShop.Application.DTOs.Tea.Request.Remove;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Application.Service.Tea.Command.RemoveTea;
using TeaShop.Domain.Repository;
using Entities = TeaShop.Domain.Entities;

namespace TeaShop.Test.Application.Tea.Command
{
    public sealed class RemoveTeaCommandHandlerTests
    {
        private readonly Mock<ITeaRepository> _teaRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public RemoveTeaCommandHandlerTests()
        {
            _teaRepositoryMock = new();
            _unitOfWorkMock = new();
        }

        [Fact]
        public async Task Handle_Should_ReturnFailResult_When_ValidationFails()
        {
            // Arrange
            var request = new RemoveTeaRequestDto(null);
            var command = new RemoveTeaCommand(request);

            var handler = new RemoveTeaCommandHandler(
                _teaRepositoryMock.Object,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Errors.ToList().ForEach(x => x.Code.Should().Be("Validation"));
        }

        [Fact]
        public async Task Handle_Should_ReturnFailResult_When_TeaNotFound()
        {
            // Arrange
            var request = new RemoveTeaRequestDto(Guid.NewGuid());
            var command = new RemoveTeaCommand(request);

            _teaRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync((Entities.Tea)null);

            var handler = new RemoveTeaCommandHandler(
                _teaRepositoryMock.Object,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Errors.ToList()[0].Should().Be(TeaErrors.TeaNotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnOkResult_When_TeaDeleted()
        {
            // Arrange
            var request = new RemoveTeaRequestDto(Guid.NewGuid());
            var command = new RemoveTeaCommand(request);

            _teaRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync(new Entities.Tea());

            var handler = new RemoveTeaCommandHandler(
                _teaRepositoryMock.Object,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            result.Should().BeEquivalentTo(Result.Ok());
        }

        [Fact]
        public async Task Handle_Should_CallDeleteAsync_When_TeaDeleted()
        {
            // Arrange
            var request = new RemoveTeaRequestDto(Guid.NewGuid());
            var command = new RemoveTeaCommand(request);

            var tea = new Entities.Tea();

            _teaRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync(tea);

            var handler = new RemoveTeaCommandHandler(
                _teaRepositoryMock.Object,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            _teaRepositoryMock.Verify(
                x => x.DeleteAsync(tea),
                Times.Once);
        }

        [Fact]
        public async Task Handle_Should_NotCallUnitOfWork_When_ValidationFails()
        {
            // Arrange
            var request = new RemoveTeaRequestDto(null);
            var command = new RemoveTeaCommand(request);

            var tea = new Entities.Tea();

            _teaRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync(tea);

            var handler = new RemoveTeaCommandHandler(
                _teaRepositoryMock.Object,
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
