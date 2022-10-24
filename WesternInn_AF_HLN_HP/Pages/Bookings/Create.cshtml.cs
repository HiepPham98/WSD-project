using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WesternInn_AF_HLN_HP.Data;
using WesternInn_AF_HLN_HP.Model;

namespace WesternInn_AF_HLN_HP.Pages.Bookings
{
    [Authorize(Roles="Guest")]
    [Authorize(Roles ="Administrator")]
    public class CreateModel : PageModel
    {
        private readonly WesternInn_AF_HLN_HP.Data.ApplicationDbContext _context;

        public CreateModel(WesternInn_AF_HLN_HP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["RoomID"] = new SelectList(_context.Room, "ID", "ID");
            return Page();
        }

        [BindProperty]
        public Booking Booking { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Booking == null || Booking == null)
            {
                return Page();
            }
            var emptyBooking = new Booking();
            /* emptyPurchase: the object to update using the values submitted.
             * "Order": the Prefix used in the input names in web form.
             * Lambda expression: list the properties to be updated. If not listed, 
             *                    no updates, thus preventing overposting.
             */
            var success = await TryUpdateModelAsync<Booking>(emptyBooking, "Booking",
                                s => s.RoomID, s => s.GuestEmail, s => s.CheckIn, s => s.CheckOut);
           /* if (success)
            {
                var theBooking = await _context.Burger.FindAsync(emptyPurchase.BurgerID);
                //calculate the total price of this oreder
                emptyPurchase.TotalCost = emptyPurchase.BurgerCount * theBurger.Price;

                // add this newly-created order into db
                _context.Purchase.Add(emptyPurchase);

                await _context.SaveChangesAsync();
                if (purchase != null)
                {
                    //double totalCost = 0;
                    ViewData["TotalCost"] = (double)emptyPurchase.TotalCost;

                }
                ViewData["BurgerName"] = theBurger.BurgerName;
                return Page();
            }*/

            _context.Booking.Add(Booking);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
