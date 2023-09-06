using ConvertImage.Helpers;
using ConvertImage.Interfaces.Logger;
using System;
using System.Text;
using static ConvertImage.Models.ConvertImageConstants;

namespace ConvertImage.Logging
{
    /// <inheritdoc cref="ILogger"/>
    public class ConsoleLogger : ILogger
    {
        private static readonly object _lockObject = new();

        /// <inheritdoc cref="ILogger.Log(LogLevel, string)"/>
        public void Log(LogLevel level, string message)
        {
            lock (_lockObject)
            {
                Console.ForegroundColor = GetColorForLogLevel(level);
                Console.WriteLine($"{PrintLevel(level)} {message}");
                Console.ResetColor();

                if (level == LogLevel.Error)
                {
                    ConsoleLoggerHelper.PressKeyToExit();
                    Environment.Exit(1);
                }
            }
        }

        /// <inheritdoc cref="ILogger.ShowMandatoryParameterError(string)"/>
        public void ShowMandatoryParameterError(string parameter)
        {
            var errorMessage = GetMandatoryFieldErrorText(parameter);
            Log(LogLevel.Error, errorMessage);
        }

        /// <inheritdoc cref="ILogger.ShowHelpMenu()"/>
        public void ShowHelpMenu()
        {
            Log(LogLevel.None, GetHelpMenuText());
        }

        #region Private Methods

        private ConsoleColor GetColorForLogLevel(LogLevel level)
        {
            return level switch
            {
                LogLevel.Info => ConsoleLoggerColor.Info,
                LogLevel.Warning => ConsoleLoggerColor.Warning,
                LogLevel.Error => ConsoleLoggerColor.Error,
                _ => ConsoleLoggerColor.Default,
            };
        }

        private static string PrintLevel(LogLevel level)
        {
            return (level == LogLevel.None) ? string.Empty : $"[{level}]";
        }

        private string GetHelpMenuText()
        {
            var sb = new StringBuilder();
            sb.AppendLine("######################################### Image Converter Helper #########################################");
            sb.AppendLine("# ConvertImage [options]");
            sb.AppendLine("# [options] --> -path= -file= -to= -quality= -destination= ");
            sb.AppendLine("#");
            sb.AppendLine("# -path          Used to indicate the source path. If not declared, will be take the executable path");
            sb.AppendLine("#                Please, path in quotes. e.g \"c:\\Program Files\"  ");
            sb.AppendLine("# -file          The name of the file to convert. You can also use, for example, this syntax *.jpg");
            sb.AppendLine("# -to            The new file extension. You can conver into png, jpeg, tiff, bmp icon");
            sb.AppendLine("# -quality       The compression level of quality. From 0(low) to 100(high)");
            sb.AppendLine("# -destination   If any, the new files will be saved at this path.");
            sb.AppendLine("#                Please, path in quotes. e.g \"c:\\Program Files\"  ");
            sb.AppendLine("#########################################################################################################");
            return sb.ToString();
        }

        private string GetMandatoryFieldErrorText(string parameter)
        {
            return $"This option {parameter} is mandatory. Please see --help";
        }

        #endregion
    }
}