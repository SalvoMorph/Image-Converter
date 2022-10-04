using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace ConvertImage
{
    class ConvertImageEngine
    {
        public void ConvertImage(Settings settings)
        {
            ImageCodecInfo ImageEncoder = GetEncoder(settings.To);

            if (ImageEncoder == null)
            {
                new TraceHelper().Error("No encoder found. Close application");
                return;
            }

            if (!String.IsNullOrEmpty(settings.Destination) && !Directory.Exists(settings.Destination))
            {
                new TraceHelper().Warning("Destination folder doesn't exist. Trying to create it");
                Directory.CreateDirectory(settings.Destination);
                new TraceHelper().Warning("Created!");
            }

            Parallel.ForEach(
                Directory.EnumerateFiles(settings.Path, settings.File),
                new ParallelOptions { MaxDegreeOfParallelism = 4 },
                file =>
                {
                    new TraceHelper().Info("Processing file " + file);

                    string newNameFile = string.IsNullOrEmpty(settings.Destination) ?
                        file + "." + settings.To.Replace(".", "") :
                        file.Replace(settings.Path, settings.Destination) + "." + settings.To.Replace(".", "");

                    if (File.Exists(newNameFile))
                    {
                        new TraceHelper().Warning(String.Format("File {0} already converted", newNameFile));
                        return;
                    }

                    Bitmap bitmap = new Bitmap(file);

                    Encoder encoder = Encoder.Quality;
                    EncoderParameters myEncoderParameters = new EncoderParameters(1);
                    EncoderParameter myEncoderParameter = new EncoderParameter(encoder, Convert.ToInt64(settings.Quality));

                    myEncoderParameters.Param[0] = myEncoderParameter;
                    bitmap.Save(newNameFile, ImageEncoder, myEncoderParameters);

                    new TraceHelper().Info("New file saved: " + newNameFile);
                });
        }

        private static ImageCodecInfo GetEncoder(string formatTo)
        {
            ImageFormat format;

            switch (formatTo.ToUpper())
            {
                case ".PNG":
                case "PNG":
                    format = ImageFormat.Png;
                    break;

                case ".TIFF":
                case "TIFF":
                    format = ImageFormat.Tiff;
                    break;

                case ".BMP":
                case "BMP":
                    format = ImageFormat.Bmp;
                    break;

                case ".JPEG":
                case "JPEG":
                case ".JPG":
                case "JPG":
                    format = ImageFormat.Jpeg;
                    break;

                case ".ICON":
                case "ICON":
                    format = ImageFormat.Icon;
                    break;

                default:
                    format = ImageFormat.Jpeg; // default
                    break;
            }

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                    return codec;
            }

            return null;
        }

    }
}
