using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSSD.Bookings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LSSD.Bookings.API.Services;

namespace LSSD.Bookings.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly ILogger<BookingsController> _logger;
        private readonly BookingService _service;

        public BookingsController(ILogger<BookingsController> logger, BookingService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("{resourceid}/{year}/{month}/{day}")]
        public IEnumerable<SingleBooking> Get(Guid resourceid, int year, int month, int day)
        {
            return _service.Find(x => 
                x.ResourceGUID == resourceid &&
                x.BookingDate.Year == year &&
                x.BookingDate.Month == month &&
                x.BookingDate.Day == day &&
                x.IsCancelled == false
            ).OrderBy(x => x.BookingDate).ThenBy(x => x.StartHourUTC).ThenBy(x => x.StartMinuteUTC);
        }

        [HttpGet("{resourceid}/{year}/{month}")]
        public IEnumerable<SingleBooking> Get(Guid resourceid, int year, int month)
        {
            return _service.Find(x => 
                x.ResourceGUID == resourceid &&
                x.BookingDate.Year == year &&
                x.BookingDate.Month == month &&
                x.IsCancelled == false
            ).OrderBy(x => x.BookingDate).ThenBy(x => x.StartHourUTC).ThenBy(x => x.StartMinuteUTC);
        }

        [HttpGet("{resourceid}/{year}")]
        public IEnumerable<SingleBooking> Get(Guid resourceid, int year)
        {
            return _service.Find(x => 
                x.ResourceGUID == resourceid &&
                x.BookingDate.Year == year &&
                x.IsCancelled == false
            ).OrderBy(x => x.BookingDate).ThenBy(x => x.StartHourUTC).ThenBy(x => x.StartMinuteUTC);
        }
    }
}
