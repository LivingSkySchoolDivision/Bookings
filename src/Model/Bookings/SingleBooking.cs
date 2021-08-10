using System;

namespace LSSD.Bookings
{
    public class SingleBooking : IGUIDable
    {
        public DateTime BookingDate { get; set; }        
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ShortDescription { get; set; }
        public string Requestor { get; set; }
        public string Details { get; set; }
        public Guid ResourceGUID { get; set; }        
        public Guid Id { get; set; }
    }
}