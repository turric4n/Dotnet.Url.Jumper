using Dotnet.Url.Jumper.Domain.Exceptions;

namespace Dotnet.Url.Jumper.Application
{
    public class AppSettings
    {
        private int _defaultredirectioncode;
        public string ProxyModeClientIPHeaderKey { get; set; }
        public string CustomRefererHeaderKey { get; set; }
        public bool RegisterStats { get; set; }
        public int Defaultredirectioncode { get => _defaultredirectioncode;
            set
            {
                if (value == 0 || (value >= 301 && value <= 308))
                {
                    _defaultredirectioncode = value;
                }
                else
                {
                    throw new DefaultRedirectCodeException("Domain Rules : Redirectioncode needs to be between 301 and 308");
                }
            }
        }
    }
}
