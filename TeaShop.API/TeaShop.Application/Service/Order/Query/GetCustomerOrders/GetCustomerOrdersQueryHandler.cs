using AutoMapper;
using MediatR;
using TeaShop.Application.DTOs.Order.Response;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Domain.Repository;

namespace TeaShop.Application.Service.Order.Query.GetCustomerOrders
{
    public sealed class GetCustomerOrdersQueryHandler : IRequestHandler<GetCustomerOrdersQuery, Result<IEnumerable<OrderResponseDto>>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetCustomerOrdersQueryHandler(
            IOrderRepository orderRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<OrderResponseDto>>> Handle(GetCustomerOrdersQuery request, CancellationToken cancellationToken)
        {
            if (request.Id is null)
                return Error.IdIsNull;

            var orders = await _orderRepository.GetCustomerOrders(request.Id.Value);

            if (orders is null)
                return OrderErrors.OrdersNotFound;

            var ordersDto = _mapper.Map<IEnumerable<OrderResponseDto>>(orders);

            return ordersDto.ToResult();
        }
    }
}
