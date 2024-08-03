using AutoMapper;
using MediatR;
using TeaShop.Application.Data;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Domain.Repository;
using Entities = TeaShop.Domain.Entities;

namespace TeaShop.Application.Service.Tea.Command.AddTea
{
    public sealed class AddTeaCommandHandler : IRequestHandler<AddTeaCommand, Result>
    {
        private readonly ITeaRepository _teaRepository;
        private readonly ITeaTypeRepository _teaTypeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AddTeaCommandHandler(
            ITeaRepository teaRepository,
            ITeaTypeRepository teaTypeRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _teaRepository = teaRepository;
            _teaTypeRepository = teaTypeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddTeaCommand request, CancellationToken cancellationToken)
        {
            var validation = new AddTeaCommandValidator().Validate(request);
            if (!validation.IsValid)
                return Result.Fail(validation);

            var teaType = await _teaTypeRepository.GetByIdAsync(request.Tea!.TeaTypeId);

            if (teaType is null)
                return TeaTypeErrors.TeaTypeNotFound;

            var tea = _mapper.Map<Entities.Tea>(request.Tea);

            tea.Type = teaType;

            var teaExists = await _teaRepository.ExistsAsync(tea);

            if (teaExists)
                return TeaErrors.TeaAlreadyExists;

            await _teaRepository.AddAsync(tea);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
