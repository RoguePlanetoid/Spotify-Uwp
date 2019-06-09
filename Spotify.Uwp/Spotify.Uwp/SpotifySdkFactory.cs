using Spotify.Uwp.Internal;
using System;

namespace Spotify.Uwp
{
    /// <summary>
    /// Spotify SDK Client Factory
    /// </summary>
    public class SpotifySdkClientFactory
    {
        /// <summary>
        /// Create Spotify SDK Client
        /// </summary>
        /// <param name="clientId">(Required) Spotify Client Id</param>
        /// <param name="clientSecret">Spotify Client Secret</param>
        /// <param name="loginRedirectUri">Login Redirect Uri</param>
        /// <param name="loginState">Login State</param>
        /// <returns>Spotify SDK Client</returns>
        public static ISpotifySdkClient CreateSpotifySdkClient(
            string clientId,
            string clientSecret = null,
            Uri loginRedirectUri = null,
            string loginState = null) => 
            new SpotifySdkClient(clientId, clientSecret, loginRedirectUri, loginState);
    }
}
