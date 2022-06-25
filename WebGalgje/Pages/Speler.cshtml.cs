using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebGalgje.Entities;

namespace WebGalgje.Pages
{
    public class SpelerModel : PageModel
    {

        private UserManager<Player> _playerManager;
        private SignInManager<Player> _signInManager;

        public SpelerModel(UserManager<Player> userManager, SignInManager<Player> signInManager)
        {
            _playerManager = userManager;
            _signInManager = signInManager;
        }

        public async Task OnGetAsync()
        {
            string action = RouteData.Values["action"].ToString();

            switch (action)
            {
                case "Registreer":
                    await RegisterPlayer();
                    break;
                case "Login":
                    await LoginPlayer();
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
            return Page();
        }
    }
}
