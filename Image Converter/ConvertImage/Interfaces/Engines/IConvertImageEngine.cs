using ConvertImage.Models;
using System.Threading.Tasks;

namespace ConvertImage.Interfaces.Engines
{
    /// <summary>
    /// The ConvertImage Engine.
    /// </summary>
    internal interface IConvertImageEngine
    {
        /// <summary>
        /// Convert the images.
        /// </summary>
        /// <param name="settings">The <see cref="ConvertImageSettings"/>.</param>
        Task ConvertImagesAsync(ConvertImageSettings settings);
    }
}