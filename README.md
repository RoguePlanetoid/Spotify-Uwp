# Spotify.Uwp

Spotify API .NET Standard Library Extensions for Universal Windows Platform

## AlbumType

Album Type

### Artist

Artist Albums

### Favourites

Favourite Albums

### NewReleases

New Releases

### Search

Search for Album

### UserSaved

User's Saved Albums


## ArtistType

Artist Type

### Favourites

Favourite Artists

### Related

Related Artists

### Search

Search for Artist

### UserFollowed

User's Followed Artists

### UserTop

User's Top Artists


## BaseClient

Base Client

### Constructor(client)

Constructor

| Name | Description |
| ---- | ----------- |
| client | *Spotify.Uwp.ISpotifySdkClient*<br> |

### Client

Spotify SDK Client


## BaseNotifyPropertyChanged

Base Notify Property Changed

### NotifyPropertyChanged(property)

Notify Property Changed

| Name | Description |
| ---- | ----------- |
| property | *System.String*<br>Member Name |

### PropertyChanged

Property Changed Event


## AuthException

Auth Exception

### Constructor(message, ex)

Constructor

| Name | Description |
| ---- | ----------- |
| message | *System.String*<br>Message |
| ex | *System.Exception*<br>Inner Exception |


## AuthStateException

Auth State Exception

### Constructor(message, ex)

Constructor

| Name | Description |
| ---- | ----------- |
| message | *System.String*<br>Message |
| ex | *System.Exception*<br>Inner Exception |


## AuthValueException

Auth Value Exception

### Constructor(message, ex)

Constructor

| Name | Description |
| ---- | ----------- |
| message | *System.String*<br>Message |
| ex | *System.Exception*<br>Inner Exception |


## TokenRequiredException

Token Required Exception

### Constructor(tokenType)

Constructor

| Name | Description |
| ---- | ----------- |
| tokenType | *Spotify.Uwp.TokenType*<br>Token Type |

### TokenType

Token Type


## FavouriteType

Favourite Type

### Album

Favourite Albums

### Artist

Favourite Artists

### Track

Favourite Tracks


## FollowType

Follow Type

### Artist

Artist

### Playlist

Playlist

### User

User

## ISpotifySdkClient

Spotify SDK Client

### Country

ISO 3166-1 alpha-2 country code e.g. GB

### Favourites

List Favourite View Model

### Follow(ids, type, playlistId)

Follow 
Scopes: FollowModify


| Name | Description |
| ---- | ----------- |
| ids | *System.Collections.Generic.List{System.String}*<br>(Required for FollowType.Artist or FollowType.User) Artist or the User Spotify IDs to Follow |
| type | *Spotify.Uwp.FollowType*<br>(Required) Either Artist, User or Playlist |
| playlistId | *System.String*<br>(Required for FollowType.Playlist) The Spotify ID of the playlist |

#### Returns

True if Successful, False if Not Successful

*Exceptions.TokenRequiredException:* Token Required and TokenRequiredEvent not Subscribed to

*System.ArgumentNullException:* 

### Follow(id, type)

Follow 
Scopes: FollowModify


| Name | Description |
| ---- | ----------- |
| id | *System.String*<br>(Required) Artist, User or Playlist Spotify ID to Follow |
| type | *Spotify.Uwp.FollowType*<br>(Required) Either Artist, User or Playlist |

#### Returns

True if Is, False if Not

*Exceptions.TokenRequiredException:* Token Required and TokenRequiredEvent not Subscribed to

*System.ArgumentNullException:* 

### GetAlbumAsync(id)

Get Album

| Name | Description |
| ---- | ----------- |
| id | *System.String*<br>Album Spotify Id |

#### Returns

Album View Model

*Exceptions.TokenRequiredException:* Token Required and TokenRequiredEvent not Subscribed to

### GetArtistAsync(id)

Get Artist

| Name | Description |
| ---- | ----------- |
| id | *System.String*<br>Artist Spotify Id |

#### Returns

Artist View Model

*Exceptions.TokenRequiredException:* Token Required and TokenRequiredEvent not Subscribed to

### GetAudioAnalysisAsync(id)

Get Audio Analysis

| Name | Description |
| ---- | ----------- |
| id | *System.String*<br>Track Spotify Id |

#### Returns

AudioAnalysis View Model

*Exceptions.TokenRequiredException:* Token Required and TokenRequiredEvent not Subscribed to

### GetCategoryAsync(id)

Get Category

| Name | Description |
| ---- | ----------- |
| id | *System.String*<br>Category Id |

