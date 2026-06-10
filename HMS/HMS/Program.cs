using HMS.Models;
using HMS.Services;
using System.Data;
using System.Drawing;
using System.Runtime.Intrinsics.X86;

namespace HMS
{

    internal class Program
    {
        public static void RegisterGuest(HotelContext context)
        {
            Console.WriteLine(" Enter your ID:");
            string guestId = Console.ReadLine();
            Console.WriteLine(" Enter your Full Name:");
            string fullName = Console.ReadLine();
            Console.WriteLine(" Enter your Email:");
            string email = Console.ReadLine();
            Console.WriteLine(" Enter your Phone Number:");
            string phoneNumber = Console.ReadLine();

            GuestModel guest = new GuestModel
            {
                guestId = guestId,
                fullName = fullName,
                email = email,
                phoneNumber = phoneNumber,
                guestBookings = new List<BookingModel>()
            };
            context.guests.Add(guest);

            EmailService.SendEmail(email, "Welcome to Grand Codeline Hotel", "Thank you for registering. We look forward to hosting you!");
        }

        public static void AddRoom(HotelContext context)
        {
            Console.WriteLine(" Enter Room Number:");
            string roomNumber = Console.ReadLine();
            Console.WriteLine(" Enter your Room Type:");
            string roomType = Console.ReadLine();
            Console.WriteLine(" Enter your Price Per Night:");
            double pricePerNight = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine(" Enter Floor Number:");
            int floor = Convert.ToInt32(Console.ReadLine());

            RoomModel room = new RoomModel
            {
                roomNumber = roomNumber,
                roomType = roomType,
                pricePerNight = pricePerNight,
                isAvailable = true,
                floor = floor
            };
            context.rooms.Add(room);
            Console.WriteLine($"Room Number: {roomNumber} is Added Succesfully.");
        }

        public static void DisplayAvailableRooms(HotelContext context)
        {
            if (context.rooms.Count == 0) { Console.WriteLine("No rooms in system."); }
            else
                RoomService.DisplayAvailableRooms(context.rooms);
        }

        public static void AddStaff(HotelContext context)
        {
            Console.WriteLine(" Enter Staff ID:");
            string staffId = Console.ReadLine();
            Console.WriteLine(" Enter Staff Full Name:");
            string fullName = Console.ReadLine();
            Console.WriteLine(" Enter Staff Role:");
            string role = Console.ReadLine();
            Console.WriteLine(" Enter Staff Email:");
            string email = Console.ReadLine();
            StaffModel staff = new StaffModel
            {
                staffId = staffId,
                fullName = fullName,
                role = role,
                email = email,
                isOnDuty = true
            };
            context.staffs.Add(staff);
            Console.WriteLine($"Staff: {fullName} is Added Succesfully.");
        }

        public static void DisplayAllStaff(HotelContext context)
        {
            StaffService.DisplayAllStaff(context.staffs);
        }

        public static void BookRoom(HotelContext context)
        {
            Console.WriteLine("Enter Guest ID:");
            string guestId = Console.ReadLine();
            GuestModel guest = GuestService.FindGuestById(context.guests, guestId);
            if (guest == null)
            {
                Console.WriteLine("Guest not found");
                return;
            }

            Console.WriteLine("Enter Room Number:");
            string roomNumber = Console.ReadLine();
            RoomModel room = RoomService.FindRoomByNumber(context.rooms, roomNumber);
            if (room == null)
            {
                Console.WriteLine("Room not found");
                return;
            }

            if (room.isAvailable == false)
            {
                Console.WriteLine("Room not available");
                return;
            }



            Console.WriteLine("Enter Check In Date yyyy-mm-dd:");
            string checkInDate = Console.ReadLine();
            Console.WriteLine("Enter Check Out Date yyyy-mm-dd :");
            string checkOutDate = Console.ReadLine();
            Console.WriteLine("Enter Number Of Nights:");
            int numberOfNights = Convert.ToInt32(Console.ReadLine());
            double totalPrice = RoomService.CalculateTotalPrice(room, numberOfNights);

            Console.WriteLine("Enter Booking ID:");
            string bookingId = Console.ReadLine();

            BookingModel booking = new BookingModel
            {
                bookingId = bookingId,
                guestId = guestId,
                roomNumber = roomNumber,
                checkInDate = checkInDate,
                checkOutDate = checkOutDate,
                totalPrice = totalPrice,
                status = "Confirmed",
                bookingReviews = new List<ReviewModel>()

            };
            context.bookings.Add(booking);
            room.isAvailable = false;
            guest.guestBookings.Add(booking);

            EmailService.SendEmail(guest.email, "Booking Confirmed", "Your booking for room :" + roomNumber + "has been confirmed. Total: " + totalPrice + "OMR");
        }

