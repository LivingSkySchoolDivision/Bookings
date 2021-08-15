using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Linq.Expressions;

namespace LSSD.Bookings.API.Services
{
    public class BookingService
    {
        private readonly IRepository<SingleBooking> _repository;


        public BookingService(IRepository<SingleBooking> Repository)
        {
            this._repository = Repository;
        }

        public IEnumerable<SingleBooking> GetForResource(Guid ResourceGUID)
        {
            return _repository.Find(x => x.ResourceGUID == ResourceGUID);
        }
               
        public IList<SingleBooking> Find(Expression<Func<SingleBooking, bool>> predicate)
        {
            return _repository.Find(predicate);
        }

    }
}
