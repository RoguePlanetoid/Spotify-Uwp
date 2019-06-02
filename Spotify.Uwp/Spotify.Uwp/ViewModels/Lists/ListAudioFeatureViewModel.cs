using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// List Audio Feature View Model
    /// </summary>
    public class ListAudioFeatureViewModel : BaseListViewModel<AudioFeatureViewModel>
    {
        #region Private Members
        private string _id;
        #endregion Private Members

        #region Constructor
        /// <summary>Constructor</summary>
        /// <param name="client">Music Client</param>
        /// <param name="id">Id</param>
        public ListAudioFeatureViewModel(
            ISpotifySdkClient client,
            string id = null)
            : base(client) => 
            _id = id;
        #endregion Constructor

        #region Public Methods
        /// <summary>
        /// Load Data
        /// </summary>
        /// <returns>IEnumerable of Album View Model</returns>
        protected override async Task<IEnumerable<AudioFeatureViewModel>> LoadItemsAsync() => 
            await Client.ListAudioFeatureAsync(_id);
        #endregion Public Methods
    }
}
