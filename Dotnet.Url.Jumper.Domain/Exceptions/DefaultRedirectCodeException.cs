using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Domain.Exceptions
{
    public class DefaultRedirectCodeException : Exception
    {
        public DefaultRedirectCodeException(string message) : base(message)
        {
        }
    }
}
