using System;

namespace LSSD.Bookings
{
    public class SingleBooking : IGUIDable
    {
        public DateTime BookingDate { get; set; }        
        public DateTime StartTimeUTC { get; set; }
        public int DurationMinutes { get; set; }
        public string ShortDescription { get; set; }
        public string Requestor { get; set; }
        public string Details { get; set; }
        public Guid ResourceGUID { get; set; }        
        public Guid Id { get; set; }

        public DateTime EndTimeUTC { 
            get 
            {
                return StartTimeUTC.AddMinutes(this.DurationMinutes);
            }
        }
    }
}