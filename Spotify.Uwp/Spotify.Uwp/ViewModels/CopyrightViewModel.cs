namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// Copyright View Model
    /// </summary>
    public class CopyrightViewModel
    {
        /// <summary>
        /// The copyright text for this album.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The type of copyright: C = the copyright, P = the sound recording (performance) copyright.
        /// </summary>
        public string Type { get; set; }
    }
}
