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
            if (String.IsNullOrEmpty(sortOrder))
            {
                // When the Index page is loaded for the first time, the sortOrder is empty.
                // By default, the movies should be displayed in the order of title_asc.
                sortOrder = "checkin_asc";
            }
            // Prepare the query for getting the entire list of movies.
            // Convert the data type from DbSet<Movie> to IQueryable<Burger>

            //retrieve currently logged in customer's email
            string _email = User.FindFirst(ClaimTypes.Name).Value;
            //query this customer email for Order
            var bookings = (IQueryable<Booking>)_context.Booking;
            //if this query has returned data
            if (!String.IsNullOrEmpty(_email))
            {
                //tries to locate the search name in both GivenName and family Name
                bookings = bookings.Where(s => s.GuestEmail.Contains(_email));

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
                    bookings = bookings.OrderBy(m => m.Cost);
                    break;
                case "cost_desc":
                    bookings = bookings.OrderByDescending(m => m.Cost);
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
