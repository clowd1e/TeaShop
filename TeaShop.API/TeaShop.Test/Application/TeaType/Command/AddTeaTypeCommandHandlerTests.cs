using AutoMapper;
using FluentAssertions;
using Moq;
using TeaShop.Application.Data;
using TeaShop.Application.DTOs.TeaType.Request.Add;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Application.Service.TeaType.Command.AddTeaType;
using TeaShop.Domain.Repository;
using TeaShop.Test.Configuration;
using Entities = TeaShop.Domain.Entities;

namespace TeaShop.Test.Application.TeaType.Command
{
    public sealed class AddTeaTypeCommandHandlerTests
    {
        private readonly Mock<ITeaTypeRepository> _teaTypeRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;

        public AddTeaTypeCommandHandlerTests()
        {
            _teaTypeRepositoryMock = new();
            _unitOfWorkMock = new();
            _mapper = AutoMapperConfiguration.GetMapper();
        }

        [Fact]
        public async Task Handle_Should_ReturnFailResult_When_ValidationFails()
        {
            // Arrange
            var request = new AddTeaTypeRequestDto("abanent", DateTime.UtcNow, "B", "tea");
            var command = new AddTeaTypeCommand(request);

            var handler = new AddTeaTypeCommandHandler(
                _teaTypeRepositoryMock.Object,
                _mapper,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Errors.ToList().ForEach(x => x.Code.Should().Be("Validation"));
        }

        [Fact]
        public async Task Handle_Should_ReturnFailResult_When_TeaTypeAlreadyExists()
        {
            // Arrange
            var request = new AddTeaTypeRequestDto("abanent", DateTime.UtcNow, "Black Tea", "Definitely Black Tea.");
            var command = new AddTeaTypeCommand(request);

            _teaTypeRepositoryMock.Setup(
                x => x.ExistsAsync(
                    It.IsAny<Entities.TeaType>()))
                .ReturnsAsync(true);

            var handler = new AddTeaTypeCommandHandler(
                _teaTypeRepositoryMock.Object,
                _mapper,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Errors.ToList()[0].Should().Be(TeaTypeErrors.TeaTypeAlreadyExists);
        }

        [Fact]
        public async Task Handle_Should_ReturnOkResult_When_TeaTypeIsAdded()
        {
            // Arrange
            var request = new AddTeaTypeRequestDto("abanent", DateTime.UtcNow, "White Tea", "White tea is white tea. Wait...");
            var command = new AddTeaTypeCommand(request);

            _teaTypeRepositoryMock.Setup(
                x => x.ExistsAsync(
                    It.IsAny<Entities.TeaType>()))
                .ReturnsAsync(false);

            var handler = new AddTeaTypeCommandHandler(
                _teaTypeRepositoryMock.Object,
                _mapper,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            result.Should().BeEquivalentTo(Result.Ok());
        }

        [Fact]
        public async Task Handle_Should_CallAddAsync_When_ValidationIsSuccessfull_And_TeaTypeIsUnique()
        {
            // Arrange
            var request = new AddTeaTypeRequestDto("abanent", DateTime.UtcNow, "White Tea", "White tea is white tea. Wait...");
            var command = new AddTeaTypeCommand(request);

            _teaTypeRepositoryMock.Setup(
                x => x.ExistsAsync(
                    It.IsAny<Entities.TeaType>()))
                .ReturnsAsync(false);

            var handler = new AddTeaTypeCommandHandler(
                _teaTypeRepositoryMock.Object,
                _mapper,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            _teaTypeRepositoryMock.Verify(
                x => x.AddAsync(
                    It.IsAny<Entities.TeaType>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_Should_NotCallUnitOfWork_When_ValidationFails()
        {
            // Arrange
            var request = new AddTeaTypeRequestDto("abanent", DateTime.UtcNow, "B", "tea");
            var command = new AddTeaTypeCommand(request);

            var handler = new AddTeaTypeCommandHandler(
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
