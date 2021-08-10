using System;

namespace LSSD.Bookings
{
    public class ResourceGroup : IGUIDable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid Id { get; set; }
    }
}