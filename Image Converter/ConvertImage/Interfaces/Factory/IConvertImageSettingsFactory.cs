using ConvertImage.Models;

namespace ConvertImage.Interfaces.Factory
{
    /// <summary>
    /// The ConvertImageSettings Factory.
    /// </summary>
    internal interface IConvertImageSettingsFactory
    {
        /// <summary>
        /// Create the settings.
        /// </summary>
        /// <param name="options">The options from the cmd line.</param>
        /// <returns><see cref="ConvertImageSettings"/>.</returns>
        ConvertImageSettings CreateSettings(string[] options);
    }
}