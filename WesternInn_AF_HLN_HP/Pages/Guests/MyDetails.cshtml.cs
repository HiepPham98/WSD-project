using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WesternInn_AF_HLN_HP.Model;

namespace WesternInn_AF_HLN_HP.Pages.Guests
{
    [Authorize(Roles = "Guest")]
    public class MyDetailsModel : PageModel
    {
        private readonly WesternInn_AF_HLN_HP.Data.ApplicationDbContext _context;

        public MyDetailsModel(WesternInn_AF_HLN_HP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Guest? Myself { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // retrieve the logged-in user's Email
            // need to add "using System.Security.Claims;"
            string _Email = User.FindFirst(ClaimTypes.Name).Value;

            Guest Guest = await _context.Guest.FirstOrDefaultAsync(m => m.Email == _Email);

            /* dealing with both cases of new user and existing user */
            if (Guest != null)
            {
                // existing user
                ViewData["ExistInDB"] = "true";
                Myself = Guest;
            }
            else
            {
                // new user
                ViewData["ExistInDB"] = "false";
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            // retrieve the logged-in user's Email
            // need to add "using System.Security.Claims;"
            string _Email = User.FindFirst(ClaimTypes.Name).Value;

            Guest Guest = await _context.Guest.FirstOrDefaultAsync(m => m.Email == _Email);

            /* dealing with both cases of new user and existing user */
            if (Guest != null)
            {
                // This ViewData entry is needed in the content file
                // The user has been created in the database
                ViewData["ExistInDB"] = "true";
            }
            else
            {
                // new user
                ViewData["ExistInDB"] = "false";
                Guest = new Guest();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            /* for preventing overposting attacks */
            Guest.Email = _Email;

            /* Guest: the object to update using the values submitted.
             * "Guest": the Prefix used in the input names in web form.
             * Lambda expression: list the properties to be updated. If not listed, 
             *                    no updates, thus preventing overposting.
             */
            var success = await TryUpdateModelAsync<Guest>(Guest, "Myself",
                                s => s.GivenName, s => s.Surname, s => s.Postcode);
            if (!success)
            {
                return Page();
            }

            if ((string)ViewData["ExistInDB"] == "true")
            {
                // Since the context doesn't allow tracking two objects with the same key,
                // we do the copying first, and then update.
                _context.Guest.Update(Guest);
            }
            else
            {
                // add this newly-created record into db
                _context.Guest.Add(Guest);
            }

            try  // catching the conflict of editing this record concurrently
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            ViewData["SuccessDB"] = "success";
            return Page();
        }
    }
}
