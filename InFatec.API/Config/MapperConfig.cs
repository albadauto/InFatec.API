using AutoMapper;
using InFatec.API.DTO;
using InFatec.API.Model;

namespace InFatec.API.Config
{
    public static class MapperConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Login, ApiLoginDTO>();
                config.CreateMap<ApiLoginDTO, Login>();
                config.CreateMap<ResetPasswordDTO, Login>();
                config.CreateMap<Login, ResetPasswordDTO>();
                config.CreateMap<Login, LoginDTO>();
                config.CreateMap<LoginDTO, ApiLoginDTO>();
                config.CreateMap<EventsDTO, Events>();
                config.CreateMap<Events, EventsDTO>();
                config.CreateMap<WarningDTO, Warnings>();
                config.CreateMap<Warnings, WarningDTO>();


            });
            return mappingConfig;
        }
    }
}
