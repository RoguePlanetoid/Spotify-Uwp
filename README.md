# Spotify.Uwp

Spotify API .NET Standard SDK Library for Universal Windows Platform Extensions

## Change Log

### Version 1.0.0

- Initial Release

## ListAlbumViewModel

List Album View Model

### Constructor(client, albumsRequest)

Constructor

| Name | Description |
| ---- | ----------- |
| client | *Spotify.NetStandard.Sdk.ISpotifySdkClient*<br>(Required) Spotify Sdk Client |
| albumsRequest | *Spotify.NetStandard.Sdk.AlbumsRequest*<br>(Required) Album Request |

### Constructor(client, albumType, value, multipleAlbumIds, searchIsExternal, includeGroup)

Constructor

| Name | Description |
| ---- | ----------- |
| client | *Spotify.NetStandard.Sdk.ISpotifySdkClient*<br>SDK Client |
| albumType | *Spotify.NetStandard.Sdk.AlbumType*<br>(Required) Album Type |
| value | *System.String*<br>(Required) Only for AlbumType.Search - Album Search Term and AlbumType.Artist - Artist Id |
| multipleAlbumIds | *System.Collections.Generic.List{System.String}*<br>(Required) Only for AlbumType.Multiple - Multiple Spotify Album Ids |
| searchIsExternal | *System.Nullable{System.Boolean}*<br>(Optional) Only for AlbumType.Search, If true the response will include any relevant audio content that is hosted externally |
| includeGroup | *Spotify.NetStandard.Sdk.IncludeGroupRequest*<br>(Optional) For AlbumType.Artist filters the response. If not supplied, all album types will be returned |


## ListArtistViewModel

List Artist View Model

### Constructor(client, artistsRequest)

Constructor

| Name | Description |
| ---- | ----------- |
| client | *Spotify.NetStandard.Sdk.ISpotifySdkClient*<br>(Required) Spotify Sdk Client |
| artistsRequest | *Spotify.NetStandard.Sdk.ArtistsRequest*<br>(Required) Artist Request |

### Constructor(client, artistType, value, multipleArtistIds, searchIsExternal, userTopTimeRangeType)

Constructor

| Name | Description |
| ---- | ----------- |
| client | *Spotify.NetStandard.Sdk.ISpotifySdkClient*<br>Sdk Client |
| artistType | *Spotify.NetStandard.Sdk.ArtistType*<br>(Required) Artist Type |
| value | *System.String*<br>(Required) Only for ArtistType.Search - Artist Search Term and ArtistType.Related - Artist Id |
| multipleArtistIds | *System.Collections.Generic.List{System.String}*<br>(Required) Only for ArtistType.Multiple - Multiple Artist Spotify Ids |
| searchIsExternal | *System.Nullable{System.Boolean}*<br>(Optional) Only for ArtistType.Search, If true the response will include any relevant audio content that is hosted externally |
| userTopTimeRangeType | *System.Nullable{Spotify.NetStandard.Sdk.UserTopTimeRangeType}*<br>(Optional) Only for ArtistType.UserTop, Long Term: calculated from several years of data and including all new data as it becomes available, Medium Term: (Default) approximately last 6 months, Short Term: approximately last 4 weeks |


## ListCategoryViewModel

List Category View Model

### Constructor(client, categoriesRequest)

Constructor

| Name | Description |
| ---- | ----------- |
| client | *Spotify.NetStandard.Sdk.ISpotifySdkClient*<br>(Required) Spotify Sdk Client |
| categoriesRequest | *Spotify.NetStandard.Sdk.CategoriesRequest*<br>(Optional) Categories Request |


## ListDeviceViewModel

List Device View Model

### Constructor(client)

Constructor

| Name | Description |
| ---- | ----------- |
| client | *Spotify.NetStandard.Sdk.ISpotifySdkClient*<br>(Required) Spotify Sdk Client |


## ListEpisodeViewModel

List Episode View Model

### Constructor(client, episodesRequest)

Constructor

| Name | Description |
| ---- | ----------- |
| client | *Spotify.NetStandard.Sdk.ISpotifySdkClient*<br>(Required) Spotify Sdk Client |
| episodesRequest | *Spotify.NetStandard.Sdk.EpisodesRequest*<br>(Required) Episodes Request |

### Constructor(client, episodeType, value, multipleEpisodeIds, searchIsExternal)

Constructor

| Name | Description |
| ---- | ----------- |
| client | *Spotify.NetStandard.Sdk.ISpotifySdkClient*<br>(Required) Spotify Sdk Client |
| episodeType | *Spotify.NetStandard.Sdk.EpisodeType*<br>(Required) Episode Type |
| value | *System.String*<br>(Required) Only for EpisodeType.Search - Episode Search Term or EpisodeType.Show - Show Id |
| multipleEpisodeIds | *System.Collections.Generic.List{System.String}*<br>(Required) Only for EpisodeType.Multiple - Multiple Spotify Episode Ids |
| searchIsExternal | *System.Nullable{System.Boolean}*<br>(Optional) Only for EpisodeType.Search, If true the response will include any relevant audio content that is hosted externally |


