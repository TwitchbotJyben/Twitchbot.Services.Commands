using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Twitchbot.Common.Base.Client;
using Twitchbot.Common.Base.Models;
using Twitchbot.Common.Models.Domain.Models;
using Twitchbot.Services.Commands.Interfaces;
using Twitchbot.Services.Commands.Interfaces.Enums;
using Twitchbot.Services.Commands.ModelsIn;
using Twitchbot.Services.Commands.ModelsOut;

namespace Twitchbot.Services.Commands.Business
{
    public class SpotifyBusiness : ISpotifyBusiness
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<SpotifyBusiness> _logger;
        private readonly IStringLocalizer<SpotifyBusiness> _localizer;
        private readonly ClientBase _client;

        public SpotifyBusiness(IConfiguration configuration, ILogger<SpotifyBusiness> logger,
            IStringLocalizer<SpotifyBusiness> localizer, ClientBase client)
        {
            _configuration = configuration;
            _logger = logger;
            _localizer = localizer;
            _client = client;
        }

        public async Task<HttpResultModel<CommandsModel>> PerformResponseSpotify(CommandsReadModel commandsReadModel, int spotifyType)
        {
            var resultCommandResponse = new HttpResultModel<CommandsModel>();
            var resultSpotifyCurrentlyPlaying = await SpotifyCurrentlyPlaing();

            if (resultSpotifyCurrentlyPlaying.Result)
            {
                var model = new CommandsModel();

                if (resultSpotifyCurrentlyPlaying.Model != null && resultSpotifyCurrentlyPlaying.Model.IsPlaying)
                {
                    if (spotifyType == (int) SpotifyTypeEnum.MUSIC)
                    {
                        model.Response = commandsReadModel.Response.Replace("{music}", resultSpotifyCurrentlyPlaying.Model.Item.Name).Replace("{artist}", resultSpotifyCurrentlyPlaying.Model.Item.Artists[0].Name);
                    }
                    else if (spotifyType == (int) SpotifyTypeEnum.PLAYLIST)
                    {
                        if (resultSpotifyCurrentlyPlaying.Model.Context != null && resultSpotifyCurrentlyPlaying.Model.Context.ExternalUrls.Spotify != string.Empty)
                        {
                            model.Response = commandsReadModel.Response.Replace("{playlist}", resultSpotifyCurrentlyPlaying.Model.Context.ExternalUrls.Spotify);
                        }
                        else
                        {
                            model.Response = _localizer["Pas de playlist associée au titre en cours."];
                        }
                    }
                }
                else
                {
                    model.Response = _localizer["Pas d'écoute Spotify en cours."];
                }

                resultCommandResponse.PerformResult(true, "", _localizer["Commande music récupérée."], model);
                return resultCommandResponse;
            }
            else
            {
                resultCommandResponse.PerformResult(false, _localizer["Impossible de récupérer les infos de la musique Spotify."], "", null);
                return resultCommandResponse;
            }
        }

        private async Task<HttpResultModel<SpotifyModel>> SpotifyCurrentlyPlaing()
        {
            _logger.LogInformation("Récupération des informations de la musique Spotify");

            var getSpotifyTokenResult = await GetSpotifyToken();

            if (getSpotifyTokenResult.Result && getSpotifyTokenResult.Model != null)
            {
                var url = _configuration["ApiUrl:Spotify:Playlist"];
                var headers = new Dictionary<string, string> { { "Bearer", getSpotifyTokenResult.Model.Token } };
                return await _client.PerformRequest<SpotifyModel>(url, HttpMethod.Get, null, headers);
            }
            else
            {
                return new HttpResultModel<SpotifyModel>
                {
                    Result = false,
                };
            }
        }

        private async Task<TwitchAuthenticationSpotifyModel> GetSpotifyToken()
        {
            _logger.LogInformation("Récupération du token Spotify");

            var url = _configuration["ApiUrl:TwitchbotAuthentication:Spotify"] + $"/?clientId={_configuration["TwitchUserSecret"]}";
            var result = await _client.PerformRequest<TwitchAuthenticationSpotifyModel>(url, HttpMethod.Get);

            return result.Model;
        }
    }
}