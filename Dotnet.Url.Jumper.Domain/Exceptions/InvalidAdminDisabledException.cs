using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Domain.Exceptions
{
    public class InvalidAdminDisabledException : Exception
    {
        public InvalidAdminDisabledException(string message) : base(message)
        {
        }
    }
}
