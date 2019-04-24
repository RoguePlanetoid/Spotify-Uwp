using Spotify.NetStandard.Responses;
using Spotify.Uwp.Internal;
using System.Windows.Input;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// Playlist View Model
    /// </summary>
    public class PlaylistViewModel : Playlist
    {
        /// <summary>
        /// Large Image
        /// </summary>
        public Image Large => Images.GetLargeImage();

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
