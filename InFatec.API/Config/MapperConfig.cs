using AutoMapper;

namespace InFatec.API.Config
{
    public static class MapperConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {

            });
            return mappingConfig;
        }
    }
}
