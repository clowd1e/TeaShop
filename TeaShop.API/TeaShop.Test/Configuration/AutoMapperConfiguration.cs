using AutoMapper;
using TeaShop.Application.Mapper;

namespace TeaShop.Test.Configuration
{
    public static class AutoMapperConfiguration
    {
        public static IMapper GetMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            return configuration.CreateMapper();
        }
    }
}
