using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Domain.Services
{
    public interface IUrlShortenerGeneratorService
    {
        string Encode(int num);
        int Decode(string str);
    }
}
