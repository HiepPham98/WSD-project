using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WesternInn_AF_HLN_HP.Model;

namespace WesternInn_AF_HLN_HP.Pages.Bookings
{
    public class SearchRoomModel : PageModel
    {
        private readonly WesternInn_AF_HLN_HP.Data.ApplicationDbContext _context;

        public SearchRoomModel(WesternInn_AF_HLN_HP.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Booking Booking { get; set; }
        [BindProperty]
        public Room Room { get; set; }

        public IList<Room> AvailableRooms { get; set; }
        public IActionResult OnGet()
        {
            ViewData["BedCountList"] = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Text ="1", Value = "1"
                },
                new SelectListItem
                {
                    Text ="2", Value = "2"
                },
                new SelectListItem
                {
                    Text ="3", Value = "3"
                },
            };
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            ViewData["BedCountList"] = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Text ="1", Value = "1"
                },
                new SelectListItem
                {
                    Text ="2", Value = "2"
                },
                new SelectListItem
                {
                    Text ="3", Value = "3"
                },
            };
            var checkInParamatta = new SqliteParameter("checkin", Booking.CheckIn);
            var checkOutParamatta = new SqliteParameter("checkout", Booking.CheckOut);
            var bedNoParamatta = new SqliteParameter("bedNo", Room.BedCount);

            var availableRooms = _context.Room.FromSqlRaw("select [Room].* " +
                                                       "from [Room] inner join [Booking] on [Room].ID = [Booking].RoomID " +
                                                       "where [Room].BedCount = @bedNo and [Booking].CheckIn<@checkout and [Booking].CheckOut<@checkin"
                                                       , bedNoParamatta,checkInParamatta,checkOutParamatta);

            AvailableRooms = await availableRooms.ToListAsync(); 
            return Page();
        }
    }
}
