using AutoMapper;
using MediatR;
using TeaShop.Application.Data;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Domain.Repository;
using Entities = TeaShop.Domain.Entities;

namespace TeaShop.Application.Service.TeaType.Command.AddTeaType
{
    public sealed class AddTeaTypeCommandHandler : IRequestHandler<AddTeaTypeCommand, Result>
    {
        private readonly ITeaTypeRepository _teaTypeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AddTeaTypeCommandHandler(
            ITeaTypeRepository teaTypeRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _teaTypeRepository = teaTypeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddTeaTypeCommand request, CancellationToken cancellationToken)
        {
            var validation = new AddTeaTypeCommandValidator().Validate(request);
            if (!validation.IsValid)
                return Result.Fail(validation);

            var teaType = _mapper.Map<Entities.TeaType>(request.TeaType);

            var teaTypeExists = await _teaTypeRepository.ExistsAsync(teaType);

            if (teaTypeExists)
                return TeaTypeErrors.TeaTypeAlreadyExists;

            await _teaTypeRepository.AddAsync(teaType);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
