using Microsoft.AspNetCore.Mvc;
using WebGalgje.Repositories;

namespace WebGalgje.api;

[ApiController]
[Route("api/game")]
public class GameApi : ControllerBase
{

    private IGameRepository _gameRepo;

    public GameApi(IGameRepository gamerepo)
    {
        _gameRepo = gamerepo;
    }

    //[HttpGet]
    //Haal status van game op?


   // [HttpPost]
    //Stuur status verandering

}