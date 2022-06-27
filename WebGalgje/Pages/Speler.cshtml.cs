using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebGalgje.Entities;

namespace WebGalgje.Pages
{
    public class SpelerModel : PageModel
    {
        public string Status { get; set; }
        public  string Action = "";
        
        [BindProperty]
        public FormUser UserInput { get; set; } // binding

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
                case "Registreer":
                    Status = "registreren";
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

        public async Task<IActionResult> OnPostAsync()
        {
            if(UserInput.Action == "Registreer")
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                var result = await _playerManager.CreateAsync(new Player
                {
                    UserName = "Justin",
                    Email = "Justin@gmail.com",
                }, "Password123!");

                if (result.Succeeded)
                {
                    Status = "Geregistreerd!";
                }
            } 
            else if (UserInput.Action == "Login")
            {
                var result = await _signInManager.PasswordSignInAsync("Justin", "Password123!", false, false);

                if (result.Succeeded)
                {
                    Status = "Ingelogd";
                }
                else
                {
                    Status = $"Kon niet inloggen: {result.RequiresTwoFactor} | {result.IsLockedOut} | {result.IsNotAllowed}";
                }
            }
            else
            {
                return RedirectToPage();
            }



            return RedirectToPage();

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
