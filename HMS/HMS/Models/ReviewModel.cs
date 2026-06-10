using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models
{
    public class ReviewModel
    {
        public string reviewId { get; set; }
        public string bookingId { get; set; }
        public int rating { get; set; } //1 to 5
        public string comment { get; set; }
    }
}
