using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Infrastructure.Mappings
{
    public class MappingProfile : Profile
    {
        public class RepresentanteObsoletoConverter : IValueConverter<string, bool>
        {
            public bool Convert(string sourceMember, ResolutionContext context)
            {
                switch (sourceMember)
                {
                    case "T":
                        return true;
                    case "F":
                        return false;
                    default:
                        return true;
                }
            }
        }

        public MappingProfile()
        {            

        }
    }
}
