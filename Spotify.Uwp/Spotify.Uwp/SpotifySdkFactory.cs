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
        /// <param name="cultureInfo">(Optional) Culture Info</param>
        /// <returns>Spotify SDK Client</returns>
        public static ISpotifySdkClient CreateSpotifySdkClient(
            string clientId,
            string clientSecret,
            CultureInfo cultureInfo = null) => 
            new SpotifySdkClient(clientId, clientSecret, cultureInfo);

        /// <summary>
        /// Create Spotify SDK Client
        /// </summary>
        /// <param name="clientId">Spotify Client Id</param>
        /// <param name="clientSecret">Spotify Client Secret</param>
        /// <param name="country">(Optional) ISO 3166-1 alpha-2 country code e.g. GB</param>
        /// <param name="locale">(Optional) ISO 639-1 language code and an ISO 3166-1 alpha-2 country code, joined by an underscore e.g. en_GB</param>
        /// <returns>Spotify SDK Client</returns>
        public static ISpotifySdkClient CreateSpotifySdkClient(
            string clientId,
            string clientSecret,
            string country = null,
            string locale = null) =>
            new SpotifySdkClient(clientId, clientSecret, country, locale);
    }
}
