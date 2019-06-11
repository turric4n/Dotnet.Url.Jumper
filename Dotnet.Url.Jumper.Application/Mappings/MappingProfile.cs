using AutoMapper;

namespace Dotnet.Url.Jumper.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Dotnet.Url.Jumper.Application.Models.Admin, Dotnet.Url.Jumper.Domain.Models.Admin>();                                 
            CreateMap<Dotnet.Url.Jumper.Domain.Models.Admin, Dotnet.Url.Jumper.Application.Models.Admin>();
            CreateMap<Dotnet.Url.Jumper.Application.Models.ShortUrl, Dotnet.Url.Jumper.Domain.Models.ShortUrl>();
            CreateMap<Dotnet.Url.Jumper.Domain.Models.ShortUrl, Dotnet.Url.Jumper.Application.Models.ShortUrl>();
            CreateMap<Dotnet.Url.Jumper.Application.Models.NewShortUrl, Dotnet.Url.Jumper.Application.Models.ShortUrl>();
            CreateMap<Dotnet.Url.Jumper.Application.Models.Stat, Dotnet.Url.Jumper.Domain.Models.Stat>();
            CreateMap<Dotnet.Url.Jumper.Domain.Models.Stat, Dotnet.Url.Jumper.Application.Models.Stat>();
            CreateMap<Dotnet.Url.Jumper.Application.Models.Visitor, Dotnet.Url.Jumper.Domain.Models.Visitor>();
            CreateMap<Dotnet.Url.Jumper.Domain.Models.Visitor, Dotnet.Url.Jumper.Application.Models.Visitor>();
        }
    }
}
