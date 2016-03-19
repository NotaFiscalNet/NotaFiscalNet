using log4net;
using System;
using System.Diagnostics;
using System.Reflection;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// log4net Log helper
    /// </summary>
    public sealed class LoggerWrapper
    {
        /// <summary>
        /// Determines whether the DEBUG Mode is enabled.
        /// </summary>
        private readonly bool isDebugEnabled;

        /// <summary>
        /// The is error enabled.
        /// </summary>
        private readonly bool isErrorEnabled;

        /// <summary>
        /// Determines whether the FATAL Mode is enabled.
        /// </summary>
        private readonly bool isFatalEnabled;

        /// <summary>
        /// Determines whether the INFO Mode is enabled.
        /// </summary>
        private readonly bool isInfoEnabled;

        /// <summary>
        /// Determines whether the WARN Mode is enabled.
        /// </summary>
        private readonly bool isWarnEnabled;

        /// <summary>
        /// The logger object
        /// </summary>
        private readonly ILog log;

        /// <summary>
        /// Initializes a new instance of the Logger class.
        /// </summary>
        public LoggerWrapper()
            : this(new StackTrace().GetFrame(2).GetMethod().DeclaringType)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Logger class.
        /// </summary>
        /// <param name="type">The type of logger.</param>
        public LoggerWrapper(Type type)
        {
            log = LogManager.GetLogger(type);

            isDebugEnabled = log.IsDebugEnabled;
            isErrorEnabled = log.IsErrorEnabled;
            isInfoEnabled = log.IsInfoEnabled;
            isFatalEnabled = log.IsFatalEnabled;
            isWarnEnabled = log.IsWarnEnabled;
        }

        /// <summary>
        /// Logs the debug message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Debug(string message)
        {
            if (isDebugEnabled)
            {
                MethodBase methodBase = new StackTrace().GetFrame(2).GetMethod();
                log.Debug(methodBase.Name + " : " + message);
            }
        }

        /// <summary>
        /// Logs the debug message and the exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Debug(string message, Exception exception)
        {
            if (isDebugEnabled)
            {
                MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
                log.Debug(methodBase.Name + " : " + message, exception);
            }
        }

        /// <summary>
        /// Logs the error message.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        public void Error(string errorMessage)
        {
            if (isErrorEnabled)
            {
                MethodBase methodBase = new StackTrace().GetFrame(2).GetMethod();
                log.Error(methodBase.Name + " : " + errorMessage);
            }
        }

        /// <summary>
        /// Logs the error message and the exception.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="exception">The exception.</param>
        public void Error(string errorMessage, Exception exception)
        {
            if (isErrorEnabled)
            {
                MethodBase methodBase = new StackTrace().GetFrame(2).GetMethod();
                log.Error(methodBase.Name + " : " + errorMessage, exception);
            }
        }

        /// <summary>
        /// Logs the fatal error message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Fatal(string message)
        {
            if (isFatalEnabled)
            {
                MethodBase methodBase = new StackTrace().GetFrame(2).GetMethod();
                log.Fatal(methodBase.Name + " : " + message);
            }
        }

        /// <summary>
        /// Logs the fatal error message and the exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Fatal(string message, Exception exception)
        {
            if (isFatalEnabled)
            {
                MethodBase methodBase = new StackTrace().GetFrame(2).GetMethod();
                log.Fatal(methodBase.Name + " : " + message, exception);
            }
        }

        /// <summary>
        /// Logs the info message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Info(string message)
        {
            if (isInfoEnabled)
            {
                MethodBase methodBase = new StackTrace().GetFrame(2).GetMethod();
                log.Info(methodBase.Name + " : " + message);
            }
        }

        /// <summary>
        /// Logs the info message and the exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Info(string message, Exception exception)
        {
            if (isInfoEnabled)
            {
                MethodBase methodBase = new StackTrace().GetFrame(2).GetMethod();
                log.Info(methodBase.Name + " : " + message, exception);
            }
        }

        /// <summary>
        /// Logs the warning message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Warn(string message)
        {
            if (isWarnEnabled)
            {
                MethodBase methodBase = new StackTrace().GetFrame(2).GetMethod();
                log.Warn(methodBase.Name + " : " + message);
            }
        }

        /// <summary>
        /// Logs the warning message and the exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Warn(string message, Exception exception)
        {
            if (isWarnEnabled)
            {
                MethodBase methodBase = new StackTrace().GetFrame(2).GetMethod();
                log.Warn(methodBase.Name + " : " + message, exception);
            }
        }
    }
}