using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using LSSD.Bookings;
using System.Linq.Expressions;

namespace WebFrontend
{
    public class BookingService
    {
        private readonly IRepository<SingleBooking> _repository;


        public BookingService(IRepository<SingleBooking> Repository)
        {
            this._repository = Repository;
        }
        public void Insert(SingleBooking obj) 
        {
            _repository.Insert(obj);
        }
        public void Update(SingleBooking obj) 
        {
            _repository.Update(obj);
        }

        public SingleBooking Get(string id)
        {
            return _repository.GetById(id);
        }

        public IList<SingleBooking> Find(Expression<Func<SingleBooking, bool>> predicate)
        {
            return _repository.Find(predicate);
        }
        

    }
}
