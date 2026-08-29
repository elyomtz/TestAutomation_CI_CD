using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomation_CI_CD.Core.Logger
{
    public static class Log4NetConfigurator
    {
        private static bool isConfigured;

        public static void Configure()
        {
            if (isConfigured)
            {
                return;
            }

            var repository = LogManager.GetRepository(
                Assembly.GetEntryAssembly()
                ?? Assembly.GetExecutingAssembly());

            var configFile = new FileInfo(
                Path.Combine(AppContext.BaseDirectory, "log4net.config"));

            if (!configFile.Exists)
            {
                throw new FileNotFoundException("log4net.config was not found.", configFile.FullName);
            }

            XmlConfigurator.Configure(repository, configFile);

            isConfigured = true;
        }
    }
}