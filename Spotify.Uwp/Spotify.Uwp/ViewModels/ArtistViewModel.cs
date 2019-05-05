using Spotify.NetStandard.Responses;
using System.Collections.Generic;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// Artist View Model
    /// </summary>
    public class ArtistViewModel : AssetViewModel
    {
        /// <summary>
        /// Information about the followers of the artist.
        /// </summary>
        public Followers Followers { get; set; }

        /// <summary>
        /// A list of the genres the artist is associated with. For example: "Prog Rock" , "Post-Grunge".
        /// </summary>
        public IList<string> Genres { get; set; }

        /// <summary>
        /// The popularity of the artist. The value will be between 0 and 100, with 100 being the most popular.
        /// </summary>
        public int Popularity { get; set; }
    }
}
