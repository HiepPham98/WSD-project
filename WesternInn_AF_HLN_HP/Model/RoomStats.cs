using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WesternInn_AF_HLN_HP.Model
{
    public class RoomStats
    {
        [Display(Name = "RoomID")]
        public int RoomID { get; set; }

        [Display(Name = "Number of Bookings")]
        public int Bookings { get; set; }
    }
}
