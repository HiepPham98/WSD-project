using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WesternInn_AF_HLN_HP.Model
{
    public class Guest
    {
        //primary key
        [Key, Required]
        [DataType(DataType.EmailAddress)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Email { get; set; } = String.Empty;


        [Required]
        [RegularExpression("^[A-z-']{2,20}")]
        public string Surname { get; set; } = String.Empty;

        [Required]
        [RegularExpression("^[A-z-']{2,20}")]
        public string GivenName { get; set; } = String.Empty;

        [RegularExpression(@"^[0-9]{4}$")]
        public string Postcode { get; set; } = String.Empty;

        //Navigation Properties
        public ICollection<Booking>? TheBookings { get; set; } 
    }
}
