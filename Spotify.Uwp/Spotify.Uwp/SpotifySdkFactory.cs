using Spotify.Uwp.Internal;

namespace Spotify.Uwp
{
    public class SpotifySdkClientFactory
    {
        /// <summary>
        /// Create Spotify SDK Client
        /// </summary>
        /// <param name="clientId">Spotify Client Id</param>
        /// <param name="clientSecret">Spotify Client Secret</param>
        /// <returns>Spotify SDK Client</returns>
        public static ISpotifySdkClient CreateSpotifySdkClient(
            string clientId,
            string clientSecret) => 
            new SpotifySdkClient(clientId, clientSecret);
    }
}
