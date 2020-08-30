using Spotify.NetStandard.Sdk;
using System.Collections.Generic;

namespace Spotify.Uwp
{
    /// <summary>
    /// List Show View Model
    /// </summary>
    public class ListShowViewModel : BaseListViewModel<ShowsRequest, ShowResponse>
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">(Required) Spotify Sdk Client</param>
        /// <param name="showsRequest">(Required) Shows Request</param>
        public ListShowViewModel(
            ISpotifySdkClient client,
            ShowsRequest showsRequest)
            : base(client, showsRequest) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">(Required) Spotify Sdk Client</param>
        /// <param name="showType">(Required) Show Type</param>
        /// <param name="value">(Required) Show Search Term</param>
        /// <param name="multipleShowIds">(Required) Only for ShowType.Multiple - Multiple Spotify Show Ids</param>
        /// <param name="searchIsExternal">(Optional) Only for ShowType.Search, If true the response will include any relevant audio content that is hosted externally</param>
        public ListShowViewModel(
            ISpotifySdkClient client,
            ShowType showType,
            string value = null,
            List<string> multipleShowIds = null,
            bool? searchIsExternal = null)
            : base(client, new ShowsRequest()
            {
                ShowType = showType,
                Value = value,
                MultipleShowIds = multipleShowIds,
                SearchIsExternal = searchIsExternal
            }) { }
        #endregion Constructor
    }
}
