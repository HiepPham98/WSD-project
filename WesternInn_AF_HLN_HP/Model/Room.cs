using System.ComponentModel.DataAnnotations;


namespace WesternInn_AF_HLN_HP.Model
{
    public class Room
    {
        //primary key
        public int ID { get; set; }

        //room level
        [RegularExpression(@"^[g,1,2,3]$", 
            ErrorMessage = "Room Level must be 'g','1','2','3'")]
        [Required]
        public string Level { get; set; } = string.Empty;

        //number of beds per room
        [RegularExpression(@"^[1,2,3]$",
            ErrorMessage = "Number of beds must be '1','2', or '3'")]
        public int BedCount { get; set; }

        //price per night
        [DataType(DataType.Currency)]
        [Range(50,300)]
        public decimal Price { get; set; }

        //Navigation properties
        public ICollection<Booking>? TheBookings { get; set; }

    }
}
