using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// List Playlist View Model
    /// </summary>
    public class ListPlaylistViewModel : BaseListViewModel<PlaylistViewModel>, IDisposable
    {
        #region Private Members
        private string _id;
        private PlaylistType _type;
        private ISpotifySdkClient _client;
        private NavigationViewModel<PlaylistViewModel> _results = null;
        #endregion Private Members

        #region Constructor
        /// <summary>Constructor</summary>
        /// <param name="client">Music Client</param>
        /// <param name="type">Playlist Type</param>
        /// <param name="id">Id</param>
        public ListPlaylistViewModel(
            ISpotifySdkClient client,
            PlaylistType type,
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
        /// <returns>IEnumerable of Playlist View Model</returns>
        protected override async Task<IEnumerable<PlaylistViewModel>> LoadItemsAsync()
        {
            _results = _results == null ?
            await _client.ListPlaylistsAsync(_type, _id) :
            await _client.ListPlaylistsAsync(_results);
            return _results?.Items;
        }

        /// <summary>Dispose</summary>
        public void Dispose() =>
            _client = null;
        #endregion Public Methods
    }
}