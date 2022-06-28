using Microsoft.AspNetCore.Mvc;
using System;
using WebGalgje.Entities;

namespace WebGalgje.DataAccess.Repositories;

public interface IWoordRepository
{
    Task<Woord> Add(Woord NewWoord);
    Task<IEnumerable<Woord>> GetAll();
    Boolean Contains(Woord NewWoord);
    Task<Woord> GetRandomWord();
}
