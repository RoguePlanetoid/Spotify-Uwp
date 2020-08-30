using System;

namespace Spotify.Uwp.Internal
{
    /// <summary>
    /// No Items Loaded
    /// </summary>
    internal interface INoItemsLoaded
    {
        #region Events
        /// <summary>
        /// No Items Loaded
        /// </summary>
        event EventHandler NoMoreItems;
        #endregion Events
    }
}
