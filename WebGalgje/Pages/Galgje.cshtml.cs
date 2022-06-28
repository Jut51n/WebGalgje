using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebGalgje.DataAccess.Repositories;
using WebGalgje.Entities;

namespace WebGalgje.Pages
{
    public class GalgjeModel : PageModel
    {
        [BindProperty]
        public Game Game { get; set; }

        public string Action = "";

        private UserManager<Player> _playerManager;
        public IWoordRepository WoordRepository { get; set; }
        public IGameRepository GameRepository { get; set; }

        public GalgjeModel(UserManager<Player> userManager, IWoordRepository woordRepository, IGameRepository gameRepository)
        {
            _playerManager = userManager;
            WoordRepository = woordRepository;
            GameRepository = gameRepository;
        }

        public async Task<Player> getPlayer()
        {
            var user = await _playerManager.GetUserAsync(User);
            return user;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (RouteData.Values["action"] != null)
                Action = RouteData.Values["action"].ToString();

            switch (Action)
            {
                case "NewGame":
                    Game =  await SetupNewGame();
                    return RedirectToPage("Galgje");
                default:
                    return Page();

            }
        }

        public async Task<Game> SetupNewGame()
        {
            var word = await WoordRepository.GetRandomWord();
            var player = await getPlayer();
            Game game = new Game();
         
            game.WordToGuess = word.Word;
            game.Speler =  player;
            game.Tries = 0;
            game.LettersGuessed = "";
            game.WrongTries = 0;

            return await GameRepository.Add(game);
        }
    }
}
