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
            MongoRepository<Resource> _resoureRepo = new MongoRepository<Resource>(connection);
            MongoRepository<SingleBooking> _bookingRepo = new MongoRepository<SingleBooking>(connection);

            // Collect the dates that we need to make bookings for

            List<DateTime> bookingDays = new List<DateTime>();

            // All the days from the paper
            bookingDays.Add(new DateTime(2022,08,01));
            bookingDays.Add(new DateTime(2022,08,01));


            foreach(DateTime date in bookingDays)
            {
                // Make a new booking with the given date
                // Add the booking to the DB
            }




        }
    }
}