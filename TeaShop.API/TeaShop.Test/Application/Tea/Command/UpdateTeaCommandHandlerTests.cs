using AutoMapper;
using FluentAssertions;
using Moq;
using TeaShop.Application.Comparison;
using TeaShop.Application.Data;
using TeaShop.Application.DTOs.Tea.Request.Update;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Application.Service.Tea.Command.UpdateTea;
using TeaShop.Domain.Repository;
using TeaShop.Test.Configuration;
using Entities = TeaShop.Domain.Entities;

namespace TeaShop.Test.Application.Tea.Command
{
    public sealed class UpdateTeaCommandHandlerTests
    {
        private readonly Mock<ITeaRepository> _mockTeaRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;

        public UpdateTeaCommandHandlerTests()
        {
            _mockTeaRepository = new();
            _mockUnitOfWork = new();
            _mapper = AutoMapperConfiguration.GetMapper();

            Entities.Tea.ComparisonDelegate = ComparisonExtensions.TeaDefaultComparison;
        }

        [Fact]
        public async Task Handle_Should_ReturnFailResult_When_ValidationFails()
        {
            // Arrange
            var request = new UpdateTeaRequestDto("abanent", DateTime.UtcNow, "Black tea", "", null, null, null, null);
            var command = new UpdateTeaCommand(null, request);

            var handler = new UpdateTeaCommandHandler(
                _mockTeaRepository.Object,
                _mapper,
                _mockUnitOfWork.Object);

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
            var request = new UpdateTeaRequestDto("abanent", DateTime.UtcNow, "Black Tea", "Definitely Black Tea.", 13.3m, true, 3, Guid.NewGuid());
            var command = new UpdateTeaCommand(Guid.NewGuid(), request);

            _mockTeaRepository.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync((Entities.Tea)null);

            var handler = new UpdateTeaCommandHandler(
                _mockTeaRepository.Object,
                _mapper,
                _mockUnitOfWork.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Errors.ToList()[0].Should().Be(TeaErrors.TeaNotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnOkResult_When_TeaUpdated()
        {
            // Arrange
            var request = new UpdateTeaRequestDto("abanent", DateTime.UtcNow, "Black Tea", "Definitely Black Tea.", 13.3m, true, 3, Guid.NewGuid());
            var command = new UpdateTeaCommand(Guid.NewGuid(), request);

            var oldTea = new Entities.Tea() { CreatedBy = "abanent", CreatedAt = DateTime.UtcNow, Name = "Black Tea", Description = "Definitely Black Tea.", Price = 13.3m, IsInStock = true, AvailableStock = 3, TeaTypeId = Guid.NewGuid() };

            _mockTeaRepository.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync(oldTea);

            var handler = new UpdateTeaCommandHandler(
                _mockTeaRepository.Object,
                _mapper,
                _mockUnitOfWork.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            result.Should().BeEquivalentTo(Result.Ok());
        }

        [Fact]
        public async Task Handle_Should_CallUpdateAsync_When_TeaUpdated()
        {
            // Arrange
            var request = new UpdateTeaRequestDto("abanent", DateTime.UtcNow, "Black Tea", "Definitely Black Tea.", 13.3m, true, 3, Guid.NewGuid());
            var command = new UpdateTeaCommand(Guid.NewGuid(), request);

            var oldTea = new Entities.Tea() { CreatedBy = "abanent", CreatedAt = DateTime.UtcNow, Name = "Black Tea", Description = "Definitely Black Tea.", Price = 13.3m, IsInStock = true, AvailableStock = 3, TeaTypeId = Guid.NewGuid() };
            _mockTeaRepository.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<Guid>()))
                .ReturnsAsync(oldTea);

            var handler = new UpdateTeaCommandHandler(
                _mockTeaRepository.Object,
                _mapper,
                _mockUnitOfWork.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            _mockTeaRepository.Verify(
                x => x.UpdateAsync(
                    It.IsAny<Entities.Tea>(),
                    It.IsAny<Entities.Tea>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_Should_NotCallUnitOfWork_When_ValidationFails()
        {
            // Arrange
            var request = new UpdateTeaRequestDto("abanent", DateTime.UtcNow, "Black Tea", "", null, null, null, null);
            var command = new UpdateTeaCommand(null, request);

            var handler = new UpdateTeaCommandHandler(
                _mockTeaRepository.Object,
                _mapper,
                _mockUnitOfWork.Object);

            // Act
            Result result = await handler.Handle(command, default);

            // Assert
            _mockUnitOfWork.Verify(
                x => x.SaveChangesAsync(
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }
    }
}
