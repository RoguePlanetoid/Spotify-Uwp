using Spotify.NetStandard.Sdk;
using Spotify.Uwp.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Spotify.Uwp
{
    /// <summary>
    /// Base List View Model
    /// </summary>
    public abstract class BaseListViewModel<TRequest, TResponse> : BaseSpotifySdkClient
        where TRequest : class
        where TResponse : class
    {
        #region Private Members
        private int _index;
        private bool _loading;
        private bool _isRemove;
        private TRequest _request;
        private List<TResponse> _items;
        private ObservableCollection<TResponse> _collection;
        #endregion Private Members

        #region Private Methods
        /// <summary>
        /// Get
        /// </summary>
        private void Get(TRequest request)
        {
            Loading = true;
            _request = request;
            Collection = new ResponseCollection<TRequest, TResponse>(Client, request);
            ((INoItemsLoaded)Collection).NoMoreItems += NoMoreItems;
            Collection.CollectionChanged += CollectionChanged;
        }

        /// <summary>
        /// No Items Loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NoMoreItems(object sender, EventArgs e) =>
            Loading = false;
        #endregion Private Methods

        #region Constructor
        /// <summary>Constructor</summary>
        /// <param name="client">Sdk Music Client</param>
        /// <param name="request">Request</param>
        public BaseListViewModel(
            ISpotifySdkClient client,
            TRequest request) :
            base(client) => 
            Get(request);
        #endregion Constructor

        #region Event Handlers
        /// <summary>
        /// Collection Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CollectionChanged(
            object sender,
            NotifyCollectionChangedEventArgs e)
        {
            Loading = false;
            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Remove:
                    _items = e.OldItems.Cast<TResponse>().ToList();
                    _index = e.OldStartingIndex;
                    if (_isRemove)
                    {
                        ResponseRemoved?.Invoke(this, new ResponseRemovedArgs<TResponse>()
                        {
                            Items = _items,
                            Index = _index
                        });
                        _isRemove = false;
                    }
                    break;
                case NotifyCollectionChangedAction.Add:
                    if (_items == null)
                        return;
                    ResponseMoved?.Invoke(this, new ResponseMovedArgs()
                    {
                        SourceIndex = _index,
                        TargetIndex = e.NewStartingIndex,
                        Total = _items.Count
                    });
                    _items = null;
                    break;
            }
        }
        #endregion Event Handlers

        #region Events
        /// <summary>
        /// Response Removed Event
        /// </summary>
        public event EventHandler<ResponseRemovedArgs<TResponse>> ResponseRemoved;

        /// <summary>
        /// Response Moved Event
        /// </summary>
        public event EventHandler<ResponseMovedArgs> ResponseMoved;
        #endregion Events

        #region Public Properties
        /// <summary>
        /// Loading Indicator
        /// </summary>
        public bool Loading
        {
            get => _loading;
            set { _loading = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Observable Collection of Response
        /// </summary>
        public ObservableCollection<TResponse> Collection
        {
            get => _collection;
            set { _collection = value; NotifyPropertyChanged(); }
        }
        #endregion Public Properties

        #region Public Methods
        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="response"></param>
        public void Remove(TResponse response)
        {
            _isRemove = true;
            Collection.Remove(response);
        }

        /// <summary>
        /// Refresh
        /// </summary>
        public void Refresh()
        {
            if (_request != null)
                Get(_request);
        }
        #endregion Public Methods
    }
}
