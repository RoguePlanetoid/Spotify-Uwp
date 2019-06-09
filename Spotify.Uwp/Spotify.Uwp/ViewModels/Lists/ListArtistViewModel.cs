using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// List Artist View Model
    /// </summary>
    public class ListArtistViewModel : BaseListViewModel<ArtistViewModel>
    {
        #region Private Members
        private string _id;
        private ArtistType _type;
        #endregion Private Members

        #region Constructor
        /// <summary>Constructor</summary>
        /// <param name="client">Music Client</param>
        /// <param name="type">Artist Type</param>
        /// <param name="id">Id</param>
        public ListArtistViewModel(
            ISpotifySdkClient client,
            ArtistType type,
            string id = null)
            : base(client)
        {
            _type = type;
            _id = id;
        }
        #endregion Constructor

        #region Public Methods
        /// <summary>
        /// Load Data
        /// </summary>
        /// <returns>IEnumerable of Album View Model</returns>
        protected override async Task<IEnumerable<ArtistViewModel>> LoadItemsAsync()
        {
            Results = Results == null ?
            await Client.ListArtistsAsync(_type, _id) :
            await Client.ListArtistsAsync(Results);
            return Results?.Items;
        }
        #endregion Public Methods
    }
}