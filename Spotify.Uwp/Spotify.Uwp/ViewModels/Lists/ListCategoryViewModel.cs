using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// List Category View Model
    /// </summary>
    public class ListCategoryViewModel : BaseListViewModel<CategoryViewModel>
    {
        #region Constructor
        /// <summary>Constructor</summary>
        /// <param name="client">Music Client</param>
        public ListCategoryViewModel(
            ISpotifySdkClient client)
            : base(client) { }
        #endregion Constructor

        #region Public Methods
        /// <summary>
        /// Load Data
        /// </summary>
        /// <returns>IEnumerable of Category View Model</returns>
        protected override async Task<IEnumerable<CategoryViewModel>> LoadItemsAsync()
        {
            Results = Results == null ?
            await Client.ListCategoriesAsync() :
            await Client.ListCategoriesAsync(Results);
            return Results?.Items;
        }
        #endregion Public Methods
    }
}