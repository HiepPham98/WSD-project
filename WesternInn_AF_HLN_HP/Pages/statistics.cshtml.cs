using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WesternInn_AF_HLN_HP.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace WesternInn_AF_HLN_HP.Pages
{
    [Authorize(Roles = "Administrator")]
    public class StatisticsModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public StatisticsModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<PostcodeStats> PostcodeStat { get; set; }
        public IList<RoomStats> RoomStat { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            var postcodeGroup = _context.Guest.GroupBy(c => c.Postcode);
            var roomstatsGroup = _context.Booking.GroupBy(b => b.RoomID);

            PostcodeStat = await postcodeGroup.Select(p => new PostcodeStats { Postcode = p.Key, Guests = p.Count() }).ToListAsync();
            RoomStat = await roomstatsGroup.Select(p => new RoomStats { RoomID = p.Key, Bookings = p.Count() }).ToListAsync();

            return Page();
        }
    }
}
