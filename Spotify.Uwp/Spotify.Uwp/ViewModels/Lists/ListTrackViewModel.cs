using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// List Track View Model
    /// </summary>
    public class ListTrackViewModel : BaseListViewModel<TrackViewModel>
    {
        #region Private Members
        private string _id;
        private TrackType _type;
        #endregion Private Members

        #region Constructor
        /// <summary>Constructor</summary>
        /// <param name="client">Music Client</param>
        /// <param name="type">Track Type</param>
        /// <param name="id">(Optional) Id</param>
        public ListTrackViewModel(
            ISpotifySdkClient client,
            TrackType type,
            string id = null) : 
            base(client)
        {
            _type = type;
            _id = id;
        }
        #endregion Constructor

        #region Public Methods
        /// <summary>
        /// Load Data
        /// </summary>
        /// <returns>IEnumerable of Track View Model</returns>
        protected override async Task<IEnumerable<TrackViewModel>> LoadItemsAsync()
        {
            Results = Results == null ?
            await Client.ListTracksAsync(_type, _id) :
            await Client.ListTracksAsync(Results);
            return Results?.Items;
        }
        #endregion Public Methods
    }
}