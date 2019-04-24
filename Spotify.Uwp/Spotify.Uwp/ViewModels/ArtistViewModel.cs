using Spotify.NetStandard.Responses;
using Spotify.Uwp.Internal;
using System.Windows.Input;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// Artist View Model
    /// </summary>
    public class ArtistViewModel : Artist
    {
        /// <summary>
        /// Large Image
        /// </summary>
        public Image Large => Images.GetLargeImage();

        /// <summary>
        /// Medium Image
        /// </summary>
        public Image Medium => Images.GetMediumImage();

        /// <summary>
        /// Small Image
        /// </summary>
        public Image Small => Images.GetSmallImage();

        /// <summary>
        /// Command
        /// </summary>
        public ICommand Command { get; set; }
    }
}
