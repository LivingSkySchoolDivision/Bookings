using System;
using System.Collections.Generic;
using System.Text;

namespace LSSD.Bookings
{
    public interface IGUIDable
    {
        Guid Id { get; set; }
    }
}