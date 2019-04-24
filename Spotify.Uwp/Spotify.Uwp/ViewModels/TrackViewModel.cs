using Spotify.NetStandard.Responses;
using Spotify.Uwp.Internal;
using System.Linq;
using System.Windows.Input;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// Track View Model
    /// </summary>
    public class TrackViewModel : Track
    {
        /// <summary>
        /// Large Image
        /// </summary>
        public Image Large => AlbumViewModel?.Images.GetLargeImage();

        /// <summary>
        /// Medium Image
        /// </summary>
        public Image Medium => AlbumViewModel?.Images.GetMediumImage();

        /// <summary>
        /// Small Image
        /// </summary>
        public Image Small => AlbumViewModel?.Images.GetSmallImage();

        /// <summary>
        /// Album View Model
        /// </summary>
        public AlbumViewModel AlbumViewModel { get; set; }

        /// <summary>
        /// Artist View Model
        /// </summary>
        public new NavigationViewModel<ArtistViewModel> Artists { get; set; }

        /// <summary>
        /// Artist
        /// </summary>
        public ArtistViewModel Artist => Artists?.Items.FirstOrDefault();

        /// <summary>
        /// Track Length
        /// </summary>
        public string Length => Duration.AsTimeSpan().AsTimeSpanString();

        /// <summary>
        /// Command
        /// </summary>
        public ICommand Command { get; set; }
    }
}
