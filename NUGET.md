# Spotify.Uwp

Spotify API .NET Standard SDK Library for Universal Windows Platform Extensions

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
using Spotify.NetStandard.Sdk;

var client = SpotifySdkClientFactory
    .CreateSpotifySdkClient(
        "client-id",
        "client-secret");

var model = new ListAlbumViewModel(client, AlbumType.NewReleases);
```

## Client Id and Client Secret

You can get a "client-id" and "client-secret" from [developer.spotify.com/dashboard](https://developer.spotify.com/dashboard/) by signing in with your Spotify Id then creating an Application.

## Change Log

### Version 1.0.0

- Initial Release