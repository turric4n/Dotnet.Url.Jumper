using AutoMapper;

namespace Dotnet.Url.Jumper.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Dotnet.Url.Jumper.Application.Models.Admin, Dotnet.Url.Jumper.Domain.Models.Admin>();                                 
            CreateMap<Dotnet.Url.Jumper.Domain.Models.Admin, Dotnet.Url.Jumper.Application.Models.Admin>();
        }
    }
}
