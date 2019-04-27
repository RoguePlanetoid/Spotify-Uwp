using Spotify.Uwp.Internal;
using System.Collections.Generic;
using System.Linq;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// Album View Model
    /// </summary>
    public class AlbumViewModel : ContentViewModel
    {
        /// <summary>
        /// The field is present when getting an artist’s albums. Possible values are “album”, “single”, “compilation”, “appears_on”.
        /// </summary>
        public string AlbumGroup { get; set; }

        /// <summary>
        /// The type of the album: one of "album" , "single" , or "compilation".
        /// </summary>
        public string AlbumType { get; set; }

        /// <summary>
        /// The artists of the album. Each artist object includes a link in href to more detailed information about the artist.
        /// </summary>
        public NavigationViewModel<ArtistViewModel> Artists { get; set; }

        /// <summary>
        /// Artist
        /// </summary>
        public ArtistViewModel Artist => Artists?.Items.FirstOrDefault();

        /// <summary>
        /// The markets in which the album is available: ISO 3166-1 alpha-2 country codes
        /// </summary>
        public IList<string> AvailableMarkets { get; set; }

        /// <summary>
        /// The copyright statements of the album.
        /// </summary>
        public IList<CopyrightViewModel> Copyrights { get; set; }

        /// <summary>
        /// Known external IDs for the album.
        /// </summary>
        public ExternalIdViewModel ExternalId { get; set; }

        /// <summary>
        /// A list of the genres used to classify the album. For example: "Prog Rock" , "Post-Grunge"
        /// </summary>
        public IList<string> Genres { get; set; }

        /// <summary>
        /// The label for the album.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// The popularity of the album. The value will be between 0 and 100, with 100 being the most popular
        /// </summary>
        public int Popularity { get; set; }

        /// <summary>
        /// The date the album was first released, for example 1981. Depending on the precision, it might be shown as 1981-12 or 1981-12-15
        /// </summary>
        public string ReleaseDate { get; set; }

        /// <summary>
        /// The precision with which release_date value is known: year , month , or day.
        /// </summary>
        public string ReleaseDatePrecision { get; set; }

        /// <summary>
        /// The total number of tracks
        /// </summary>
        public int TotalTracks { get; set; }

        /// <summary>
        /// The tracks of the album.
        /// </summary>
        public NavigationViewModel<TrackViewModel> Tracks { get; set; }

        /// <summary>
        /// Release Year
        /// </summary>
        public string ReleaseYear => ReleaseDate.GetReleaseYear();

        /// <summary>
        /// Songs
        /// </summary>
        public string Songs => TotalTracks.GetSongs();
    }
}
