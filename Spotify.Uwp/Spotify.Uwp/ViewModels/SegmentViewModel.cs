using System.Collections.Generic;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// Segment View Model
    /// </summary>
    public class SegmentViewModel : TimeIntervalViewModel
    {
        /// <summary>
        /// The onset loudness of the segment in decibels (dB).
        /// </summary>
        public float? LoudnessStart { get; set; }

        /// <summary>
        /// The peak loudness of the segment in decibels (dB). 
        /// </summary>
        public float? LoudnessMax { get; set; }

        /// <summary>
        /// The segment-relative offset of the segment peak loudness in seconds. 
        /// </summary>
        public float? LoudnessMaxTime { get; set; }

        /// <summary>
        /// The offset loudness of the segment in decibels (dB).
        /// </summary>
        public float? LoudnessEnd { get; set; }

        /// <summary>
        /// A “chroma” vector representing the pitch content of the segment, corresponding to the 12 pitch classes C, C#, D to B, with values ranging from 0 to 1 that describe the relative dominance of every pitch in the chromatic scale
        /// </summary>
        public List<float> Pitches { get; set; }

        /// <summary>
        /// Timbre is the quality of a musical note or sound that distinguishes different types of musical instruments, or voices.
        /// </summary>
        public List<float> Timbre { get; set; }
    }
}
