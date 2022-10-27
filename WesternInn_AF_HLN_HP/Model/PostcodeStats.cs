using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WesternInn_AF_HLN_HP.Model
{
    public class PostcodeStats
    {
        [Display(Name = "Postcode")]
        public string Postcode { get; set; }

        [Display(Name = "Guests")]
        public int Guests { get; set; }
    }
}
