using WebGalgje.Entities;

namespace WebGalgje.DataAccess.Repositories;
public interface IGameRepository
{
    Task<Game> Add(Game NewGame);

}
