namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// Content View Model
    /// </summary>
    public class ContentViewModel : BaseViewModel
    {
        /// <summary>
        /// The base-62 identifier that you can find at the end of the Spotify URI for the object
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the content
        /// </summary>
        public string Name { get; set; }
    }
}
