namespace ConvertImage.Models
{
    /// <summary>
    /// The ConvertImage Constants.
    /// </summary>
	public class ConvertImageConstants
    {
        /// <summary>
        /// The Log Level.
        /// </summary>
        public enum LogLevel
        {
            Info,
            Warning,
            Error
        }

        /// <summary>
        /// The CmdLine Params.
        /// </summary>
        public class CmdLineParams
        {
            public const string FilePath = "-path";
            public const string FileName = "-file";
            public const string ConvertInto = "-to";
            public const string Quality = "-quality";
            public const string DestinationPath = "-destination";
            public const string Help = "HELP";
        }
    }
}

