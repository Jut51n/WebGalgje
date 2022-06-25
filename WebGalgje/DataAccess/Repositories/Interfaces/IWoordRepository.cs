using WebGalgje.Entities;

namespace WebGalgje.DataAccess.Repositories;

public interface IWoordRepository
{
    Task<Woord> Add(Woord NewWoord);
    Task<IEnumerable<Woord>> GetAll();
}