#### Returns

Category View Model

*Exceptions.TokenRequiredException:* Token Required and TokenRequiredEvent not Subscribed to

### GetCurrentUserAsync

Get Current User 
Scopes: UserReadPrivate, UserReadEmail, UserReadBirthDate, UserReadPrivate


#### Returns

Current User View Model

*Exceptions.TokenRequiredException:* Token Required and TokenRequiredEvent not Subscribed to

### GetLoginTokenAsync(type, responseUri)

Get Login Token

| Name | Description |
| ---- | ----------- |
| type | *Spotify.Uwp.LoginType*<br>Login Type |
| responseUri | *System.Uri*<br>(Required for LoginType.AuthorisationCode or LoginType.ImplicitGrant) Response Uri |

#### Returns

AccessToken on Success, Null if Not

*Exceptions.AuthValueException:* AuthValueException

*Exceptions.AuthStateException:* AuthStateException

### GetLoginUri(type, scope, showDialog)

Get Login Uri

| Name | Description |
| ---- | ----------- |
| type | *Spotify.Uwp.LoginType*<br>(Required) LoginType.AuthorisationCode or LoginType.ImplicitGrant |
| scope | *Spotify.Uwp.ViewModels.ScopeViewModel*<br>(Optional) Authorisation Scopes |
| showDialog | *System.Boolean*<br>(Optional) Whether or not to force the user to approve the app again if they’ve already done so. |

*System.ArgumentNullException:* 

### GetPlaylistAsync(id)

Get Playlist

| Name | Description |
| ---- | ----------- |
| id | *System.String*<br>Playlist Spotify Id |

#### Returns

Playlist View Model

*Exceptions.TokenRequiredException:* Token Required and TokenRequiredEvent not Subscribed to

### GetTrackAsync(id)

Get Track

| Name | Description |
| ---- | ----------- |
| id | *System.String*<br>Track Spotify Id |

#### Returns

Track View Model

*Exceptions.TokenRequiredException:* Token Required and TokenRequiredEvent not Subscribed to

### GetUserAsync(id)

Get User

| Name | Description |
| ---- | ----------- |
| id | *System.String*<br>User Spotify Id |

#### Returns

Public User View Model

*Exceptions.TokenRequiredException:* Token Required and TokenRequiredEvent not Subscribed to

### IsFollowing(ids, type, playlistId)

Is Following Artists or Users and Check if Users Follow a Playlist 
Scopes: FollowRead, PlaylistReadPrivate


| Name | Description |
| ---- | ----------- |
| ids | *System.Collections.Generic.List{System.String}*<br>(Required for FollowType.Artist or FollowType.User) List of the Artist or the User Spotify IDs to check |
| type | *Spotify.Uwp.FollowType*<br>(Required) Either Artist, User or Playlist |
| playlistId | *System.String*<br>(Required for FollowType.Playlist) The Spotify ID of the playlist |

#### Returns

List of True or False values

*Exceptions.TokenRequiredException:* Token Required and TokenRequiredEvent not Subscribed to

*System.ArgumentNullException:* 

### IsFollowing(id, type)

Is Following 
Scopes: FollowRead, PlaylistReadPrivate


| Name | Description |
| ---- | ----------- |
| id | *System.String*<br>(Required) Artist, User or Playlist Spotify ID to check |
| type | *Spotify.Uwp.FollowType*<br>(Required) Either Artist, User or Playlist |

#### Returns

True if Is, False if Not

*Exceptions.TokenRequiredException:* Token Required and TokenRequiredEvent not Subscribed to

*System.ArgumentNullException:* 

### IsUserLoggedIn

Is User Logged In

### Limit

Number of items to return per request

### ListAlbumsAsync(type, id)

List Albums

| Name | Description |
| ---- | ----------- |
| type | *Spotify.Uwp.AlbumType*<br>Album Type |
| id | *System.String*<br>Album Spotify Id |

#### Returns

Navigation View Model of Album View Model

*Exceptions.TokenRequiredException:* Token Required and TokenRequiredEvent not Subscribed to

### ListAlbumsAsync(navigation)

List Albums

| Name | Description |
| ---- | ----------- |
| navigation | *Spotify.Uwp.ViewModels.NavigationViewModel{Spotify.Uwp.ViewModels.AlbumViewModel}*<br>Navigation View Model of Album View Model |

#### Returns

Navigation View Model of Album View Model

*Exceptions.TokenRequiredException:* Token Required and TokenRequiredEvent not Subscribed to

