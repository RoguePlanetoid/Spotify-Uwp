namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// Time Interval View Model
    /// </summary>
    public class TimeIntervalViewModel
    {
        /// <summary>
        /// The starting point in seconds.
        /// </summary>
        public float? Start { get; set; }

        /// <summary>
        /// The duration in seconds
        /// </summary>
        public float? Duration { get; set; }

        /// <summary>
        /// The reliability confidence, from 0.0 to 1.0
        /// </summary>
        public float? Confidence { get; set; }
    }
}
