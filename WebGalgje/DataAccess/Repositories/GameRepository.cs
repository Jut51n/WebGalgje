using WebGalgje.Entities;

namespace WebGalgje.DataAccess.Repositories;

public class GameRepository : IGameRepository
{
    private GalgContext _context;

    public GameRepository(GalgContext context)
    {
        _context = context;
    }

    public async Task<Game> Add(Game newGame)
    {
        _context.Games.Add(newGame);
        await _context.SaveChangesAsync();
        return newGame;
    }
}
