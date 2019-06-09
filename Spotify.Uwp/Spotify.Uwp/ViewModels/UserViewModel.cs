namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// User View Model
    /// </summary>
    public class UserViewModel : AssetViewModel
    {
        /// <summary>
        /// The name displayed on the user’s profile. null if not available.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Information about the followers of this user.
        /// </summary>
        public FollowersViewModel Followers { get; set; }

        /// <summary>
        /// The user’s Spotify subscription level: “premium”, “free”, etc. This field is only available when the current user has granted access to the user-read-private scope.
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        /// Is Premium Subscription
        /// </summary>
        public bool IsPremium => Product == "premium";
    }
}