        public static void CancelBooking(HotelContext context)
        {
            Console.WriteLine("Enter Booking ID:");
            string bookingId = Console.ReadLine();
            BookingModel booking = BookingService.FindBookingById(context.bookings, bookingId);

            if (booking == null)
            {
                Console.WriteLine("Booking not found");
                return;
            }

            if (BookingService.CancelBooking(booking) == false)
            {
                Console.WriteLine("Booking already cancelled.");
            }

            RoomModel room = RoomService.FindRoomByNumber(context.rooms, booking.roomNumber);
            if (room != null)
            {
                room.isAvailable = true;
            }
            

            GuestModel guest = GuestService.FindGuestById(context.guests, booking.guestId);
            if (guest != null)
            {
                EmailService.SendEmail(guest.email, "Booking Cancelled", "Your booking " + bookingId + " has been cancelled.");
            }
            Console.WriteLine("Booking cancelled Successfully");

        }

        public static void AddReviewToBooking(HotelContext context)
        {
            Console.WriteLine("Enter your Booking ID:");
            string bookingId = Console.ReadLine();
            BookingModel booking = BookingService.FindBookingById(context.bookings, bookingId);
            if (booking == null)
            {
                Console.WriteLine("Booking not found.");
                return;
            }
            if (booking.status != "Completed")
            {
                Console.WriteLine("Reviews can only be added to completed bookings.");
                return;
            }
            Console.WriteLine("Enter Review ID: ");
            string reviewId = Console.ReadLine();

            Console.WriteLine("Enter your rating : ");
            int rating = Convert.ToInt32(Console.ReadLine());
            if (rating < 0 || rating > 5)
            {
                Console.WriteLine("Your Rating is out of Range.");
                return;
            }

            Console.WriteLine("Write your comment : ");
            string comment = Console.ReadLine();

            ReviewModel review = new ReviewModel
            {
                reviewId = reviewId,
                bookingId = booking.bookingId,
                rating = rating,
                comment = comment
            };
            ReviewService.AddReview(booking, review);
            context.reviews.Add(review);

            GuestModel guest = GuestService.FindGuestById(context.guests, booking.guestId);
            if (guest != null)
            {
                EmailService.SendEmail(guest.email, "Thank You for Your Review", "We appreciate your feedback! Rating: " + rating + "/ 5");
            }
            Console.WriteLine("Review Added successfully!");
        }

        public static void ToggleStaffDuty(HotelContext context)
        {
            Console.WriteLine("Enter Staff ID: ");
            string staffId = Console.ReadLine();
            StaffModel staff = StaffService.FindStaffById(context.staffs, staffId);
            if (staff == null)
            {
                Console.WriteLine("Staff Not Found");
                return;
            }
            StaffService.ToggleDutyStatus(staff);
        }

        public static void DisplayGuestBookingHistory(HotelContext context)
        {
            Console.WriteLine("Enter Guest ID: ");
            string guestId = Console.ReadLine();
            GuestModel guest = GuestService.FindGuestById(context.guests, guestId);
            if (guest== null)
            {
                Console.WriteLine("Guest Not Found");
                return;
            }
            if (guest.guestBookings == null)
            {
                Console.WriteLine("No booking history for this guest.");
            }
            else
            {
                foreach (var g in guest.guestBookings)
                {
                    Console.WriteLine($"Booking ID: {g.bookingId}, Room NUmber: {g.roomNumber}, Status: {g.status}, Total Price: {g.totalPrice}");
                }
            }
        }

        public static void CompleteBooking(HotelContext context)
        {
            Console.WriteLine("Enter Booking ID :");
            string bookingId = Console.ReadLine();
            BookingModel booking = BookingService.FindBookingById(context.bookings, bookingId);
            if (booking == null)
            {
                Console.WriteLine("No Booking is Found");
                return;
            }
            RoomModel room = RoomService.FindRoomByNumber(context.rooms, booking.roomNumber);
            if (BookingService.CompleteBooking(booking, room)== false)
            {
                Console.WriteLine("Only confirmed bookings can be completed." );
                return;
            }
            GuestModel guest = GuestService.FindGuestById(context.guests, booking.guestId);
            if (guest != null)
            {
                EmailService.SendEmail(guest.email, "Stay Completed — Share Your Experience", "Your stay at Grand Codeline Hotel is complete. Please leave a review!");
            }
            Console.WriteLine("Booking completed successfully!");
        }

