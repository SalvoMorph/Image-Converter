using System;

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
            Error,
            None
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

        /// <summary>
        /// The Colors to use in the console.
        /// </summary>
        public class ConsoleLoggerColor
        {
            /// <summary>
            /// Dark Green to use to log Info.
            /// </summary>
            public const ConsoleColor Info = ConsoleColor.DarkGreen;

            /// <summary>
            /// Yellow to use to log Info.
            /// </summary>
            public const ConsoleColor Warning = ConsoleColor.Yellow;

            /// <summary>
            /// Red to use to log Info.
            /// </summary>
            public const ConsoleColor Error = ConsoleColor.Red;

            /// <summary>
            /// White for tdefault.
            /// </summary>
            public const ConsoleColor Default = ConsoleColor.White;
        }
    }
}