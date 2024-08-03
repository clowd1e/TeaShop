using AutoMapper;
using MediatR;
using TeaShop.Application.Data;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Domain.Repository;
using Entities = TeaShop.Domain.Entities;

namespace TeaShop.Application.Service.Customer.Command.AddCustomer
{
    public sealed class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, Result>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AddCustomerCommandHandler(
            ICustomerRepository customerRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            var validation = new AddCustomerCommandValidator().Validate(request);
            if (!validation.IsValid)
                return Result.Fail(validation);

            var customer = _mapper.Map<Entities.Customer>(request.Customer);

            var customerExists = await _customerRepository.ExistsAsync(customer);
            if (customerExists)
                return CustomerErrors.CustomerAlreadyExists;

            await _customerRepository.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
