using System;
using System.Runtime.Serialization;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// Token View Model
    /// </summary>
    [DataContract]
    public class TokenViewModel
    {
        /// <summary>
        /// Access Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Refresh
        /// </summary>
        public string Refresh { get; set; }

        /// <summary>
        /// Token Expiration Date
        /// </summary>
        public DateTime Expiration { get; set; }

        /// <summary>
        /// Token Type
        /// </summary>
        public TokenType TokenType { get; set; }

        /// <summary>
        /// Scopes
        /// </summary>
        public string Scopes { get; set; }
    }
}
