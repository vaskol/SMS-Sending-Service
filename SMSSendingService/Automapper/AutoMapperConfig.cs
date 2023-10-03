using AutoMapper;
using SMSSendingService.Models;

namespace SMSSendingService.Automapper
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SMSRequestDTO, Models.SMS>();
                cfg.CreateMap<Models.SMS, SMSResponseDTO>(); 
            });

            return config.CreateMapper();
        }
    }

}