### ListArtistsAsync(type, id)

List Artists

| Name | Description |
| ---- | ----------- |
| type | *Spotify.Uwp.ArtistType*<br>Artist Type |
| id | *System.String*<br>Artist Spotify Id |

#### Returns

Navigation View Model of Artist View Model

*Exceptions.TokenRequiredException:* Token Required and TokenRequiredEvent not Subscribed to

### ListArtistsAsync(navigation)

List Artists

| Name | Description |
| ---- | ----------- |
| navigation | *Spotify.Uwp.ViewModels.NavigationViewModel{Spotify.Uwp.ViewModels.ArtistViewModel}*<br>Navigation View Model of Artist View Model |

#### Returns

Navigation View Model of Artist View Model

*Exceptions.TokenRequiredException:* Token Required and TokenRequiredEvent not Subscribed to

### ListAudioFeatureAsync(id)

List Audio Features

| Name | Description |
| ---- | ----------- |
| id | *System.String*<br>Track Spotify Id |

#### Returns

List of Audio Feature View Model

*Exceptions.TokenRequiredException:* Token Required and TokenRequiredEvent not Subscribed to

### ListAudioFeaturesAsync(ids)

List Audio Features

| Name | Description |
| ---- | ----------- |
| ids | *System.Collections.Generic.List{System.String}*<br>List of Track Spotify Id |

#### Returns

List of List of Audio Feature View Model

*Exceptions.TokenRequiredException:* Token Required and TokenRequiredEvent not Subscribed to

### ListCategoriesAsync

List Category

#### Returns

Navigation View Model of Category View Model

*Exceptions.TokenRequiredException:* Token Required and TokenRequiredEvent not Subscribed to

### ListCategoriesAsync(navigation)

List Categories

| Name | Description |
| ---- | ----------- |
| navigation | *Spotify.Uwp.ViewModels.NavigationViewModel{Spotify.Uwp.ViewModels.CategoryViewModel}*<br>Navigation View Model of Category View Model |

#### Returns

Navigation View Model of Category View Model

*Exceptions.TokenRequiredException:* Token Required and TokenRequiredEvent not Subscribed to

### ListPlaylistsAsync(type, id)

List Playlists

| Name | Description |
| ---- | ----------- |
| type | *Spotify.Uwp.PlaylistType*<br>Playlist Type |
| id | *System.String*<br>Playlist Spotify Id |

#### Returns

Navigation View Model of Playlist View Model

*Exceptions.TokenRequiredException:* Token Required and TokenRequiredEvent not Subscribed to

### ListPlaylistsAsync(navigation)

List Playlists

| Name | Description |
| ---- | ----------- |
| navigation | *Spotify.Uwp.ViewModels.NavigationViewModel{Spotify.Uwp.ViewModels.PlaylistViewModel}*<br>Navigation View Model of Playlist View Model |

#### Returns

Navigation View Model of Playlist View Model

*Exceptions.TokenRequiredException:* Token Required and TokenRequiredEvent not Subscribed to

### ListRecommendationGenresAsync

List Recommendation Genres

#### Returns

List of Recommendation View Model

*Exceptions.TokenRequiredException:* Token Required and TokenRequiredEvent not Subscribed to

### ListTracksAsync(type, id)

List Tracks

| Name | Description |
| ---- | ----------- |
| type | *Spotify.Uwp.TrackType*<br>Track Type |
| id | *System.String*<br>Track Spotify Id |

#### Returns

Navigation ViewModel of Track View Model

*Exceptions.TokenRequiredException:* Token Required and TokenRequiredEvent not Subscribed to

### ListTracksAsync(navigation)

List Tracks

| Name | Description |
| ---- | ----------- |
| navigation | *Spotify.Uwp.ViewModels.NavigationViewModel{Spotify.Uwp.ViewModels.TrackViewModel}*<br>Navigation View Model of Track View Model |

#### Returns

Navigation ViewModel of Track View Model

*Exceptions.TokenRequiredException:* Token Required and TokenRequiredEvent not Subscribed to

### Locale

ISO 639-1 language code and an ISO 3166-1 alpha-2 country code, joined by an underscore e.g. en_GB

### LoginRedirectUri

Login Redirect Uri

### LoginState

Login State

### Set(cultureInfo)

Set

| Name | Description |
| ---- | ----------- |
| cultureInfo | *System.Globalization.CultureInfo*<br>Culture Info |

#### Returns

ISpotifySdkClient

### Set(country, locale)

Set

