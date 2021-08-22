using System;
using System.ComponentModel.DataAnnotations;

namespace LSSD.Bookings
{
    public class SingleBooking : IGUIDable
    {
        public DateTime BookingDate { get; set; }  
        [Required]
        [MinLength(4, ErrorMessage = "{0} must be at least {1} characters")]      
        public string ShortDescription { get; set; }
        public string BookedByName { get; set; }
        public string BookedByObjectId { get; set; }
        public string Details { get; set; }
        public Guid ResourceGUID { get; set; }        
        public Guid Id { get; set; }
        public DateTime CreatedUTC { get; set; }
        public int StartHourUTC { get; set; } // Blazor InputNumber doesn't support Bytes, otherwise I'd use Byte here
        public int StartMinuteUTC { get; set; } // Blazor InputNumber doesn't support Bytes, otherwise I'd use Byte here
        public int DurationMinutes { get; set; }


        public bool IsCancelled { get; set; }
        public string CancelledBy { get; set; }

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