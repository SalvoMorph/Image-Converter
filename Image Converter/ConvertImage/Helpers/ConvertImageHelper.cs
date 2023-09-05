using System;

namespace ConvertImage.Helpers
{
    /// <summary>
    /// The ConvertImage Helper
    /// </summary>
    public static class ConvertImageHelper
    {
        /// <summary>
        /// Print the message Press any key to exit, and wait the press. 
        /// </summary>
        public static void PressKeyToExit()
        {
            Console.WriteLine("Press any key to exit..");
            Console.ReadKey();
        }
    }
}

