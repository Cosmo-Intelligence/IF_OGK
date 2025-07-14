using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Utils
{
	public class log4netUtils
    {
        public static log4net.Appender.IAppender GetLog4netAppender(String appenderName)
        {
            foreach (log4net.Repository.ILoggerRepository repository in log4net.LogManager.GetAllRepositories())
            {
                foreach (log4net.Appender.IAppender appender in repository.GetAppenders())
                {
                    if (appender.Name != appenderName)
                    {
                        continue;
                    }
                    return appender;
                }
            }
            return null;
        }
    }
}