| Name | Description |
| ---- | ----------- |
| country | *System.String*<br>ISO 3166-1 alpha-2 country code e.g. GB |
| locale | *System.String*<br>ISO 639-1 language code and an ISO 3166-1 alpha-2 country code, joined by an underscore e.g. en_GB |

#### Returns



### SpotifyClient

Spotify Client

### Token

Token View Model

### TokenRequiredEvent

Token Required Event

### Unfollow(ids, type, playlistId)

Unfollow 
Scopes: FollowModify, PlaylistModifyPublic, PlaylistModifyPrivate


| Name | Description |
| ---- | ----------- |
| ids | *System.Collections.Generic.List{System.String}*<br>(Required for FollowType.Artist or FollowType.User) Artist or the User Spotify IDs to Follow |
| type | *Spotify.Uwp.FollowType*<br>(Required) Either Artist, User or Playlist |
| playlistId | *System.String*<br>(Required for FollowType.Playlist) The Spotify ID of the playlist |

#### Returns

True if Successful, False if Not Successful

*Exceptions.TokenRequiredException:* Token Required and TokenRequiredEvent not Subscribed to

*System.ArgumentNullException:* 

### Unfollow(id, type)

Unfollow 
Scopes: FollowModify, PlaylistModifyPublic, PlaylistModifyPrivate


| Name | Description |
| ---- | ----------- |
| id | *System.String*<br>(Required) Artist or the User Spotify ID to Unfollow |
| type | *Spotify.Uwp.FollowType*<br>(Required) Either Artist, User or Playlist |

#### Returns

True if Is, False if Not

*Exceptions.TokenRequiredException:* Token Required and TokenRequiredEvent not Subscribed to

*System.ArgumentNullException:* 

### UserTopTimeFrame

Time Frame for User Top Artists and Tracks. Long Term: calculated from several years of data and including all new data as it becomes available, Medium Term: (Default) approximately last 6 months, Short Term: approximately last 4 weeks


## LoginType

Login Type

### AuthorisationCode

Authorisation Code Flow

### ClientCredentials

Client Credentials Flow

### ImplicitGrant

Implicit Grant Flow


## PlaylistType

Playlist Type

### CategoriesPlaylists

Category Playlists

### Featured

Featured Playlists

### Search

Search for Playlist

### User

User's Playlists


## SpotifySdkClientFactory

Spotify SDK Client Factory

### CreateSpotifySdkClient(clientId, clientSecret, loginRedirectUri, loginState)

Create Spotify SDK Client

| Name | Description |
| ---- | ----------- |
| clientId | *System.String*<br>(Required) Spotify Client Id |
| clientSecret | *System.String*<br>Spotify Client Secret |
| loginRedirectUri | *System.Uri*<br>Login Redirect Uri |
| loginState | *System.String*<br>Login State |

#### Returns

Spotify SDK Client


## TokenRequiredArgs

Token Required Arguments

### Constructor(tokenType)

Constructor

| Name | Description |
| ---- | ----------- |
| tokenType | *Spotify.Uwp.TokenType*<br>Token Type |

### TokenType

Token Type


## TokenType

Token Type

### Access

Access Token

### User

User Token


## TrackType

Track Type

### Album

Album Tracks

### Artist

Artist Top Tracks

### Favourites

Favourite Tracks

### Playlist

Playlist Tracks

### Recommended

Recommended Track by Genre

### Search

Search for Track

### UserRecentlyPlayed

User's Recently Played Tracks

### UserSaved

User's Saved Tracks

### UserTop

User's Top Tracks


## UserTopTimeFrame

User Top Time Frame

### LongTerm

Calculated from several years of data and including all new data as it becomes available

### MediumTerm

Approximately last 6 months

### ShortTerm

Approximately last 4 weeks


## AlbumViewModel

Album View Model

### AlbumGroup

The field is present when getting an artist’s albums. Possible values are “album”, “single”, “compilation”, “appears_on”.

### AlbumType

The type of the album: one of "album" , "single" , or "compilation".

### Artist

Artist

### Artists

The artists of the album. Each artist object includes a link in href to more detailed information about the artist.

### AvailableMarkets

The markets in which the album is available: ISO 3166-1 alpha-2 country codes

### Copyrights

The copyright statements of the album.

### ExternalId

Known external IDs for the album.

### Genres

A list of the genres used to classify the album. For example: "Prog Rock" , "Post-Grunge"

### Label

The label for the album.

### Popularity

The popularity of the album. The value will be between 0 and 100, with 100 being the most popular