## ListPlaylistItemViewModel

List Playlist Item View Model

### Constructor(client, playlistItemsRequest)

Constructor

| Name | Description |
| ---- | ----------- |
| client | *Spotify.NetStandard.Sdk.ISpotifySdkClient*<br>(Required) Spotify Sdk Client |
| playlistItemsRequest | *Spotify.NetStandard.Sdk.PlaylistItemsRequest*<br>(Required) Playlist Items Request |

### Constructor(client, playlistId, fields)

Constructor

| Name | Description |
| ---- | ----------- |
| client | *Spotify.NetStandard.Sdk.ISpotifySdkClient*<br>(Required) Spotify Sdk Client |
| playlistId | *System.String*<br>(Required) Spotify Playlist Id |
| fields | *System.String*<br>(Optional) Filters for the query: a comma-separated list of the fields to return. If omitted, all fields are returned |


## ListPlaylistViewModel

List Playlist View Model

### Constructor(client, playlistsRequest)

Constructor

| Name | Description |
| ---- | ----------- |
| client | *Spotify.NetStandard.Sdk.ISpotifySdkClient*<br>(Required) Spotify Sdk Client |
| playlistsRequest | *Spotify.NetStandard.Sdk.PlaylistsRequest*<br>(Required) Playlists Request |

### Constructor(client, playlistType, value, searchIsExternal)

Constructor

| Name | Description |
| ---- | ----------- |
| client | *Spotify.NetStandard.Sdk.ISpotifySdkClient*<br>(Required) Spotify Sdk Client |
| playlistType | *Spotify.NetStandard.Sdk.PlaylistType*<br>(Required) Playlist Type |
| value | *System.String*<br>(Required) For PlaylistType.Search - Playlist Search Term, PlaylistType.CategoriesPlaylists - Category Id, and PlaylistType.User - User Id |
| searchIsExternal | *System.Nullable{System.Boolean}*<br>(Optional) Only for PlaylistType.Search, If true the response will include any relevant audio content that is hosted externally |


## ListRecommendationGenreViewModel

List Recommenation Genre View Model

### Constructor(client)

Constructor

| Name | Description |
| ---- | ----------- |
| client | *Spotify.NetStandard.Sdk.ISpotifySdkClient*<br>(Required) Spotify Sdk Client |


## ListShowViewModel

List Show View Model

### Constructor(client, showsRequest)

Constructor

| Name | Description |
| ---- | ----------- |
| client | *Spotify.NetStandard.Sdk.ISpotifySdkClient*<br>(Required) Spotify Sdk Client |
| showsRequest | *Spotify.NetStandard.Sdk.ShowsRequest*<br>(Required) Shows Request |

### Constructor(client, showType, value, multipleShowIds, searchIsExternal)

Constructor

| Name | Description |
| ---- | ----------- |
| client | *Spotify.NetStandard.Sdk.ISpotifySdkClient*<br>(Required) Spotify Sdk Client |
| showType | *Spotify.NetStandard.Sdk.ShowType*<br>(Required) Show Type |
| value | *System.String*<br>(Required) Show Search Term |
| multipleShowIds | *System.Collections.Generic.List{System.String}*<br>(Required) Only for ShowType.Multiple - Multiple Spotify Show Ids |
| searchIsExternal | *System.Nullable{System.Boolean}*<br>(Optional) Only for ShowType.Search, If true the response will include any relevant audio content that is hosted externally |


## ListTrackViewModel

List Track View Model

### Constructor(client, tracksRequest)

Constructor

| Name | Description |
| ---- | ----------- |
| client | *Spotify.NetStandard.Sdk.ISpotifySdkClient*<br>(Required) Spotify Sdk Client |
| tracksRequest | *Spotify.NetStandard.Sdk.TracksRequest*<br>(Required) Tracks Request |

### Constructor(client, trackType, value, multipleTrackIds, searchIsExternal, recommendation)

Constructor

| Name | Description |
| ---- | ----------- |
| client | *Spotify.NetStandard.Sdk.ISpotifySdkClient*<br>(Required) Spotify Sdk Client |
| trackType | *Spotify.NetStandard.Sdk.TrackType*<br>(Required) Track Type |
| value | *System.String*<br>(Required) Only for TrackType.Search - Track Search Term, TrackType.Album - Spotify Album Id and TrackType.Artist - Spotify Artist Id |
| multipleTrackIds | *System.Collections.Generic.List{System.String}*<br>(Required) Only for TrackType.Multiple - Multiple Spotify Track Ids |
| searchIsExternal | *System.Nullable{System.Boolean}*<br>(Optional) Only for TrackType.Search, If true the response will include any relevant audio content that is hosted externally |
| recommendation | *Spotify.NetStandard.Sdk.RecommendationRequest*<br>(Optional) Only for TrackType.Recommended - Recommendation Request |
