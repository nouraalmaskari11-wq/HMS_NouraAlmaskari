using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS
{
    public class HotelContext
    {
        public List<GuestModel> guests { get; set; }
        public List<RoomModel> rooms { get; set; }
        public List<BookingModel> bookings { get; set; }
        public List<ReviewModel> reviews { get; set; }
        public List<StaffModel> staffs { get; set; }
    }
}
