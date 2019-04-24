using Spotify.NetStandard.Responses;
using Spotify.Uwp.Internal;
using System.Linq;
using System.Windows.Input;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// Album View Model
    /// </summary>
    public class AlbumViewModel : Album
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
        /// Artists
        /// </summary>
        public new NavigationViewModel<ArtistViewModel> Artists { get; set; }

        /// <summary>
        /// Artist
        /// </summary>
        public ArtistViewModel Artist => Artists?.Items.FirstOrDefault();

        /// <summary>
        /// Release Year
        /// </summary>
        public string ReleaseYear => ReleaseDate.GetReleaseYear();

        /// <summary>
        /// Songs
        /// </summary>
        public string Songs => TotalTracks.GetSongs();

        /// <summary>
        /// Command
        /// </summary>
        public ICommand Command { get; set; }
    }
}
