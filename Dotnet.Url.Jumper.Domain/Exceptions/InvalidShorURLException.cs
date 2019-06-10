using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Domain.Exceptions
{
    public class InvalidShorURLException : Exception
    {
        public InvalidShorURLException(string message) : base(message)
        {
        }
    }
}