### ReleaseDate

The date the album was first released, for example 1981. Depending on the precision, it might be shown as 1981-12 or 1981-12-15

### ReleaseDatePrecision

The precision with which release_date value is known: year , month , or day.

### ReleaseYear

Release Year

### Songs

Songs

### TotalTracks

The total number of tracks

### Tracks

The tracks of the album.


## ArtistViewModel

Artist View Model

### Followers

Information about the followers of the artist.

### Genres

A list of the genres the artist is associated with. For example: "Prog Rock" , "Post-Grunge".

### Popularity

The popularity of the artist. The value will be between 0 and 100, with 100 being the most popular.


## AssetViewModel

Asset View Model

### Images

Images in various sizes, widest first.

### Large

Large Image

### Medium

Medium Image

### Small

Small Image


## AudioAnalysisViewModel

Audio Analysis View Model

### Bars

The time intervals of the bars throughout the track

### Beats

The time intervals of beats throughout the track.

### Sections

Sections are defined by large variations in rhythm or timbre, e.g.chorus, verse, bridge, guitar solo, etc.

### Segments

Audio segments attempts to subdivide a song into many segments, with each segment containing a roughly consitent sound throughout its duration.

### Tatums

A tatum represents the lowest regular pulse train that a listener intuitively infers from the timing of perceived musical events


## AudioFeatureViewModel

Audio Feature View Model

### Value

Value


## BaseListViewModel

Base List View Model

#### Type Parameters

- TViewModel - 

### Constructor(client)

Constructor

| Name | Description |
| ---- | ----------- |
| client | *Spotify.Uwp.ISpotifySdkClient*<br> |

### Client

Spotify SDK Client

### Dispose

Dispose

### GetMoreItemsAsync(System.UInt32)

Load More Items

#### Returns

LoadMoreItemsResult

### HasMoreItems

Has More Items to get in Collection

### LoadItemsAsync

Load Items

#### Returns

IEnumerable of TViewModel

### LoadMoreItemsAsync(count)

Load More Items

| Name | Description |
| ---- | ----------- |
| count | *System.UInt32*<br>Count |

#### Returns

LoadMoreItemsResult

### Results

Navigation View Model of View Model Type


## BaseViewModel

Base View Model

### Command

Command

### ExternalUrl

External URLs for this object.

### Href

A link to the Web API endpoint providing full details of the object

### Type

The object type of the object

### Uri

The Spotify URI for the object


## CategoryViewModel

Category View Model


## ContentViewModel

Content View Model

### Id

The base-62 identifier that you can find at the end of the Spotify URI for the object

### Name

The name of the content


## CopyrightViewModel

Copyright View Model

### Text

The copyright text for this album.

### Type

The type of copyright: C = the copyright, P = the sound recording (performance) copyright.


## CurrentUserViewModel

Current User View Model

### BirthDate

The user’s date-of-birth.This field is only available when the current user has granted access to the user-read-birthdate scope.

### Country

The country of the user, as set in the user’s account profile.An ISO 3166-1 alpha-2 country code.This field is only available when the current user has granted access to the user-read-private scope.

### Email

The user’s email address, as entered by the user when creating their account. his field is only available when the current user has granted access to the user-read-email scope


## ErrorViewModel

Error View Model

### Message

A short description of the cause of the error.

### StatusCode

The HTTP status code


## ExternalIdViewModel

External Id View Model

### Ean

International Article Number

### Isrc

International Standard Recording Code

### Upc

Universal Product Code


## FollowersViewModel

Followers View Model

### Href

A link to the Web API endpoint providing full details of the followers; null if not available

### Total

The total number of followers.


## ImageViewModel

Image View Model

### Height

The image height in pixels. If unknown: null or not returned.

### Url

The source URL of the image.

### Width

The image width in pixels. If unknown: null or not returned.


## LinkedTrackViewModel

Linked Track View Model


## ListAlbumViewModel

List Album View Model

### Constructor(client, type, id)

Constructor

| Name | Description |
| ---- | ----------- |
| client | *Spotify.Uwp.ISpotifySdkClient*<br>Music Client |
| type | *Spotify.Uwp.AlbumType*<br>Album Type |
| id | *System.String*<br>Id |

### LoadItemsAsync

Load Data

#### Returns

IEnumerable of Album View Model


## ListArtistViewModel

List Artist View Model

### Constructor(client, type, id)

Constructor

