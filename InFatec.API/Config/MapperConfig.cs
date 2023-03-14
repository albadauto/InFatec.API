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
                config.CreateMap<ApiLogin, ApiLoginDTO>();
                config.CreateMap<ApiLoginDTO, ApiLogin>();
                config.CreateMap<ResetPasswordDTO, ApiLogin>();
                config.CreateMap<ApiLogin, ResetPasswordDTO>();


            });
            return mappingConfig;
        }
    }
}
