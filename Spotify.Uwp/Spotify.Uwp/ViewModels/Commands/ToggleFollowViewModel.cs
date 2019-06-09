using System.Threading.Tasks;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// Toggle Follow View Model
    /// </summary>
    public class ToggleFollowViewModel : BaseClient
    {
        #region Private Members
        private bool _value;
        private string _id;
        private FollowType _type;
        #endregion Private Members

        #region Private Methods
        /// <summary>
        /// Toggle
        /// </summary>
        /// <param name="value">Value</param>
        public async void Toggle(bool value)
        {
            if (value)
                await Client.Follow(_id, _type);
            else
                await Client.Unfollow(_id, _type);
            _value = value;
        }

        /// <summary>
        /// Get
        /// </summary>
        public async Task<bool> Get() => 
            Value = (await Client.IsFollowing(_id, _type));
        #endregion Private Methods

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">Spotify SDK Client</param>
        /// <param name="type">Follow Type</param>
        /// <param name="id">Id</param>
        public ToggleFollowViewModel(
            ISpotifySdkClient client,
            FollowType type,
            string id) : 
            base(client)
        {
            _type = type;
            _id = id;
            _ = Get();
        }
        #endregion Constructor

        #region Public Properties
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public bool Value
        {
            get => _value;
            set
            {
                Toggle(value);
                NotifyPropertyChanged();
            }
        }
        #endregion Public Properties
    }
}
