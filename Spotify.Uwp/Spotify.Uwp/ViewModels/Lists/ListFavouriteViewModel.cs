using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// List Favourite View Model
    /// </summary>
    [DataContract]
    public class ListFavouriteViewModel
    {
        #region Public Properties
        [DataMember(Name = "albums")]
        public List<string> AlbumIds { get; set; } = new List<string>();

        [DataMember(Name = "artists")]
        public List<string> ArtistIds { get; set; } = new List<string>();

        [DataMember(Name = "tracks")]
        public List<string> TrackIds { get; set; } = new List<string>();
        #endregion Public Properties

        #region Public Methods
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="id">Id</param>
        public void Add(FavouriteType type, string id)
        {
            switch (type)
            {
                case FavouriteType.Album:
                    if (AlbumIds == null) AlbumIds = new List<string>();
                    if (!AlbumIds.Contains(id)) AlbumIds.Add(id);
                    break;
                case FavouriteType.Artist:
                    if (ArtistIds == null) ArtistIds = new List<string>();
                    if (!ArtistIds.Contains(id)) ArtistIds.Add(id);
                    break;
                case FavouriteType.Track:
                    if (TrackIds == null) TrackIds = new List<string>();
                    if (!TrackIds.Contains(id)) TrackIds.Add(id);
                    break;
            }
        }

        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="id">Id</param>
        public void Remove(FavouriteType type, string id)
        {
            switch (type)
            {
                case FavouriteType.Album:
                    if (AlbumIds != null && AlbumIds.Contains(id))
                        AlbumIds.Remove(id);
                    break;
                case FavouriteType.Artist:
                    if (ArtistIds != null && ArtistIds.Contains(id))
                        ArtistIds.Remove(id);
                    break;
                case FavouriteType.Track:
                    if (TrackIds != null && TrackIds.Contains(id))
                        TrackIds.Remove(id);
                    break;
            }
        }

        /// <summary>
        /// Contains
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Contains(FavouriteType type, string id)
        {
            var result = false;
            switch(type)
            {
                case FavouriteType.Album:
                    result = AlbumIds?.Contains(id) ?? false;
                    break;
                case FavouriteType.Artist:
                    result = ArtistIds?.Contains(id) ?? false;
                    break;
                case FavouriteType.Track:
                    result = TrackIds?.Contains(id) ?? false;
                    break;
            }
            return result;
        }

        /// <summary>
        /// Any
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="id">Id</param>
        /// <returns></returns>
        public bool Any(FavouriteType type)
        {
            var result = false;
            switch (type)
            {
                case FavouriteType.Album:
                    result = (AlbumIds?.Count ?? 0) > 0;
                    break;
                case FavouriteType.Artist:
                    result = (ArtistIds?.Count ?? 0) > 0;
                    break;
                case FavouriteType.Track:
                    result = (TrackIds?.Count ?? 0) > 0;
                    break;
            }
            return result;
        }

        /// <summary>
        /// Toggle
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Toggle(FavouriteType type, string id, bool value)
        {
            if(value)
            {
                Remove(type, id);
            }
            else
            {
                Add(type, id);
            }
            return Contains(type, id);
        }
        #endregion Public Methods
    }
}
