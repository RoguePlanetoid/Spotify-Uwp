using System.Collections.Generic;

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

        /// <summary>
        /// Images in various sizes, widest first.
        /// </summary>
        public IList<ImageViewModel> Images { get; set; }

        /// <summary>
        /// Large Image
        /// </summary>
        public ImageViewModel Large { get; set; }

        /// <summary>
        /// Medium Image
        /// </summary>
        public ImageViewModel Medium { get; set; }

        /// <summary>
        /// Small Image
        /// </summary>
        public ImageViewModel Small { get; set; }
    }
}
