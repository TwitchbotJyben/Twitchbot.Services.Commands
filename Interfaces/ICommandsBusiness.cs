using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Twitchbot.Common.Base.Models;
using Twitchbot.Common.Models.Domain.Models;
using Twitchbot.Services.Commands.ModelsIn;

namespace Twitchbot.Services.Commands.Interfaces
{
    public interface ICommandsBusiness
    {
        Task<HttpResultModel<CommandsModel>> GetCommandResponse(CommandsReadModel commandsReadModel);
        Task<IReadOnlyList<CommandsReadModel>> GetCommand(string commandName, CancellationToken cancellationToken);
        Task<IReadOnlyList<CommandsReadModel>> GetCommands(CancellationToken cancellationToken);
        Task<CommandsReadModel> UpdateCommand(CancellationToken cancellationToken, CommandsUpdateModel commandsUpdateModel);
        Task<CommandsReadModel> CreateCommand(CancellationToken cancellationToken, CommandsCreateModel commandsCreateModel);
        Task<CommandsReadModel> DeleteCommand(CancellationToken cancellationToken, int id);
    }
}