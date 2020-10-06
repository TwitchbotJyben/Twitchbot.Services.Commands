using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Twitchbot.Common.Base.Models;
using Twitchbot.Common.Models.Data.Enums;
using Twitchbot.Common.Models.Domain.Models;
using Twitchbot.Services.Commands.Dao;
using Twitchbot.Services.Commands.Interfaces;
using Twitchbot.Services.Commands.Interfaces.Enums;
using Twitchbot.Services.Commands.ModelsIn;

namespace Twitchbot.Services.Commands.Business
{
    public class CommandsBusiness : ICommandsBusiness
    {
        private readonly ILogger<CommandsBusiness> _logger;
        private readonly IStringLocalizer<CommandsBusiness> _localizer;
        private readonly CommandsDao _commandsDao;
        private readonly IUptimeBusiness _uptimeBusiness;
        private readonly ISpotifyBusiness _spotifyBusiness;

        public CommandsBusiness(ILogger<CommandsBusiness> logger, ISpotifyBusiness spotifyBusiness,
            IStringLocalizer<CommandsBusiness> localizer, CommandsDao commandsDao, IUptimeBusiness uptimeBusiness)
        {
            _logger = logger;
            _localizer = localizer;
            _commandsDao = commandsDao;
            _uptimeBusiness = uptimeBusiness;
            _spotifyBusiness = spotifyBusiness;
        }

        public async Task<HttpResultModel<CommandsModel>> GetCommandResponse(CommandsReadModel commandsReadModel)
        {
            var result = new HttpResultModel<CommandsModel>();

            _logger.LogInformation("Command type id : {0}", commandsReadModel.TypeId);

            switch (commandsReadModel.TypeId)
            {
                case (int) CommandsType.Automatic:
                case (int) CommandsType.Response:
                    result = PerformResponseModel(commandsReadModel);
                    break;
                case (int) CommandsType.Mystery:
                    break;
                case (int) CommandsType.Playlist:
                    result = await _spotifyBusiness.PerformResponseSpotify(commandsReadModel, (int) SpotifyTypeEnum.PLAYLIST);
                    break;
                case (int) CommandsType.Spotify:
                    result = await _spotifyBusiness.PerformResponseSpotify(commandsReadModel, (int) SpotifyTypeEnum.MUSIC);
                    break;
                case (int) CommandsType.Uptime:
                    result = await _uptimeBusiness.PerformResponseUptime(commandsReadModel);
                    break;
                default:
                    break;
            }

            return result;
        }

        public async Task<IReadOnlyList<CommandsReadModel>> GetCommand(string commandName, CancellationToken cancellationToken)
        {
            return await _commandsDao.QueryModel(x => x.Name == commandName, cancellationToken);
        }

        public async Task<IReadOnlyList<CommandsReadModel>> GetCommands(CancellationToken cancellationToken)
        {
            return await _commandsDao.QueryModel(x => true, cancellationToken);
        }

        public async Task<CommandsReadModel> UpdateCommand(CancellationToken cancellationToken, CommandsUpdateModel commandsUpdateModel)
        {
            return await _commandsDao.UpdateModel(commandsUpdateModel.Id, commandsUpdateModel, cancellationToken);
        }

        public async Task<CommandsReadModel> CreateCommand(CancellationToken cancellationToken, CommandsCreateModel commandsCreateModel)
        {
            return await _commandsDao.CreateModel(commandsCreateModel, cancellationToken);
        }

        public async Task<CommandsReadModel> DeleteCommand(CancellationToken cancellationToken, int id)
        {
            return await _commandsDao.DeleteModel(id, cancellationToken);
        }

        private HttpResultModel<CommandsModel> PerformResponseModel(CommandsReadModel commandsReadModel)
        {
            var resultCommandResponse = new HttpResultModel<CommandsModel>();

            var commandsModel = new CommandsModel()
            {
                Response = commandsReadModel.Response
            };

            resultCommandResponse.PerformResult(true, "", _localizer["Réponse trouvée."], commandsModel);
            return resultCommandResponse;
        }
    }
}