| Name | Description |
| ---- | ----------- |
| client | *Spotify.Uwp.ISpotifySdkClient*<br>Music Client |
| type | *Spotify.Uwp.ArtistType*<br>Artist Type |
| id | *System.String*<br>Id |

### LoadItemsAsync

Load Data

#### Returns

IEnumerable of Album View Model


## ListAudioFeatureViewModel

List Audio Feature View Model

### Constructor(client, id)

Constructor

| Name | Description |
| ---- | ----------- |
| client | *Spotify.Uwp.ISpotifySdkClient*<br>Music Client |
| id | *System.String*<br>Id |

### LoadItemsAsync

Load Data

#### Returns

IEnumerable of Album View Model


## ListCategoryViewModel

List Category View Model

### Constructor(client)

Constructor

| Name | Description |
| ---- | ----------- |
| client | *Spotify.Uwp.ISpotifySdkClient*<br>Music Client |

### LoadItemsAsync

Load Data

#### Returns

IEnumerable of Category View Model


## ListFavouriteViewModel

List Favourite View Model

### Add(type, id)

Add

| Name | Description |
| ---- | ----------- |
| type | *Spotify.Uwp.FavouriteType*<br>Type |
| id | *System.String*<br>Id |

### AlbumIds

Album Spotify Ids

### Any(type)

Any

| Name | Description |
| ---- | ----------- |
| type | *Spotify.Uwp.FavouriteType*<br>Type |

#### Returns

True if Present, False if Not

### ArtistIds

Artist Spotify Ids

### Contains(type, id)

Contains

| Name | Description |
| ---- | ----------- |
| type | *Spotify.Uwp.FavouriteType*<br> |
| id | *System.String*<br> |

#### Returns



### Remove(type, id)

Remove

| Name | Description |
| ---- | ----------- |
| type | *Spotify.Uwp.FavouriteType*<br>Type |
| id | *System.String*<br>Id |

### Set(type, items)

Set

| Name | Description |
| ---- | ----------- |
| type | *Spotify.Uwp.FavouriteType*<br>Favourite Type |
| items | *System.Collections.Generic.List{System.String}*<br>Items |

### Toggle(type, id, value)

Toggle

| Name | Description |
| ---- | ----------- |
| type | *Spotify.Uwp.FavouriteType*<br> |
| id | *System.String*<br> |
| value | *System.Boolean*<br> |

#### Returns



### TrackIds

Track Spotify Ids


## ListPlaylistViewModel

List Playlist View Model

### Constructor(client, type, id)

Constructor

| Name | Description |
| ---- | ----------- |
| client | *Spotify.Uwp.ISpotifySdkClient*<br>Music Client |
| type | *Spotify.Uwp.PlaylistType*<br>Playlist Type |
| id | *System.String*<br>Id |

### LoadItemsAsync

Load Data

#### Returns

IEnumerable of Playlist View Model


## ListRecommendationViewModel

List Recommendation View Model

### Constructor(client)

Constructor

| Name | Description |
| ---- | ----------- |
| client | *Spotify.Uwp.ISpotifySdkClient*<br>Music Client |

### LoadItemsAsync

Load Data

#### Returns

IEnumerable of Album View Model


## ListTrackViewModel

List Track View Model

### Constructor(client, type, id)

Constructor

| Name | Description |
| ---- | ----------- |
| client | *Spotify.Uwp.ISpotifySdkClient*<br>Music Client |
| type | *Spotify.Uwp.TrackType*<br>Track Type |
| id | *System.String*<br>(Optional) Id |

### LoadItemsAsync

Load Data

#### Returns

IEnumerable of Track View Model


## NavigationViewModel

Navigation View Model

#### Type Parameters

- TViewModel - View Model Type

### Back

Back Link

### Items

Items

### Limit

Limit

### Next

Next Link

### Offset

Offset

### Total

Total

### Type

Type


## PlaylistViewModel

Playlist View Model

### Collaborative

true if the owner allows other users to modify the playlist.

### Description

The playlist description. Only returned for modified, verified playlists, otherwise null.

### Followers

Information about the followers of the playlist.

### Owner

The user who owns the playlist

### Public

The playlist’s public/private status: true the playlist is public, false the playlist is private, null the playlist status is not relevant

### SnapshotId

The version identifier for the current playlist.

### Tracks

Information about the tracks of the playlist.


## RecommendationViewModel

Recommendation View Model

### Command

Comand

### Id

Id

### Name

Name


## ScopeViewModel

Scope View Model

### AllPermissions

Returns a new Scope ViewModel with all scopes set to true Usage : Scope.AllPermissions

