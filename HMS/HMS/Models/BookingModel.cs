using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models
{
    public class BookingModel
    {
        public string bookingId { get; set; }
        public string guestId { get; set; } // id of the guest who booked
        public string roomNumber { get; set; } // room that was booked
        public string checkInDate { get; set; } //accept as plain string, e.g. "2025-06-10"
        public string checkOutDate { get; set; }
        public double totalPrice { get; set; }
        public string status { get; set; } // values: "Confirmed", "Cancelled", "Completed"
        public List<ReviewModel> bookingReviews { get; set; }  // reviews for this booking
    }
}
