using System;
using System.Text;
using ConvertImage.Helpers;
using static ConvertImage.Models.ConvertImageConstants;

namespace ConvertImage.Logging
{
    /// <summary>
    /// The Logger.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Log the message in the console.
        /// </summary>
        /// <param name="level">The <see cref="LogLevel"/>.</param>
        /// <param name="message">The message.</param>
        void Log(LogLevel level, string message);

        /// <summary>
        /// Show the Help Menu.
        /// </summary>
        void ShowHelpMenu();

        /// <summary>
        /// Print an error for the mandatory parameter.
        /// </summary>
        /// <param name="parameter"></param>
        void ShowMandatoryParameterError(string parameter);
    }

    /// <inheritdoc cref="ILogger"/>
    public class ConsoleLogger : ILogger
    {
        private static readonly object lockObject = new();
        private readonly ConsoleColor infoColor = ConsoleColor.DarkGreen;
        private readonly ConsoleColor warningColor = ConsoleColor.Yellow;
        private readonly ConsoleColor errorColor = ConsoleColor.Red;

        public void Log(LogLevel level, string message)
        {
            lock (lockObject)
            {
                Console.ForegroundColor = GetColorForLogLevel(level);
                Console.WriteLine($"[{level}] {message}");
                Console.ResetColor();

                if (level == LogLevel.Error)
                {
                    ConvertImageHelper.PressKeyToExit();
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
            var helpMenu = GetHelpMenuText();
            Console.WriteLine(helpMenu);
        }

        #region Provate Methods

        private ConsoleColor GetColorForLogLevel(LogLevel level)
        {
            return level switch
            {
                LogLevel.Info => infoColor,
                LogLevel.Warning => warningColor,
                LogLevel.Error => errorColor,
                _ => ConsoleColor.White,
            };
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
