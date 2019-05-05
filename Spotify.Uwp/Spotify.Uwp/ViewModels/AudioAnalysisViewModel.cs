using System.Collections.Generic;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// Audio Analysis ViewModel
    /// </summary>
    public class AudioAnalysisViewModel : ErrorViewModel
    {
        /// <summary>
        /// The time intervals of the bars throughout the track
        /// </summary>
        public List<TimeIntervalViewModel> Bars { get; set; }

        /// <summary>
        /// The time intervals of beats throughout the track.
        /// </summary>
        public List<TimeIntervalViewModel> Beats { get; set; }

        /// <summary>
        /// Sections are defined by large variations in rhythm or timbre, e.g.chorus, verse, bridge, guitar solo, etc.
        /// </summary>
        public List<SectionViewModel> Sections { get; set; }

        /// <summary>
        /// Audio segments attempts to subdivide a song into many segments, with each segment containing a roughly consitent sound throughout its duration.
        /// </summary>
        public List<SegmentViewModel> Segments { get; set; }

        /// <summary>
        /// A tatum represents the lowest regular pulse train that a listener intuitively infers from the timing of perceived musical events
        /// </summary>
        public List<TimeIntervalViewModel> Tatums { get; set; }
    }
}
