using Microsoft.EntityFrameworkCore;
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

    public bool UserHasGame(string userId)
    {
        return _context.Games.Any(game => game.Speler.Id == userId);
    }

    public async Task<Game> GetCurrentGame(string userId)
    {
        int id = _context.Games.First(g => g.Speler.Id == userId).Id;
        return await _context.Games.FindAsync(id);
    }

    public async Task<int> DeleteActiveGame(int gameId)
    {
        _context.Games.Remove(_context.Games.Find(gameId));
        return await _context.SaveChangesAsync();
    }

    public async Task<string> AddGuessToLettersGuessed(int gameId, char guess, bool goodguess)
    {
        Game game = _context.Games.Where(g => g.Id == gameId).First();

        game.LettersGuessed += guess;
        game.Tries++;
        if (!goodguess)
        {
            game.WrongTries++;
        }
        _context.Update(game);
        await _context.SaveChangesAsync();
        return "";
    }

}
