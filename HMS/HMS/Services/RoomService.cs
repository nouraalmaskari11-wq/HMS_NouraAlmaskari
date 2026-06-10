using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace HMS.Services
{
    public static class RoomService
    {
        public static void DisplayAllRooms(List<RoomModel> rooms)
        {
            foreach (var r in rooms)
            {
                Console.WriteLine($"Room Number:{r.roomNumber}, Room Type:{r.roomType}, Price Per Night:{r.pricePerNight}, Is Available: {r.isAvailable}");
            }

        }

        public static void DisplayAvailableRooms(List<RoomModel> rooms)
        {
            foreach (var r in rooms)
            {
                if (r.isAvailable == true)
                {
                    Console.WriteLine($"Room Number:{r.roomNumber}, Room Type:{r.roomType}, Price Per Night:{r.pricePerNight}, Is Available: {r.isAvailable}");
                }
            }
        }

        public static RoomModel FindRoomByNumber(List<RoomModel> rooms , string roomNumber)
        {
            foreach (var r in rooms)
            {
                if (r.roomNumber== roomNumber)
                {
                    return r;
                }
            }
            return null;
        }

        public static double CalculateTotalPrice(RoomModel room, int nights)
        {

            return room.pricePerNight * nights;
        }

    }
}



