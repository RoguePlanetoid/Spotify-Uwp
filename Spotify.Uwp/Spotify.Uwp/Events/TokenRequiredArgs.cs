using System;

namespace Spotify.Uwp
{
    /// <summary>
    /// Token Required Arguments
    /// </summary>
    public class TokenRequiredArgs : EventArgs
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tokenType">Token Type</param>
        public TokenRequiredArgs(TokenType tokenType) =>
            TokenType = tokenType;

        /// <summary>
        /// Token Type
        /// </summary>
        public TokenType TokenType { get; set; }
    }
}
