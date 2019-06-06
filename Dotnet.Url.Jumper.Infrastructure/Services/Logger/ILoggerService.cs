using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Infrastructure.Services.Logger
{
    public interface ILoggerService
    {
        void Info(string className, string msg);
        void Success(string className, string msg);
        void Warning(string className, string msg);
        void Error(string className, string msg);
    }
}