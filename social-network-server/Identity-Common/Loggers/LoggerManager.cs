using Identity_Common.Interfaces.Loggers;

namespace Identity_Common.Loggers
{
    public class LoggerManager
    {
        ILogger _logger;
        IPathLogger _pathLogger;
        string _path;

        public LoggerManager(ILogger logger)
        {
            _logger = logger;

        }

        public LoggerManager(IPathLogger logger, string path)
        {
            _pathLogger = logger;
            _path = path;
        }

        public void Log(string msg)
        {
            if (_path == null)
            {
                _logger.Log(msg);
            }
            else
            {
                _pathLogger.Log(msg, _path);
            }
        }
    }
}
