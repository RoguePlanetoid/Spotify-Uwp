using System;

namespace Spotify.Uwp.Exceptions
{
    /// <summary>
    /// Token Required Exception
    /// </summary>
    public class TokenRequiredException : Exception
    {
        public TokenRequiredException(TokenType tokenType) => 
            TokenType = tokenType;

        public TokenType TokenType { get; }
    }
}
