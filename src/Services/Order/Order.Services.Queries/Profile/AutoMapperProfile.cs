using Order.Services.Queries.DTOs;

namespace Order.Services.Queries.Profile
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Domain.Order, OrderDto>().ReverseMap();
            CreateMap<Domain.OrderDetail, OrderDetailDto>().ReverseMap();
        }
    }
}
