using Spotify.Uwp.Internal;
using System.Collections.Generic;
using System.Linq;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// Track View Model
    /// </summary>
    public class TrackViewModel : ContentViewModel
    {
        /// <summary>
        /// The album on which the track appears.The album object includes a link in href to full information about the album.
        /// </summary>
        public AlbumViewModel Album { get; set; }

        /// <summary>
        /// The artists who performed the track. Each artist object includes a link in href to more detailed information about the artist.
        /// </summary>
        public NavigationViewModel<ArtistViewModel> Artists { get; set; }

        /// <summary>
        /// Artist ViewModel
        /// </summary>
        public ArtistViewModel Artist => Artists?.Items.FirstOrDefault();

        /// <summary>
        /// A list of the countries in which the track can be played, identified by their ISO 3166-1 alpha-2 code.
        /// </summary>
        public IList<string> AvailableMarkets { get; set; }

        /// <summary>
        /// The disc number(usually 1 unless the album consists of more than one disc).
        /// </summary>
        public int DiscNumber { get; set; }

        /// <summary>
        /// The track length in milliseconds.
        /// </summary>
        public long Duration { get; set; }

        /// <summary>
        /// Known external IDs for the track.
        /// </summary>
        public ExternalIdViewModel ExternalId { get; set; }

        /// <summary>
        /// Whether or not the track has explicit lyrics ( true = yes it does; false = no it does not OR unknown).
        /// </summary>
        public bool IsExplicit { get; set; }

        /// <summary>
        /// Part of the response when Track Relinking is applied. If true , the track is playable in the given market. Otherwise false.
        /// </summary>
        public bool IsPlayable { get; set; }

        /// <summary>
        /// Part of the response when Track Relinking is applied and is only part of the response if the track linking, in fact, exists
        /// </summary>
        public LinkedTrackViewModel LinkedFrom { get; set; }

        /// <summary>
        /// The popularity of the track. The value will be between 0 and 100, with 100 being the most popular.
        /// </summary>
        public int Popularity { get; set; }

        /// <summary>
        /// Part of the response when Track Relinking is applied, the original track is not available in the given market
        /// </summary>
        public IList<string> Restrictions { get; set; }

        /// <summary>
        /// A link to a 30 second preview(MP3 format) of the track.
        /// </summary>
        public string Preview { get; set; }

        /// <summary>
        /// The number of the track. If an album has several discs, the track number is the number on the specified disc.
        /// </summary>
        public int TrackNumber { get; set; }

        /// <summary>
        /// Track Length
        /// </summary>
        public string Length => Duration.AsTimeSpan().AsTimeSpanString();
    }
}
