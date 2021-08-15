using System;

namespace LSSD.Bookings
{
    public class SingleBooking : IGUIDable
    {
        public DateTime BookingDate { get; set; }        
        public string ShortDescription { get; set; }
        public string Requestor { get; set; }
        public string Details { get; set; }
        public Guid ResourceGUID { get; set; }        
        public Guid Id { get; set; }
        
        public string ActualRequestor { get; set; }
        public DateTime CreatedUTC { get; set; }

        public int StartHourUTC { get; set; } // Blazor InputNumber doesn't support Bytes, otherwise I'd use Byte here
        public int StartMinuteUTC { get; set; } // Blazor InputNumber doesn't support Bytes, otherwise I'd use Byte here
        public int DurationMinutes { get; set; }



        public DateTime StartTimeUTC { get
            {
                return new DateTime(
                    this.BookingDate.Year,
                    this.BookingDate.Month,
                    this.BookingDate.Day,
                    this.StartHourUTC,
                    this.StartMinuteUTC,
                    0);
            }
        }
        public DateTime EndTimeUTC { 
            get 
            {
                return StartTimeUTC.AddMinutes(this.DurationMinutes);
            }
        }
    }
}