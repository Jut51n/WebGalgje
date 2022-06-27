using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebGalgje.Entities;

namespace WebGalgje.DataAccess.Repositories;

public class WoordRepository : IWoordRepository
{
    private GalgContext _context;

    public WoordRepository(GalgContext context)
    {
        _context = context;
    }

    public async Task<Woord> Add(Woord newWoord)
    {   
            _context.Words.Add(newWoord);
            await _context.SaveChangesAsync();
            return newWoord;
    }

    public async Task<IEnumerable<Woord>> GetAll()
    {
        return await _context.Words.ToListAsync();
    }

    public Boolean Contains (Woord newWoord)
    {
        var result = _context.Words.Any(c => c.Word.Equals(newWoord.Word));
        return result;
    }
}
