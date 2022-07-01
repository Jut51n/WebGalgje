using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebGalgje.DataAccess.Repositories;
using WebGalgje.Entities;
using WebGalgje.Utilities;

namespace WebGalgje.Pages
{
    public class PlayModel : PageModel
    {
        [BindProperty]
        public GameInput Input { get; set; }
        public Game Game { get; set; }
        public string Status = "Active";
        public string UserMessage = "";
        public string Pogingen = "";
        public string GoodGuesses = "";
        public string GalgjeImage = "";

        private UserManager<Player> _playerManager;
        public IGameRepository GameRepository { get; set; }
        public IStatsRepository StatRepository { get; set; }

        public PlayModel(UserManager<Player> userManager, IGameRepository gameRepository, IStatsRepository statrepo)
        {
            _playerManager = userManager;
            GameRepository = gameRepository;
            StatRepository = statrepo;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!await UserHasGame(await GetPlayerId()))
            {
                return Redirect("/Galgje");
            }

            Game = await GetCurrentGame(await GetPlayerId());

            if (RouteData.Values["action"] != null)
            {
                if (RouteData.Values["action"].ToString() == "WrapUp")
                {
                    return Redirect("/Galgje/Delete");
                }
                return Redirect("/Galgje");//Voor straf terug naar af
            }
            return RenderCommunicatie();
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

        public async Task<bool> UserHasGame(string userid)
        {
            return GameRepository.UserHasGame(userid);
        }

        public async Task<Game> GetCurrentGame(string userid)
        {
            return await GameRepository.GetCurrentGame(userid);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Game = await GetCurrentGame(await GetPlayerId());

            if (ModelState.IsValid)
            {
                char input = Input.ReturnGuessAsChar();

                if (!Game.LettersGuessed.Contains(input))
                {
                    bool GoodGuess = Game.WordToGuess.Contains(input);
                    await GameRepository.AddGuessToLettersGuessed(Game.Id, input, GoodGuess);

                    if (GoodGuess)
                    {
                        UserMessage = $"Ja, de {input} zit er in";
                        if (RenderGoodGuesses() == Game.WordToGuess)
                        {
                            UserMessage = "Je hebt gewonnen!";
                            Status = "Done";
                            await TurnGameToStat(won: true);
                        }
                    }
                    else
                    {
                        UserMessage = $"De {input} zit er niet in";
                        if (Game.WrongTries == 8)
                        {
                            UserMessage = "Je hebt verloren";
                            Status = "Done";
                            await TurnGameToStat(won: false);     
                        }
                    }
                }
                else
                {
                    UserMessage = "Die heb je al gebrobeerd!";
                }
            }   
            return RenderCommunicatie();
        }

        public IActionResult RenderCommunicatie()
        {
            Pogingen = PogingMessage();
            GoodGuesses =  RenderGoodGuesses();
            GalgjeImage = GetImage();
            return Page();
        }

        public async Task<Stat> TurnGameToStat(bool won)
        {
            return await StatRepository.AddStatFromGame(Game, won, await _playerManager.GetUserAsync(User));
        }

        public string PogingMessage()
        {
            if (Game == null || Game.Tries == 0)
            {
                return "Probeer het maar te raden.";
            }
            else
            {
                return $"Letters geprobeerd: {Game.Tries}";
            }
        }

        public string RenderGoodGuesses()
        {
            string guessed = "";
            foreach (char letter in Game.WordToGuess)
            {
                if (Game.LettersGuessed.Contains(letter))
                    guessed += letter.ToString();
                else
                    guessed += ".";
            }
            return guessed;
        }

        public string GetImage()
        {
            return $"<img src='/images/Galgje{Game.WrongTries}.png' alt='1'>";
        }
    }
}
