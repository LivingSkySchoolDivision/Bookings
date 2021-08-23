using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using LSSD.Bookings;
using System.Linq.Expressions;
using Microsoft.Extensions.Configuration;

namespace WebFrontend
{
    public class BookingService
    {
        private readonly IRepository<SingleBooking> _repository;
        private readonly IConfiguration _configuration;

        private TimeZoneInfo AdjustedTimeZone;
        private bool AdjustTimeZone = false;


        public BookingService(IRepository<SingleBooking> Repository, IConfiguration Configuration)
        {
            this._repository = Repository;
            _configuration = Configuration;

            if(!string.IsNullOrEmpty(_configuration["Settings:TimeZone"])) {
            try {
                TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(_configuration["Settings:TimeZone"]);
                AdjustedTimeZone = timeZone;
                AdjustTimeZone = true;
            }
            catch {}
        }
        }
        public void Insert(SingleBooking obj)
        {
            SingleBooking newObj = obj;

            if(AdjustTimeZone) {
                newObj = AdjustTimeZoneToUTC(obj, AdjustedTimeZone);
            }

            _repository.Insert(newObj);
        }
        public void Update(SingleBooking obj)
        {
            SingleBooking newObj = obj;

            if(AdjustTimeZone) {
                newObj = AdjustTimeZoneToUTC(obj, AdjustedTimeZone);
            }

            _repository.Update(newObj);
        }

        public SingleBooking Get(string id)
        {
            // Adjust time zones to UTC
            SingleBooking UTCBooking =  _repository.GetById(id);

            if (AdjustTimeZone)  {
                UTCBooking = AdjustTimeZoneFromUTC(UTCBooking, AdjustedTimeZone);
            }

            return UTCBooking;
        }

        public IList<SingleBooking> Find(Expression<Func<SingleBooking, bool>> predicate)
        {
            // Adjust time zones to UTC
            IList<SingleBooking> UTCResults = _repository.Find(predicate);

            if (AdjustTimeZone) {
                IList<SingleBooking> _convertedResults = new List<SingleBooking>();
                foreach(SingleBooking booking in UTCResults) {
                    _convertedResults.Add(AdjustTimeZoneFromUTC(booking, AdjustedTimeZone));
                }
                return _convertedResults;
            } else {
                return UTCResults;
            }
        }

        private static SingleBooking AdjustTimeZoneToUTC(SingleBooking booking, TimeZoneInfo TimeZone) {
            // Adjust time zones to UTC

            // Create a date in the user's timezone
            DateTime nonUTCStartDate = new DateTime(
                booking.BookingDate.Year,
                booking.BookingDate.Month,
                booking.BookingDate.Day,
                booking.StartHourUTC,
                booking.StartMinuteUTC,
                0);

            // Adjust that date to the server timezone and extract parts
            DateTime UTCStartDateWithTime = TimeZoneInfo.ConvertTimeToUtc(nonUTCStartDate, TimeZone);

            booking.BookingDate = UTCStartDateWithTime.Date;
            booking.StartHourUTC = UTCStartDateWithTime.Hour;
            booking.StartMinuteUTC = UTCStartDateWithTime.Minute;

            return booking;
        }

        private static SingleBooking AdjustTimeZoneFromUTC(SingleBooking booking, TimeZoneInfo TimeZone) {
            // Adjust time zones to UTC

            // Create a date in the user's timezone
            DateTime nonUTCStartDate = new DateTime(
                booking.BookingDate.Year,
                booking.BookingDate.Month,
                booking.BookingDate.Day,
                booking.StartHourUTC,
                booking.StartMinuteUTC,
                0);

            // Adjust that date to the server timezone and extract parts
            DateTime UTCStartDateWithTime = TimeZoneInfo.ConvertTimeFromUtc(nonUTCStartDate, TimeZone);

            booking.BookingDate = UTCStartDateWithTime.Date;
            booking.StartHourUTC = UTCStartDateWithTime.Hour;
            booking.StartMinuteUTC = UTCStartDateWithTime.Minute;

            return booking;
        }



    }
}
