using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Spotify.Uwp
{
    /// <summary>
    /// Base Notify Property Changed 
    /// </summary>
    public abstract class BaseNotifyPropertyChanged : INotifyPropertyChanged
    {
        #region Public Events
        /// <summary>
        /// Property Changed Event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion Public Events

        #region Protected Methods
        /// <summary>
        /// Notify Property Changed
        /// </summary>
        /// <param name="property">Member Name</param>
        protected void NotifyPropertyChanged([CallerMemberName] string property = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        #endregion Protected Methods
    }
}
