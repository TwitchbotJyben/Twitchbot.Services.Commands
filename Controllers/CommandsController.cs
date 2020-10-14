using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Twitchbot.Common.Base.Models;
using Twitchbot.Common.Models.Domain.Models;
using Twitchbot.Services.Commands.Business;
using Twitchbot.Services.Commands.Interfaces;
using Twitchbot.Services.Commands.ModelsIn;

namespace Twitchbot.Services.Commands.Controllers
{
    [Route("api/Commands")]
    public class CommandsController : ControllerBase
    {
        private readonly ILogger<CommandsController> _logger;
        private readonly ICommandsBusiness _commandsBusiness;
        private readonly IStringLocalizer<CommandsController> _localizer;

        public CommandsController(ILogger<CommandsController> logger,
            ICommandsBusiness commandsBusiness, IStringLocalizer<CommandsController> localizer)
        {
            _logger = logger;
            _commandsBusiness = commandsBusiness;
            _localizer = localizer;
        }

        [HttpGet("/response")]
        public async Task<ActionResult<HttpResultModel<CommandsModel>>> Get(CancellationToken cancellationToken, string commandName)
        {
            _logger.LogInformation("Get command response for : {0}", commandName);

            var resultCommandsReadModel = await _commandsBusiness.GetCommand(commandName, cancellationToken);

            if (!resultCommandsReadModel.Any())
            {
                return NotFound();
            }

            return await _commandsBusiness.GetCommandResponse(resultCommandsReadModel.First());
        }

        [HttpGet("/list")]
        public async Task<ActionResult<HttpResultModel<List<CommandsReadModel>>>> Get(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get commands");

            var resultCommandsReadModel = await _commandsBusiness.GetCommands(cancellationToken);

            if (!resultCommandsReadModel.Any())
            {
                return NotFound();
            }

            return new HttpResultModel<List<CommandsReadModel>>()
            {
                Result = true,
                    BusinessMessage = _localizer["Liste des commandes récupérée."],
                    Model = resultCommandsReadModel.ToList()
            };
        }

        [HttpPost("/command")]
        public async Task<ActionResult<HttpResultModel<CommandsReadModel>>> Post(CancellationToken cancellationToken, CommandsCreateModel commandsCreateModel)
        {
            _logger.LogInformation("Create command {0}", commandsCreateModel.Name);

            var resultCommandsReadModel = await _commandsBusiness.CreateCommand(cancellationToken, commandsCreateModel);

            return new HttpResultModel<CommandsReadModel>()
            {
                Result = true,
                    BusinessMessage = _localizer["Commande créée avec succès."],
                    Model = resultCommandsReadModel
            };
        }

        [HttpPut("/command")]
        public async Task<ActionResult<HttpResultModel<CommandsReadModel>>> Put(CancellationToken cancellationToken, CommandsUpdateModel commandsUpdateModel)
        {
            _logger.LogInformation("Update command {0} - {1}", commandsUpdateModel.Id, commandsUpdateModel.Name);

            var resultCommandsReadModel = await _commandsBusiness.UpdateCommand(cancellationToken, commandsUpdateModel);

            return new HttpResultModel<CommandsReadModel>()
            {
                Result = true,
                    BusinessMessage = _localizer["Commande mise à jour avec succès."],
                    Model = resultCommandsReadModel
            };
        }

        [HttpDelete("/command")]
        public async Task<ActionResult<HttpResultModel<CommandsReadModel>>> Delete(CancellationToken cancellationToken, int id)
        {
            _logger.LogInformation("Delete command {0}", id);

            var resultCommandsReadModel = await _commandsBusiness.DeleteCommand(cancellationToken, id);

            return new HttpResultModel<CommandsReadModel>()
            {
                Result = true,
                    BusinessMessage = _localizer["Commande supprimée avec succès."],
                    Model = resultCommandsReadModel
            };
        }
    }
}