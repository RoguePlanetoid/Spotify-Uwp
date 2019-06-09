using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// List Playlist View Model
    /// </summary>
    public class ListPlaylistViewModel : BaseListViewModel<PlaylistViewModel>
    {
        #region Private Members
        private string _id;
        private PlaylistType _type;
        #endregion Private Members

        #region Constructor
        /// <summary>Constructor</summary>
        /// <param name="client">Music Client</param>
        /// <param name="type">Playlist Type</param>
        /// <param name="id">Id</param>
        public ListPlaylistViewModel(
            ISpotifySdkClient client,
            PlaylistType type,
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
        /// <returns>IEnumerable of Playlist View Model</returns>
        protected override async Task<IEnumerable<PlaylistViewModel>> LoadItemsAsync()
        {
            Results = Results == null ?
            await Client.ListPlaylistsAsync(_type, _id) :
            await Client.ListPlaylistsAsync(Results);
            return Results?.Items;
        }
        #endregion Public Methods
    }
}