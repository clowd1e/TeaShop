using AutoMapper;
using MediatR;
using TeaShop.Application.DTOs.Order.Response;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Domain.Repository;

namespace TeaShop.Application.Service.Order.Query.GetAllOrders
{
    public sealed class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, Result
        <IEnumerable<OrderResponseDto>>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetAllOrdersQueryHandler(
            IOrderRepository orderRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<OrderResponseDto>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAsync();

            var ordersMap = _mapper.Map<IEnumerable<OrderResponseDto>>(orders);

            return orders is null
                ? OrderErrors.OrderNotFound 
                : ordersMap.ToResult();
        }
    }
}
