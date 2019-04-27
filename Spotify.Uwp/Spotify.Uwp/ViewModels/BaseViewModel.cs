using System.Windows.Input;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// Base View Model
    /// </summary>
    public abstract class BaseViewModel
    {
        /// <summary>
        /// The object type of the object
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// A link to the Web API endpoint providing full details of the object
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// External URLs for this object.
        /// </summary>
        public string ExternalUrl { get; set; }

        /// <summary>
        /// The Spotify URI for the object
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Command
        /// </summary>
        public ICommand Command { get; set; }
    }
}
