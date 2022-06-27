using Microsoft.AspNetCore.Mvc;
using WebGalgje.DataAccess.Repositories;

namespace WebGalgje.api;

[ApiController]
[Route("Api/Stats")]

public class StatsApi : ControllerBase
{

    private IStatsRepository _statsRepo;

    public StatsApi(IStatsRepository statsrepo)
    {
        _statsRepo = statsrepo;
    }

    //[HttpGet]
    //Haal alle stats op

    //[HttpPost]
    //Statistieken van game naar stats wegschrijven


}
