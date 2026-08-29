using log4net;

namespace TestAutomation_CI_CD.Core.Logger
{
    public static class LoggerManager
    {
        public static ILog Create<T>()
        {
            Log4NetConfigurator.Configure();
            return LogManager.GetLogger(typeof(T));
        }
    }
}