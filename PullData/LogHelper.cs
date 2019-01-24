using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace PullData
{
    /// <summary>
    /// 类名：LogNetHelper
    /// 功能描述：日志助手, 负责记录系统日志信息
    /// </summary>
    public class LogNetHelper
    {
        #region Private Member

        // Get Log File Ref Name
        private static readonly string _logName = typeof(LogNetHelper).Name;

        #endregion

        #region Public Method

        /// <summary>
        /// Debug Mode
        /// </summary>
        /// <param name="message">Your Message</param>
        public static void Debug(string message)
        {
            ILog log = log4net.LogManager.GetLogger(_logName);

            if (log.IsDebugEnabled)
            {
                log.Debug(message);
            }

            log = null;
        }

        /// <summary>
        /// Debug Exception Mode
        /// </summary>
        /// <param name="message">Your Message</param>
        /// <param name="e">Exception</param>
        public static void Debug(string message, Exception e)
        {
            ILog log = log4net.LogManager.GetLogger(_logName);

            if (log.IsDebugEnabled)
            {
                log.Debug(message, e);
            }

            log = null;
        }

        /// <summary>
        /// Error Mode
        /// </summary>
        /// <param name="message">Your Message</param>
        public static void Error(string message)
        {
            ILog log = log4net.LogManager.GetLogger(_logName);

            if (log.IsErrorEnabled)
            {
                log.Error(message);
            }

            log = null;
        }

        /// <summary>
        /// Error Exception Mode
        /// </summary>
        /// <param name="message">Your Message</param>
        /// <param name="e">Exception</param>
        public static void Error(string message, Exception e)
        {
            ILog log = log4net.LogManager.GetLogger(_logName);

            if (log.IsErrorEnabled)
            {
                log.Error(message, e);
            }

            log = null;
        }

        /// <summary>
        /// Fatal
        /// </summary>
        /// <param name="message">Your Message</param>
        public static void Fatal(string message)
        {
            ILog log = log4net.LogManager.GetLogger(_logName);

            if (log.IsFatalEnabled)
            {
                log.Fatal(message);
            }

            log = null;
        }

        /// <summary>
        /// Fatal Exception Mode
        /// </summary>
        /// <param name="message">Your Message</param>
        /// <param name="e">Exception</param>
        public static void Fatal(string message, Exception e)
        {
            ILog log = log4net.LogManager.GetLogger(_logName);

            if (log.IsFatalEnabled)
            {
                log.Fatal(message, e);
            }

            log = null;
        }

        /// <summary>
        /// Warn
        /// </summary>
        /// <param name="message">Your Message</param>
        public static void Warn(string message)
        {
            ILog log = log4net.LogManager.GetLogger(_logName);

            if (log.IsWarnEnabled)
            {
                log.Warn(message);
            }

            log = null;
        }

        /// <summary>
        /// Warn Exception Mode
        /// </summary>
        /// <param name="message">Your Message</param>
        /// <param name="e">Exception</param>
        public static void Warn(string message, Exception e)
        {
            ILog log = log4net.LogManager.GetLogger(_logName);

            if (log.IsWarnEnabled)
            {
                log.Warn(message, e);
            }

            log = null;
        }

        /// <summary>
        /// Info
        /// </summary>
        /// <param name="message">Your Message</param>
        public static void Info(string message)
        {
            ILog log = log4net.LogManager.GetLogger(_logName);
            
            if (log.IsInfoEnabled)
            {
                log.Info(message);
            }

            log = null;
        }

        /// <summary>
        /// Info Exception Mode
        /// </summary>
        /// <param name="message">Your Message</param>
        /// <param name="e">Exception</param>
        public static void Info(string message, Exception e)
        {
            ILog log = log4net.LogManager.GetLogger(_logName);

            if (log.IsInfoEnabled)
            {
                log.Info(message, e);
            }

            log = null;
        }

        #endregion
    }
}
