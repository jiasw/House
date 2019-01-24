using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz.Logging;

namespace PullData
{
    public class ConsoleLogProvider : ILogProvider
    {

        public Logger GetLogger(string name)
        {
            return (level, func, exception, parameters) =>
            {
                if (level >= LogLevel.Info && func != null)
                {
                    LogNetHelper.Info("[" + DateTime.Now.ToLongTimeString() + "] [" + level + "] " + func() + "parameters:" + parameters);
                }
                return true;
            };
        }

        public IDisposable OpenMappedContext(string key, string value)
        {
            throw new NotImplementedException();
        }

        public IDisposable OpenNestedContext(string message)
        {
            throw new NotImplementedException();
        }
    }
}
