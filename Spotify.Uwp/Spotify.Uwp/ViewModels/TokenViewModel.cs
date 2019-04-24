using Spotify.NetStandard.Client.Authentication;
using System.Runtime.Serialization;

namespace Spotify.Uwp.ViewModels
{
    /// <summary>
    /// Token View Model
    /// </summary>
    [DataContract]
    public class TokenViewModel : AccessToken { }
}
