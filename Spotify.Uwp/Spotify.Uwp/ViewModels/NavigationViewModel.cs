using System.Collections.Generic;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// Navigation View Model
    /// </summary>
    /// <typeparam name="TViewModel">View Model Type</typeparam>
    public class NavigationViewModel<TViewModel> : ErrorViewModel
    {
        /// <summary>
        /// Type
        /// </summary>
        public object Type { get; set; }

        /// <summary>
        /// Offset
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Total
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Limit
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Back Link
        /// </summary>
        public string Back { get; set; }

        /// <summary>
        /// Next Link
        /// </summary>
        public string Next { get; set; }

        /// <summary>
        /// Items
        /// </summary>
        public List<TViewModel> Items { get; set; }
    }
}
