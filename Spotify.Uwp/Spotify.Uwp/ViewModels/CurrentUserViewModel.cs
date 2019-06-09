namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// Current User View Model
    /// </summary>
    public class CurrentUserViewModel : UserViewModel
    {
        /// <summary>
        /// The user’s date-of-birth.This field is only available when the current user has granted access to the user-read-birthdate scope.
        /// </summary>
        public string BirthDate { get; set; }

        /// <summary>
        /// The country of the user, as set in the user’s account profile.An ISO 3166-1 alpha-2 country code.This field is only available when the current user has granted access to the user-read-private scope.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// The user’s email address, as entered by the user when creating their account. his field is only available when the current user has granted access to the user-read-email scope
        /// </summary>
        public string Email { get; set; }
    }
}
