using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WesternInn_AF_HLN_HP.Data;
using WesternInn_AF_HLN_HP.Model;

namespace WesternInn_AF_HLN_HP.Pages.Guests
{
    public class DetailsModel : PageModel
    {
        private readonly WesternInn_AF_HLN_HP.Data.ApplicationDbContext _context;

        public DetailsModel(WesternInn_AF_HLN_HP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Guest Guest { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Guest == null)
            {
                return NotFound();
            }

            var guest = await _context.Guest.FirstOrDefaultAsync(m => m.Email == id);
            if (guest == null)
            {
                return NotFound();
            }
            else 
            {
                Guest = guest;
            }
            return Page();
        }
    }
}
