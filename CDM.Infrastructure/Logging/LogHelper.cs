using log4net;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDM.Infrastructure.Logging
{
    public class LogHelper
    {
        /// <summary>
        /// A reference to the logger.
        /// </summary>
        public static ILog Logger { get; private set; }

        static LogHelper()
        {
            Logger = ServiceLocator.Current.GetInstance<ILog>();
        }

        public static void LogException(Exception ex)
        {
            LogException(Logger, ex);
        }

        public static void LogException(ILog logger, Exception ex)
        {
            logger.Error(ex.GetType().ToString(), ex);
        }
    }
}
