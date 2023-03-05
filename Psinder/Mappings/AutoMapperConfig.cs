using AutoMapper;
using Psinder.Models;

namespace Psinder.Mappings
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Post, PostDTO>();
            cfg.CreateMap<PostDTO, Post>();
        }).CreateMapper();
    }
}
