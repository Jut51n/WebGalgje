using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebGalgje.Entities;

namespace WebGalgje.Pages
{
    public class SpelerModel : PageModel
    {
        public string Status { get; set; }

        private UserManager<Player> _playerManager;
        private SignInManager<Player> _signInManager;

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
                    //await RegisterPlayer();
                    break;
                case "Login":
                    Status = "Inloggen";
                    //await LoginPlayer();
                    break;
                case "SignOut":
                    Status = "Outsignen";
                    //await SignOutPlayer();
                    break;
                default:
                    Status = "default";
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
            return Page();
        }
    }
}