### ConnectModifyPlaybackState

Pause a User's Playback 
Required For

Seek To Position In Currently Playing Track, Set Repeat Mode On User’s Playback, Set Volume For User's Playback

Skip User’s Playback To Next Track, Skip User’s Playback To Previous Track, Start/Resume a User's Playback

Toggle Shuffle For User’s Playback Transfer a User's Playback


### ConnectReadCurrentlyPlaying

Read access to a user’s currently playing track 
Required For

Get the User's Currently Playing Track


### ConnectReadPlaybackState

Read access to a user’s player state. 
Required For

Get a User's Available Devices, Get Information About The User's Current Playback, Get the User's Currently Playing Track


### FollowAll

Returns a new Scope ViewModel with all scopes within the confines of Users set to true Usage : Scope.FollowAll

### FollowModify

Write/delete access to the list of artists and other users that the user follows. 
Required For

Follow Artists or Users, Unfollow Artists or Users


### FollowRead

Read access to the list of artists and other users that the user follows. 
Required For

Check if Current User Follows Artists or Users, Get User's Followed Artists


### LibraryAll

Returns a new Scope ViewModel with all scopes within the confines of Library set to true Usage : Scope.LibraryAll

### LibraryModify

Write/delete access to a user's "Your Music" library. 
Required For

Remove Albums for Current User, Remove User's Saved Tracks, Save Albums for Current User Save Tracks for User


### LibraryRead

Read access to a user's "Your Music" library. 
Required For

Check User's Saved Albums Check User's Saved Tracks, Get Current User's Saved Albums Get a User's Saved Tracks


### ListeningHistoryAll

Returns a new Scope ViewModel with all scopes within the confines of Listening History set to true Usage : ScopeViewModel.ListeningHistoryAll

### ListeningRecentlyPlayed

Read access to a user’s recently played tracks 
Required For

Get Current User's Recently Played Tracks


### ListeningTopRead

Read access to a user's top artists and tracks. 
Required For

Get a User's Top Artists and Tracks


### ModifyAllAccess

Returns a new Scope ViewModel with all "modify" scopes set to true Usage : ScopeViewModel.ModifyAllAccess

### PlaybackAll

Returns a new Scope ViewModel with all scopes within the confines of Playback set to true Usage : ScopeViewModel.PlaybackAll

### PlaybackAppRemoteControl

Remote control playback of Spotify.

### PlaybackStreaming

Control playback of a Spotify track. The user must have a Spotify Premium account.

### PlaylistAll

Returns a new Scope ViewModel with all scopes within the confines of Playlists set to true Usage : ScopeViewModel.PlaylistAll

### PlaylistModifyPrivate

Write access to a user's private playlists. 
Required For

Follow a Playlist, Unfollow a Playlist, Add Tracks to a Playlist

Change a Playlist's Details, Create a Playlist, Remove Tracks from a Playlist

Reorder a Playlist's Tracks, Replace a Playlist's Tracks, Upload a Custom Playlist Cover Image


### PlaylistModifyPublic

Write access to a user's public playlists. 
Required For

Follow a Playlist, Unfollow a Playlist, Add Tracks to a Playlist

Change a Playlist's Details, Create a Playlist, Remove Tracks from a Playlist

Reorder a Playlist's Tracks, Replace a Playlist's Tracks, Upload a Custom Playlist Cover Image


### PlaylistReadCollaborative

Include collaborative playlists when requesting a user's playlists. 
Required For

Get a List of Current User's Playlists, Get a List of a User's Playlists


### PlaylistReadPrivate

Read access to user's private playlists. 
Required For

Check if Users Follow a Playlist, Get a List of Current User's Playlists, Get a List of a User's Playlists


### ReadAllAccess

Returns a new Scope ViewModel with all "read" scopes set to true Usage : ScopeViewModel.ReadAllAccess

### SpotifyConnectAll

Returns a new Scope ViewModel with all scopes within the confines of Spotify Connect set to true Usage : ScopeViewModel.SpotifyConnectAll

### UserGeneratedContentImageUpload

User Generated Content Image Upload 
Required For

Upload a Custom Playlist Cover Image


### UserReadBirthDate

Read access to the user's birthdate. 
Required For

Get Current User's Profile


### UserReadEmail

Read access to user’s email address. 
Required For

Get Current User's Profile


### UserReadPrivate

Read access to user’s subscription details (type of user account). 
Required For

Search for an Item, Get Current User's Profile


### UsersAll

