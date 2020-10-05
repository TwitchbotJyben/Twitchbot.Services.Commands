using AutoMapper;
using Twitchbot.Common.Base.Dao;
using Twitchbot.Common.Models.Data;
using Twitchbot.Common.Models.Domain.Models;

namespace Twitchbot.Services.Commands.Dao
{
    public class CommandsDao : BaseDao<Common.Models.Data.Entities.Commands, CommandsReadModel, CommandsCreateModel, CommandsUpdateModel>
    {
        public CommandsDao(TwitchbotContext dataContext, IMapper mapper) : base(dataContext, mapper) { }
    }
}