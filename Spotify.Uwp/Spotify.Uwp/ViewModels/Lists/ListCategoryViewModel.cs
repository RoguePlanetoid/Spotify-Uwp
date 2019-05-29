using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// List Category View Model
    /// </summary>
    public class ListCategoryViewModel : BaseListViewModel<CategoryViewModel>, IDisposable
    {
        #region Private Members
        private ISpotifySdkClient _client;
        private NavigationViewModel<CategoryViewModel> _results = null;
        #endregion Private Members

        #region Constructor
        /// <summary>Constructor</summary>
        /// <param name="client">Music Client</param>
        /// <param name="type">Artist Type</param>
        /// <param name="id">Id</param>
        public ListCategoryViewModel(
            ISpotifySdkClient client) => 
            _client = client;
        #endregion Constructor

        #region Public Methods
        /// <summary>
        /// Load Data
        /// </summary>
        /// <returns>IEnumerable of Category View Model</returns>
        protected override async Task<IEnumerable<CategoryViewModel>> LoadItemsAsync()
        {
            _results = _results == null ?
            await _client.ListCategoriesAsync() :
            await _client.ListCategoriesAsync(_results);
            return _results?.Items;
        }

        /// <summary>Dispose</summary>
        public void Dispose() =>
            _client = null;
        #endregion Public Methods
    }
}