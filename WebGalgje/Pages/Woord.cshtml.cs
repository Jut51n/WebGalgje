using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebGalgje.DataAccess.Repositories;
using WebGalgje.Entities;

namespace WebGalgje.Pages
{
    public class WoordModel : PageModel
    {
		public IWoordRepository WoordRepository { get; set; }
		public IEnumerable<Woord> WoordList { get; set; }

        [BindProperty]
        public Woord NewWoord { get; set; } //model binding!!!!

		public WoordModel(IWoordRepository woordRepository)
		{
			WoordRepository = woordRepository;
		}

		public async Task OnGetAsync()
		{
			WoordList = await WoordRepository.GetAll();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (WoordRepository.Contains(NewWoord))
			{
				ModelState.AddModelError("WoordAlInDb", "Mysterieuze foutmelding");
            }

			if (!ModelState.IsValid)
			{
				WoordList = await WoordRepository.GetAll();
				return Page(); 
			}

			await WoordRepository.Add(NewWoord);
			return RedirectToPage(); 

		}
	}
}
