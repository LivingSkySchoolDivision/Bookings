using System;
using System.Configuration;
using LSSD.Bookings;
using LSSD.Bookings.Data;
using Microsoft.Extensions.Configuration;

namespace DebugConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddUserSecrets<Program>()
                .Build();

            // Connect to the DB
            MongoDbConnection connection = new MongoDbConnection(config.GetConnectionString("Internal"));

            MongoRepository<ResourceGroup> _resourceGroupRepo = new MongoRepository<ResourceGroup>(connection);
            MongoRepository<Resource> _resourceRepo = new MongoRepository<Resource>(connection);
            MongoRepository<SingleBooking> _bookingRepo = new MongoRepository<SingleBooking>(connection);

            // Collect the dates that we need to make bookings for

            List<DateTime> bookingDays = new List<DateTime>();

            // Add the dates for the booking batch
            bookingDays.Add(new DateTime(2022,09,08));

            // Get the resource to be booked
            // NBCHS LAB 108
            Resource lab108 = _resourceRepo.GetById("b239887b-48f8-451e-abaf-2e97aae3161c");

            Console.WriteLine(lab108.Name);

            // Start time
            int startHourUTC = 20; // 14:15 but in UTC
            int startMinuteUTC =  15;

            // Duration
            int durationMinutes = 70;

            foreach(DateTime date in bookingDays)
            {
                // Make a new booking with the given date
                SingleBooking booking = new SingleBooking()
                {
                    BookingDate = new DateTime(date.Year, date.Month, date.Day),
                    StartHourUTC = startHourUTC,
                    StartMinuteUTC = startMinuteUTC,
                    ResourceGUID = lab108.Id,
                    BookedByName = "Ms Puff",
                    BookedByObjectId = "16b8e97d-a5de-41bb-8ab8-56c95b51ff6d",
                    ShortDescription = "Puff",
                    Details = "",
                    CreatedUTC = DateTime.UtcNow,
                    DurationMinutes = durationMinutes
                };

                Console.WriteLine(booking.BookingDate.ToShortDateString() + " " + booking.StartHourUTC + ":" + booking.StartMinuteUTC);

                // Add the booking to the DB
                //_bookingRepo.Update(booking);
            }
        }
    }
}