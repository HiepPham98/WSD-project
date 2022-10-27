using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WesternInn_AF_HLN_HP.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WesternInn_AF_HLN_HP.Pages.Bookings
{
    public class ManageHomeModel : PageModel
    {
        private readonly WesternInn_AF_HLN_HP.Data.ApplicationDbContext _context;

        public ManageHomeModel(WesternInn_AF_HLN_HP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Booking> Booking { get; set; }

        public async Task OnGetAsync()
        {
            Booking = await _context.Booking
                .Include(b => b.TheGuest)
                .Include(b => b.TheRoom).ToListAsync();
        }
    }
}
