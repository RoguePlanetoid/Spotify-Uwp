# Spotify.NetStandard

Spotify API .NET Standard Library Extensions for Universal Windows Platform 

## Documentation and Source Code

Project Documentation and Source Code can be found at [https://github.com/RoguePlanetoid/Spotify-Uwp](https://github.com/RoguePlanetoid/Spotify-Uwp)

## NuGet

To add to your project from [nuget.org](https://www.nuget.org/packages/Spotify.Uwp/) use:
```
Install-Package Spotify.Uwp
```

## Example

```c#
using Spotify.Uwp;

var client = SpotifySdkClientFactory
    .CreateSpotifySdkClient(
    "client-id","client-secret");
var browse = await client.ListAlbumsAsync(
                AlbumType.NewReleases)
foreach (var album in browse.Items)
{
    ...
}
```

## Client Id and Client Secret

You can get a "client-id" and "client-secret" from [developer.spotify.com/dashboard](https://developer.spotify.com/dashboard/) by signing in with your Spotify Id then creating an Application.