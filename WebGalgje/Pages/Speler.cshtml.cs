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
            if(UserInput.Action == "Registreer")
            {
                Player check = await _playerManager.FindByNameAsync(UserInput.UserName);
                if(check != null)
                {
                    ModelState.AddModelError("UserNameBezet", "Mysterieuze foutmelding voor username");
                }
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                await RegisterPlayer(UserInput.UserName, UserInput.Password, UserInput.Email);
            } 
            else if (UserInput.Action == "Login")
            {
                await LoginPlayer(UserInput.UserName, UserInput.Password);
            }
                return RedirectToPage(); 
        }

        public async Task<IActionResult> RegisterPlayer(string username, string password, string? email)
        {
            var result = await _playerManager.CreateAsync( new Player
            {
                UserName = username,
                Email = email,
            }, password);

            if (result.Succeeded)
            {
                await LoginPlayer(username, password);
            }
            else
            {
                ModelState.AddModelError("Login", "Password Heeft geen special character");
                return Page();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> LoginPlayer(string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, false, false);

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
