using Spotify.NetStandard.Sdk;
using Spotify.Uwp.Internal;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace Spotify.Uwp
{
    /// <summary>
    /// Response Collection
    /// </summary>
    /// <typeparam name="TRequest">Request Type</typeparam>
    /// <typeparam name="TResponse">Response Type</typeparam>
    internal class ResponseCollection<TRequest, TResponse> :
        ObservableCollection<TResponse>,
        INotifyPropertyChanged,
        ISupportIncrementalLoading,
        INoItemsLoaded,
        IDisposable
        where TRequest : class
        where TResponse : class
    {
        #region Private Members
        private int _count = 0;
        #endregion Private Members

        #region Events
        /// <summary>
        /// No More Items
        /// </summary>
        public event EventHandler NoMoreItems;
        #endregion Events

        #region Private Methods
        /// <summary>
        /// Get Results
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="count">Count</param>
        /// <returns>Navigation Response of Response Object</returns>
        private async Task<NavigationResponse<TResponse>> GetResults(
            CancellationToken cancellationToken, uint count) => 
            Results == null ?
                await Client.ListAsync<TRequest, TResponse>(Request) :
                await Client.ListAsync(Results);

        /// <summary>
        /// Set More Items
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="count">Item Count</param>
        /// <returns>Item Count</returns>
        private async Task<uint> SetMoreItemsAsync(CancellationToken cancellationToken, uint count)
        {
            Results = await GetResults(cancellationToken, count);
            if (Results?.Items == null)
            {
                NoMoreItems?.Invoke(this, EventArgs.Empty);
                return default;
            }
            if (!Results.Items.Any())
                NoMoreItems?.Invoke(this, EventArgs.Empty);
            _count += Results.Items.Count;
            Results.Items.ForEach(f => Add(f));
            return (uint)Results.Items.Count;
        }

        /// <summary>
        /// Get More Items
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="count">Item Count</param>
        /// <returns>LoadMoreItemsResult</returns>
        private async Task<LoadMoreItemsResult> GetMoreItemsAsync(
            CancellationToken cancellationToken, uint count)
        {
            try
            {
                return new LoadMoreItemsResult
                {
                    Count = await SetMoreItemsAsync(cancellationToken, count)
                };
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion Private Methods

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">Spotify SDK Client</param>
        /// <param name="request">Request</param>
        public ResponseCollection(ISpotifySdkClient client, TRequest request)
        {
            Client = client;
            Request = request;
            LoadMoreItemsCommand = new RelayCommand(
                async param => await LoadMoreItemsAsync((uint)param));
            RefreshItemsCommand = new RelayCommand(
                async param => await RefreshItemsAsync((uint)param));
        }
        #endregion Constructor

        #region Protected Properties
        /// <summary>
        /// Spotify SDK Client
        /// </summary>
        protected ISpotifySdkClient Client { get; set; }

        /// <summary>
        /// Request
        /// </summary>
        protected TRequest Request { get; set; }

        /// <summary>
        /// Navigation Response
        /// </summary>
        protected NavigationResponse<TResponse> Results { get; set; }
        #endregion Protected Properties

        #region Public Methods
        /// <summary>
        /// Load More Items
        /// </summary>
        /// <param name="count">Count</param>
        /// <returns>LoadMoreItemsResult</returns>
        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            if (IsBusy)
                throw new InvalidOperationException();
            IsBusy = true;
            return AsyncInfo.Run((cancellationToken) =>
                GetMoreItemsAsync(cancellationToken, count));
        }

        /// <summary>
        /// Refresh Items
        /// </summary>
        public IAsyncOperation<LoadMoreItemsResult> RefreshItemsAsync(uint count)
        {
            _count = 0;
            Results = null;
            return LoadMoreItemsAsync(count);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Client = null;
            Results = null;
        }
        #endregion Public Methods

        #region Public Properties
        /// <summary>
        /// Is Busy
        /// </summary>
        public bool IsBusy { get; set; }

        /// <summary>
        /// Has More Items
        /// </summary>
        public bool HasMoreItems =>
            Results == null || _count < Results.Total;

        /// <summary>
        /// Load More Command
        /// </summary>
        public ICommand LoadMoreItemsCommand { get; private set; }

        /// <summary>
        /// Refresh Items
        /// </summary>
        public ICommand RefreshItemsCommand { get; private set; }
        #endregion Public Properties
    }
}
