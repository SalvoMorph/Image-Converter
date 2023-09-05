using ConvertImage.Interfaces.Factory;
using ConvertImage.Logging;
using ConvertImage.Models;
using System;
using System.Collections.Generic;
using static ConvertImage.Models.ConvertImageConstants;

namespace ConvertImage.Factory
{
    /// <inheritdoc cref="IConvertImageSettingsFactory"/>
    internal class ConvertImageSettingsFactory : IConvertImageSettingsFactory
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Ctor of <see cref="ConvertImageSettingsFactory"/>.
        /// </summary>
        /// <param name="logger">The Logger Interface.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ConvertImageSettingsFactory(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc cref="IConvertImageSettingsFactory.CreateSettings(string[])"/>
        public ConvertImageSettings CreateSettings(string[] options)
        {
            if (options == null || options.Length == 0)
            {
                _logger.Log(LogLevel.Error, "Please enter options. --help or -help or help");
                throw new ArgumentException("Invalid options");
            }

            Dictionary<string, string> parserSettings = new();

            foreach (var option in options)
            {
                var split = option.Split('=');

                if (split[0].ToUpper().Contains(CmdLineParams.Help))
                {
                    _logger.ShowHelpMenu();
                    Environment.Exit(0);
                }

                parserSettings.Add(split[0], split[1]);
                _logger.Log(LogLevel.Info, $"Setting {split[0]}:{split[1]}");
            }

            return InitSettings(parserSettings);
        }

        #region Private Methods

        private ConvertImageSettings InitSettings(Dictionary<string, string> parserSettings)
        {
            if (!parserSettings.ContainsKey(CmdLineParams.FileName))
                _logger.ShowMandatoryParameterError(CmdLineParams.FileName);

            var settings = new ConvertImageSettings(filePath: GetFilePath(parserSettings), fileName: parserSettings[CmdLineParams.FileName])
            {
                ConvertInto = parserSettings.ContainsKey(CmdLineParams.ConvertInto) ? parserSettings[CmdLineParams.ConvertInto] : ".jpeg",
                Quality = parserSettings.ContainsKey(CmdLineParams.Quality) ? parserSettings[CmdLineParams.Quality] : "100",
                DestinationPath = parserSettings.ContainsKey(CmdLineParams.DestinationPath) ? parserSettings[CmdLineParams.DestinationPath] : GetAssemblyPath()
            };

            return settings;
        }

        private static string GetFilePath(Dictionary<string, string> parserSettings)
        {
            return parserSettings.ContainsKey(CmdLineParams.FilePath) ? parserSettings[CmdLineParams.FilePath] : GetAssemblyPath();
        }

        private static string GetAssemblyPath()
        {
            return System.Reflection.Assembly.GetEntryAssembly().Location.Replace("ConvertImage.exe", "");
        }

        #endregion

    }
}

