using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// List Album
    /// </summary>
    public class ListAlbumViewModel : ObservableCollection<AlbumViewModel>,
        ISupportIncrementalLoading, IDisposable
    {
        #region Private Members
        private NavigationViewModel<AlbumViewModel> _results = null;
        private ISpotifySdkClient _client = null;
        private bool _hasMore = true;
        private int _count = 0;
        #endregion Private Members

        #region Private Methods
        /// <summary>
        /// Init
        /// </summary>
        private async void Init(
            AlbumType type,
            string id = null)
        {
            _results = await _client.ListAlbums(type, id);
            _count = _results?.Items?.Count ?? 0;
            if (_count > 0)
            {
                _results.Items.ForEach(f => Add(f));
            }
        }
        #endregion Private Methods

        #region Constructor
        /// <summary>Constructor</summary>
        public ListAlbumViewModel(
            ISpotifySdkClient client,
            AlbumType type,
            string id = null)
        {
            _client = client;
            Init(type, id);
        }
        #endregion Constructor

        #region Public Methods
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
                _results = await _client.ListAlbums(_results);
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