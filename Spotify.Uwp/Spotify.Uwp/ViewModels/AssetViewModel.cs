using Spotify.Uwp.Internal;
using System.Collections.Generic;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// Asset View Model
    /// </summary>
    public class AssetViewModel : ContentViewModel
    {
        /// <summary>
        /// Images in various sizes, widest first.
        /// </summary>
        public IList<ImageViewModel> Images { get; set; }

        /// <summary>
        /// Large Image
        /// </summary>
        public ImageViewModel Large => Images.GetLargeImage();

        /// <summary>
        /// Medium Image
        /// </summary>
        public ImageViewModel Medium => Images.GetMediumImage();

        /// <summary>
        /// Small Image
        /// </summary>
        public ImageViewModel Small => Images.GetSmallImage();
    }
}
