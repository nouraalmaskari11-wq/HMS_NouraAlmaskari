using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public static class ReviewService
    {
        public static void AddReview(BookingModel booking, ReviewModel review)
        {
            booking.bookingReviews.Add(review);
        }

        public static void DisplayReviewsForBooking(BookingModel booking)
        {
            if (booking.bookingReviews.Count == 0)
            {
                Console.WriteLine("No reviews yet.");
                return;
            }

            foreach (var r in booking.bookingReviews)
            {
                Console.WriteLine($"Review rating: {r.rating}, Comment: {r.comment} ");
            }
        }
    }
}

