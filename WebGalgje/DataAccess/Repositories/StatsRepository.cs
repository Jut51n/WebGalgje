using WebGalgje.Entities;

namespace WebGalgje.DataAccess.Repositories;

public class StatsRepository : IStatsRepository
{
    private GalgContext _context;

    public StatsRepository(GalgContext context)
    {
        _context = context;
    }

    public async Task<Stat> AddStatFromGame(Game game, bool won, Player speler)
    {
        Stat newStat = new Stat();
        newStat.PlayDate = DateTime.Now;
        newStat.Won = won;
        newStat.Tries = game.Tries;
        newStat.WrongLettersGuessed = game.WrongTries;
        newStat.Speler = speler;

        _context.Stats.Add(newStat);
        await _context.SaveChangesAsync();
        return newStat;
    }
}
