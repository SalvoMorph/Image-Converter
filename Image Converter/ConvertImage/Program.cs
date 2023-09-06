using ConvertImage.Engines;
using ConvertImage.Factory;
using ConvertImage.Helpers;
using ConvertImage.Interfaces.Engines;
using ConvertImage.Interfaces.Factory;
using ConvertImage.Interfaces.Logger;
using ConvertImage.Logging;
using System;
using System.Threading.Tasks;
using static ConvertImage.Models.ConvertImageConstants;

namespace ConvertImage
{
    /// <summary>
    /// The main program.
    /// </summary>
    class Program
    {
        private static readonly ILogger _logger = new ConsoleLogger();
        private static readonly IConvertImageSettingsFactory _convertImageSettingsFactory = new ConvertImageSettingsFactory(_logger);
        private static readonly IConvertImageEngine _convertImageEngine = new ConvertImageEngine(_logger);

        /// <summary>
        /// The run method.
        /// </summary>
        /// <param name="args">The cmdline arguments.</param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {
            var settings = _convertImageSettingsFactory.CreateSettings(args);

            try
            {
                await _convertImageEngine.ConvertImagesAsync(settings);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.ToString());
            }

            ConsoleLoggerHelper.PressKeyToExit();
        }
    }
}