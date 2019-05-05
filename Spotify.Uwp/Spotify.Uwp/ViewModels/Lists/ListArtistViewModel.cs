using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// List Artist
    /// </summary>
    public class ListArtistViewModel : ObservableCollection<ArtistViewModel>,
        ISupportIncrementalLoading, IDisposable
    {
        #region Private Members
        private NavigationViewModel<ArtistViewModel> _results = null;
        private ISpotifySdkClient _client = null;
        private bool _hasMore = true;
        private int _count = 0;
        #endregion Private Members

        #region Constructor
        /// <summary>Constructor</summary>
        /// <param name="client">Spotify SDK Client</param>
        public ListArtistViewModel(
            ISpotifySdkClient client) => 
            _client = client;
        #endregion Constructor

        #region Public Methods
        /// <summary>
        /// Set
        /// </summary>
        ///<param name="type">AlbumType</param>
        ///<param name="id">Value</param>
        ///<returns>ListArtist ViewModel</returns>
        public async Task<ListArtistViewModel> Set(
            ArtistType type,
            string id = null)
        {
            _results = await _client.ListArtistsAsync(type, id);
            _count = _results?.Items?.Count ?? 0;
            if (_count > 0)
            {
                _results.Items.ForEach(f => Add(f));
            }
            return this;
        }

        /// <summary>
        /// Has More Items
        /// </summary>
        public bool HasMoreItems =>
            (Count < _results?.Total && _hasMore);

        /// <summary>LoadMoreItemsAsync</summary>
        /// <param name="count">Count</param>
        /// <returns>Items</returns>
        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return AsyncInfo.Run(async (task) =>
            {
                _results = await _client.ListArtistsAsync(_results);
                _count = _results?.Items?.Count ?? 0;
                if (_count > 0)
                    _results?.Items?.ForEach(item => Add(item));
                else
                    _hasMore = false;
                return new LoadMoreItemsResult()
                {
                    Count = (uint)_count
                };
            });
        }

        /// <summary>Dispose</summary>
        public void Dispose() => 
            _client = null;
        #endregion Public Methods
    }
}