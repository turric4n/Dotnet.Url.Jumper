using Dotnet.Url.Jumper.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Application.Services
{
    public interface IVisitorLocatorService
    {
        Visitor GetCurrentVisitor();        
    }
}
