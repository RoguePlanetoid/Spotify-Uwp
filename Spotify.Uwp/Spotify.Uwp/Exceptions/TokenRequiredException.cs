﻿using System;

namespace Spotify.Uwp.Exceptions
{
    /// <summary>
    /// Token Required Exception
    /// </summary>
    public class TokenRequiredException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tokenType">Token Type</param>
        public TokenRequiredException(TokenType tokenType) => 
            TokenType = tokenType;

        /// <summary>
        /// Token Type
        /// </summary>
        public TokenType TokenType { get; }
    }
}
