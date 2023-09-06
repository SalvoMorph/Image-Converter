using System;
using static ConvertImage.Models.ConvertImageConstants;

namespace ConvertImage.Interfaces.Logger
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
}