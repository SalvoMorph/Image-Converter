namespace ConvertImage.Models
{
    /// <summary>
    /// The ConvertImage Settings.
    /// </summary>
    public class ConvertImageSettings
    {
        /// <summary>
        /// The path where the file is stored.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// The File name.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The convertion format.
        /// </summary>
        public string ConvertInto { get; set; }

        /// <summary>
        /// The qualiti of the converted file.
        /// </summary>
        public string Quality { get; set; }

        /// <summary>
        /// The destination path where to save the converted file.
        /// </summary>
        public string DestinationPath { get; set; }

        /// <summary>
        /// Ctor of <see cref="ConvertImageSettings"/>.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="fileName">The file name.</param>
        public ConvertImageSettings(string filePath, string fileName)
        {
            FilePath = filePath;
            FileName = fileName;
        }
    }
}

