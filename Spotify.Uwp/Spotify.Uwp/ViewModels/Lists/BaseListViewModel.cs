using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// Base List View Model
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public abstract class BaseListViewModel<TViewModel> : 
        ObservableCollection<TViewModel>, 
        ISupportIncrementalLoading, IDisposable
    {
        #region Private Members
        private bool _hasMoreItems = true;
        #endregion Private Members

        #region Private Methods
        /// <summary>
        /// Load More Items
        /// </summary>
        /// <returns>LoadMoreItemsResult</returns>
        private async Task<LoadMoreItemsResult> GetMoreItemsAsync(uint count)
        {
            uint resultCount = 0;
            var data = await LoadItemsAsync();
            if (data?.Count() > 0)
            {
                resultCount = (uint)data.Count();
                foreach (var item in data)
                    Add(item);
            }
            else
                HasMoreItems = false;
            return new LoadMoreItemsResult { Count = resultCount };
        }
        #endregion Private Methods

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client"></param>
        public BaseListViewModel(ISpotifySdkClient client) => 
            Client = client;
        #endregion Constructor

        #region Public Methods
        /// <summary>
        /// Load More Items
        /// </summary>
        /// <param name="count">Count</param>
        /// <returns>LoadMoreItemsResult</returns>
        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count) => 
            GetMoreItemsAsync(count).AsAsyncOperation();

        /// <summary>
        /// Load Items
        /// </summary>
        /// <returns>IEnumerable of TViewModel</returns>
        protected virtual async Task<IEnumerable<TViewModel>> LoadItemsAsync() =>
            await Task.FromException<IEnumerable<TViewModel>>(new NotImplementedException());

        /// <summary>Dispose</summary>
        public void Dispose() =>
            Client = null;
        #endregion Public Methods

        #region Protected Properties
        /// <summary>
        /// Spotify SDK Client
        /// </summary>
        protected ISpotifySdkClient Client { get; set; }

        /// <summary>
        /// Navigation View Model of View Model Type
        /// </summary>
        protected NavigationViewModel<TViewModel> Results { get; set; }
        #endregion Protected Properties

        #region Public Properties
        /// <summary>
        /// Has More Items to get in Collection
        /// </summary>
        public bool HasMoreItems
        {
            get
            {
                return _hasMoreItems;
            }
            private set
            {
                if (value != _hasMoreItems)
                {
                    _hasMoreItems = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(HasMoreItems)));
                }
            }
        }
        #endregion Public Properties
    }
}
