using System;

namespace LSSD.Bookings
{
    public class Resource : IGUIDable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid Id { get; set; }
        public Guid GroupGUID { get; set; }
        public bool IsEnabled { get; set; }
    }
}