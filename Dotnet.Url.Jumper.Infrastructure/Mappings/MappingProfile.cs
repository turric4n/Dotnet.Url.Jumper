﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Infrastructure.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Dotnet.Url.Jumper.Domain.Models.Admin, Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels.DBAdmin>();
            CreateMap<Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels.DBAdmin, Dotnet.Url.Jumper.Domain.Models.Admin>();
            CreateMap<Dotnet.Url.Jumper.Domain.Models.ShortUrl, Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels.DbShortUrl>();
            CreateMap<Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels.DbShortUrl, Dotnet.Url.Jumper.Domain.Models.ShortUrl>();
            CreateMap<Dotnet.Url.Jumper.Domain.Models.Stat, Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels.DbStat>();
            CreateMap<Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels.DbStat, Dotnet.Url.Jumper.Domain.Models.Stat>();
            CreateMap<Dotnet.Url.Jumper.Domain.Models.Visitor, Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels.DbVisitor>();
            CreateMap<Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels.DbVisitor, Dotnet.Url.Jumper.Domain.Models.Visitor>();
        }
    }
}
