using Microsoft.Extensions.Logging;
using Serilog.Core;


namespace Common.Logging
{
    public class SysLogger(string host, int port, Func<string, LogLevel, bool> filter) : ILoggerProvider
    {
        private string _host = host;
        private int _port = port;
        private readonly Func<string, LogLevel, bool> _filter = filter;

        public ILogger CreateLogger(string categoryName)
        {
            //return new Logger(_host, _port, categoryName, _filter);
            return null;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
