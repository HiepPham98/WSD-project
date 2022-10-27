using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WesternInn_AF_HLN_HP.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace WesternInn_AF_HLN_HP.Pages.Bookings
{
    [Authorize]
    public class ManageCreateModel : PageModel
    {
        private readonly WesternInn_AF_HLN_HP.Data.ApplicationDbContext _context;

        public ManageCreateModel(WesternInn_AF_HLN_HP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["GuestEmail"] = new SelectList(_context.Guest, "email", "FullName");
            ViewData["RoomID"] = new SelectList(_context.Room, "RoomID", "RoomID");
            return Page();
        }

        [BindProperty]
        public Booking Booking { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["GuestEmail"] = new SelectList(_context.Guest, "email", "FullName");
            ViewData["RoomID"] = new SelectList(_context.Room, "RoomID", "RoomID");

            if (Booking.CheckIn > Booking.CheckOut)
            {
                ViewData["oninvaliddate"] = "true";
                return Page();
            }

            var RoomID = new SqliteParameter("RoomID", Booking.RoomID);
            var CheckIn = new SqliteParameter("CheckIn", Booking.CheckIn);
            var CheckOut = new SqliteParameter("CheckOut", Booking.CheckOut);

            String sqlStatement = "SELECT * FROM Room as r " +
                "WHERE r.RoomID = @RoomID " +
                "AND RoomID IN (" +
                "SELECT RoomID FROM Booking " +
                "WHERE (CheckIn BETWEEN @CheckIn AND @CheckOut) " +
                "AND (CheckOut BETWEEN @CheckIn AND @CheckOut)" +
                ")";


            var query = _context.Room.FromSqlRaw(sqlStatement, RoomID, CheckIn, CheckOut);

            // Check if room is already booked (exists inside Room)
            var rooms = await query.ToListAsync();


            if (rooms.Count() > 0) // room exists
            {
                ViewData["AlreadyBooked"] = "true";
                return Page();
            }


            _context.Booking.Add(Booking);
            await _context.SaveChangesAsync();
            ViewData["Success"] = "true";



            return Page();
        }
    }
}
