using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// Toggle Favourite View Model
    /// </summary>
    public class ToggleFavouriteViewModel : INotifyPropertyChanged
    {
        #region Private Members
        private bool _value;
        #endregion Private Members

        #region Private Methods
        /// <summary>
        /// Notify Property Changed
        /// </summary>
        /// <param name="property"></param>
        private void NotifyPropertyChanged([CallerMemberName] string property = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        #endregion Private Methods

        #region Public Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion Public Events

        #region Public Properties
        public string Id { get; set; }

        public bool Value
        {
            get => _value;
            set { _value = value; NotifyPropertyChanged(); }
        }

        public ICommand Command { get; set; }
        #endregion Public Properties
    }
}
