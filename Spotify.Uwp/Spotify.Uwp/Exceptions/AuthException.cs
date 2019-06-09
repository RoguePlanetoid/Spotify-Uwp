using System;

namespace Spotify.Uwp.Exceptions
{
    /// <summary>
    /// Auth Exception
    /// </summary>
    public abstract class AuthException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="ex">Inner Exception</param>
        public AuthException(string message, Exception ex) : base(message, ex) { }
    }

    /// <summary>
    /// Auth Value Exception
    /// </summary>
    public class AuthValueException : AuthException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="ex">Inner Exception</param>
        public AuthValueException(string message, Exception ex) : base(message, ex) { }
    }

    /// <summary>
    /// Auth State Exception
    /// </summary>
    public class AuthStateException : AuthException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="ex">Inner Exception</param>
        public AuthStateException(string message, Exception ex) : base(message, ex) { }
    }
}
