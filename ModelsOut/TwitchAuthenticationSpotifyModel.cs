using System.Runtime.Serialization;

namespace Twitchbot.Services.Commands.ModelsOut
{
    [DataContract]
    public class Model
    {

        [DataMember(Name = "token")]
        public string Token { get; set; }
    }

    [DataContract]
    public class TwitchAuthenticationSpotifyModel
    {

        [DataMember(Name = "result")]
        public bool Result { get; set; }

        [DataMember(Name = "errorMessage")]
        public string ErrorMessage { get; set; }

        [DataMember(Name = "businessMessage")]
        public string BusinessMessage { get; set; }

        [DataMember(Name = "model")]
        public Model Model { get; set; }
    }
}