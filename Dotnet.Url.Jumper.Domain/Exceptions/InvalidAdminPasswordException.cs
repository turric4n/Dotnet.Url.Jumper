using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Domain.Exceptions
{
    public class InvalidAdminPasswordException : Exception
    {
        public InvalidAdminPasswordException(string message) : base(message)
        {
        }
    }
}
