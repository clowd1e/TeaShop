using AutoMapper;
#region Dto namespaces
using TeaShop.Application.DTOs.Address.Request.Add;
using TeaShop.Application.DTOs.Address.Request.Update;
using TeaShop.Application.DTOs.Address.Response;
using TeaShop.Application.DTOs.Customer.Request.Add;
using TeaShop.Application.DTOs.Customer.Request.Update;
using TeaShop.Application.DTOs.Customer.Response;
using TeaShop.Application.DTOs.Order.Request.Add;
using TeaShop.Application.DTOs.Order.Request.Update;
using TeaShop.Application.DTOs.Order.Response;
using TeaShop.Application.DTOs.OrderDetails.Request.Add;
using TeaShop.Application.DTOs.OrderDetails.Request.Update;
using TeaShop.Application.DTOs.OrderDetails.Response;
using TeaShop.Application.DTOs.Tea.Request.Add;
using TeaShop.Application.DTOs.Tea.Request.Remove;
using TeaShop.Application.DTOs.Tea.Request.Update;
using TeaShop.Application.DTOs.Tea.Response;
using TeaShop.Application.DTOs.TeaItem.Request.Add;
using TeaShop.Application.DTOs.TeaItem.Request.Update;
using TeaShop.Application.DTOs.TeaItem.Response;
using TeaShop.Application.DTOs.TeaType.Request.Add;
using TeaShop.Application.DTOs.TeaType.Request.Remove;
using TeaShop.Application.DTOs.TeaType.Request.Update;
using TeaShop.Application.DTOs.TeaType.Response;
#endregion
using TeaShop.Domain.Entities;
using TeaShop.Domain.ValueObjects;

namespace TeaShop.Application.Mapper
{
    /// <summary>
    /// Mapping profile for AutoMapper
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Mapping
            CreateMap<AddAddressRequestDto, Address>();
            CreateMap<UpdateAddressRequestDto, Address>();
            CreateMap<Address, AddressResponseDto>();

            CreateMap<AddCustomerRequestDto, Customer>();
            CreateMap<UpdateCustomerRequestDto, Customer>();
            CreateMap<Customer, CustomerResponseDto>();

            CreateMap<AddOrderRequestDto, Order>();
            CreateMap<UpdateOrderRequestDto, Order>();
            CreateMap<Order, OrderResponseDto>();

            CreateMap<AddOrderDetailsRequestDto, OrderDetails>();
            CreateMap<UpdateOrderDetailsRequestDto, OrderDetails>();
            CreateMap<OrderDetails, OrderDetailsResponseDto>();

            CreateMap<AddTeaRequestDto, Tea>();
            CreateMap<RemoveTeaRequestDto, Tea>();
            CreateMap<UpdateTeaRequestDto, Tea>();
            CreateMap<Tea, TeaResponseDto>();
            CreateMap<Tea, TeaAdminResponseDto>();

            CreateMap<AddTeaTypeRequestDto, TeaType>();
            CreateMap<RemoveTeaTypeRequestDto, TeaType>();
            CreateMap<UpdateTeaTypeRequestDto, TeaType>();
            CreateMap<TeaType, TeaTypeResponseDto>();
            CreateMap<TeaType, TeaTypeAdminResponseDto>();

            CreateMap<AddTeaItemRequestDto, TeaItem>();
            CreateMap<UpdateTeaItemRequestDto, TeaItem>();
            CreateMap<TeaItem, TeaItemResponseDto>();
            #endregion
        }
    }
}
