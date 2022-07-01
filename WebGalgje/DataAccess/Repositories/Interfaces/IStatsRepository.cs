using WebGalgje.Entities;

namespace WebGalgje.DataAccess.Repositories;

public interface IStatsRepository
{

    Task<Stat> AddStatFromGame(Game game, bool won, Player speler);
}
