using ConvertImage.Interfaces.Engines;
using ConvertImage.Interfaces.Logger;
using ConvertImage.Logging;
using ConvertImage.Models;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static ConvertImage.Models.ConvertImageConstants;

namespace ConvertImage.Engines
{
    /// <inheritdoc cref="IConvertImageEngine"/>
    class ConvertImageEngine : IConvertImageEngine
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Ctor of <see cref="ConvertImageEngine"/>.
        /// </summary>
        /// <param name="logger">The Logger Interface.</param>
        public ConvertImageEngine(ILogger logger)
        {
            _logger = logger ?? new ConsoleLogger();
        }

        /// <inheritdoc cref="IConvertImageEngine.ConvertImages(ConvertImageSettings)"/>
        public async Task ConvertImagesAsync(ConvertImageSettings settings)
        {
            ImageCodecInfo imageEncoder = GetEncoder(settings.ConvertInto);

            if (imageEncoder == null)
            {
                _logger.Log(LogLevel.Error, "No encoder found. Closing application");
                return;
            }

            if (!string.IsNullOrEmpty(settings.DestinationPath) && !Directory.Exists(settings.DestinationPath))
            {
                _logger.Log(LogLevel.Warning, "Destination folder doesn't exist. Trying to create it");
                Directory.CreateDirectory(settings.DestinationPath);
                _logger.Log(LogLevel.Info, "Created!");
            }

            var tasks = Directory.EnumerateFiles(settings.FilePath, settings.FileName)
            .Select(async file =>
                {
                    _logger.Log(LogLevel.Info, "Processing file " + file);

                    string newNameFile = string.IsNullOrEmpty(settings.DestinationPath)
                        ? file + "." + settings.ConvertInto.Replace(".", "")
                        : file.Replace(settings.FilePath, settings.DestinationPath) + "." + settings.ConvertInto.Replace(".", "");

                    if (File.Exists(newNameFile))
                    {
                        _logger.Log(LogLevel.Warning, $"File {newNameFile} already converted");
                        return;
                    }

                    using (var bitmap = new Bitmap(file))
                    {
                        Encoder encoder = Encoder.Quality;
                        EncoderParameters myEncoderParameters = new(1);
                        EncoderParameter myEncoderParameter = new(encoder, Convert.ToInt64(settings.Quality));

                        myEncoderParameters.Param[0] = myEncoderParameter;

                        bitmap.Save(newNameFile, imageEncoder, myEncoderParameters);
                    }

                    _logger.Log(LogLevel.Info, $"New file saved: {newNameFile}");
                });

            await Task.WhenAll(tasks);
        }

        private static ImageCodecInfo GetEncoder(string formatTo)
        {
            ImageFormat format = formatTo.ToUpper() switch
            {
                ".PNG" or "PNG" => ImageFormat.Png,
                ".TIFF" or "TIFF" => ImageFormat.Tiff,
                ".BMP" or "BMP" => ImageFormat.Bmp,
                ".JPEG" or "JPEG" or ".JPG" or "JPG" => ImageFormat.Jpeg,
                ".ICON" or "ICON" => ImageFormat.Icon,
                _ => ImageFormat.Jpeg, // default
            };

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            return codecs.FirstOrDefault(codec => codec.FormatID == format.Guid);
        }

    }
}