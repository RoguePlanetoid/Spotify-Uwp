using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// List Album View Model
    /// </summary>
    public class ListAlbumViewModel : BaseListViewModel<AlbumViewModel>, IDisposable
    {
        #region Private Members
        private string _id;
        private AlbumType _type;
        private ISpotifySdkClient _client;
        private NavigationViewModel<AlbumViewModel> _results = null;
        #endregion Private Members

        #region Constructor
        /// <summary>Constructor</summary>
        /// <param name="client">Music Client</param>
        /// <param name="type">Album Type</param>
        /// <param name="id">Id</param>
        public ListAlbumViewModel(
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
        protected override async Task<IEnumerable<AlbumViewModel>> LoadItemsAsync()
        {
            _results = _results == null ?
            await _client.ListAlbumsAsync(_type, _id) :
            await _client.ListAlbumsAsync(_results);
            return _results?.Items;
        }

        /// <summary>Dispose</summary>
        public void Dispose() =>
            _client = null;
        #endregion Public Methods
    }
}