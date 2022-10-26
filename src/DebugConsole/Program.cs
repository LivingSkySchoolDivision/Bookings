using System;
using System.Configuration;
using LSSD.Bookings;
using LSSD.Bookings.Data;
using Microsoft.Extensions.Configuration;

namespace DebugConsole
{
    internal class Program
    {
        public static List<DateTime> DaysBetween(DateTime firstDate, DateTime endDate)
        {
            return DaysBetween(firstDate, endDate, new List<DayOfWeek>() {
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday
            });
        }

        public static List<DateTime> DaysBetween(DateTime firstDate, DateTime endDate, List<DayOfWeek> includeDaysOfWeek)
        {
            DateTime adjustedFirstDate = firstDate;
            DateTime adjustedEndDate = endDate;

            if (firstDate > endDate)
            {
                adjustedFirstDate = endDate;
                adjustedEndDate = firstDate;
            }

            List<DateTime> returnMe = new List<DateTime>();

            DateTime workingDate = adjustedFirstDate;
            while(workingDate <= adjustedEndDate)
            {
                if (includeDaysOfWeek.Contains(workingDate.DayOfWeek))
                {
                    returnMe.Add(workingDate);
                }
                workingDate = workingDate.AddDays(1);
            }

            return returnMe;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Loading configuration...");
            IConfiguration config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddUserSecrets<Program>()
                .Build();


            Console.WriteLine("Loading booking dates...");
            List<DateTime> bookingDays = new List<DateTime>();

            // Add the dates for the booking batch
            bookingDays = DaysBetween(
                new DateTime(2022,10,21),
                new DateTime(2023,6,30)
            );

            // Connect to the DB
            Console.WriteLine("Making db connection...");
            MongoDbConnection connection = new MongoDbConnection(config.GetConnectionString("Internal"));

            MongoRepository<ResourceGroup> _resourceGroupRepo = new MongoRepository<ResourceGroup>(connection);
            MongoRepository<Resource> _resourceRepo = new MongoRepository<Resource>(connection);
            MongoRepository<SingleBooking> _bookingRepo = new MongoRepository<SingleBooking>(connection);

            // Get the resource to be booked
            // NBCHS LAB 112
            Resource lab112 = _resourceRepo.GetById("3baa6960-f759-4654-a692-4cc57e080c2d");

            Console.WriteLine("Creating bookings for calendar: " + lab112.Name);

            // Start time
            int startHourUTC = 9; // the start hour in Saskatchewan time (24-hour)
            int startMinuteUTC =  0;

            // Add 6 hours to adjust for UTC, as our database needs to store time in UTC
            startHourUTC += 6;

            // Duration
            int durationMinutes = 70;

            Console.WriteLine("Creating " + bookingDays.Count + " bookings...");
            foreach(DateTime date in bookingDays)
            {
                // Make a new booking with the given date
                SingleBooking booking = new SingleBooking()
                {
                    BookingDate = new DateTime(date.Year, date.Month, date.Day),
                    StartHourUTC = startHourUTC,
                    StartMinuteUTC = startMinuteUTC,
                    ResourceGUID = lab112.Id,
                    BookedByName = "A Christensen",
                    BookedByObjectId = "86549f68-455b-46aa-8dcd-255af10a33cc",
                    ShortDescription = "A Christensen",
                    Details = "",
                    CreatedUTC = DateTime.UtcNow,
                    DurationMinutes = durationMinutes
                };

                Console.WriteLine(booking.BookingDate.ToString("ddd") + " " + booking.BookingDate.ToShortDateString() + " " + booking.StartHourUTC + ":" + booking.StartMinuteUTC);

                // Add the booking to the DB
                //_bookingRepo.Update(booking);
            }

            Console.WriteLine("Done!");
        }
    }
}