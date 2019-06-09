using System.Windows.Input;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// Toggle Favourite View Model
    /// </summary>
    public class ToggleFavouriteViewModel : BaseNotifyPropertyChanged
    {
        #region Private Members
        private bool _value;
        #endregion Private Members

        #region Public Properties
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public bool Value
        {
            get => _value;
            set { _value = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Command
        /// </summary>
        public ICommand Command { get; set; }
        #endregion Public Properties
    }
}