Returns a new Scope ViewModel with all scopes within the confines of Users set to true Usage : Scope.UsersAll


## SectionViewModel

Section View Model

### Key

The estimated overall key of the section. The values in this field ranging from 0 to 11 mapping to pitches using standard Pitch Class notation

### KeyConfidence

The confidence, from 0.0 to 1.0, of the reliability of the key.

### Loudness

The overall loudness of the section in decibels (dB).

### Mode

Indicates the modality (major or minor) of a track, the type of scale from which its melodic content is derived.This field will contain a 0 for “minor”, a 1 for “major”, or a -1 for no result.

### ModeConfidence

The confidence, from 0.0 to 1.0, of the reliability of the mode.

### Tempo

The overall estimated tempo of the section in beats per minute (BPM).

### TempoConfidence

The confidence, from 0.0 to 1.0, of the reliability of the tempo.

### TimeSignature

An estimated overall time signature of a track. The time signature (meter) is a notational convention to specify how many beats are in each bar (or measure). The time signature ranges from 3 to 7 indicating time signatures of “3/4”, to “7/4”.

### TimeSignatureConfidence

The confidence, from 0.0 to 1.0, of the reliability of the time_signature.


## SegmentViewModel

Segment View Model

### LoudnessEnd

The offset loudness of the segment in decibels (dB).

### LoudnessMax

The peak loudness of the segment in decibels (dB).

### LoudnessMaxTime

The segment-relative offset of the segment peak loudness in seconds.

### LoudnessStart

The onset loudness of the segment in decibels (dB).

### Pitches

A “chroma” vector representing the pitch content of the segment, corresponding to the 12 pitch classes C, C#, D to B, with values ranging from 0 to 1 that describe the relative dominance of every pitch in the chromatic scale

### Timbre

Timbre is the quality of a musical note or sound that distinguishes different types of musical instruments, or voices.


## TimeIntervalViewModel

Time Interval View Model

### Confidence

The reliability confidence, from 0.0 to 1.0

### Duration

The duration in seconds

### Start

The starting point in seconds.


## ToggleFavouriteViewModel

Toggle Favourite View Model

### Command

Command

### Id

Id

### Value

Value


## ToggleFollowViewModel

Toggle Follow View Model

### Constructor(client, type, id)

Constructor

| Name | Description |
| ---- | ----------- |
| client | *Spotify.Uwp.ISpotifySdkClient*<br>Spotify SDK Client |
| type | *Spotify.Uwp.FollowType*<br>Follow Type |
| id | *System.String*<br>Id |

### Get

Get

### Id

Id

### Toggle(value)

Toggle

| Name | Description |
| ---- | ----------- |
| value | *System.Boolean*<br>Value |

### Value

Value


## TokenViewModel

Token View Model

### Error

Error

### Expiration

Token Expiration Date

### IsLoggedIn

Is Logged In

### IsValid

Is Valid

### Refresh

Refresh

### Scopes

Scopes

### Token

Access Token

### TokenType

Token Type


## TrackViewModel

Track View Model

### Album

The album on which the track appears.The album object includes a link in href to full information about the album.

### Artist

Artist View Model

### Artists

The artists who performed the track. Each artist object includes a link in href to more detailed information about the artist.

### AvailableMarkets

A list of the countries in which the track can be played, identified by their ISO 3166-1 alpha-2 code.

### DiscNumber

The disc number(usually 1 unless the album consists of more than one disc).

### Duration

The track length in milliseconds.

### ExternalId

Known external IDs for the track.

### IsExplicit

Whether or not the track has explicit lyrics ( true = yes it does; false = no it does not OR unknown).

### IsPlayable

Part of the response when Track Relinking is applied. If true , the track is playable in the given market. Otherwise false.

### Length

Track Length

### LinkedFrom

Part of the response when Track Relinking is applied and is only part of the response if the track linking, in fact, exists

### Popularity

The popularity of the track. The value will be between 0 and 100, with 100 being the most popular.

### Preview

A link to a 30 second preview(MP3 format) of the track.

### Restrictions

Part of the response when Track Relinking is applied, the original track is not available in the given market

### TrackNumber

The number of the track. If an album has several discs, the track number is the number on the specified disc.


## UserViewModel

User View Model

### DisplayName

The name displayed on the user’s profile. null if not available.

### Followers

Information about the followers of this user.

### IsPremium

Is Premium Subscription

### Product

The user’s Spotify subscription level: “premium”, “free”, etc. This field is only available when the current user has granted access to the user-read-private scope.
