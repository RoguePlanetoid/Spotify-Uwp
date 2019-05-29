using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// List Artist View Model
    /// </summary>
    public class ListArtistViewModel : BaseListViewModel<ArtistViewModel>, IDisposable
    {
        #region Private Members
        private string _id;
        private AlbumType _type;
        private ISpotifySdkClient _client;
        private NavigationViewModel<ArtistViewModel> _results = null;
        #endregion Private Members

        #region Constructor
        /// <summary>Constructor</summary>
        /// <param name="client">Music Client</param>
        /// <param name="type">Artist Type</param>
        /// <param name="id">Id</param>
        public ListArtistViewModel(
            ISpotifySdkClient client,
            AlbumType type,
            string id = null)
        {
            _client = client;
            _type = type;
            _id = id;
        }
        #endregion Constructor

        #region Public Methods
        /// <summary>
        /// Load Data
        /// </summary>
        /// <returns>IEnumerable of Album View Model</returns>
        protected override async Task<IEnumerable<ArtistViewModel>> LoadItemsAsync()
        {
            _results = _results == null ?
            await _client.ListArtistsAsync(_type, _id) :
            await _client.ListArtistsAsync(_results);
            return _results?.Items;
        }

        /// <summary>Dispose</summary>
        public void Dispose() =>
            _client = null;
        #endregion Public Methods
    }
}