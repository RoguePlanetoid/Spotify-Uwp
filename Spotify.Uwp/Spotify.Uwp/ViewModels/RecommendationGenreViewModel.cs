using System.Windows.Input;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// Recommendation View Model
    /// </summary>
    public class RecommendationViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Comand
        /// </summary>
        public ICommand Command { get; set; }
    }
}
