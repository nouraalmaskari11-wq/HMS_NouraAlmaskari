using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public static class BookingService
    {
        public static void DisplayAllBookings(List<BookingModel> bookings)
        {
            foreach(var b in bookings)
            {
                Console.WriteLine($"Booking ID: {b.bookingId}, Guest ID: {b.guestId}, Room Number: {b.roomNumber}, Check In Date: {b.checkInDate}, check Out Date: {b.checkOutDate}, Total Price: {b.totalPrice}, Status: {b.status}, Booking Reviews: {b.bookingReviews} ");
            }
        }
        public static BookingModel FindBookingById(List<BookingModel> bookings, string bookingId)
        {
            foreach (var b in bookings)
            {
                if (b.bookingId== bookingId)
                {
                    return b;
                }
            }
            return null;
        }

        public static bool CancelBooking(BookingModel booking)
        {
            if (booking.status == "Cancelled")
            {
                return false;
            }

                booking.status = "Cancelled";
                return true;
        }

        public static bool CompleteBooking(BookingModel booking, RoomModel room)
        {
            if (booking.status != "Confirmed")
            { 
                return false; 
            }
            booking.status = "Completed";
            room.isAvailable = true;
            return true;
            
        }
    }
}



