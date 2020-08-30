using Spotify.NetStandard.Sdk;

namespace Spotify.Uwp
{
    /// <summary>
    /// List Device View Model
    /// </summary>
    public class ListDeviceViewModel : BaseListViewModel<DeviceResponse, DeviceResponse>
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">(Required) Spotify Sdk Client</param>
        public ListDeviceViewModel(
            ISpotifySdkClient client)
            : base(client, null) { }
        #endregion Constructor
    }
}
