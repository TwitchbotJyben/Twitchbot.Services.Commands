using System.Threading.Tasks;
using Twitchbot.Common.Base.Models;
using Twitchbot.Common.Models.Domain.Models;
using Twitchbot.Services.Commands.ModelsIn;

namespace Twitchbot.Services.Commands.Interfaces
{
    public interface ISpotifyBusiness
    {
        Task<HttpResultModel<CommandsModel>> PerformResponseSpotify(CommandsReadModel commandsReadModel, int spotifyType);
    }
}