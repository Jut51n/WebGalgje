using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebGalgje.Entities;

namespace WebGalgje.Pages
{
    public class SpelerModel : PageModel
    {
        public string Status { get; set; }
        public string FormAction =  "< a href='/Speler/Registreer'>Registreer</a><br/><a href = '/Speler/Login' > login </ a >< br />";

        private UserManager<Player> _playerManager;
        private SignInManager<Player> _signInManager;

        public FormUser userInput { get; set; } // binding

        public SpelerModel(UserManager<Player> userManager, SignInManager<Player> signInManager)
        {
            _playerManager = userManager;
            _signInManager = signInManager;
        }

        public async Task OnGetAsync()
        {
            string action = "";
            if (RouteData.Values["action"] != null)
                action = RouteData.Values["action"].ToString();

            switch (action)
            {
                case "Registreer":
                    Status = "registreren";
                    FormAction = ;

                    //await RegisterPlayer();
                    break;
                case "Login":
                    Status = "Inloggen";
                    //await LoginPlayer();
                    break;
                case "SignOut":
                    await SignOutPlayer();
                    break;
                default:
                    break;
            }
        }

        public async Task RegisterPlayer()
        {

        }

        public async Task LoginPlayer()
        {

        }


        public async Task<IActionResult> SignOutPlayer()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage();
        }
    }
}
