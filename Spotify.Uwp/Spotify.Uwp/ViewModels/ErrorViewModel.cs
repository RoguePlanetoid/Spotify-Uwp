namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// Error View Model
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// The HTTP status code
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// A short description of the cause of the error. 
        /// </summary>
        public string Message { get; set; }
    }
}
