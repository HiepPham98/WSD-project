using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WesternInn_AF_HLN_HP.Model
{
    /* May use this for 6.4 not sure yet */
    public class MyBookings
    {
        [Display(Name = "Surname")]
        public string Surname { get; set; } = string.Empty;
        [Display(Name = "Given Name")]
        public string GivenName { get; set; } = string.Empty;

        [Display(Name = "Room ID")]
        public int RoomID { get; set; }

        [Display(Name = "Check In Date")]
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }

        //check-out date
        [Display(Name = "Check Out Date")]
        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }

        [Display(Name ="Cost")]
        public decimal cost { get; set; }


    }
}
