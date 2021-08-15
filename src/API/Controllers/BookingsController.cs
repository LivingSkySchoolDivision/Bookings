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

        [HttpGet("{id}/{year}/{month}/{day}")]
        public IEnumerable<SingleBooking> Get(Guid id, int year, int month, int day)
        {
            return _service.Find(x => 
                x.ResourceGUID == id &&
                x.BookingDate.Year == year &&
                x.BookingDate.Month == month &&
                x.BookingDate.Day == day
            );
        }

        [HttpGet("{id}/{year}/{month}")]
        public IEnumerable<SingleBooking> Get(Guid id, int year, int month)
        {
            return _service.Find(x => 
                x.ResourceGUID == id &&
                x.BookingDate.Year == year &&
                x.BookingDate.Month == month
            );
        }

        [HttpGet("{id}/{year}")]
        public IEnumerable<SingleBooking> Get(Guid id, int year)
        {
            return _service.Find(x => 
                x.ResourceGUID == id &&
                x.BookingDate.Year == year
            );
        }
    }
}