        public static void DisplayRoomReviewSummary(HotelContext context)
        {
            Console.WriteLine("Enter Room NUmber.");
            string roomNumber = Console.ReadLine();
            RoomModel room = RoomService.FindRoomByNumber(context.rooms, roomNumber);
            if (room== null)
            {
                Console.WriteLine("Room Not found. ");
                return;
            }

            int totalReviews = 0;
            double sumRatin = 0;
            bool hasReview = false;

            foreach ( var booking in context.bookings)
            {
                if (booking.roomNumber == roomNumber)
                {
                    foreach (var reviews in booking.bookingReviews)
                    {
                        totalReviews++;
                        sumRatin += reviews.rating;
                        hasReview = true;
                        Console.WriteLine("Reviews rating: " + reviews.rating + "/5 , Rwview comment: " + reviews.comment);

                    }
                }
            }
            if (hasReview == false)
            {
                Console.WriteLine("No reviews for this room.");
            }
            else
            {
                double avg = sumRatin / totalReviews ;
                Console.WriteLine($"Total Number of Reviews:{totalReviews}");
                Console.WriteLine("Average: " + avg.ToString("F2"));

            }
        }

        public static void FullGuestProfile(HotelContext context)
        {
            Console.WriteLine("Enter Guest ID: ");
            string guestId = Console.ReadLine();
            GuestModel guest = GuestService.FindGuestById(context.guests, guestId);
            if (guest == null)
            {
                Console.WriteLine("Guest Not found");
                return;
            }

            Console.WriteLine("Guest Profile");
            Console.WriteLine($"Full Name : {guest.fullName}");
            Console.WriteLine($"Email : {guest.email}");
            Console.WriteLine($"PhoneNumber : {guest.phoneNumber}");
            Console.WriteLine($"Nationality : {GuestModel.nationality}");
            Console.WriteLine($"Total Number Bookings for Guest : {guest.guestBookings.Count}");

            int totalReviews ;
            int numCompleted = 0;
            foreach (var booking in guest.guestBookings)
            {
                totalReviews = 0;
                if (booking.status == "Completed")
                {
                    numCompleted++;
                }
                foreach (var review in booking.bookingReviews)
                {
                    totalReviews++;
                }
                Console.WriteLine($"Booking ID : {booking.bookingId}");
                Console.WriteLine($"Room Number : {booking.roomNumber}");
                Console.WriteLine($"Status : {booking.status} ");
                Console.WriteLine($"Total Price : {booking.totalPrice}");
                Console.WriteLine($"Total Reviews : {totalReviews}");
            }
            Console.WriteLine($"Completed stays:{numCompleted}");
        }



        static void Main(string[] args)
        {
            HotelContext context = new HotelContext();
            context.staffs = new List<StaffModel>();
            context.rooms= new List<RoomModel>();
            context.bookings = new List<BookingModel>();
            context.reviews = new List<ReviewModel>();
            context.guests = new List<GuestModel>();

            bool exite = false;
            while (exite == false)
            {

                Console.WriteLine("==== Hotle Manegment System ====");
                Console.WriteLine(" ");
                Console.WriteLine("------ Choose an option: ------ ");
                Console.WriteLine("1. Register Guest ");
                Console.WriteLine("2. Add Room ");
                Console.WriteLine("3. Display Available Rooms ");
                Console.WriteLine("4. Book Room ");
                Console.WriteLine("5. Cancel Booking ");
                Console.WriteLine("6. Complete Booking");
                Console.WriteLine("7. Add Review To Booking ");
                Console.WriteLine("8. Display Guest Booking History ");
                Console.WriteLine("9. Display Room Review Summary  ");
                Console.WriteLine("10.Full Guest Profile ");
                Console.WriteLine("11.Add Staff");
                Console.WriteLine("12.Display All Staff ");
                Console.WriteLine("13.Toggle Staff Duty ");
                Console.WriteLine("0. Exit");
                Console.WriteLine(" ");


                int option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        RegisterGuest(context);
                        break;
                    case 2:
                        AddRoom(context);
                        break;
                    case 3:
                        DisplayAvailableRooms(context);
                        break;
                    case 4:
                        BookRoom(context);
                        break;
                    case 5:
                        CancelBooking(context);
                        break;
                    case 6:
                        CompleteBooking(context);
                        break;
                    case 7:
                        AddReviewToBooking(context);
                        break;
                    case 8:
                        DisplayGuestBookingHistory(context);
                        break;
                    case 9:
                        DisplayRoomReviewSummary(context);
                        break;
                    case 10:
                        FullGuestProfile(context);
                        break;
                    case 11:
                        AddStaff(context);
                        break;
                    case 12:
                        DisplayAllStaff(context);
                        break;
                    case 13:
                        ToggleStaffDuty(context);
                        break;
                    case 0:
                        exite = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }

                Console.WriteLine("Press any key to continue ...");
                Console.ReadKey();
                Console.Clear();
            } 
        }
    }
}
