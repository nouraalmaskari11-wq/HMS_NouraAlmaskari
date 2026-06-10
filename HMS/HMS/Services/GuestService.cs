using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public static class GuestService
    {
        public static void DisplayAllGuests(List<GuestModel> guests)
        {
            foreach (var a in guests)
            {
                Console.WriteLine($" Guest Id:{a.guestId}, Full Name: {a.fullName}, Email: {a.email}, Phone Number:{a.phoneNumber} ");
            }
        }

        public static GuestModel FindGuestById(List<GuestModel> guests, string guestId)
        {
           foreach (var g in guests)
            {
                if (g.guestId == guestId) return g;
            }
            return null;

        }
    }
}
