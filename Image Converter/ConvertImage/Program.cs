using ConvertImage.Engines;
using ConvertImage.Factory;
using ConvertImage.Helpers;
using ConvertImage.Interfaces.Engines;
using ConvertImage.Interfaces.Factory;
using ConvertImage.Logging;
using System;
using System.Threading.Tasks;
using static ConvertImage.Models.ConvertImageConstants;

namespace ConvertImage
{
    class Program
    {
        private static readonly ILogger _logger = new ConsoleLogger();
        private static readonly IConvertImageSettingsFactory _convertImageSettingsFactory = new ConvertImageSettingsFactory(_logger);
        private static readonly IConvertImageEngine _convertImageEngine = new ConvertImageEngine(_logger);

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

            ConvertImageHelper.PressKeyToExit();
        }
    }
}
