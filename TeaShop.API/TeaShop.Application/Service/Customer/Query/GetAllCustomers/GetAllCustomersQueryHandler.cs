using AutoMapper;
using MediatR;
using TeaShop.Application.DTOs.Customer.Response;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Domain.Repository;

namespace TeaShop.Application.Service.Customer.Query.GetAllCustomers
{
    public sealed class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, Result
        <IEnumerable<CustomerResponseDto>>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetAllCustomersQueryHandler(
            ICustomerRepository customerRepository,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<CustomerResponseDto>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetAllAsync();

            var customersMap = _mapper.Map<IEnumerable<CustomerResponseDto>>(customers);

            return customers is null
                ? CustomerErrors.CustomersNotFound
                : customersMap.ToResult();
        }
    }
}
