using System;
using System.Collections.Generic;

namespace LSSD.Bookings
{
    public class Resource : IGUIDable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid Id { get; set; }
        public Guid GroupGUID { get; set; }
        public bool IsEnabled { get; set; }
        public List<string> OIDC_CanViewResource { get; set; } = new List<string>();
        public List<string> OIDC_CanEditBookings { get; set; } = new List<string>();
        public int DefaultBookingMinutes { get; set; }
        public int BookingWindowDays { get; set; } = 9999;
        public string HTMLColorCode = string.Empty;
    }
}