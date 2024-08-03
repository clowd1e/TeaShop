using AutoMapper;
using FluentAssertions;
using Moq;
using TeaShop.Application.Data;
using TeaShop.Application.DTOs.Tea.Request.Add;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Application.Service.Tea.Command.AddTea;
using TeaShop.Domain.Repository;
using TeaShop.Test.Configuration;
using Entities = TeaShop.Domain.Entities;

namespace TeaShop.Test.Application.Tea.Command
{
    public sealed class AddTeaCommandHandlerTests
    {
        private readonly Mock<ITeaRepository> _teaRepositoryMock;
        private readonly Mock<ITeaTypeRepository> _teaTypeRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;

        public AddTeaCommandHandlerTests()
        {
            _teaRepositoryMock = new();
            _teaTypeRepositoryMock = new();
            _unitOfWorkMock = new();
            _mapper = AutoMapperConfiguration.GetMapper();
        }

        [Fact]
        public async Task Handle_Should_ReturnFailResult_When_ValidationFails()
        {
            // Arrange
            var request = new AddTeaRequestDto("abanent", DateTime.UtcNow, "B", "tea", 32m, null, 123, null);
            var command = new AddTeaCommand(request);

            var handler = new AddTeaCommandHandler(
                _teaRepositoryMock.Object,
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
        public async Task Handle_Should_ReturnFailResult_When_TeaTypeNotFound()
        {
            // Arrange
            var request = new AddTeaRequestDto("abanent", DateTime.UtcNow, "Black Tea", "Definitely Black Tea.", 32m, true, 123, Guid.NewGuid());
            var command = new AddTeaCommand(request);

            _teaTypeRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync((Entities.TeaType)null);

            var handler = new AddTeaCommandHandler(
                _teaRepositoryMock.Object,
                _teaTypeRepositoryMock.Object,
                _mapper,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Errors.ToList()[0].Should().Be(TeaTypeErrors.TeaTypeNotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailResult_When_TeaAlreadyExists()
        {
            // Arrange
            var request = new AddTeaRequestDto("abanent", DateTime.UtcNow, "Black Tea", "Definitely Black Tea.", 32m, true, 123, Guid.NewGuid());
            var command = new AddTeaCommand(request);

            _teaTypeRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync(new Entities.TeaType());

            _teaRepositoryMock.Setup(
                x => x.ExistsAsync(
                    It.IsAny<Entities.Tea>()))
                .ReturnsAsync(true);

            var handler = new AddTeaCommandHandler(
                _teaRepositoryMock.Object,
                _teaTypeRepositoryMock.Object,
                _mapper,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Errors.ToList()[0].Should().Be(TeaErrors.TeaAlreadyExists);
        }

        [Fact]
        public async Task Handle_Should_ReturnOkResult_When_TeaIsAdded()
        {
            // Arrange
            var request = new AddTeaRequestDto("abanent", DateTime.UtcNow, "Black Tea", "Definitely Black Tea.", 32m, true, 123, Guid.NewGuid());
            var command = new AddTeaCommand(request);

            _teaTypeRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync(new Entities.TeaType());

            _teaRepositoryMock.Setup(
                x => x.ExistsAsync(
                    It.IsAny<Entities.Tea>()))
                .ReturnsAsync(false);

            var handler = new AddTeaCommandHandler(
                _teaRepositoryMock.Object,
                _teaTypeRepositoryMock.Object,
                _mapper,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            result.Should().BeEquivalentTo(Result.Ok());
        }

        [Fact]
        public async Task Handle_Should_CallAddAsync_When_ValidationIsSuccessfull()
        {
            // Arrange
            var request = new AddTeaRequestDto("abanent", DateTime.UtcNow, "Black Tea", "Definitely Black Tea.", 32m, true, 123, Guid.NewGuid());
            var command = new AddTeaCommand(request);

            _teaTypeRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync(new Entities.TeaType());

            _teaRepositoryMock.Setup(
                x => x.ExistsAsync(
                    It.IsAny<Entities.Tea>()))
                .ReturnsAsync(false);

            var handler = new AddTeaCommandHandler(
                _teaRepositoryMock.Object,
                _teaTypeRepositoryMock.Object,
                _mapper,
                _unitOfWorkMock.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            _teaRepositoryMock.Verify(
                x => x.AddAsync(
                    It.IsAny<Entities.Tea>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_Should_NotCallUnitOfWork_When_ValidationFails()
        {
            // Arrange
            var request = new AddTeaRequestDto("abanent", DateTime.UtcNow, "B", "tea", 32m, true, 123, Guid.NewGuid());
            var command = new AddTeaCommand(request);

            var handler = new AddTeaCommandHandler(
                _teaRepositoryMock.Object,
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
