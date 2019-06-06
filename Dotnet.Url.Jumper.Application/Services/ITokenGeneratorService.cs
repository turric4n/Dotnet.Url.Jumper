using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Application.Services
{
    public interface ITokenGeneratorService
    {
        string Generate(string Id);
    }
}
