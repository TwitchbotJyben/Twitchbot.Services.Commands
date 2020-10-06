using System.Threading.Tasks;
using Twitchbot.Common.Base.Models;
using Twitchbot.Common.Models.Domain.Models;
using Twitchbot.Services.Commands.ModelsIn;

namespace Twitchbot.Services.Commands.Interfaces
{
    public interface IUptimeBusiness
    {
        Task<HttpResultModel<CommandsModel>> PerformResponseUptime(CommandsReadModel commandsReadModel);
    }
}