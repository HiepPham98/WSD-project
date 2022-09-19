using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WesternInn_AF_HLN_HP.Model
{
    public class Guest
    {
        [Key]
        public string Email { get; set; }
        
        public string Surname { get; set; }
        public string GivenName { get; set; }
        public string Postcode { get; set; }
        public int TheBooking{ get; set; }
    }
}
