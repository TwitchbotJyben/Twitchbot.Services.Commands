using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using TimeZoneConverter;
using Twitchbot.Common.Base.Client;
using Twitchbot.Common.Base.Models;
using Twitchbot.Common.Models.Domain.Models;
using Twitchbot.Services.Commands.ModelsIn;
using Twitchbot.Services.Commands.ModelsOut;

namespace Twitchbot.Services.Commands.Business
{
    public class UptimeBusiness
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UptimeBusiness> _logger;
        private readonly IStringLocalizer<UptimeBusiness> _localizer;
        private readonly string _clientId;
        private readonly ClientBase _client;

        public UptimeBusiness(IConfiguration configuration, ILogger<UptimeBusiness> logger,
            IStringLocalizer<UptimeBusiness> localizer, ClientBase client)
        {
            _configuration = configuration;
            _logger = logger;
            _localizer = localizer;
            _clientId = _configuration["TwitchClientId"];
            _client = client;
        }

        internal async Task<HttpResultModel<CommandsModel>> PerformResponseUptime(CommandsReadModel commandsReadModel)
        {
            var resultCommandResponse = new HttpResultModel<CommandsModel>();
            var resultStreamInfo = await GetStreamInformations();

            if (resultStreamInfo.Result && resultStreamInfo.Model.Stream != null)
            {
                var stream = resultStreamInfo.Model.Stream;
                var nowEuw = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TZConvert.WindowsToIana("Romance Standard Time"));
                var createdAtEuw = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(Convert.ToDateTime(stream.Created_at), TZConvert.WindowsToIana("Romance Standard Time"));
                var timeStampDiff = nowEuw - createdAtEuw;
                var message = $"{timeStampDiff.Hours}h:{timeStampDiff.Minutes}m:{timeStampDiff.Seconds}s";

                var model = new CommandsModel()
                {
                    Response = commandsReadModel.Response.Replace("{uptime}", message)
                };

                resultCommandResponse.PerformResult(true, "", _localizer["Uptime calculé."], model);
                return resultCommandResponse;
            }
            else
            {
                resultCommandResponse.PerformResult(false, _localizer["Impossible de calculer l'uptime."], "", null);
                return resultCommandResponse;
            }
        }

        private async Task<HttpResultModel<TwitchStreamModel>> GetStreamInformations()
        {
            _logger.LogInformation("Récupération des informations du stream");

            var url = _configuration["ApiUrl:Twitch:StreamInfo"].Replace("{streamId}", _configuration["ApiParams:Twitch:StreamId"]);
            var headers = new Dictionary<string, string> { { "Client-ID", _clientId }, { "Accept", _configuration["ApiParams:Twitch:TwitchAccept"] } };
            var result = await _client.PerformRequest<TwitchStreamModel>(url, HttpMethod.Get, null, headers);

            return result;
        }
    }
}