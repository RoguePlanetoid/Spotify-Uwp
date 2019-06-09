namespace Spotify.Uwp
{
    /// <summary>
    /// Base Client
    /// </summary>
    public abstract class BaseClient : BaseNotifyPropertyChanged
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client"></param>
        public BaseClient(ISpotifySdkClient client) =>
            Client = client;
        #endregion Constructor

        #region Protected Properties
        /// <summary>
        /// Spotify SDK Client
        /// </summary>
        protected ISpotifySdkClient Client { get; set; }
        #endregion Protected Properties
    }
}
