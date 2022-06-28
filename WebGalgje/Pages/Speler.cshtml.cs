using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebGalgje.Entities;

namespace WebGalgje.Pages
{
    public class SpelerModel : PageModel
    {
        [BindProperty]
        public FormUser UserInput { get; set; } // binding

        public string Status { get; set; }
        public  string Action = "";
        
        private UserManager<Player> _playerManager;
        private SignInManager<Player> _signInManager;

        public SpelerModel(UserManager<Player> userManager, SignInManager<Player> signInManager)
        {
            _playerManager = userManager;
            _signInManager = signInManager;
        }

        public async Task OnGetAsync()
        {    
            if (RouteData.Values["action"] != null)
                Action = RouteData.Values["action"].ToString();

            switch (Action)
            {
                case "Playerinfo":
                    Status = "info";
                    break;
                case "Signout":
                    await SignOutPlayer();
                    break;
                default:
                    break;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(UserInput.Action == "Registreer" && UserInput.UserName != null)
            {

                if(await _playerManager.FindByNameAsync(UserInput.UserName) != null)
                {
                    ModelState.AddModelError("UserNameBezet", "Mysterieuze foutmelding voor username");
                }
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                await RegisterPlayer(UserInput);
            } 
            else if (UserInput.Action == "Login" && UserInput.UserName != null)
            {
                await LoginPlayer(UserInput);
            }
                return RedirectToPage("Speler"); 
        }

        public async Task<IActionResult> RegisterPlayer(FormUser input)
        {
            var result = await _playerManager.CreateAsync( new Player
            {
                UserName = input.UserName,
                Email = input.Email,
            }, input.Password);

            if (result.Succeeded)
            {
                await LoginPlayer(input);
            }
            else
            {
                ModelState.AddModelError("Login", "Password Heeft geen special character");
                return Page();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> LoginPlayer(FormUser input)
        {
            var result = await _signInManager.PasswordSignInAsync(input.UserName, input.Password, false, false);

            if (result.Succeeded)
            {
                return RedirectToPage();
            }
            else
            {
                Status = $"Kon niet inloggen: {result.RequiresTwoFactor} | {result.IsLockedOut} | {result.IsNotAllowed}";
                return Page();
            }
        }

        public async Task<IActionResult> SignOutPlayer()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("Speler");
        }
    }
}
