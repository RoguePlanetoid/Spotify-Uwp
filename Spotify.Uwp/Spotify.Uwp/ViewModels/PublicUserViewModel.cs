namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// Public User View Model
    /// </summary>
    public class PublicUserViewModel : AssetViewModel
    {
        /// <summary>
        /// The name displayed on the user’s profile. null if not available.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Information about the followers of this user.
        /// </summary>
        public int Followers { get; set; }

        /// <summary>
        /// The user’s Spotify subscription level: “premium”, “free”, etc. This field is only available when the current user has granted access to the user-read-private scope.
        /// </summary>
        public string Product { get; set; }
    }
}
