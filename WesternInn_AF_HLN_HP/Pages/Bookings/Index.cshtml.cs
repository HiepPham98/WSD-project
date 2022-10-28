using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WesternInn_AF_HLN_HP.Data;
using WesternInn_AF_HLN_HP.Model;

namespace WesternInn_AF_HLN_HP.Pages.Bookings
{
    [Authorize(Roles = "Guest")]
   
    public class IndexModel : PageModel
    {
        private readonly WesternInn_AF_HLN_HP.Data.ApplicationDbContext _context;

        public IndexModel(WesternInn_AF_HLN_HP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Booking> Booking { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder)
        {
            var bookings = (IQueryable<Booking>)_context.Booking;
            if (String.IsNullOrEmpty(sortOrder))
            {
                // When the Index page is loaded for the first time, the sortOrder is empty.
                // By default, the movies should be displayed in the order of title_asc.
                sortOrder = "checkin_asc";
            }
           
            // Sort the movies by specified order
            switch (sortOrder)
            {
                case "checkin_asc":
                    bookings = bookings.OrderBy(m => m.CheckIn);
                    break;
                case "checkin_desc":
                    bookings = bookings.OrderByDescending(m => m.CheckIn);
                    break;
                case "cost_asc":
                    bookings = bookings.OrderBy(m => (double)m.Cost);
                    break;
                case "cost_desc":
                    bookings = bookings.OrderByDescending(m =>(double) m.Cost);
                    break;
               
            }
            // Deciding the query string (sortOrder=xxx) to include in the heading links
            // for Title and Price respectively.
            // They specify the next display order if a heading link is clicked. 
            // Store them in ViewData dictionary to pass them to View.
            ViewData["NextCheckInOrder"] = sortOrder != "checkin_asc" ? "checkin_asc" : "checkin_desc";
            ViewData["NextCostOrder"] = sortOrder != "cost_asc" ? "cost_asc" : "cost_desc";
           
            Booking = await bookings.Include(p => p.TheRoom).Include(p => p.TheGuest).AsNoTracking().ToListAsync();
           
        }
    }
}
