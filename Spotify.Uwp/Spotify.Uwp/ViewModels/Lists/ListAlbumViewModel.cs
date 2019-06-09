using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// List Album View Model
    /// </summary>
    public class ListAlbumViewModel : BaseListViewModel<AlbumViewModel>
    {
        #region Private Members
        private string _id;
        private AlbumType _type;
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
        protected override async Task<IEnumerable<AlbumViewModel>> LoadItemsAsync()
        {
            Results = Results == null ?
            await Client.ListAlbumsAsync(_type, _id) :
            await Client.ListAlbumsAsync(Results);
            return Results?.Items;
        }
        #endregion Public Methods
    }
}