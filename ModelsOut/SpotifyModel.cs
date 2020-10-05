using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Twitchbot.Services.Commands.ModelsOut
{

    [DataContract]
    public class SpotifyModel
    {

        [DataMember(Name = "timestamp")]
        public long Timestamp { get; set; }

        [DataMember(Name = "context")]
        public Context Context { get; set; }

        [DataMember(Name = "progress_ms")]
        public int ProgressMs { get; set; }

        [DataMember(Name = "item")]
        public Item Item { get; set; }

        [DataMember(Name = "currently_playing_type")]
        public string CurrentlyPlayingType { get; set; }

        [DataMember(Name = "actions")]
        public Actions Actions { get; set; }

        [DataMember(Name = "is_playing")]
        public bool IsPlaying { get; set; }
    }

    [DataContract]
    public class Context
    {
        [DataMember(Name = "external_urls")]
        public ExternalUrls ExternalUrls { get; set; }
    }

    [DataContract]
    public class ExternalUrls
    {

        [DataMember(Name = "spotify")]
        public string Spotify { get; set; }
    }

    [DataContract]
    public class Image
    {

        [DataMember(Name = "height")]
        public int Height { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "width")]
        public int Width { get; set; }
    }

    [DataContract]
    public class Album
    {

        [DataMember(Name = "album_type")]
        public string AlbumType { get; set; }

        [DataMember(Name = "artists")]
        public IList<Artist> Artists { get; set; }

        [DataMember(Name = "available_markets")]
        public IList<string> AvailableMarkets { get; set; }

        [DataMember(Name = "external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [DataMember(Name = "href")]
        public string Href { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "images")]
        public IList<Image> Images { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "release_date")]
        public string ReleaseDate { get; set; }

        [DataMember(Name = "release_date_precision")]
        public string ReleaseDatePrecision { get; set; }

        [DataMember(Name = "total_tracks")]
        public int TotalTracks { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "uri")]
        public string Uri { get; set; }
    }

    [DataContract]
    public class Artist
    {

        [DataMember(Name = "external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [DataMember(Name = "href")]
        public string Href { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "uri")]
        public string Uri { get; set; }
    }

    [DataContract]
    public class ExternalIds
    {

        [DataMember(Name = "isrc")]
        public string Isrc { get; set; }
    }

    [DataContract]
    public class Item
    {

        [DataMember(Name = "album")]
        public Album Album { get; set; }

        [DataMember(Name = "artists")]
        public IList<Artist> Artists { get; set; }

        [DataMember(Name = "available_markets")]
        public IList<string> AvailableMarkets { get; set; }

        [DataMember(Name = "disc_number")]
        public int DiscNumber { get; set; }

        [DataMember(Name = "duration_ms")]
        public int DurationMs { get; set; }

        [DataMember(Name = "explicit")]
        public bool Explicit { get; set; }

        [DataMember(Name = "external_ids")]
        public ExternalIds ExternalIds { get; set; }

        [DataMember(Name = "external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [DataMember(Name = "href")]
        public string Href { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "is_local")]
        public bool IsLocal { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "popularity")]
        public int Popularity { get; set; }

        [DataMember(Name = "preview_url")]
        public string PreviewUrl { get; set; }

        [DataMember(Name = "track_number")]
        public int TrackNumber { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "uri")]
        public string Uri { get; set; }
    }

    [DataContract]
    public class Disallows
    {

        [DataMember(Name = "resuming")]
        public bool Resuming { get; set; }
    }

    [DataContract]
    public class Actions
    {

        [DataMember(Name = "disallows")]
        public Disallows Disallows { get; set; }
    }
}