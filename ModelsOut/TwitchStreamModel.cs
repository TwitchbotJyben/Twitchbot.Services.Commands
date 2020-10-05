using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Twitchbot.Services.Commands.ModelsOut
{
    [DataContract]
    public class TwitchStreamModel
    {
        [DataMember(Name = "stream")]
        public Stream Stream { get; set; }
    }

    public class Preview
    {

        [DataMember(Name = "small")]
        public string Small { get; set; }

        [DataMember(Name = "medium")]
        public string Medium { get; set; }

        [DataMember(Name = "large")]
        public string Large { get; set; }

        [DataMember(Name = "template")]
        public string Template { get; set; }

    }

    public class Channel
    {
        [DataMember(Name = "mature")]
        public bool Mature { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "broadcaster_language")]
        public string Broadcaster_language { get; set; }

        [DataMember(Name = "broadcaster_software")]
        public string Broadcaster_software { get; set; }

        [DataMember(Name = "display_name")]
        public string Display_name { get; set; }

        [DataMember(Name = "game")]
        public string Game { get; set; }

        [DataMember(Name = "language")]
        public string Language { get; set; }

        [DataMember(Name = "_id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "created_at")]
        public DateTime Created_at { get; set; }

        [DataMember(Name = "updated_at")]
        public DateTime Updated_at { get; set; }

        [DataMember(Name = "partner")]
        public bool Partner { get; set; }

        [DataMember(Name = "logo")]
        public string Logo { get; set; }

        [DataMember(Name = "video_banner")]
        public string Video_banner { get; set; }

        [DataMember(Name = "profile_banner")]
        public string Profile_banner { get; set; }

        [DataMember(Name = "profile_banner_background_color")]
        public string Profile_banner_background_color { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "views")]
        public int Views { get; set; }

        [DataMember(Name = "followers")]
        public int Followers { get; set; }

        [DataMember(Name = "broadcaster_type")]
        public string Broadcaster_type { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "private_video")]
        public bool Private_video { get; set; }

        [DataMember(Name = "privacy_options_enabled")]
        public bool Privacy_options_enabled { get; set; }

    }

    public class Stream
    {
        [DataMember(Name = "_id")]
        public int Id { get; set; }

        [DataMember(Name = "game")]
        public string Game { get; set; }

        [DataMember(Name = "broadcast_platform")]
        public string Broadcast_platform { get; set; }

        [DataMember(Name = "community_id")]
        public string Community_id { get; set; }

        [DataMember(Name = "community_ids")]
        public IList<object> Community_ids { get; set; }

        [DataMember(Name = "viewers")]
        public int Viewers { get; set; }

        [DataMember(Name = "video_height")]
        public int Video_height { get; set; }

        [DataMember(Name = "average_fps")]
        public int Average_fps { get; set; }

        [DataMember(Name = "delay")]
        public int Delay { get; set; }

        [DataMember(Name = "created_at")]
        public DateTime Created_at { get; set; }

        [DataMember(Name = "is_playlist")]
        public bool Is_playlist { get; set; }

        [DataMember(Name = "stream_type")]
        public string Stream_type { get; set; }

        [DataMember(Name = "preview")]
        public Preview Preview { get; set; }

        [DataMember(Name = "channel")]
        public Channel Channel { get; set; }
    }
}