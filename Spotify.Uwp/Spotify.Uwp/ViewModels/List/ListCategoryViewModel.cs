using Spotify.NetStandard.Sdk;

namespace Spotify.Uwp
{
    /// <summary>
    /// List Category View Model
    /// </summary>
    public class ListCategoryViewModel : BaseListViewModel<CategoriesRequest, CategoryResponse>
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">(Required) Spotify Sdk Client</param>
        /// <param name="categoriesRequest">(Optional) Categories Request</param>
        public ListCategoryViewModel(
            ISpotifySdkClient client,
            CategoriesRequest categoriesRequest = null)
            : base(client, categoriesRequest) { }
        #endregion Constructor
    }
}
