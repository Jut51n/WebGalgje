using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using WebGalgje.DataAccess.Repositories;
using WebGalgje.Entities;
using WebGalgje.Utilities;

namespace WebGalgje.Pages
{
    public class GalgjeModel : PageModel
    {
        [BindProperty]
        public Game Game { get; set; }

        public string Action = "";
        public string Link = "";
        public string Status = "";

        private UserManager<Player> _playerManager;
        public IWoordRepository WoordRepository { get; set; }
        public IGameRepository GameRepository { get; set; }

        public GalgjeModel(UserManager<Player> userManager, IWoordRepository woordRepository, IGameRepository gameRepository)
        {
            _playerManager = userManager;
            WoordRepository = woordRepository;
            GameRepository = gameRepository;
        }

        public async Task<string> GetPlayerId()
        {
            if (!HttpContext.Session.Keys.Contains("Player"))
            {
                var getPlayer = await _playerManager.GetUserAsync(User);
                HttpContext.Session.Set("Player", getPlayer.Id);
            }
            return HttpContext.Session.Get<string>("Player");
        }

        public async Task<IActionResult> OnGetAsync()
        {
            
            if (RouteData.Values["action"] != null)
                Action = RouteData.Values["action"].ToString();

            switch (Action)
            {
                case "NewGame":
                    Game =  await SetupNewGame();
                    return Redirect("/Play");
                case "Delete":
                    Game = await GetCurrentGame(await GetPlayerId());
                    await DeleteCurrentGame(Game.Id);
                    return Redirect("/Galgje");
                default:
                    if( await UserHasGame(await GetPlayerId()))
                    {
                        Link = "<a href='/Play'>Verder spelen</a>";
                    }
                    else
                    {
                        Link = "<a href='/Galgje/NewGame'>Start nieuwe game</a>";
                    }
                    return Page();
            }
        }

            public async Task<Game> SetupNewGame()
        {
            var word = await WoordRepository.GetRandomWord();
            var player = await _playerManager.GetUserAsync(User);
            Game game = new Game();
         
            game.WordToGuess = word.Word;
            game.Speler =  player;
            game.Tries = 0;
            game.LettersGuessed = "";
            game.WrongTries = 0;

            return await GameRepository.Add(game);
        }

        public async Task<bool> UserHasGame(string userid)
        {
            return GameRepository.UserHasGame(userid);
        }

        public async Task<Game> GetCurrentGame(string userid)
        {
            return await GameRepository.GetCurrentGame(userid);
        }

        public async Task<int> DeleteCurrentGame(int gameId)
        {
            var delete =  await GameRepository.DeleteActiveGame(gameId);
            return delete;
        }
    }
}
