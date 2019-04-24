using Spotify.Uwp.Internal;
using System.Globalization;

namespace Spotify.Uwp
{
    public class SpotifySdkClientFactory
    {
        /// <summary>
        /// Create Spotify SDK Client
        /// </summary>
        /// <param name="clientId">Spotify Client Id</param>
        /// <param name="clientSecret">Spotify Client Secret</param>
        /// <returns>Spotify Client</returns>
        public static ISpotifySdkClient CreateSpotifySdkClient(
            string clientId,
            string clientSecret,
            CultureInfo cultureInfo = null) => 
            new SpotifySdkClient(clientId, clientSecret, cultureInfo);
    }
}
