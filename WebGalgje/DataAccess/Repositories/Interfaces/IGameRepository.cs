using WebGalgje.Entities;

namespace WebGalgje.DataAccess.Repositories;
public interface IGameRepository
{
    Task<Game> Add(Game newGame);
    bool UserHasGame(string userId);
    Task<Game> GetCurrentGame(string userId);
    Task<int> DeleteActiveGame(int gameId);   
    Task<string> AddGuessToLettersGuessed(int gameId, char input, bool goodguess);


}
