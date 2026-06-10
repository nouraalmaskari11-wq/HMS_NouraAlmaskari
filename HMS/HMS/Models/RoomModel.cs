using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models
{
    public class RoomModel
    {
        public string roomNumber { get; set; } // e.g. "101", "202"

        public string roomType { get; set; } // e.g. "Single", "Double", "Suite"

        public double pricePerNight { get; set; }

        public bool isAvailable  { get; set; } = true;

        public int floor { get; set; }

    }
}
