using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Domain.Exceptions
{
    public class DuplicateURLException : Exception
    {
        public DuplicateURLException(string message) : base(message)
        {
        }
    }
}
