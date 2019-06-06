using Microsoft.Extensions.Configuration;
using QuickLogger.NetStandard;
using QuickLogger.NetStandard.Abstractions;
using System.IO;
using System.Reflection;

namespace Dotnet.Url.Jumper.Infrastructure.Services.Logger
{
    public class QuickLoggerService : ILoggerService
    {
        private static string currentPath => Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) != null ?
        Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)) :
        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static ILogger _quicklogger;
        private static IConfiguration _config;
        private void AssignProviderCallbacks(ILoggerProvider provider)
        {
            provider.CriticalError += (x => _quicklogger.Info(provider.getProviderProperties().GetProviderName() + " Provider Critical Error : " + x));
            provider.Error += (x => _quicklogger.Info(provider.getProviderProperties().GetProviderName() + " Provider Error : " + x));
            provider.QueueError += (x => _quicklogger.Info("Provider QueueError : " + x));
            provider.StatusChanged += (x => _quicklogger.Info("Provider Status Changed : " + x));
            provider.Started += (x => _quicklogger.Info("Provider Started : " + x));
        }
        public QuickLoggerService(IConfiguration config)
        {
            _quicklogger = new QuickLoggerNative(currentPath);
            _config = config;
            ILoggerConfigManager configManager = new QuickLoggerFileConfigManager(_config["QuickLogger:ConfigPath"]);
            configManager.Load();
            //Lazy 
            configManager.GetSettings().Providers().ForEach(x => _quicklogger?.AddProvider(x));
            configManager.GetSettings().Providers().ForEach(x => AssignProviderCallbacks(x));
        }
        public void Error(string classname, string msg)
        {
            _quicklogger?.Error("[" + classname + "] " + msg);
        }
        public void Info(string classname, string msg)
        {
            _quicklogger?.Info("[" + classname + "] " + msg);
        }
        public void Warning(string classname, string msg)
        {
            _quicklogger?.Warning("[" + classname + "] " + msg);
        }
        public void Success(string classname, string msg)
        {
            _quicklogger?.Success("[" + classname + "] " + msg);
        }
    }
}
