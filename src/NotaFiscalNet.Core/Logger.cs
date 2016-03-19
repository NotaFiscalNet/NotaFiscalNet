namespace NotaFiscalNet.Core
{
    public static class Logger
    {
        public enum Level
        {
            Warn,
            Info,
            Error,
            Debug,
            Fatal
        }

        public static void Log(Level level, string message, params object[] args)
        {
            LoggerWrapper logger = new LoggerWrapper();
            switch (level)
            {
                case Level.Warn:
                    logger.Warn(string.Format(message, args));
                    break;
                case Level.Info:
                    logger.Info(string.Format(message, args));
                    break;
                case Level.Error:
                    logger.Error(string.Format(message, args));
                    break;
                case Level.Debug:
                    logger.Debug(string.Format(message, args));
                    break;
                case Level.Fatal:
                    logger.Debug(string.Format(message, args));
                    break;
            }
        }

        public static void Log(Level level, string message)
        {
            LoggerWrapper logger = new LoggerWrapper();
            switch (level)
            {
                case Level.Warn:
                    logger.Warn(message);
                    break;
                case Level.Info:
                    logger.Info(message);
                    break;
                case Level.Error:
                    logger.Error(message);
                    break;
                case Level.Debug:
                    logger.Debug(message);
                    break;
                case Level.Fatal:
                    logger.Debug(message);
                    break;
            }
        }
    }
}
